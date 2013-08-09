@echo off
@setlocal enableextensions

set DIR=%~dp0
if %DIR:~-1%==\ set DIR=%DIR:~0,-1%


set SERVICECONTRACT=lbkmobile
set CLASS=LbkMobile

:main
REM set SHAREDLIB="%DIR%\..\..\lib\app shared\AssemblyDto.dll"
REM if exist %SHAREDLIB% goto buildproxy
REM echo Unable to detect current %SHAREDLIB%. Build may not succeed.
REM goto end


:buildproxy
set TOOLPATH="%DIR%\..\..\tools\svcutil\wsdl.exe"
set SERVICEURL="https://lbkmobile.loewenbraeukeller.com/service/service1.asmx?WSDL"
REM set SERVICEURL="%DIR%\Wsdl\%SERVICECONTRACT%.wsdl"

set OUTPUTPATH=%DIR%\Proxies\
REM same as set OUTPUTPATH="%cd%"

set CONFIGNAME=
REM set CONFIGNAME="%CLASS%.config"
set OUTPUTFILE="%OUTPUTPATH%%CLASS%Proxy4.cs"
set NAMESPACE="Lbk.Mobile.Data.Service.Proxies"
set COLLECTIONTYPE=System.Collections.Generic.List`1
set TARGETCLIENTVERSION=
REM set TARGETCLIENTVERSION=Version35

set REFERENCEDASSEMBLYFLAG1=
set REFERENCEDASSEMBLYFLAG2=
set REFERENCEDASSEMBLYFLAG3=
REM set REFERENCEDASSEMBLYFLAG?=/reference:"%DIR%\..\..\lib\AssemblyDto.dll"

set ASYNCFLAG=
REM set ASYNCFLAG=/async

set SERIALIZABLEFLAG=
REM set SERIALIZABLEFLAG=/serializable

set SERIALIZERFLAG=
REM /serializer:XmlSerializer
REM /serializer:DataContractSerializer
REM /serializer:Auto

set ENABLEDATABINDINGFLAG=
REM set ENABLEDATABINDINGFLAG=/enableDataBinding


%TOOLPATH% ^
/out:%OUTPUTFILE% ^
/namespace:%NAMESPACE% ^
/l:CS ^
%SERVICEURL%

goto end


:end
pause