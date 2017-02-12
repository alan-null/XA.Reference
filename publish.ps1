#### Configuration
$deleteExistingFiles = $false
$configuration  = "Debug"

$publishsettings = ".\publishsettings.targets"
$sxaReferenceConfig = ".\zzz.Sxa.Reference.config"
####


function Test-ConfigExists($configName){
    if(Test-Path $configName){
        $true
    }else{
        Write-Warning "Could not find config: '$($configName)"
        Write-Warning "Make a copy of '$($currentDirectory)\$($configName).example' file. "
        Write-Warning "Then remove '.example' from the file name and fill its content with your settings."
        $false
    }
}


if(-not (Test-ConfigExists $sxaReferenceConfig) -or -not(Test-ConfigExists $publishsettings)){
    exit
}

[xml]$targets = Get-Content -Path $publishsettings
$publishUrl = $targets.Project.PropertyGroup.publishUrl
$siteName = (Split-Path $publishUrl -NoQualifier).TrimStart("\").TrimStart("/")

$sxa_site = Get-Website | ? { $_.Name -eq $siteName }
$publishPath = $sxa_site.physicalPath
$currentDirectory = Get-Item .

clear

Write-Host "1. Restoring Nuget packages" -ForegroundColor "Green"
.\nuget\nuget.exe restore .\XA.Reference.sln 

Write-Host "2. Building projects" -ForegroundColor "Green"
Get-ChildItem $currentDirectory.FullName -Recurse -Filter "*.csproj"| %{
    $projectPath = $_.FullName.Replace($currentDirectory.FullName,".")
    Write-Host "`tBuilding project $($_.Name)" -ForegroundColor "Cyan"
    msbuild $projectPath /p:Configuration=$configuration /p:Platform=AnyCPU /t:WebPublish /p:WebPublishMethod=FileSystem /p:DeleteExistingFiles=$deleteExistingFiles /p:publishUrl=$publishPath /v:q
}


Write-Host "3. Deploying '$sxaReferenceConfig'" -ForegroundColor "Green"
Copy-Item $sxaReferenceConfig "$($publishPath)\App_Config\Include"


