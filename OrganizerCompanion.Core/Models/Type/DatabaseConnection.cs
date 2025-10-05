using OrganizerCompanion.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrganizerCompanion.Core.Models.Type
{
    internal class DatabaseConnection
    {
        #region Fields
        private readonly static JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Converters = { new JsonStringEnumConverter() }
        };
        #endregion

        #region Properties
        [Required, JsonPropertyName("connectionString")]
        public string? ConnectionString { get; set; } = null;
        [Required, JsonPropertyName("databaseType")]
        public SupportedDatabases? DatabaseType { get; set; } = null;
        #endregion

        #region Constructors
        public DatabaseConnection() { }

        [JsonConstructor]
        public DatabaseConnection(string? connectionString, SupportedDatabases? databaseType)
        {
            ConnectionString = connectionString;
            DatabaseType = databaseType;
        }
        #endregion

        #region Methods
        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public static DatabaseConnection? FromJson(string json)
        {
            return JsonSerializer.Deserialize<DatabaseConnection>(json, _serializerOptions);
        }
        #endregion
    }
}
