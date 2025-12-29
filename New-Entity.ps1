<#
.SYNOPSIS
    Generates interfaces and concrete classes for a new entity following GreenCross.Mammals patterns.

.DESCRIPTION
    Creates all necessary files for a new entity:
    - Entity class (implements IEntity<int>)
    - Repository interface (extends IRepository<TEntity>)
    - Repository implementation (extends Repository<TEntity>)
    - Service interface (extends IBaseService<TEntity>)
    - Service implementation (extends BaseService<TEntity>)
    - Optionally registers services in Bootstrap/ServiceRegistration.cs

.PARAMETER EntityName
    The name of the entity (e.g., "Species", "Habitat")

.PARAMETER CustomKeyProperty
    Optional. If the entity uses a custom key property name (e.g., "SpeciesId" instead of "Id").
    If not specified, uses standard "Id" property.

.PARAMETER RegisterServices
    Optional. If specified, automatically adds service registration to ServiceRegistration.cs

.EXAMPLE
    .\New-Entity.ps1 -EntityName "Species"
    Creates all files for a Species entity with standard Id property

.EXAMPLE
    .\New-Entity.ps1 -EntityName "Habitat" -CustomKeyProperty "HabitatId" -RegisterServices
    Creates all files for a Habitat entity with custom HabitatId property and registers services
#>

param(
    [Parameter(Mandatory = $true)]
    [string]$EntityName,

    [Parameter(Mandatory = $false)]
    [string]$CustomKeyProperty = "",

    [Parameter(Mandatory = $false)]
    [switch]$RegisterServices
)

# Validate entity name
if ([string]::IsNullOrWhiteSpace($EntityName)) {
    Write-Error "EntityName cannot be empty"
    exit 1
}

# Determine if using custom key property
$useCustomKey = -not [string]::IsNullOrWhiteSpace($CustomKeyProperty)
if (-not $useCustomKey) {
    $CustomKeyProperty = "Id"
}

# Base paths (relative to script location)
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$srcPath = Join-Path $scriptPath "src"

# Project paths
$entitiesPath = Join-Path $srcPath "GreenCross.Mammals.Entities"
$contractsRepoPath = Join-Path $srcPath "GreenCross.Mammals.Contracts\Repositories"
$contractsServicesPath = Join-Path $srcPath "GreenCross.Mammals.Contracts\Services"
$dataRepoPath = Join-Path $srcPath "GreenCross.Mammals.Data\Repositories"
$bllPath = Join-Path $srcPath "GreenCross.Mammals.BLL"
$bootstrapPath = Join-Path $srcPath "GreenCross.Mammals.Bootstrap"

# Validate paths exist
$pathsToCheck = @($entitiesPath, $contractsRepoPath, $contractsServicesPath, $dataRepoPath, $bllPath)
foreach ($path in $pathsToCheck) {
    if (-not (Test-Path $path)) {
        Write-Error "Path not found: $path"
        exit 1
    }
}

Write-Host "============================================================" -ForegroundColor Yellow
Write-Host "  Generating Entity: $EntityName" -ForegroundColor Cyan
Write-Host "============================================================" -ForegroundColor Yellow
Write-Host "Key Property: $CustomKeyProperty" -ForegroundColor White
Write-Host "Register Services: $RegisterServices" -ForegroundColor White
Write-Host ""

# ============================================================================
# 1. Entity Class
# ============================================================================
$entityFile = Join-Path $entitiesPath "$EntityName.cs"

if (Test-Path $entityFile) {
    Write-Warning "Entity file already exists: $entityFile"
    $overwrite = Read-Host "Overwrite? (y/N)"
    if ($overwrite -ne "y") {
        Write-Host "Skipping entity file..." -ForegroundColor Yellow
        exit 0
    }
}

$entityContent = @"
namespace GreenCross.Mammals.Entities;

public class $EntityName : IEntity<int>
{
"@

if ($useCustomKey) {
    $entityContent += @"

    public int Id
    {
        get => $CustomKeyProperty;
        set => $CustomKeyProperty = value;
    }

    public int $CustomKeyProperty { get; set; }
"@
}
else {
    $entityContent += @"

    public int Id { get; set; }
"@
}

$entityContent += @"

    // Add your entity-specific properties here
    // Example: public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
"@

Set-Content -Path $entityFile -Value $entityContent -Encoding UTF8
Write-Host "  Created: $entityFile" -ForegroundColor Green

# ============================================================================
# 2. Repository Interface
# ============================================================================
$repoInterfaceFile = Join-Path $contractsRepoPath "I${EntityName}Repository.cs"

$repoInterfaceContent = @"
using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Repositories;

public interface I${EntityName}Repository : IRepository<$EntityName>
{
    // Add any $EntityName-specific repository methods here if needed
    // Example: Task<IEnumerable<$EntityName>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
}
"@

Set-Content -Path $repoInterfaceFile -Value $repoInterfaceContent -Encoding UTF8
Write-Host "  Created: $repoInterfaceFile" -ForegroundColor Green

# ============================================================================
# 3. Repository Implementation
# ============================================================================
$repoImplFile = Join-Path $dataRepoPath "${EntityName}Repository.cs"

$repoImplContent = @"
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenCross.Mammals.Data.Repositories;

public class ${EntityName}Repository : Repository<$EntityName>, I${EntityName}Repository
{
    public ${EntityName}Repository(AppDbContext context) : base(context)
    {
    }
"@

if ($useCustomKey) {
    $repoImplContent += @"


    // Override for custom key property
    public override async Task<${EntityName}?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.$CustomKeyProperty == id, cancellationToken);
    }

    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _dbSet.Where(e => e.$CustomKeyProperty == id).ExecuteDeleteAsync(cancellationToken);
    }

    public override async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.$CustomKeyProperty == id, cancellationToken);
    }
"@
}

$repoImplContent += @"

}
"@

Set-Content -Path $repoImplFile -Value $repoImplContent -Encoding UTF8
Write-Host "  Created: $repoImplFile" -ForegroundColor Green

# ============================================================================
# 4. Service Interface
# ============================================================================
$serviceInterfaceFile = Join-Path $contractsServicesPath "I${EntityName}Service.cs"

$serviceInterfaceContent = @"
using GreenCross.Mammals.Entities;

namespace GreenCross.Mammals.Contracts.Services;

public interface I${EntityName}Service : IBaseService<$EntityName>
{
    // Add any $EntityName-specific service methods here if needed
}
"@

Set-Content -Path $serviceInterfaceFile -Value $serviceInterfaceContent -Encoding UTF8
Write-Host "  Created: $serviceInterfaceFile" -ForegroundColor Green

# ============================================================================
# 5. Service Implementation
# ============================================================================
$serviceImplFile = Join-Path $bllPath "${EntityName}Service.cs"

$serviceImplContent = @"
using GreenCross.Mammals.Contracts.Data;
using GreenCross.Mammals.Contracts.Repositories;
using GreenCross.Mammals.Contracts.Services;
using GreenCross.Mammals.Entities;
using Microsoft.Extensions.Logging;

namespace GreenCross.Mammals.BLL;

public class ${EntityName}Service : BaseService<$EntityName>, I${EntityName}Service
{
    public ${EntityName}Service(
        I${EntityName}Repository repository,
        IUnitOfWork unitOfWork,
        ILogger<${EntityName}Service> logger)
        : base(repository, unitOfWork, logger)
    {
    }

}
"@

Set-Content -Path $serviceImplFile -Value $serviceImplContent -Encoding UTF8
Write-Host "  Created: $serviceImplFile" -ForegroundColor Green

# ============================================================================
# 6. Optional: Register Services in DI Container
# ============================================================================
if ($RegisterServices) {
    Write-Host ""
    Write-Host "Registering services in DI container..." -ForegroundColor Cyan

    $serviceRegFile = Join-Path $bootstrapPath "ServiceRegistration.cs"

    if (-not (Test-Path $serviceRegFile)) {
        Write-Warning "ServiceRegistration.cs not found at: $serviceRegFile"
        Write-Warning "Skipping service registration..."
    }
    else {
        $content = Get-Content $serviceRegFile -Raw

        # Check if already registered
        if ($content -match "I${EntityName}Repository") {
            Write-Warning "$EntityName is already registered in ServiceRegistration.cs"
        }
        else {
            # Find the insertion point (look for the last repository registration)
            $lines = Get-Content $serviceRegFile

            $insertIndex = -1
            for ($i = $lines.Count - 1; $i -ge 0; $i--) {
                if ($lines[$i] -match 'AddScoped.*Repository') {
                    $insertIndex = $i + 1
                    break
                }
            }

            if ($insertIndex -eq -1) {
                Write-Warning "Could not find insertion point in ServiceRegistration.cs"
                Write-Warning "Please manually add the following lines:"
                $msg1 = '    services.AddScoped<I' + $EntityName + 'Repository, ' + $EntityName + 'Repository>();'
                $msg2 = '    services.AddScoped<I' + $EntityName + 'Service, ' + $EntityName + 'Service>();'
                Write-Host $msg1 -ForegroundColor Gray
                Write-Host $msg2 -ForegroundColor Gray
            }
            else {
                # Insert the new registrations
                $line1 = '        services.AddScoped<I' + $EntityName + 'Repository, ' + $EntityName + 'Repository>();'
                $line2 = '        services.AddScoped<I' + $EntityName + 'Service, ' + $EntityName + 'Service>();'
                $newLines = @($line1, $line2)

                $updatedLines = $lines[0..($insertIndex - 1)] + $newLines + $lines[$insertIndex..($lines.Count - 1)]

                Set-Content -Path $serviceRegFile -Value $updatedLines -Encoding UTF8

                Write-Host "  Added $EntityName registration to ServiceRegistration.cs" -ForegroundColor Green
                Write-Host ""
                Write-Host "  Added lines:" -ForegroundColor DarkGray
                $newLines | ForEach-Object { Write-Host "  $_" -ForegroundColor DarkGray }
            }
        }
    }
}

# ============================================================================
# Summary
# ============================================================================
Write-Host ""
Write-Host "========================================================================================================================" -ForegroundColor Yellow
Write-Host "  Successfully generated all files for: $EntityName" -ForegroundColor Green
Write-Host "========================================================================================================================" -ForegroundColor Yellow
Write-Host ""
Write-Host "Files created:" -ForegroundColor Cyan
Write-Host "  1. Entities\$EntityName.cs" -ForegroundColor White
Write-Host "  2. Contracts\Repositories\I${EntityName}Repository.cs" -ForegroundColor White
Write-Host "  3. Data\Repositories\${EntityName}Repository.cs" -ForegroundColor White
Write-Host "  4. Contracts\Services\I${EntityName}Service.cs" -ForegroundColor White
Write-Host "  5. BLL\${EntityName}Service.cs" -ForegroundColor White

if ($RegisterServices) {
    Write-Host "  6. Updated Bootstrap\ServiceRegistration.cs" -ForegroundColor White
}

Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  1. Add entity-specific properties to: $EntityName.cs" -ForegroundColor White

if (-not $RegisterServices) {
    Write-Host "  2. Register services in Bootstrap/ServiceRegistration.cs:" -ForegroundColor White
    $msg1 = '     services.AddScoped<I' + $EntityName + 'Repository, ' + $EntityName + 'Repository>();'
    $msg2 = '     services.AddScoped<I' + $EntityName + 'Service, ' + $EntityName + 'Service>();'
    Write-Host $msg1 -ForegroundColor DarkGray
    Write-Host $msg2 -ForegroundColor DarkGray
    $nextStep = 3
}
else {
    $nextStep = 2
}

Write-Host "  $nextStep. Add EF Core configuration in Data/Configurations/${EntityName}Configuration.cs" -ForegroundColor White
$nextStep++
Write-Host "  $nextStep. Add DbSet to AppDbContext:" -ForegroundColor White
Write-Host "     public DbSet<$EntityName> ${EntityName}s { get; set; }" -ForegroundColor DarkGray
$nextStep++
Write-Host "  $nextStep. Create and run migration:" -ForegroundColor White
Write-Host "     dotnet ef migrations add Add${EntityName} --project src\GreenCross.Mammals.Data --startup-project src\GreenCross.Mammals.UI" -ForegroundColor DarkGray
Write-Host "     dotnet ef database update --project src\GreenCross.Mammals.Data --startup-project src\GreenCross.Mammals.UI" -ForegroundColor DarkGray
Write-Host ""


