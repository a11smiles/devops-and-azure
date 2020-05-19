resource "azurerm_application_insights" "site" {
    name                    = "${var.geographyLbl}-${var.regionLbl}-${var.envLbl}-${var.scopeLbl}-ai"
    location                = "${var.location}"
    resource_group_name     = "${var.azurerm_resource_group_name}"
    application_type        = "web"
    tags                    = {
        deployment = "1.0.0"
    }
}

output "instrumentation_key" {
  value = "${azurerm_application_insights.site.instrumentation_key}"
}

output "app_id" {
  value = "${azurerm_application_insights.site.app_id}"
}