using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Registries;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class SubAccount : ISubAccount
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private int _linkedEntityId = 0;
        private string? _linkedEntityType = null;
        private IDomainEntity? _linkedEntity = null;
        private int? _accountId = null;
        private IAccount? _account = null;
        private readonly DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Id must be a non-negative number.");
                _id = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int LinkedEntityId
        {
            get => _linkedEntityId;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Linked Entity Id must be a non-negative number.");
                _linkedEntityId = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("linktedEntityType"), MaxLength(50, ErrorMessage = "Linked Account Id cannot exceed 50 characters.")]
        public string? LinkedEntityType
        {
            get => _linkedEntityType;
        }

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get => _linkedEntity;
            set
            {
                _linkedEntity = value;
                _linkedEntityType = value?.GetType().Name;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int? AccountId
        {
            get => _accountId;
            set
            {
                if (value != null && value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Account Id must be a non-negative number.");
                _accountId = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("account")]
        public IAccount? Account
        {
            get => _account;
            set
            {
                _account = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public SubAccount() { }

        [JsonConstructor]
        public SubAccount(
            int id, 
            int linkedEntityId, 
            string? linkedEntityType, 
            IDomainEntity? linkedEntity, 
            int? accountId, 
            IAccount? account, 
            DateTime dateCreated, 
            DateTime? dateModified)
        {
            _id = id;
            _linkedEntityId = linkedEntityId;
            _linkedEntityType = linkedEntityType; // Set the type first
            _linkedEntity = linkedEntity; // Set the entity without triggering the setter that would override _linkedEntityType
            _accountId = accountId;
            _account = account;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public SubAccount(
            IDomainEntity? linkedEntity,
            IAccount? account)
        {
            _linkedEntityId = linkedEntity?.Id ?? 0;
            _linkedEntityType = linkedEntity?.GetType().Name;
            LinkedEntity = linkedEntity;
            _accountId = account?.Id;
            Account = account;
        }
        public SubAccount(ISubAccountDTO account)
        {
            _id = account.Id;
            _linkedEntityId = account.LinkedEntityId;
            _linkedEntityType = account.LinkedEntityType;
            _linkedEntity = account.LinkedEntity?.Cast<IDomainEntity>();
            _accountId = account.AccountId;
            _account = account.Account?.Cast<IAccount>();
            _dateCreated = account.DateCreated;
            DateModified = account.DateModified;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Dynamically casts the LinkedEntity using TypeRegistry
        /// </summary>
        /// <typeparam name="T">The target type to cast to</typeparam>
        /// <returns>The LinkedEntity cast to type T</returns>
        /// <exception cref="InvalidOperationException">Thrown when LinkedEntity is null or LinkedEntityType is not set</exception>
        /// <exception cref="InvalidCastException">Thrown when the cast is not possible</exception>
        public T CastLinkedEntity<T>() where T : class, IDomainEntity
        {
            if (_linkedEntity == null)
                throw new InvalidOperationException("LinkedEntity is null and cannot be cast.");
            
            if (string.IsNullOrEmpty(_linkedEntityType))
                throw new InvalidOperationException("LinkedEntityType is not set.");

            // Check if type is registered in the TypeRegistry
            var targetType = TypeRegistry.GetType(_linkedEntityType);
            if (targetType == null)
            {
                // Fallback to reflection if not in registry
                targetType = System.Type.GetType($"OrganizerCompanion.Core.Models.Domain.{_linkedEntityType}") 
                           ?? System.Type.GetType($"OrganizerCompanion.Core.Models.DataTransferObject.{_linkedEntityType}")
                           ?? System.Type.GetType(_linkedEntityType);
                
                if (targetType == null)
                    throw new InvalidCastException($"Could not find type '{_linkedEntityType}'.");
            }

            if (!typeof(T).IsAssignableFrom(targetType) && targetType != typeof(T))
                throw new InvalidCastException($"Cannot cast LinkedEntity of type '{_linkedEntityType}' to '{typeof(T).Name}'.");

            // If the LinkedEntity is already of the correct type, return it directly
            if (_linkedEntity is T directCast)
                return directCast;

            // If the LinkedEntity has a Cast method, use it
            if (_linkedEntity.GetType().GetMethod("Cast")?.MakeGenericMethod(typeof(T)) is var castMethod && castMethod != null)
            {
                try
                {
                    return (T)castMethod.Invoke(_linkedEntity, null)!;
                }
                catch (Exception ex)
                {
                    throw new InvalidCastException($"Failed to cast LinkedEntity to type '{typeof(T).Name}': {ex.Message}", ex);
                }
            }

            throw new InvalidCastException($"Cannot cast LinkedEntity of type '{_linkedEntity.GetType().Name}' to '{typeof(T).Name}'.");
        }

        /// <summary>
        /// Gets the LinkedEntity as a specific type without casting, using pattern matching
        /// </summary>
        /// <typeparam name="T">The target type</typeparam>
        /// <returns>The LinkedEntity as type T, or null if not of that type</returns>
        public T? GetLinkedEntityAs<T>() where T : class, IDomainEntity
        {
            return _linkedEntity as T;
        }

        /// <summary>
        /// Checks if the LinkedEntity can be cast to the specified type using TypeRegistry
        /// </summary>
        /// <typeparam name="T">The target type to check</typeparam>
        /// <returns>True if the cast is possible, false otherwise</returns>
        public bool CanCastLinkedEntityTo<T>() where T : class, IDomainEntity
        {
            if (_linkedEntity == null || string.IsNullOrEmpty(_linkedEntityType))
                return false;

            var targetType = TypeRegistry.GetType(_linkedEntityType);
            return _linkedEntity is T || (targetType != null && typeof(T).IsAssignableFrom(targetType));
        }

        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(SubAccountDTO) || typeof(T) == typeof(ISubAccountDTO))
                {
                    // Cast LinkedEntity using TypeRegistry approach if possible
                    IDomainEntity? castedLinkedEntity = null;
                    if (_linkedEntity != null && !string.IsNullOrEmpty(_linkedEntityType))
                    {
                        try
                        {
                            var targetTypeName = string.Format("{0}DTO", typeof(T).Name);
                            // Try to use TypeRegistry for LinkedEntity casting
                            var linkedEntityType = TypeRegistry.GetType(targetTypeName);
                            if (linkedEntityType != null)
                            {
                                // Use reflection to call the Cast method with the dynamic type
                                var castMethod = typeof(IDomainEntity).GetMethod("Cast")!.MakeGenericMethod(linkedEntityType);
                                castedLinkedEntity = (IDomainEntity)castMethod.Invoke(_linkedEntity, null)!;
                            }
                            else
                            {
                                // Fallback to original approach
                                castedLinkedEntity = _linkedEntity.Cast<IDomainEntity>();
                            }
                        }
                        catch
                        {
                            // If casting fails, use the original entity
                            castedLinkedEntity = _linkedEntity;
                        }
                    }

                    var dto = new SubAccountDTO(
                        _id,
                        _linkedEntityId,
                        _linkedEntityType,
                        castedLinkedEntity,
                        _accountId,
                        _account!.Cast<AccountDTO>(),
                        _dateCreated,
                        DateModified);
                    return (T)(IDomainEntity)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast SubAccount to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.LinkedEntityId:{1}.LinkedEntityType:{2}", _id, _linkedEntityId, _linkedEntityType);
        #endregion
    }
}
