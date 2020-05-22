resource "azurerm_app_service_plan" "site" {
    name                = "${var.geographyLbl}-${var.regionLbl}-${var.envLbl}-${var.scopeLbl}-appsvc-plan"
    location            = "${var.location}"
    resource_group_name = "${var.azurerm_resource_group_name}"
    tags                = {
        deployment = "1.0.1"
    }

    sku {
        tier    = "Basic"
        size    = "B1"
    }
}

output "service_plan_id" {
    value = "${azurerm_app_service_plan.site.id}"
}