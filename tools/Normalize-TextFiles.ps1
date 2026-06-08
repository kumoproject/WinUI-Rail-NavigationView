[CmdletBinding()]
param(
    [string]$Root,
    [switch]$DryRun
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if ([string]::IsNullOrWhiteSpace($Root)) {
    $gitRoot = & git rev-parse --show-toplevel 2>$null
    if ($LASTEXITCODE -eq 0 -and -not [string]::IsNullOrWhiteSpace($gitRoot)) {
        $Root = $gitRoot.Trim()
    }
    else {
        $Root = (Get-Location).Path
    }
}

$Root = (Resolve-Path -LiteralPath $Root).Path
$utf8Strict = [System.Text.UTF8Encoding]::new($false, $true)
$utf8Bom = [System.Text.UTF8Encoding]::new($true)
$binaryExtensions = [System.Collections.Generic.HashSet[string]]::new(
    [string[]]@(
        '.appx',
        '.dll',
        '.exe',
        '.gif',
        '.ico',
        '.jpg',
        '.jpeg',
        '.nupkg',
        '.pdb',
        '.png',
        '.pri',
        '.snupkg',
        '.xbf',
        '.zip'
    ),
    [System.StringComparer]::OrdinalIgnoreCase)

function Get-RepositoryFile {
    Push-Location -LiteralPath $Root
    try {
        $raw = & git ls-files -z --cached --others --exclude-standard
        if ($LASTEXITCODE -ne 0) {
            throw 'git ls-files failed.'
        }
    }
    finally {
        Pop-Location
    }

    ($raw -split "`0") |
        Where-Object { -not [string]::IsNullOrWhiteSpace($_) } |
        Sort-Object -Unique
}

function Test-BinaryFile {
    param([string]$Path)

    $extension = [System.IO.Path]::GetExtension($Path)
    if ($binaryExtensions.Contains($extension)) {
        return $true
    }

    $buffer = [byte[]]::new(4096)
    $stream = [System.IO.File]::OpenRead($Path)
    try {
        $read = $stream.Read($buffer, 0, $buffer.Length)
        for ($i = 0; $i -lt $read; $i++) {
            if ($buffer[$i] -eq 0) {
                return $true
            }
        }
    }
    finally {
        $stream.Dispose()
    }

    return $false
}

function Convert-ToUtf8BomCrlf {
    param([string]$Path)

    if (Test-BinaryFile -Path $Path) {
        return 'SkippedBinary'
    }

    $oldBytes = [System.IO.File]::ReadAllBytes($Path)
    $offset = 0
    if ($oldBytes.Length -ge 3 -and $oldBytes[0] -eq 0xEF -and $oldBytes[1] -eq 0xBB -and $oldBytes[2] -eq 0xBF) {
        $offset = 3
    }

    try {
        $text = $utf8Strict.GetString($oldBytes, $offset, $oldBytes.Length - $offset)
    }
    catch {
        return 'SkippedNonUtf8'
    }

    if ($text.Length -gt 0 -and $text[0] -eq [char]0xFEFF) {
        $text = $text.Substring(1)
    }

    $text = $text -replace "`r`n|`n|`r", "`r`n"
    $newBytes = $utf8Bom.GetPreamble() + $utf8Bom.GetBytes($text)

    if ([System.Linq.Enumerable]::SequenceEqual([byte[]]$oldBytes, [byte[]]$newBytes)) {
        return 'Unchanged'
    }

    if (-not $DryRun) {
        [System.IO.File]::WriteAllBytes($Path, $newBytes)
    }

    return 'Changed'
}

$changed = 0
$unchanged = 0
$skippedBinary = 0
$skippedNonUtf8 = 0

foreach ($relativePath in Get-RepositoryFile) {
    $path = Join-Path -Path $Root -ChildPath $relativePath
    if (-not [System.IO.File]::Exists($path)) {
        continue
    }

    $result = Convert-ToUtf8BomCrlf -Path $path
    switch ($result) {
        'Changed' {
            $changed++
            Write-Host "normalized $relativePath"
        }
        'Unchanged' { $unchanged++ }
        'SkippedBinary' { $skippedBinary++ }
        'SkippedNonUtf8' {
            $skippedNonUtf8++
            Write-Warning "skipped non-UTF-8 file: $relativePath"
        }
    }
}

$mode = if ($DryRun) { 'dry run' } else { 'updated' }
Write-Host "${mode}: changed=$changed unchanged=$unchanged skippedBinary=$skippedBinary skippedNonUtf8=$skippedNonUtf8"
