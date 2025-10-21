using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.Validation.Attributes
{
    /// <summary>
    /// Validates database connection strings based on the database type.
    /// Supports SQL Server, SQLite, MySQL, and PostgreSQL connection string formats.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal class DatabaseConnectionValidatorAttribute : ValidationAttribute
    {
        public DatabaseConnectionValidatorAttribute()
        {
            ErrorMessage = "The database connection string is not in a valid format for the specified database type.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Database connection cannot be null.");
            }

            // Check if the value is a DatabaseConnection object
            if (value is DatabaseConnection dbConnection)
            {
                if (dbConnection.DatabaseType is null)
                {
                    return new ValidationResult("Database type must be specified.");
                }

                if (string.IsNullOrWhiteSpace(dbConnection.ConnectionString))
                {
                    return new ValidationResult("Connection string cannot be null or empty.");
                }

                // Validate the connection string based on the database type
                string pattern = GetPatternForDatabaseType(dbConnection.DatabaseType.Value);
                
                if (!Regex.IsMatch(dbConnection.ConnectionString, pattern, RegexOptions.IgnoreCase))
                {
                    return new ValidationResult($"The connection string is not in a valid format for {dbConnection.DatabaseType} database.");
                }

                return ValidationResult.Success;
            }

            // If value is just a string, we can't determine the database type
            // Fall back to a generic validation
            if (value is string connectionString)
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    return new ValidationResult("Connection string cannot be null or empty.");
                }

                // Try to validate against any of the supported patterns
                if (IsValidForAnyDatabaseType(connectionString))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("The connection string is not in a valid format for any supported database type.");
            }

            return new ValidationResult("Invalid value type. Expected DatabaseConnection or string.");
        }

        private static string GetPatternForDatabaseType(SupportedDatabases databaseType)
        {
            return databaseType switch
            {
                SupportedDatabases.SQLServer => RegexValidators.SQLServerDbConnectionStringRegexPattern,
                SupportedDatabases.SQLite => RegexValidators.SQLiteDbConnectionStringRegexPattern,
                SupportedDatabases.MySQL => RegexValidators.MySQLDbConnectionStringRegexPattern,
                SupportedDatabases.PostgreSQL => RegexValidators.PostgreSQLDbConnectionStringRegexPattern,
                _ => throw new ArgumentException($"Unsupported database type: {databaseType}")
            };
        }

        private static bool IsValidForAnyDatabaseType(string connectionString)
        {
            // Check if the connection string matches any of the supported database patterns
            return Regex.IsMatch(connectionString, RegexValidators.SQLServerDbConnectionStringRegexPattern, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(connectionString, RegexValidators.SQLiteDbConnectionStringRegexPattern, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(connectionString, RegexValidators.MySQLDbConnectionStringRegexPattern, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(connectionString, RegexValidators.PostgreSQLDbConnectionStringRegexPattern, RegexOptions.IgnoreCase);
        }
    }
}
