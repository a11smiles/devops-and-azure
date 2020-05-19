resource "azurerm_app_service" "site" {
    name                    = "${var.geographyLbl}-${var.regionLbl}-${var.envLbl}-${var.scopeLbl}-appsvc"
    location                = "${var.location}"
    resource_group_name     = "${var.azurerm_resource_group_name}"
    app_service_plan_id     = "${var.azurerm_app_service_plan_id}"
    https_only              = true
    client_affinity_enabled = false
    tags                    = {
        deployment = "1.0.0"
    }

    site_config {
        always_on                = true
    }

    app_settings = {
        "APPINSIGHTS_INSTRUMENTATIONKEY"                    = "${var.azurerm_application_insights_instrumentation_key}"
        "APPINSIGHTS_PROFILERFEATURE_VERSION"               = "1.0.0"
        "APPINSIGHTS_SNAPSHOTFEATURE_VERSION"               = "1.0.0"
        "ApplicationInsightsAgent_EXTENSION_VERSION"        = "~2"
        "DiagnosticServices_EXTENSION_VERSION"              = "~3"
        "InstrumentationEngine_EXTENSION_VERSION"           = "~1"
        "SnapshotDebugger_EXTENSION_VERSION"                = "~1"
        "XDT_MicrosoftApplicationInsights_BaseExtensions"   = "~1"
        "XDT_MicrosoftApplicationInsights_Mode"             = "recommended"
        "APPINSIGHTS_JAVASCRIPT_ENABLED"                    = "true"
    }
}