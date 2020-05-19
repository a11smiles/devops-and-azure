resource "azurerm_resource_group" "site" {
    name        = "${var.geographyLbl}-${var.regionLbl}-${var.envLbl}-${var.scopeLbl}"
    location    = "${var.location}"
    tags        = {
        deployment = "1.0.0"
    }
}

module "ai" {
    source = "./ai"
    geographyLbl = "${var.geographyLbl}"
    regionLbl = "${var.regionLbl}"
    envLbl = "${var.envLbl}"
    scopeLbl = "${var.scopeLbl}"
    location = "${var.location}"
    azurerm_resource_group_name = "${var.azurerm_resource_group_monitoring_name}"
}

module "appsvc-plan" {
    source = "./appsvc-plan"
    geographyLbl = "${var.geographyLbl}"
    regionLbl = "${var.regionLbl}"
    envLbl = "${var.envLbl}"
    scopeLbl = "${var.scopeLbl}"
    location = "${var.location}"
    azurerm_resource_group_name = "${azurerm_resource_group.site.name}"
}

module "appsvc" {
    source = "./appsvc"
    geographyLbl = "${var.geographyLbl}"
    regionLbl = "${var.regionLbl}"
    envLbl = "${var.envLbl}"
    scopeLbl = "${var.scopeLbl}"
    location = "${var.location}"
    azurerm_resource_group_name = "${azurerm_resource_group.site.name}"
    azurerm_app_service_plan_id = "${module.appsvc-plan.service_plan_id}"
    azurerm_application_insights_instrumentation_key = "${module.ai.instrumentation_key}"
    azurerm_application_insights_app_id = "${module.ai.app_id}"
}