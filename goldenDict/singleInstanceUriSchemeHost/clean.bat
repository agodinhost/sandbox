@ECHO OFF

@ECHO cleaning the house with no undies ...

rmdir /S /Q .\.vs
rmdir /S /Q .\bin
rmdir /S /Q .\Client\obj
rmdir /S /Q .\Installer\obj

del /S /F /AH *.suo
del /S /F *.aps
del /S /F *.user 
del /S /F *.ncb
del /S /F *.sbr
del /S /F *.log

@rem delete solution binary file.
for /R /D %%i in (*.suo) do @ECHO "%%i"
for /R /D %%i in (*.suo) do @DEL "%%i"

@rem delete the Intellisense file.
for /R /D %%i in (*.aps) do @ECHO "%%i"
for /R /D %%i in (*.aps) do @DEL "%%i"

@rem delete the Intellisense file. (vs2008)
for /R /D %%i in (*.ncb) do @ECHO "%%i"
for /R /D %%i in (*.ncb) do @DEL "%%i"

@rem delete release binary folder.
for /R /D %%i in (*.Release) do @ECHO "%%i"
for /R /D %%i in (*.Release) do @RMDIR /S /Q "%%i"

@rem delete debug binary folder.
for /R /D %%i in (*.Debug) do @ECHO "%%i"
for /R /D %%i in (*.Debug) do @RMDIR /S /Q "%%i"

@ECHO done!