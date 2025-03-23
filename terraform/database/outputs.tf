output "database_connection_name" {
  description = "The connection name of the database instance to be used in connection strings"
  value       = google_sql_database_instance.mssql_instance.connection_name
}

output "database_instance_name" {
  description = "The name of the database instance"
  value       = google_sql_database_instance.mssql_instance.name
}

output "database_public_ip" {
  description = "The public IP address of the database instance"
  value       = google_sql_database_instance.mssql_instance.public_ip_address
}

output "database_name" {
  description = "The name of the created database"
  value       = google_sql_database.database.name
}