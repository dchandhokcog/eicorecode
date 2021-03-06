; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "FieldTech Toolbox"
!define PRODUCT_VERSION "x.x.x.x"
!define PRODUCT_PUBLISHER "Environmental Intellect"
!define PRODUCT_WEB_SITE "http://www.env-int.com"
!define PRODUCT_UNINST_KEY "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
RequestExecutionLevel admin

; MUI 1.67 compatible ------
!include "MUI2.nsh"
!include "LogicLib.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "D:\eicore\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\cube.ico"
!define MUI_UNICON "D:\eicore\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\cube.ico"
!define MUI_WELCOMEFINISHPAGE_BITMAP "D:\eicore\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\Resources\EiLogoTall.bmp"
!define MUI_FINISHPAGE_RUN
!define MUI_FINISHPAGE_RUN_TEXT "Launch FieldTech Toolbox"
!define MUI_FINISHPAGE_RUN_FUNCTION "LaunchLink"

;setup script directory and input file(s)
!define baseFileDir "D:\eicore\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox\obj\x86\Release\"
!define scriptDir "D:\eicore\EnvInt.Win32\EnvInt.Win32.FieldTech\EnvInt.Win32.FieldTech.Toolbox.Install\"
!define installSubFolder "\Environmental Intellect\FieldTech Toolbox"
!define installerOutFile "FieldTechToolbox_Setup_Chinese_${PRODUCT_VERSION}.exe"
!define defaultInstallDir "$PROGRAMFILES\EnvironmentalIntellect\FieldTechToolbox"
!define linkName "FieldTechToolbox.lnk"
!define installFile "EnvInt.Win32.FieldTech.exe"
!define UninstallIcon "$MUI_UNICON"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page

!macro VerifyUserIsAdmin
UserInfo::GetAccountType
pop $0
${If} $0 != "admin" ;Require admin rights on NT4+
        messageBox mb_iconstop "Administrator rights required!"
        setErrorLevel 740 ;ERROR_ELEVATION_REQUIRED
        quit
${EndIf}
!macroend

Page instFiles

Function LaunchLink
  ExecShell "" "$INSTDIR\${linkName}"
FunctionEnd

Function .onInit

IfFileExists $WINDIR\SYSWOW64\*.* Is64bit Is32bit
Is32bit:
    SetRegView 32
    GOTO End32Bitvs64BitCheck

Is64bit:
    SetRegView 64

End32Bitvs64BitCheck:

FunctionEnd

!insertmacro MUI_PAGE_INSTFILES
; Finish page
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile ${installerOutFile}
InstallDir ${defaultInstallDir}
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetShellVarContext current
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File /r "${baseFileDir}"
  CreateShortCut "$DESKTOP\${linkName}" "$INSTDIR\${installFile}"
  CreateShortCut "$INSTDIR\${linkName}" "$INSTDIR\${installFile}"
  WriteRegStr HKLM "Software\Classes\.eid" "" "FieldTechToolbox.Document"
  WriteRegStr HKLM "Software\Classes\FieldTechToolbox.Document" "" "FieldTechToolbox Document"
  WriteRegStr HKLM "Software\Classes\FieldTechToolbox.Document\DefaultIcon" "" "$INSTDIR\${installFile},0"
  WriteRegStr HKLM "Software\Classes\FieldTechToolbox.Document\shell\open\command" "" '"$INSTDIR\${installFile}" "%1"'
SectionEnd

Section -AdditionalIcons
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateDirectory "$SMPROGRAMS\Environmental Intellect"
  CreateDirectory "$SMPROGRAMS${installSubFolder}"
  CreateShortCut "$SMPROGRAMS${installSubFolder}\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS${installSubFolder}\${linkName}" "$INSTDIR\${installFile}"
  CreateShortCut "$SMPROGRAMS${installSubFolder}\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  CreateDirectory "$INSTDIR\Resources"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "${PRODUCT_NAME}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "QuietUninstallString" "$\"$INSTDIR\uninst.exe$\" /S"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\uninst.exe,0"
  
  ;AccessControl::GrantOnFile "$INSTDIR\${installFile}" "(BU)" "FullAccess"
  ;AccessControl::GrantOnFile "$INSTDIR" "(BU)" "FullAccess"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit

  IfFileExists $WINDIR\SYSWOW64\*.* Is64bit Is32bit
  Is32bit:
    SetRegView 32
    GOTO End32Bitvs64BitCheck

  Is64bit:
    SetRegView 64

  End32Bitvs64BitCheck:

  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\*"
  Delete "$INSTDIR\Resources\*"
  Delete "$INSTDIR\TempPE\*"
  Delete "$INSTDIR\zh-CHS\*"
  Delete "$INSTDIR\zh-Hans\*"

  Delete "$SMPROGRAMS${installSubFolder}\Uninstall.lnk"
  Delete "$SMPROGRAMS${installSubFolder}\Website.lnk"
  Delete "$SMPROGRAMS${installSubFolder}\${linkName}"
  Delete "$DESKTOP\${linkName}"
  Delete "$INSTDIR\uninst.exe"
  
  RMDir "$SMPROGRAMS${installSubFolder}"
  RMDir "$INSTDIR\Resources"
  RMDir "$INSTDIR\TempPE"
  RMDir "$INSTDIR\zh-CHS"
  RMDir "$INSTDIR\zh-Hans"
  RMDir /r "$INSTDIR"

  DeleteRegKey HKLM "Software\Classes\.eid"
  DeleteRegKey HKLM "Software\Classes\FieldTechToolbox.Document"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
  SetAutoClose true
  
SectionEnd