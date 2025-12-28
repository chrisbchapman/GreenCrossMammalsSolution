<#
.SYNOPSIS
    Renames GreenCross template to a new project name
.DESCRIPTION
    Replaces all instances of 'GreenCross.App' with your new project name
.PARAMETER NewName
    The new project name (e.g., 'MyCompany.MyApp')
#>
param(
    [Parameter(Mandatory=$true)]
    [string]$NewName
)

$ErrorActionPreference = "Stop"

$oldName = "GreenCross.App"
$rootPath = Split-Path -Parent $PSScriptRoot

Write-Host "Renaming solution from '$oldName' to '$NewName'..." -ForegroundColor Cyan

# Rename files
Get-ChildItem -Path $rootPath -Recurse -File | Where-Object {
    $_.Name -like "*$oldName*" -and $_.FullName -notlike "*\.git\*" -and $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
} | ForEach-Object {
    $newFileName = $_.Name.Replace($oldName, $NewName)
    $newPath = Join-Path $_.DirectoryName $newFileName
    Write-Host "  Renaming file: $($_.Name) -> $newFileName" -ForegroundColor Yellow
    Rename-Item -Path $_.FullName -NewName $newFileName
}

# Rename directories
Get-ChildItem -Path $rootPath -Recurse -Directory | Where-Object {
    $_.Name -like "*$oldName*" -and $_.FullName -notlike "*\.git\*" -and $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
} | Sort-Object -Property FullName -Descending | ForEach-Object {
    $newDirName = $_.Name.Replace($oldName, $NewName)
    $newPath = Join-Path (Split-Path $_.FullName) $newDirName
    Write-Host "  Renaming directory: $($_.Name) -> $newDirName" -ForegroundColor Yellow
    Rename-Item -Path $_.FullName -NewName $newDirName
}

# Replace content in files
$fileExtensions = @("*.cs", "*.csproj", "*.sln", "*.json", "*.xml", "*.config", "*.md")
Get-ChildItem -Path $rootPath -Recurse -Include $fileExtensions | Where-Object {
    $_.FullName -notlike "*\.git\*" -and $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*"
} | ForEach-Object {
    $content = Get-Content $_.FullName -Raw -Encoding UTF8
    if ($content -match [regex]::Escape($oldName)) {
        Write-Host "  Updating content: $($_.Name)" -ForegroundColor Green
        $content = $content.Replace($oldName, $NewName)
        Set-Content -Path $_.FullName -Value $content -Encoding UTF8 -NoNewline
    }
}

Write-Host "`nRename complete! Don't forget to:" -ForegroundColor Cyan
Write-Host "  1. Update connection strings in appsettings.json" -ForegroundColor White
Write-Host "  2. Update namespace references if needed" -ForegroundColor White
Write-Host "  3. Clean and rebuild solution" -ForegroundColor White
