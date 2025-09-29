using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Account : IAccount
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        
        private int _id = 0;
        private string? _accountName = null;
        private string? _accountNumber = null;
        private int _linkedEntityId = 0;
        private string? _linkedEntityType = null;
        private IDomainEntity? _linkedEntity = null;
        private bool _allowAnnonymousUsers = false;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(1, int.MaxValue, ErrorMessage = "ID must be a positive number")]
        public int Id 
        { 
            get => _id; 
            set 
            { 
                _id = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("userName")]
        public string? AccountName 
        { 
            get => _accountName; 
            set 
            { 
                _accountName = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("accountNumber")]
        public string? AccountNumber 
        { 
            get => _accountNumber; 
            set 
            { 
                _accountNumber = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(1, int.MaxValue, ErrorMessage = "ID must be a positive number")]
        public int LinkedEntityId 
        { 
            get => _linkedEntityId; 
            set 
            { 
                _linkedEntityId = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType 
        { 
            get => _linkedEntityType; 
            set 
            { 
                _linkedEntityType = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity 
        { 
            get => _linkedEntity; 
            set 
            { 
                _linkedEntity = value; 
                DateModified = DateTime.Now; 
            }
        }

        [Required, JsonPropertyName("allowAnnonymousUsers")]
        public bool AllowAnnonymousUsers
        {
            get => _allowAnnonymousUsers; 
            set 
            { 
                _allowAnnonymousUsers = value; 
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public Account() { }

        [JsonConstructor]
        public Account(
            int id,
            string? accountName,
            string? accountNumber,
            int linkedEntityId,
            string? linkedEntityType,
            IDomainEntity? linkedEntity,
            bool allowAnnonymousUsers,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));

                _id = id;
                _accountName = accountName;
                _accountNumber = accountNumber;
                _linkedEntityId = linkedEntityId;
                _linkedEntityType = linkedEntityType;
                _linkedEntity = linkedEntity;
                _allowAnnonymousUsers = allowAnnonymousUsers;
                _dateCreated = dateCreated;
                DateModified = dateModified;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Account object", ex);
            }
        }

        public Account(
            string? accountName,
            string? accountNumber,
            IDomainEntity linkedEntity,
            bool allowAnnonymousUsers,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            try
            {
                _accountName = accountName;
                _accountNumber = accountNumber;
                _linkedEntityId = linkedEntity.Id;
                _linkedEntityType = linkedEntity.GetType().Name;
                _linkedEntity = linkedEntity;
                _allowAnnonymousUsers = allowAnnonymousUsers;
                _dateCreated = dateCreated;
                DateModified = dateModified;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating Account object", ex);
            }
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.AccountName:{1}", _id, _accountName);
        #endregion
    }
}
