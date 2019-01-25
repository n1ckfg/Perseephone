@echo off

cd %homepath%\AppData\Local\Android\Sdk\platform-tools
adb install %1

@pause