;------------------------------------------------------
;GiniMonara
;Version 1.0.0.0
;Copyright (C) 2008 GiniMonara Team
;Developer: Kesara Nanayakkara Rathnayake
;------------------------------------------------------

;--------------------------------
;Include

	!include "MUI.nsh"

;--------------------------------
;General

	;Name and file
	Name "GiniMonara"
	BrandingText "GiniMonara Version 1.0.0.0"
	OutFile "GiniMonara-1.0.0.0-Setup.exe"

	;Default installation folder
	InstallDir "$PROGRAMFILES\GiniMonara"
	
	;Get installation folder from registry if available
	InstallDirRegKey HKCU "Software\GiniMonara" ""

;--------------------------------
;Functions

;	Function .onInit
;		Push $R0 
;		ClearErrors
;		ReadRegStr $R0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5" "Version"
;		IfErrors lbl_err lbl_end
;		lbl_err:
;		MessageBox MB_OK|MB_ICONSTOP ".NET runtime library v3.5 or newer is required."
;		Abort

;		lbl_end:
;		Exch $0
;	FunctionEnd

 
;--------------------------------
;Variables

	Var STARTMENU_FOLDER
	Var MUI_TEMP

;--------------------------------
;Interface Settings

	!define MUI_ABORTWARNING
	
	!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\orange-install.ico"
	!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\orange-uninstall.ico"

	!define MUI_HEADERIMAGE
	!define MUI_HEADERIMAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Header\orange.bmp"
	!define MUI_HEADERIMAGE_UNBITMAP "${NSISDIR}\Contrib\Graphics\Header\orange-uninstall.bmp"

	!define MUI_HEADER_TRANSPARENT_TEXT "GiniMonara Version 1.0.0.0"

	!define MUI_WELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\orange.bmp"
	!define MUI_UNWELCOMEFINISHPAGE_BITMAP "${NSISDIR}\Contrib\Graphics\Wizard\orange-uninstall.bmp"

	!define MUI_FINISHPAGE_LINK "GiniMonara Home Page"
	!define MUI_FINISHPAGE_LINK_LOCATION "http://ginimonara.sourceforge.net/"

	!define MUI_FINISHPAGE_RUN "$INSTDIR\GiniMonara.exe"
	!define MUI_FINISHPAGE_RUN_TEXT "Run GiniMonara now!"

;--------------------------------
;Pages

	!insertmacro MUI_PAGE_WELCOME
	!insertmacro MUI_PAGE_LICENSE "GPLv3.txt"
	;!insertmacro MUI_PAGE_COMPONENTS
	!insertmacro MUI_PAGE_DIRECTORY

	;Start Menu Folder Page Configuration
	!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
	!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\GiniMonara" 
	!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"

	!insertmacro MUI_PAGE_STARTMENU Application $STARTMENU_FOLDER
	!insertmacro MUI_PAGE_INSTFILES
	!insertmacro MUI_UNPAGE_CONFIRM
	!insertmacro MUI_UNPAGE_INSTFILES
	!insertmacro MUI_PAGE_FINISH
	
;--------------------------------
;Languages
 
	!insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Calculator" SecMain

	SetOutPath "$INSTDIR"
	
	;Files
	File "GiniMonara.exe"
	File "GiniMonara.pdb"
	File "FlickrNet.dll"
	File "Google.GData.Client.dll"
	File "Google.GData.Extensions.dll"
	File "Google.GData.Photos.dll"
	File "Google.GData.YouTube.dll"
	File "Interop.QuartzTypeLib.dll"
	File "mRibbon.dll"

	SetOutPath "$INSTDIR\xml"
	File "Category.xml"
	
	;Store installation folder
	WriteRegStr HKCU "Software\GiniMonara" "" $INSTDIR
	
	;Create uninstaller
	WriteUninstaller "$INSTDIR\Uninstall.exe"

	!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
	  
	;Create shortcuts
	CreateDirectory "$SMPROGRAMS\$STARTMENU_FOLDER"
	CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\GiniMonara.lnk" "$INSTDIR\GiniMonara.exe"
	CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Online Help.url" "http://ginimonara.sourceforge.net/help/"
	CreateShortCut "$SMPROGRAMS\$STARTMENU_FOLDER\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
	
	!insertmacro MUI_STARTMENU_WRITE_END

	;Adding to Windows Add or Remove Programs
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GiniMonara" "DisplayName" "GiniMonara Version 1.0.0.0"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GiniMonara" "UninstallString" "$INSTDIR\uninstall.exe"

SectionEnd

;--------------------------------
;Descriptions

	;Language strings
	;LangString DESC_SecMain ${LANG_ENGLISH} "GiniMonara"

	;Assign language strings to sections
	;!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
	;!insertmacro MUI_DESCRIPTION_TEXT ${SecMain} $(DESC_SecMain)
	;!insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Section "Uninstall"

	;Files
	Delete "$INSTDIR\GiniMonara.exe"
	Delete "$INSTDIR\GiniMonara.pdb"
	Delete "$INSTDIR\FlickrNet.dll"
	Delete "$INSTDIR\Google.GData.Client.dll"
	Delete "$INSTDIR\Google.GData.Extensions.dll"
	Delete "$INSTDIR\Google.GData.Photos.dll"
	Delete "$INSTDIR\Google.GData.YouTube.dll"
	Delete "$INSTDIR\Interop.QuartzTypeLib.dll"
	Delete "$INSTDIR\mRibbon.dll"
	Delete "$INSTDIR\xml\Category.xml"

	RMDir "$INSTDIR\xml"
	

	Delete "$INSTDIR\Uninstall.exe"

	RMDir "$INSTDIR"

	!insertmacro MUI_STARTMENU_GETFOLDER Application $MUI_TEMP

	Delete "$SMPROGRAMS\$MUI_TEMP\Uninstall.lnk"
	Delete "$SMPROGRAMS\$MUI_TEMP\GiniMonara.lnk"
	Delete "$SMPROGRAMS\$MUI_TEMP\Online Help.url"


	;Delete empty start menu parent diretories
	StrCpy $MUI_TEMP "$SMPROGRAMS\$MUI_TEMP"
 
	startMenuDeleteLoop:
		ClearErrors
		RMDir $MUI_TEMP
		GetFullPathName $MUI_TEMP "$MUI_TEMP\.."
		IfErrors startMenuDeleteLoopDone
		StrCmp $MUI_TEMP $SMPROGRAMS startMenuDeleteLoopDone startMenuDeleteLoop
	startMenuDeleteLoopDone:
	

	DeleteRegKey /ifempty HKCU "Software\GiniMonara"
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\GiniMonara"

SectionEnd
