resource "google_sql_database_instance" "mssql_instance" {
  name             = var.instance_name
  database_version = "SQLSERVER_2017_EXPRESS"
  region           = var.region
  root_password = var.root_password
  
  settings {
    tier = "db-custom-1-3840" 

    ip_configuration {
      ipv4_enabled    = true
      
      authorized_networks {
        name  = "all"
        value = "0.0.0.0/0" # For development only - restrict in production
      }
    }
  }
  
  deletion_protection = false # Set to true for production
}

resource "google_sql_database" "database" {
  name     = var.database_name
  instance = google_sql_database_instance.mssql_instance.name
}

resource "google_sql_user" "users" {
  name     = "sql_user"
  instance = google_sql_database_instance.mssql_instance.name
  password = var.database_password
}
