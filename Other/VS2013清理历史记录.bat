@echo off
cd \
@echo on
@REG Delete HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\12.0\FileMRUList /va /f
@REG Delete HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\12.0\ProjectMRUList /va /f
@echo off
cmd

@echo on
