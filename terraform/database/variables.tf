variable "project_id" {
  description = "Timelogger Google Cloud Project ID"
  type        = string
}

variable "region" {
  description = "Google Cloud region"
  type        = string
  default     = "europe-west1"
}

variable "database_password" {
  description = "Password for the database user"
  type        = string
  sensitive   = true
}

variable "database_name" {
  description = "Name of the database to create"
  type        = string
  default     = "timelogger-prod-db"
}

variable "instance_name" {
  description = "Name of the SQL Server instance"
  type        = string
  default     = "timelogger-prod-mssql-instance"
}

variable "root_password" {
  description = "Password for the database root"
  type        = string
  sensitive   = true
}