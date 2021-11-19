Clear-Host
Push-Location $PSScriptRoot
gci . -r -Filter ".scindex" | Remove-Item

dotnet sitecore itemres create -o ../_out/xa.reference --overwrite -i  XA.Reference.*
Copy-Item ../_out/items.master.xa.reference.dat -Destination ../_out/items.web.xa.reference.dat -Force

Pop-Location


