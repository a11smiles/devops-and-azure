terraform {
  backend "azurerm" {}
}

provider "azurerm" {
  version = "=1.32.1"
  subscription_id = "${var.subscription_id}"
  tenant_id = "${var.tenant_id}"
}

provider "null" {
  version = "~>2.1"
}

module "monitoring" {
    source = "./modules/monitoring"
    geographyLbl = "${var.geographyLbl}"
    regionLbl = "${var.regionLbl}"
    envLbl = "${var.envLbl}"
    location = "${var.location}"
}

module "site" {
    source = "./modules/site"
    geographyLbl = "${var.geographyLbl}"
    regionLbl = "${var.regionLbl}"
    envLbl = "${var.envLbl}"
    location = "${var.location}"
    azurerm_resource_group_monitoring_name = "${module.monitoring.azurerm_resource_group_monitoring_name}"
}