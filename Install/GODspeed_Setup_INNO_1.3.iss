; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "GODspeed"
#define MyAppVersion "1.3"
#define MyAppPublisher "Neurotoxin"
#define MyAppURL "https://twicker.ru/godspeed"
#define MyAppExeName "Neurotoxin.Godspeed.Shell.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{2709773A-8541-4CF4-B1EC-860F1D2199CE}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputDir=c:\Users\User\source\repos\Nemiroff\godspeed\Install\Output
OutputBaseFilename=GODspeed
SetupIconFile=c:\Users\User\source\repos\Nemiroff\godspeed\Neurotoxin.Godspeed\Neurotoxin.Godspeed.Shell\Resources\app.ico
UninstallDisplayIcon={app}\Neurotoxin.Godspeed.Shell.exe
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "c:\Users\User\source\repos\Nemiroff\godspeed\Install\Output\Neurotoxin.Godspeed.Shell.exe"; DestDir: "{app}"; Flags: ignoreversion; AfterInstall: FirewallInstall; BeforeInstall: NotifyInstall
Source: "c:\Users\User\source\repos\Nemiroff\godspeed\Install\Output\*"; Excludes: "*.xml, *.exe"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "dotNetFx40_Client_setup.exe"; DestDir: {tmp}; Flags: deleteafterinstall; AfterInstall: InstallFramework; Check: FrameworkIsNotInstalled

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent



[Code]
//NOTIFY INSTALL and UNINSTALL
procedure NotifyInstall();
var
  WinHttpReq: Variant;
begin
   WinHttpReq := CreateOleObject('WinHttp.WinHttpRequest.5.1');
   WinHttpReq.Open('POST', 'http://twicker.ru/godspeed/install.php', false);
   WinHttpReq.SetRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
   WinHttpReq.setRequestHeader('User-Agent', 'GODspeed');
   WinHttpReq.Send('Install');
end;
procedure NotifyUnInstall();
var
  WinHttpReq: Variant;
begin
   WinHttpReq := CreateOleObject('WinHttp.WinHttpRequest.5.1');
   WinHttpReq.Open('POST', 'http://twicker.ru/godspeed/uninstall.php', false);
   WinHttpReq.SetRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
   WinHttpReq.setRequestHeader('User-Agent', 'GODspeed');
   WinHttpReq.Send('UnInstall');
end;

//FRAMEWORK
function FrameworkIsNotInstalled: Boolean;
begin
    Result := not RegKeyExists(HKEY_LOCAL_MACHINE, 'Software\Microsoft\.NETFramework\policy\v4.0');
end;
procedure InstallFramework;
var
  StatusText: string;
  ResultCode: Integer;
begin
  StatusText := WizardForm.StatusLabel.Caption;
  WizardForm.StatusLabel.Caption := 'Installing .NET framework 4';
  WizardForm.ProgressGauge.Style := npbstMarquee;
  try
    if not Exec(ExpandConstant('{tmp}\dotNetFx40_Client_setup.exe'), '/q /noreboot', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
    begin
      MsgBox('.NET installation failed with code: ' + IntToStr(ResultCode) + '. Please install .net Framework 4.0', mbError, MB_OK);
    end;
  finally
    WizardForm.StatusLabel.Caption := StatusText;
    WizardForm.ProgressGauge.Style := npbstNormal;
  end;
end;

//FIREWALL
const
   NET_FW_IP_VERSION_ANY = 2;
   NET_FW_SCOPE_ALL = 0;
procedure FirewallInstall();
var
  Firewall, Application: Variant;
begin
  if MsgBox('Setup will now add itself to Windows Firewall as an authorized application for the current profile (' + GetUserNameString + '). Do you want to continue?', mbInformation, mb_YesNo) = idNo then
    Exit;
  try
    Firewall := CreateOleObject('HNetCfg.FwMgr');
  except
    RaiseException('Please install Windows Firewall first.'#13#13'(Error ''' + GetExceptionMessage + ''' occurred)');
  end;
  Application := CreateOleObject('HNetCfg.FwAuthorizedApplication');
  Application.Name := 'GODspeed';
  Application.IPVersion := NET_FW_IP_VERSION_ANY;
  Application.ProcessImageFileName := ExpandConstant(CurrentFilename);
  Application.Scope := NET_FW_SCOPE_ALL;
  Application.Enabled := True;
  Firewall.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(Application);
  MsgBox('GODspeed is now an authorized application in Firewall for the current profile', mbInformation, mb_Ok);
end;

//UNINSTALL APPDATA and NOTIFY
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
begin
  if CurUninstallStep = usPostUninstall then
  begin
    NotifyUnInstall();
    if DirExists(ExpandConstant('{userappdata}\Neurotoxin\GODspeed\')) = True then 
    begin
      if MsgBox('Do you want to delete database files?', mbConfirmation, MB_YESNO or MB_DEFBUTTON2) = IDYES then
      begin
        DelTree(ExpandConstant('{userappdata}\Neurotoxin\GODspeed\'), True, True, True);
      end;
    end;
  end;
end;

//CHECK UNINSTALL OLD
function InitializeSetup: Boolean;
var
  V: Integer;
  iResultCode: Integer;
  sUnInstallString: string;
begin
  Result := True; 
  if RegValueExists(HKEY_LOCAL_MACHINE,'Software\Microsoft\Windows\CurrentVersion\Uninstall\{88774D3E-772E-468D-8292-6427888E4270}', 'UninstallString') then 
  begin
    if MsgBox('An old version of GODspeed was detected. Do you want to uninstall it?', mbInformation, MB_YESNO) = IDYES then
    begin
      if Exec('msiexec.exe', '/uninstall {88774D3E-772E-468D-8292-6427888E4270}', '', SW_SHOW, ewWaitUntilTerminated, iResultCode) then
      begin
      end
      else begin
        MsgBox('Uninstall did not complete. It is possible that the old installation still exists. Please uninstall it manually.', mbInformation, MB_YESNO);
      end;
    end;
  end;
end;
