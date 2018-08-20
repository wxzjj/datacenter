rem *******************************Code Start*****************************
@echo off
set "Ymd=%date:~,4%_%date:~5,2%_%date:~8,2%"

C:\7-Zip\7z a Z:\WJSJZX_backup\WJSJZX_backup_%Ymd%.7z D:\4lib_dbbackup\WJSJZX_backup_%Ymd%_*.bak

@echo on
rem *******************************Code End*****************************
