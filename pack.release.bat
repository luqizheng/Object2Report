dotnet restore src/Coder.Object2Report
dotnet restore src/Coder.Object2Report.Renders.NPOI
dotnet pack src/Coder.Object2Report.Renders.NPOI -c:release -o:%cd%
dotnet pack src/Coder.Object2Report -c:release -o:%cd%
