Hale-MRI Deployment & Installer Summary
1. Database Master SettingSetting Name: DbConnectionStringScope: User | Type: StringDesigner Value: (None) / Empty. This ensures the "First Run" logic triggers on all new client installs.
2. Startup Logic (Sub Main)Initialization: Call EnsureDatabaseIsReady() before building the DI container.
   Debug Automation: Uses #If DEBUG to auto-resolve the path to your Git LibDatabase folder so you aren't prompted while coding.
   Production Prompt: If the setting is empty, it prompts the user to use the default (C:\ProgramData\Hale-MRI) or Browse for a central shop database (supporting UNC/Network paths).
3. Setup Project Properties (F4 Window)ProductName: Hale-MRIManufacturer: Hale PropellerInstallAllUsers: True (Required for the icacls command to have Admin power).
   TargetPlatform: x86 (Matches your 32-bit DLLs and Access Engine).
4. File System & PermissionsApplication Folder: Includes .exe, AccessDatabaseEngine.exe, and logo.ico.
   Common App Data Folder: Contains a subfolder Hale-MRI with the template HaleMRI.accdb.Database
   File Properties: Permanent=True, ReadOnly=False, Vital=True.
5. Custom Actions (Custom Actions Editor)
   Install Node:AccessDatabaseEngine.exe -> Arguments: /quiet, InstallerClass=False. (Silent driver install).Commit Node:cmd.exe -> Arguments: /c icacls "[CommonAppDataFolder]Hale-MRI" /grant Users:(OI)(CI)M /T
   (Unlocks the folder so all shop users can share the database and create .laccdb lock files).
6. Branding & UIShortcuts: Created in User's Desktop with logo.ico linked via the Icon property.
   Add/Remove Programs: AddRemoveProgramsIcon set to logo.ico.
   UI Editor: Installation Folder -> InstallAllUsersVisible=False (Hides the "Just Me" option to force the Admin/Everyone install).
7. Build ConfigurationBitness: Solution and all projects set to x86 in Configuration Manager.Dev Machine Fix: 32-bit Access Engine installed via AccessDatabaseEngine.exe /quiet to coexist with 64-bit Office.
