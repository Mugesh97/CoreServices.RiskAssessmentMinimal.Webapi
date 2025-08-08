mkdir -p $SCANNER_DIRECTORY
dotnet tool update dotnet-sonarscanner --ignore-failed-sources --tool-path $SCANNER_DIRECTORY