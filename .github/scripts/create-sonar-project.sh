curl -v -X POST https://$SONARCLOUD_TOKEN@sonarcloud.io/api/projects/create -d "name=$SONAR_PROJECT_NAME&project=$SONAR_PROJECT_NAME&organization=$SONARCLOUD_ORGANIZATION"
