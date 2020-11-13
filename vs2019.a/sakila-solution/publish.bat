@echo off

rd "\\192.168.56.146\var\www\sakila" /q /s
md "\\192.168.56.146\var\www\sakila"
dotnet publish "D:\github\mysql-sakila\vs2019.a\sakila-solution\sakila.web" -t:Clean -p:Configuration=Release
xcopy "D:\github\mysql-sakila\vs2019.a\sakila-solution\sakila.web\bin\Release\netcoreapp3.1\publish\" "\\192.168.56.146\var\www\sakila" /h /i /c /k /e /r /y