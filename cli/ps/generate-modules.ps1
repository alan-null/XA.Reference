Clear-Host
Push-Location $PSScriptRoot
Get-ChildItem -Path '..\..\src\Foundation' -Recurse -Filter "*.Serialization.config" | % {
    .\cmdlets\Generate-Modules.ps1 -filePath $_.FullName -outputFolder "..\modules\Foundation"
}

Get-ChildItem -Path '..\..\src\Feature' -Recurse -Filter "*.Serialization.config" | % {
    .\cmdlets\Generate-Modules.ps1 -filePath $_.FullName -outputFolder "..\modules\Feature"
}

Get-ChildItem -Path '..\..\src\Project' -Recurse -Filter "*.Serialization.config" | % {
    .\cmdlets\Generate-Modules.ps1 -filePath $_.FullName -outputFolder "..\modules\Project"
}

Pop-Location