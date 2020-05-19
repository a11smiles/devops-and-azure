resource "azurerm_resource_group" "monitoring" {
    name    = "${var.geographyLbl}-${var.regionLbl}-${var.envLbl}-${var.scopeLbl}"
    location = "${var.location}"
    tags = {
        deployment = "1.0.0"
    }
}

output "azurerm_resource_group_monitoring_name" {
    value = "${azurerm_resource_group.monitoring.name}"
}