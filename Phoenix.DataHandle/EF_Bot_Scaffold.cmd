@echo off
set /p cs="Enter PhoenixDB Connection String: "

dotnet ef dbcontext scaffold "%cs%" Microsoft.EntityFrameworkCore.SqlServer ^
--context PhoenixBotContext ^
--output-dir "Bot\Models" ^
--use-database-names ^
--force ^
--table "dbo.BotFeedback"

Pause