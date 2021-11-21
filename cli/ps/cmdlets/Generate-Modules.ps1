param(
    [string]$filePath,
    [string]$physicalRootPathOldValue = "`$(sourceFolder)",
    [string]$physicalRootPathReplacement = "~/../src",
    [string]$outputFolder = ".\cli\_out"
)

Write-Host $filePath -ForegroundColor Green
[xml]$config = Get-Content -Path $filePath
if ($null -eq $config.configuration.sitecore.unicorn) {
    return
}

$config.configuration.sitecore.unicorn.configurations.configuration | % {
    Write-Host $_.name -ForegroundColor Yellow
    if ($_.targetDataStore -eq $null) {
        return
    }
    [System.Collections.Specialized.OrderedDictionary]$result = @{}
    $result.namespace = $_.name
    $result.items = [System.Collections.Specialized.OrderedDictionary]@{}
    $result.items.path = $_.targetDataStore.physicalRootPath.Replace($physicalRootPathOldValue, $physicalRootPathReplacement).Replace("\", "/")

    [array]$result.items.includes = $_.predicate.include | % {
        $include = $_

        [System.Collections.Specialized.OrderedDictionary]$obj = @{}
        $obj.name = $include.name
        $obj.database = $include.database
        $obj.path = $include.path
        if ($include.exclude.children -eq $true) {
            if ($include.exclude.except -ne $null) {
                #workaround
                Write-Host "Contains - EXCLUDE WITH EXCEPT" -ForegroundColor DarkRed
                $obj.scope = "ItemAndDescendants"
            }
            else {
                $obj.scope = "SingleItem"
            }
        }
        else {
            $obj.scope = "ItemAndDescendants"
            if ($include.exclude.path.length -gt 0) {
                $obj.rules = @()
                $include.exclude.path | ? { $_ -ne $null } | % {
                    $path = $_
                    $obj.rules += @{
                        path  = "/$path"
                        scope = "ignored"
                    }
                }
            }
        }
        $obj
    }
    $val = $result | ConvertTo-Json -Depth 10
    Set-Content -Value $val -Path "$outputFolder\$($_.name).module.json" -Force
} | Out-Null