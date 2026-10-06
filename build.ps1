param([string]$KspRoot=(Join-Path (Split-Path -Parent $PSScriptRoot) 'template_instance'),[string]$RingworldRoot=(Join-Path (Split-Path -Parent $PSScriptRoot) 'Ring World KSP'),[switch]$Install,[switch]$Package)
$ErrorActionPreference='Stop'
& dotnet build (Join-Path $PSScriptRoot 'src/Ringworld.Scattering.csproj') -c Release --nologo "-p:KspRoot=$KspRoot" "-p:RingworldRoot=$RingworldRoot"
if($LASTEXITCODE -ne 0){throw 'Extension build failed; build the base mod first.'}
$taskStage=Join-Path $PSScriptRoot ('artifacts/stage-'+[Guid]::NewGuid().ToString('N'))
$taskMod=Join-Path $taskStage 'GameData/RingworldScattering'
New-Item -ItemType Directory -Path $taskMod -Force | Out-Null
Copy-Item -Path (Join-Path $PSScriptRoot 'GameData/RingworldScattering/*') -Destination $taskMod -Recurse -Force
New-Item -ItemType Directory -Path (Join-Path $taskMod 'Plugins') -Force | Out-Null
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'src/bin/Release/net472/Ringworld.Scattering.dll') -Destination (Join-Path $taskMod 'Plugins') -Force
if(-not(Test-Path -LiteralPath (Join-Path $taskMod 'Assets/ringworldscattering'))){throw 'Build shaders with build-visuals.ps1 first.'}
if($Install){$taskInstall=Join-Path $KspRoot 'GameData/RingworldScattering';New-Item -ItemType Directory -Path $taskInstall -Force | Out-Null;Copy-Item -Path (Join-Path $taskMod '*') -Destination $taskInstall -Recurse -Force}
if($Package){
 $taskDocs=Join-Path $taskMod 'Documentation'
 New-Item -ItemType Directory -Path $taskDocs -Force | Out-Null
 foreach($doc in @('README.md','RELEASE-NOTES.md','LICENSE','CREDITS.md','THIRD-PARTY-NOTICES.md','docs')){Copy-Item -LiteralPath (Join-Path $PSScriptRoot $doc) -Destination $taskDocs -Recurse -Force}
 $taskZip=Join-Path $PSScriptRoot ('artifacts/RingworldScattering-'+([xml](Get-Content -LiteralPath (Join-Path $PSScriptRoot 'src/Ringworld.Scattering.csproj') -Raw)).Project.PropertyGroup.Version+'.zip')
 Compress-Archive -Path (Join-Path $taskStage '*') -DestinationPath $taskZip -Force
 $taskHash=(Get-FileHash -LiteralPath $taskZip -Algorithm SHA256).Hash.ToLowerInvariant()
 ($taskHash+'  '+[IO.Path]::GetFileName($taskZip)) | Set-Content -LiteralPath ($taskZip+'.sha256')
}
