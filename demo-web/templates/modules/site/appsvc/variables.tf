variable "geographyLbl" { 
    type    = "string"
}

variable "regionLbl" { 
    type    = "string"
}

variable "envLbl" {
    type    = "string"
}

variable "scopeLbl" {
    type    = "string"
    default = "SITE"
}

variable "location" {
    type    = "string"
}

variable "azurerm_resource_group_name" {
    type    = "string"
}

variable "azurerm_app_service_plan_id" {
    type    = "string"
}

variable "azurerm_application_insights_instrumentation_key" {
    type    = "string"
}

variable "azurerm_application_insights_app_id" {
    type    = "string"
}
