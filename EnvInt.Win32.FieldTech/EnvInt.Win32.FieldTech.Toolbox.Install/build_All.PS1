Clear-Host

Write-Host "Please make sure you do not have any instances of Visual studio running before running the script..." -ForegroundColor Red -BackgroundColor White

$svnRoot = "D:\eicore"
$scriptRoot = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install"
$googleDriveRoot = "$env:HOMEDRIVE\$env:HOMEPATH\Google Drive"
$FTTPath = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\obj\x86\Release\EnvInt.Win32.FieldTech.exe"
$FTTDirectory = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\obj\x86\Release\"
$msbuild = "C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"
$confuserex = "$svnRoot\Other\ConfuserEx\Confuser.CLI.exe"
$devenv = "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe"
$programVersion = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($FTTPath).FileVersion
$sharedInstallersFolder = "$googleDriveRoot\Ei_Software\Ei\FieldTechToolbox-MOC-Manager\Installers\$programVersion"
$NSI_FieldTech_Chinese = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install\FieldTechToolbox_Install_Chinese.nsi"
$NSI_FieldTech = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install\FieldTechToolbox_Install.nsi"
$NSI_FieldTech_MOC = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install\EiMOC_Install.nsi"
$NSI_FieldTech_MOC_Chinese = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install\EiMOC_Install_Chinese.nsi"
$NSI_FieldTech_Manager = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install\FieldTechManager_Install.nsi"
$NSIS = "C:\Program Files (x86)\NSIS\Bin\makensis.exe"

$CFG_FTT_Chinese = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\configs\EnvInt.Win32.FieldTech.exe_chinese.config"
$CFG_EIMOC_Chinese = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\configs\EnvInt.Win32.FieldTech.exe_eimoc_chinese.config"
$CFG_FTT = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\configs\EnvInt.Win32.FieldTech.exe.config"
$CFG_EIMOC = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\configs\EnvInt.Win32.FieldTech.exe_eimoc.config"
$CFG_Target = "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\obj\x86\Release\EnvInt.Win32.FieldTech.exe.config"

if (!(Test-Path -Path $sharedInstallersFolder))
{
    mkdir $sharedInstallersFolder
}

function setNSIVersion($file) {
    $content = Get-Content $file
    $content -replace "x.x.x.x", $programVersion | Set-Content "$file.nsi" -Force
}

function setAppConfig($file) {
    $content = Get-Content $file
    $content -replace "x.x.x.x", $programVersion | Set-Content "$file.nsi" -Force
}

# setup build environment
#Start-Process "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\Tools\VsDevCmd.bat"
#Write-Host $devenv
#Write-Host "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\EnvInt.Win32.FieldTech.Setup32bit.sln /rebuild Release"
#Write-Host $devenv "$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\EnvInt.Win32.FieldTech.sln /rebuild Release"

#build/obfuscate FieldTech
#Start-Process -FilePath $msbuild -ArgumentList ("$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\EnvInt.Win32.FieldTech.sln","/t:rebuild","/p:Configuration=Release") -Wait
Start-Process -FilePath $confuserex -ArgumentList ("$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\ConfuserWin32FieldTechToolbox.crproj -n") -Wait

#build FieldTech Manager
#Start-Process -FilePath $msbuild -ArgumentList ("$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Manager\EnvInt.Win32.FieldTech.Manager.sln","/t:rebuild","/p:Configuration=Release") -Wait
Start-Process -FilePath $confuserex -ArgumentList ("$svnRoot\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Manager\ConfuserWin32FieldTechManager.crproj -n") -Wait

#build FieldTech Setup
Write-Host "Building FTT Setup"
Copy-Item $CFG_FTT $FTTDirectory -Force
setNSIVersion $NSI_FieldTech
Start-Process -FilePath $NSIS -ArgumentList "$NSI_FieldTech.nsi" -Wait
del "$NSI_FieldTech.nsi"

#build EiMOC Setup
Write-Host "Building MOC Setup"
Copy-Item $CFG_EIMOC $CFG_Target -Force
setNSIVersion $NSI_FieldTech_MOC
Start-Process -FilePath $NSIS -ArgumentList "$NSI_FieldTech_MOC.nsi" -Wait
del "$NSI_FieldTech_MOC.nsi"

#build EiMOC Chinese Setup
Write-Host "Building MOC Chinese Setup"
Copy-Item $CFG_EIMOC_Chinese $CFG_Target -Force
setNSIVersion $NSI_FieldTech_MOC_Chinese
Start-Process -FilePath $NSIS -ArgumentList "$NSI_FieldTech_MOC_Chinese.nsi" -Wait
del "$NSI_FieldTech_MOC_Chinese.nsi"

#build FTT Chinese Setup
Write-Host "Building FTT Chinese Setup"
Copy-Item $CFG_FTT_Chinese $CFG_Target -Force
setNSIVersion $NSI_FieldTech_Chinese
Start-Process -FilePath $NSIS -ArgumentList "$NSI_FieldTech_Chinese.nsi" -Wait
del "$NSI_FieldTech_Chinese.nsi"

#build Manager Setup
Write-Host "Building Manager Setup"
setNSIVersion $NSI_FieldTech_Manager
Start-Process -FilePath $NSIS -ArgumentList "$NSI_FieldTech_Manager.nsi" -Wait
del "$NSI_FieldTech_Manager.nsi"

ForEach($insFileNew in Get-ChildItem "$scriptRoot\*.exe")
{
    Copy-Item -Path $insFileNew -Destination $sharedInstallersFolder -Force
}

Write-Host "Done!"