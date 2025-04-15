
REM deleting publish folder
rmdir /Q /S "%~dp0bin\Publish\win-x64"
dotnet publish --nologo -r win-x64 --self-contained true -p:PublishSingleFile=true --configuration Release --output "%~dp0bin\Publish\win-x64"
pause

