using OrganizerCompanion.Core.Interfaces.Domain;
using System.Collections.Concurrent;

namespace OrganizerCompanion.Core.Registries
{
    /// <summary>
    /// Registry for managing dynamic type resolution and casting for domain entities
    /// </summary>
    internal static class TypeRegistry
    {
        private static readonly ConcurrentDictionary<string, System.Type> _typeCache = new();
        private static readonly object _lock = new();
        private static bool _initialized = false;

        /// <summary>
        /// Initialize the type registry with known domain entity types
        /// </summary>
        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            lock (_lock)
            {
                if (_initialized)
                {
                    return;
                }

                // Register Domain types
                RegisterType<Models.Domain.Account>("Account");
                RegisterType<Models.Domain.AccountFeature>("AccountFeature");
                RegisterType<Models.Domain.AnnonymousUser>("AnnonymousUser");
                RegisterType<Models.Domain.ProjectAssignment>("ProjectAssignment");
                RegisterType<Models.Domain.CAAddress>("CAAddress");
                RegisterType<Models.Domain.Contact>("Contact");
                RegisterType<Models.Domain.Email>("Email");
                RegisterType<Models.Domain.Feature>("Feature");
                RegisterType<Models.Domain.Group>("Group");
                RegisterType<Models.Domain.MXAddress>("MXAddress");
                RegisterType<Models.Domain.Organization>("Organization");
                RegisterType<Models.Domain.Password>("Password");
                RegisterType<Models.Domain.PhoneNumber>("PhoneNumber");
                RegisterType<Models.Domain.SubAccount>("SubAccount");
                RegisterType<Models.Domain.USAddress>("USAddress");
                RegisterType<Models.Domain.User>("User");
                
                // Register DTO types
                RegisterType<Models.DataTransferObject.AccountDTO>("AccountDTO");
                RegisterType<Models.DataTransferObject.AnnonymousUserDTO>("AnnonymousUserDTO");
                RegisterType<Models.DataTransferObject.ProjectAssignmentDTO>("ProjectAssignmentDTO");
                RegisterType<Models.DataTransferObject.CAAddressDTO>("CAAddressDTO");
                RegisterType<Models.DataTransferObject.ContactDTO>("ContactDTO");
                RegisterType<Models.DataTransferObject.EmailDTO>("EmailDTO");
                RegisterType<Models.DataTransferObject.FeatureDTO>("FeatureDTO");
                RegisterType<Models.DataTransferObject.GroupDTO>("GroupDTO");
                RegisterType<Models.DataTransferObject.MXAddressDTO>("MXAddressDTO");
                RegisterType<Models.DataTransferObject.OrganizationDTO>("OrganizationDTO");
                RegisterType<Models.DataTransferObject.PhoneNumberDTO>("PhoneNumberDTO");
                RegisterType<Models.DataTransferObject.SubAccountDTO>("SubAccountDTO");
                RegisterType<Models.DataTransferObject.USAddressDTO>("USAddressDTO");
                RegisterType<Models.DataTransferObject.UserDTO>("UserDTO");

                _initialized = true;
            }
        }

        /// <summary>
        /// Register a type with its string identifier
        /// </summary>
        /// <typeparam name="T">The type to register</typeparam>
        /// <param name="typeName">The string identifier for the type</param>
        public static void RegisterType<T>(string typeName) where T : IDomainEntity
        {
            _typeCache.TryAdd(typeName, typeof(T));
        }

        /// <summary>
        /// Get a type by its string identifier
        /// </summary>
        /// <param name="typeName">The string identifier for the type</param>
        /// <returns>The Type if found, null otherwise</returns>
        public static System.Type? GetType(string typeName)
        {
            if (!_initialized)
            {
                Initialize();
            }

            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }

            return _typeCache.TryGetValue(typeName, out var type) ? type : null;
        }

        /// <summary>
        /// Check if a type is registered
        /// </summary>
        /// <param name="typeName">The string identifier for the type</param>
        /// <returns>True if the type is registered, false otherwise</returns>
        public static bool IsTypeRegistered(string typeName)
        {
            if (!_initialized)
            {
                Initialize();
            }

            if (string.IsNullOrEmpty(typeName))
            {
                return false;
            }

            return _typeCache.ContainsKey(typeName);
        }

        /// <summary>
        /// Get all registered type names
        /// </summary>
        /// <returns>Collection of registered type names</returns>
        public static IEnumerable<string> GetRegisteredTypeNames()
        {
            if (!_initialized)
            {
                Initialize();
            }

            return _typeCache.Keys;
        }

        /// <summary>
        /// Clear the type registry (primarily for testing)
        /// </summary>
        public static void Clear()
        {
            _typeCache.Clear();
            _initialized = false;
        }
    }
}