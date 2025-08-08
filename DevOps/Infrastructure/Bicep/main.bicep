targetScope = 'subscription'

param resourceGroupUKSParam object
param logAnalyticsParam object
param applicationInsightsParam object
param tagsParam object = {}

var serviceName = 'core-transaction-request'

module resourceGroupUKS 'Modules/resource-group.bicep' = {
  name: '${serviceName}-ResourceGroupUks'
  scope: subscription()
  params: {
    resourceGroup: resourceGroupUKSParam
    tags: tagsParam
  }
}

module logAnalytics 'Modules/log-analytics.bicep' = {
  scope: resourceGroup(resourceGroupUKSParam.name)
  dependsOn: [
    resourceGroupUKS
  ]
  name: '${serviceName}-LogAnalytics'
  params: {
    logAnalytics: logAnalyticsParam
    location: resourceGroupUKSParam.location
    tags: tagsParam
  }
}

module applicationInsights 'Modules/application-insights.bicep' = {
  scope: resourceGroup(resourceGroupUKSParam.name)
  dependsOn: [
    resourceGroupUKS
    logAnalytics
  ]
  name: '${serviceName}-ApplicationInsights'
  params: {
    applicationInsights: applicationInsightsParam
    logAnalytics: logAnalyticsParam
    location: resourceGroupUKSParam.location
    tags: tagsParam
  }
}
