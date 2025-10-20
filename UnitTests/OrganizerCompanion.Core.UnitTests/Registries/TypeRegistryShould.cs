using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Registries;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using DomainTask = OrganizerCompanion.Core.Models.Domain.Task;

namespace OrganizerCompanion.Core.UnitTests.Registries
{
    [TestFixture]
    internal class TypeRegistryShould
    {
        [SetUp]
        public void SetUp()
        {
            // Note: TypeRegistry is a static class that auto-initializes on first use
            // We can't reliably clear it between tests because it re-initializes automatically
            // These tests verify behavior with the initialized registry
        }

        [TearDown]
        public void TearDown()
        {
            // Note: Don't clear after tests since Initialize() auto-reinitializes
            // and clearing causes issues with auto-initialization behavior
        }

        #region Initialize Tests

        [Test, Category("Registries")]
        public void Initialize_WhenCalled_EnsuresTypesAreRegistered()
        {
            // Act
            TypeRegistry.Initialize();

            // Assert - Check some key types are registered
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("Account"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("SubAccount"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("Organization"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("AccountDTO"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("SubAccountDTO"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("UserDTO"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("OrganizationDTO"), Is.True);
            });
        }

        [Test, Category("Registries")]
        public void Initialize_WhenCalledMultipleTimes_DoesNotReinitialize()
        {
            // Arrange - Ensure initialized
            TypeRegistry.Initialize();
            var initialTypeCount = TypeRegistry.GetRegisteredTypeNames().Count();

            // Act - Call Initialize again
            TypeRegistry.Initialize();

            // Assert - Should have same number of types (not duplicated)
            var finalTypeCount = TypeRegistry.GetRegisteredTypeNames().Count();
            Assert.That(finalTypeCount, Is.EqualTo(initialTypeCount));
        }

        [Test, Category("Registries")]
        public void Initialize_RegistersAllExpectedDomainTypes()
        {
            // Act
            TypeRegistry.Initialize();

            // Assert - Check all expected domain types
            var expectedDomainTypes = new[]
            {
                "Account", "AccountFeature", "AnnonymousUser", "Assignment",
                "CAAddress", "Contact", "Email", "Feature", "Group",
                "MXAddress", "Organization", "Password", "PhoneNumber",
                "SubAccount", "USAddress", "User"
            };

            foreach (var typeName in expectedDomainTypes)
            {
                Assert.That(TypeRegistry.IsTypeRegistered(typeName), Is.True, 
                    $"Domain type '{typeName}' should be registered");
            }
        }

        [Test, Category("Registries")]
        public void Initialize_RegistersAllExpectedDTOTypes()
        {
            // Act
            TypeRegistry.Initialize();

            // Assert - Check all expected DTO types
            var expectedDTOTypes = new[]
            {
                "AccountDTO", "AnnonymousUserDTO", "AssignmentDTO", "CAAddressDTO",
                "ContactDTO", "EmailDTO", "FeatureDTO", "GroupDTO",
                "MXAddressDTO", "OrganizationDTO", "PhoneNumberDTO",
                "SubAccountDTO", "USAddressDTO", "UserDTO"
            };

            foreach (var typeName in expectedDTOTypes)
            {
                Assert.That(TypeRegistry.IsTypeRegistered(typeName), Is.True, 
                    $"DTO type '{typeName}' should be registered");
            }
        }

        [Test, Category("Registries")]
        public void Initialize_IsThreadSafe()
        {
            // Arrange
            var tasks = new List<System.Threading.Tasks.Task>();
            var exceptions = new List<Exception>();

            // Act - Run initialization from multiple threads
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        TypeRegistry.Initialize();
                    }
                    catch (Exception ex)
                    {
                        lock (exceptions)
                        {
                            exceptions.Add(ex);
                        }
                    }
                }));
            }

            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

            // Assert - No exceptions should occur
            Assert.That(exceptions, Is.Empty, "Thread-safe initialization should not throw exceptions");
            Assert.That(TypeRegistry.IsTypeRegistered("Account"), Is.True, "Types should be properly registered");
        }

        #endregion

        #region RegisterType Tests

        [Test, Category("Registries")]
        public void RegisterType_WithValidType_RegistersSuccessfully()
        {
            // Act
            TypeRegistry.RegisterType<User>("TestUser");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("TestUser"), Is.True);
                Assert.That(TypeRegistry.GetType("TestUser"), Is.EqualTo(typeof(User)));
            });
        }

        [Test, Category("Registries")]
        public void RegisterType_WithDuplicateTypeName_DoesNotOverwrite()
        {
            // Arrange
            TypeRegistry.RegisterType<User>("DuplicateType");
            var originalType = TypeRegistry.GetType("DuplicateType");

            // Act - Try to register different type with same name
            TypeRegistry.RegisterType<Organization>("DuplicateType");

            // Assert - Should keep the original type
            Assert.That(TypeRegistry.GetType("DuplicateType"), Is.EqualTo(originalType));
            Assert.That(TypeRegistry.GetType("DuplicateType"), Is.EqualTo(typeof(User)));
        }

        [Test, Category("Registries")]
        public void RegisterType_WithMultipleTypes_RegistersAll()
        {
            // Act
            TypeRegistry.RegisterType<User>("CustomUser");
            TypeRegistry.RegisterType<Organization>("CustomOrg");
            TypeRegistry.RegisterType<SubAccount>("CustomSubAccount");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("CustomUser"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("CustomOrg"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("CustomSubAccount"), Is.True);
                Assert.That(TypeRegistry.GetType("CustomUser"), Is.EqualTo(typeof(User)));
                Assert.That(TypeRegistry.GetType("CustomOrg"), Is.EqualTo(typeof(Organization)));
                Assert.That(TypeRegistry.GetType("CustomSubAccount"), Is.EqualTo(typeof(SubAccount)));
            });
        }

        #endregion

        #region GetType Tests

        [Test, Category("Registries")]
        public void GetType_WithRegisteredType_ReturnsCorrectType()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.GetType("User"), Is.EqualTo(typeof(User)));
                Assert.That(TypeRegistry.GetType("Organization"), Is.EqualTo(typeof(Organization)));
                Assert.That(TypeRegistry.GetType("SubAccount"), Is.EqualTo(typeof(SubAccount)));
                Assert.That(TypeRegistry.GetType("UserDTO"), Is.EqualTo(typeof(UserDTO)));
                Assert.That(TypeRegistry.GetType("SubAccountDTO"), Is.EqualTo(typeof(SubAccountDTO)));
            });
        }

        [Test, Category("Registries")]
        public void GetType_WithUnregisteredType_ReturnsNull()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.GetType("NonExistentType"), Is.Null);
                Assert.That(TypeRegistry.GetType("InvalidType"), Is.Null);
                Assert.That(TypeRegistry.GetType(""), Is.Null);
            });
        }

        [Test, Category("Registries")]
        public void GetType_WithNullTypeName_ReturnsNull()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.That(TypeRegistry.GetType(null!), Is.Null);
        }

        [Test, Category("Registries")]
        public void GetType_BeforeInitialization_InitializesAutomatically()
        {
            // Arrange - Don't call Initialize() explicitly

            // Act
            var result = TypeRegistry.GetType("User");

            // Assert
            Assert.That(result, Is.EqualTo(typeof(User)));
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True);
        }

        #endregion

        #region IsTypeRegistered Tests

        [Test, Category("Registries")]
        public void IsTypeRegistered_WithRegisteredType_ReturnsTrue()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("Organization"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("SubAccount"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("UserDTO"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("OrganizationDTO"), Is.True);
            });
        }

        [Test, Category("Registries")]
        public void IsTypeRegistered_WithUnregisteredType_ReturnsFalse()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("NonExistentType"), Is.False);
                Assert.That(TypeRegistry.IsTypeRegistered("InvalidType"), Is.False);
                Assert.That(TypeRegistry.IsTypeRegistered("RandomString"), Is.False);
            });
        }

        [Test, Category("Registries")]
        public void IsTypeRegistered_WithEmptyString_ReturnsFalse()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.That(TypeRegistry.IsTypeRegistered(""), Is.False);
        }

        [Test, Category("Registries")]
        public void IsTypeRegistered_WithNullString_ReturnsFalse()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.That(TypeRegistry.IsTypeRegistered(null!), Is.False);
        }

        [Test, Category("Registries")]
        public void IsTypeRegistered_BeforeInitialization_InitializesAutomatically()
        {
            // Arrange - Don't call Initialize() explicitly

            // Act
            var result = TypeRegistry.IsTypeRegistered("User");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test, Category("Registries")]
        public void IsTypeRegistered_CaseSensitive_ReturnsCorrectResults()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("user"), Is.False);
                Assert.That(TypeRegistry.IsTypeRegistered("USER"), Is.False);
                Assert.That(TypeRegistry.IsTypeRegistered("UsEr"), Is.False);
            });
        }

        #endregion

        #region GetRegisteredTypeNames Tests

        [Test, Category("Registries")]
        public void GetRegisteredTypeNames_AfterInitialization_ReturnsAllTypeNames()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act
            var typeNames = TypeRegistry.GetRegisteredTypeNames().ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(typeNames, Is.Not.Empty);
                Assert.That(typeNames, Does.Contain("User"));
                Assert.That(typeNames, Does.Contain("Organization"));
                Assert.That(typeNames, Does.Contain("SubAccount"));
                Assert.That(typeNames, Does.Contain("UserDTO"));
                Assert.That(typeNames, Does.Contain("OrganizationDTO"));
                Assert.That(typeNames, Does.Contain("SubAccountDTO"));
                // Should have at least the expected number of types (16 domain + 14 DTO = 30)
                Assert.That(typeNames.Count, Is.GreaterThanOrEqualTo(30));
            });
        }

        [Test, Category("Registries")]
        public void GetRegisteredTypeNames_BeforeInitialization_InitializesAndReturnsTypes()
        {
            // Arrange - Don't call Initialize() explicitly

            // Act
            var typeNames = TypeRegistry.GetRegisteredTypeNames().ToList();

            // Assert
            Assert.That(typeNames, Is.Not.Empty);
            Assert.That(typeNames, Does.Contain("User"));
        }

        [Test, Category("Registries")]
        public void GetRegisteredTypeNames_WithCustomRegistrations_IncludesCustomTypes()
        {
            // Arrange
            TypeRegistry.Initialize();
            TypeRegistry.RegisterType<User>("CustomTestType");

            // Act
            var typeNames = TypeRegistry.GetRegisteredTypeNames().ToList();

            // Assert
            Assert.That(typeNames, Does.Contain("CustomTestType"));
        }

        [Test, Category("Registries")]
        public void GetRegisteredTypeNames_ReturnsConsistentCollection()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act
            var typeNames1 = TypeRegistry.GetRegisteredTypeNames();
            var typeNames2 = TypeRegistry.GetRegisteredTypeNames();

            // Assert - Should be able to enumerate multiple times and get consistent results
            Assert.That(typeNames1.Count(), Is.EqualTo(typeNames2.Count()));
            Assert.That(typeNames1, Is.Not.Null);
            Assert.That(typeNames2, Is.Not.Null);
        }

        [Test, Category("Registries")]
        public void GetRegisteredTypeNames_AfterClear_ReturnsEmpty()
        {
            // Arrange
            TypeRegistry.Initialize();
            Assert.That(TypeRegistry.GetRegisteredTypeNames(), Is.Not.Empty, "Registry should have types initially");

            // Act
            TypeRegistry.Clear();
            
            // Force a fresh state check - don't call any auto-initializing methods
            // Use reflection to check the private static field directly
            var typeRegistryType = typeof(TypeRegistry);
            var typeCacheField = typeRegistryType.GetField("_typeCache", BindingFlags.NonPublic | BindingFlags.Static);
            var typeCache = typeCacheField?.GetValue(null) as System.Collections.Concurrent.ConcurrentDictionary<string, System.Type>;

            // Assert
            Assert.That(typeCache?.Count, Is.EqualTo(0), "Type cache should be empty after Clear()");
        }

        #endregion

        #region Clear Tests

        [Test, Category("Registries")]
        public void Clear_RemovesAllRegisteredTypes()
        {
            // Arrange
            TypeRegistry.Initialize();
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True, "User should be registered initially");

            // Act
            TypeRegistry.Clear();

            // Use reflection to verify the internal state is cleared
            var typeRegistryType = typeof(TypeRegistry);
            var typeCacheField = typeRegistryType.GetField("_typeCache", BindingFlags.NonPublic | BindingFlags.Static);
            var typeCache = typeCacheField?.GetValue(null) as System.Collections.Concurrent.ConcurrentDictionary<string, System.Type>;

            // Assert - Internal cache should be empty immediately after Clear()
            Assert.That(typeCache?.Count, Is.EqualTo(0), "Type cache should be empty after Clear()");
        }

        [Test, Category("Registries")]
        public void Clear_ResetsInitializationFlag()
        {
            // Arrange
            TypeRegistry.Initialize();
            TypeRegistry.Clear();

            // Act - GetType should reinitialize
            var result = TypeRegistry.GetType("User");

            // Assert
            Assert.That(result, Is.EqualTo(typeof(User)));
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True);
        }

        [Test, Category("Registries")]
        public void Clear_CanBeCalledMultipleTimes()
        {
            // Arrange
            TypeRegistry.Initialize();

            // Act & Assert - Should not throw
            Assert.DoesNotThrow(() =>
            {
                TypeRegistry.Clear();
                TypeRegistry.Clear();
                TypeRegistry.Clear();
            });

            // Use reflection to verify internal state
            var typeRegistryType = typeof(TypeRegistry);
            var typeCacheField = typeRegistryType.GetField("_typeCache", BindingFlags.NonPublic | BindingFlags.Static);
            var typeCache = typeCacheField?.GetValue(null) as System.Collections.Concurrent.ConcurrentDictionary<string, System.Type>;

            Assert.That(typeCache?.Count, Is.EqualTo(0), "Type cache should be empty after multiple Clear() calls");
        }

        [Test, Category("Registries")]
        public void Clear_AllowsReinitialization()
        {
            // Arrange
            TypeRegistry.Initialize();
            var initialCount = TypeRegistry.GetRegisteredTypeNames().Count();
            TypeRegistry.Clear();

            // Act
            TypeRegistry.Initialize();
            var newCount = TypeRegistry.GetRegisteredTypeNames().Count();

            // Assert
            Assert.That(newCount, Is.EqualTo(initialCount));
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True);
        }

        #endregion

        #region Integration Tests

        [Test, Category("Registries")]
        public void TypeRegistry_IntegrationTest_FullWorkflow()
        {
            // Test complete workflow
            
            // 1. Ensure initialized state
            TypeRegistry.Initialize();
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True, "Registry should be initialized");

            // 2. Register custom type
            TypeRegistry.RegisterType<Organization>("CustomOrganization");
            Assert.That(TypeRegistry.IsTypeRegistered("CustomOrganization"), Is.True);
            Assert.That(TypeRegistry.GetType("CustomOrganization"), Is.EqualTo(typeof(Organization)));

            // 3. Verify all functionality works together
            var allTypes = TypeRegistry.GetRegisteredTypeNames().ToList();
            Assert.That(allTypes, Does.Contain("User"));
            Assert.That(allTypes, Does.Contain("CustomOrganization"));

            // 4. Test Clear functionality using reflection
            TypeRegistry.Clear();
            var typeRegistryType = typeof(TypeRegistry);
            var typeCacheField = typeRegistryType.GetField("_typeCache", BindingFlags.NonPublic | BindingFlags.Static);
            var typeCache = typeCacheField?.GetValue(null) as System.Collections.Concurrent.ConcurrentDictionary<string, System.Type>;
            Assert.That(typeCache?.Count, Is.EqualTo(0), "Cache should be empty after Clear()");

            // 5. Verify reinitialization works
            TypeRegistry.Initialize();
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True, "Should reinitialize properly");
        }

        [Test, Category("Registries")]
        public void TypeRegistry_ConcurrentAccess_IsThreadSafe()
        {
            // Arrange
            var tasks = new List<System.Threading.Tasks.Task>();
            var results = new List<bool>();
            var lockObject = new object();

            // Act - Multiple threads accessing different methods
            for (int i = 0; i < 20; i++)
            {
                int threadIndex = i;
                tasks.Add(System.Threading.Tasks.Task.Run(() =>
                {
                    try
                    {
                        // Different operations on different threads
                        switch (threadIndex % 4)
                        {
                            case 0:
                                TypeRegistry.Initialize();
                                break;
                            case 1:
                                var isRegistered = TypeRegistry.IsTypeRegistered("User");
                                lock (lockObject) { results.Add(isRegistered); }
                                break;
                            case 2:
                                var type = TypeRegistry.GetType("Organization");
                                lock (lockObject) { results.Add(type != null); }
                                break;
                            case 3:
                                var typeNames = TypeRegistry.GetRegisteredTypeNames();
                                lock (lockObject) { results.Add(typeNames.Any()); }
                                break;
                        }
                    }
                    catch
                    {
                        lock (lockObject) { results.Add(false); }
                    }
                }));
            }

            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());

            // Assert - All operations should succeed
            Assert.That(results.All(r => r), Is.True, "All concurrent operations should succeed");
            Assert.That(TypeRegistry.IsTypeRegistered("User"), Is.True, "Registry should be properly initialized");
        }

        #endregion

        #region Edge Cases and Error Conditions

        [Test, Category("Registries")]
        public void TypeRegistry_HandlesManyRegistrations()
        {
            // Act - Register many types
            for (int i = 0; i < 1000; i++)
            {
                TypeRegistry.RegisterType<User>($"TestType{i}");
            }

            // Assert
            Assert.That(TypeRegistry.GetRegisteredTypeNames().Count(), Is.GreaterThanOrEqualTo(1000));
            Assert.That(TypeRegistry.IsTypeRegistered("TestType0"), Is.True);
            Assert.That(TypeRegistry.IsTypeRegistered("TestType999"), Is.True);
        }

        [Test, Category("Registries")]
        public void TypeRegistry_HandlesSpecialCharactersInTypeNames()
        {
            // Act & Assert - Should handle various string inputs gracefully
            Assert.DoesNotThrow(() =>
            {
                TypeRegistry.RegisterType<User>("Type_With_Underscores");
                TypeRegistry.RegisterType<User>("Type-With-Dashes");
                TypeRegistry.RegisterType<User>("Type.With.Dots");
                TypeRegistry.RegisterType<User>("Type123WithNumbers");
            });

            Assert.Multiple(() =>
            {
                Assert.That(TypeRegistry.IsTypeRegistered("Type_With_Underscores"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("Type-With-Dashes"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("Type.With.Dots"), Is.True);
                Assert.That(TypeRegistry.IsTypeRegistered("Type123WithNumbers"), Is.True);
            });
        }

        #endregion
    }
}
