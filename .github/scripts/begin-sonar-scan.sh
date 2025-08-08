./.sonar/scanner/dotnet-sonarscanner begin /k:"$SONAR_PROJECT_NAME" \
/d:sonar.login="$SONARCLOUD_TOKEN" \
/o:"$SONARCLOUD_ORGANIZATION" \
/d:sonar.host.url="https://sonarcloud.io" \
/d:sonar.coverage.exclusions="$SONAR_COVERAGE_EXCLUSION" \
/d:sonar.exclusion="$SONAR_EXCLUSION" \
/d:sonar.cs.opencover.reportsPaths="$SONAR_OPENCOVER_REPORTPATHS" \
/d:sonar.cs.vstest.reportsPaths="$SONAR_VSTEST_REPORTPATHS" 