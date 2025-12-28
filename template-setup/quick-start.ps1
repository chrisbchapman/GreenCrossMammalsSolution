<#
.SYNOPSIS
    Quick setup for new solution from GreenCross template
.DESCRIPTION
    Renames the template and optionally updates database configuration
.PARAMETER ProjectName
    The new project name (e.g., 'MyCompany.MyApp')
.PARAMETER DatabaseName
    Optional database name to configure in appsettings.json
#>
param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectName,
    
    [Parameter(Mandatory=$false)]
    [string]$DatabaseName
)

$ErrorActionPreference = "Stop"

Write-Host "Creating new solution: $ProjectName" -ForegroundColor Cyan

# Get the template-setup directory path
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path

# Run rename
& "$scriptPath\rename-template.ps1" -NewName $ProjectName

# Update database name if provided
if ($DatabaseName) {
    $rootPath = Split-Path -Parent $scriptPath
    $appsettingsPath = Join-Path $rootPath "src\$ProjectName.UI\appsettings.json"
    if (Test-Path $appsettingsPath) {
        $json = Get-Content $appsettingsPath -Raw | ConvertFrom-Json
        $json.ConnectionStrings.AppDb = $json.ConnectionStrings.AppDb -replace "Database=\w+", "Database=$DatabaseName"
        $json | ConvertTo-Json -Depth 10 | Set-Content $appsettingsPath -Encoding UTF8
        Write-Host "Updated database name to: $DatabaseName" -ForegroundColor Green
    }
}

Write-Host "`n✅ Setup complete!" -ForegroundColor Green
Write-Host "`nNext steps:" -ForegroundColor Cyan
Write-Host "  1. Open solution in Visual Studio" -ForegroundColor White
Write-Host "  2. Build solution" -ForegroundColor White
Write-Host "  3. Run EF migrations if needed" -ForegroundColor White
Write-Host "  4. Start coding!" -ForegroundColor White
