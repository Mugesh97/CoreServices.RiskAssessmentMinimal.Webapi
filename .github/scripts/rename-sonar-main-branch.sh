curl -v -X POST https://$SONARCLOUD_TOKEN@sonarcloud.io/api/project_branches/rename -d "name=$SONAR_MAIN_BRANCH_NAME&project=$SONAR_PROJECT_NAME&organization=$SONARCLOUD_ORGANIZATION"
