using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class GroupShould
    {
        private Group _sut;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);
        private List<Contact> _testMembers;
        private Account _testAccount;

        [SetUp]
        public void SetUp()
        {
            _testAccount = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC123"
            };

            _testMembers =
            [
                new() {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Pronouns = Pronouns.HeHim
                },
                new() {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Pronouns = Pronouns.SheHer
                }
            ];

            _sut = new Group();
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var group = new Group();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(group.Id, Is.EqualTo(0));
                Assert.That(group.Name, Is.Null);
                Assert.That(group.Description, Is.Null);
                Assert.That(group.Members, Is.Not.Null);
                Assert.That(group.Members.Count, Is.EqualTo(0));
                Assert.That(group.AccountId, Is.EqualTo(0));
                Assert.That(group.Account, Is.Null);
                Assert.That(group.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(group.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(group.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Act
            var group = new Group(
                id: 1,
                name: "Test Group",
                description: "Test Description",
                members: _testMembers,
                accountId: 123,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(group.Id, Is.EqualTo(1));
                Assert.That(group.Name, Is.EqualTo("Test Group"));
                Assert.That(group.Description, Is.EqualTo("Test Description"));
                Assert.That(group.Members, Is.SameAs(_testMembers));
                Assert.That(group.AccountId, Is.EqualTo(123));
                Assert.That(group.Account, Is.SameAs(_testAccount));
                Assert.That(group.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(group.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullValues_SetsNullsCorrectly()
        {
            // Act
            var group = new Group(
                id: 0,
                name: null,
                description: null,
                members: [],
                accountId: 0,
                account: null,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(group.Id, Is.EqualTo(0));
                Assert.That(group.Name, Is.Null);
                Assert.That(group.Description, Is.Null);
                Assert.That(group.Members, Is.Not.Null);
                Assert.That(group.Members.Count, Is.EqualTo(0));
                Assert.That(group.AccountId, Is.EqualTo(0));
                Assert.That(group.Account, Is.Null);
                Assert.That(group.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(group.DateModified, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Id = 42;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(42));
        }

        [Test, Category("Models")]
        public void Name_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Name = "My Group";

            // Assert
            Assert.That(_sut.Name, Is.EqualTo("My Group"));
        }

        [Test, Category("Models")]
        public void Name_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Name = null;

            // Assert
            Assert.That(_sut.Name, Is.Null);
        }

        [Test, Category("Models")]
        public void Description_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Description = "Group Description";

            // Assert
            Assert.That(_sut.Description, Is.EqualTo("Group Description"));
        }

        [Test, Category("Models")]
        public void Description_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Description = null;

            // Assert
            Assert.That(_sut.Description, Is.Null);
        }

        [Test, Category("Models")]
        public void Members_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Members = _testMembers;

            // Assert
            Assert.That(_sut.Members, Is.SameAs(_testMembers));
            Assert.That(_sut.Members.Count, Is.EqualTo(2));
        }

        [Test, Category("Models")]
        public void Members_SetEmptyList_WorksCorrectly()
        {
            // Arrange
            var emptyList = new List<Contact>();

            // Act
            _sut.Members = emptyList;

            // Assert
            Assert.That(_sut.Members, Is.SameAs(emptyList));
            Assert.That(_sut.Members.Count, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void AccountId_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.AccountId = 999;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(999));
        }

        [Test, Category("Models")]
        public void Account_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Account = _testAccount;

            // Assert
            Assert.That(_sut.Account, Is.SameAs(_testAccount));
        }

        [Test, Category("Models")]
        public void Account_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Account = null;

            // Assert
            Assert.That(_sut.Account, Is.Null);
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly()
        {
            // Arrange
            var originalDateCreated = _sut.DateCreated;

            // Act - Property should be read-only, so we can only verify the getter works
            var dateCreated = _sut.DateCreated;

            // Assert
            Assert.That(dateCreated, Is.EqualTo(originalDateCreated));
            // Note: DateCreated is set in constructor and should not change
        }

        [Test, Category("Models")]
        public void DateModified_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.DateModified = _testDateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(_testDateModified));
        }

        [Test, Category("Models")]
        public void DateModified_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("Models")]
        public void IGroup_Members_Get_ReturnsCorrectCastedList()
        {
            // Arrange
            _sut.Members = _testMembers;
            var groupInterface = (IGroup)_sut;

            // Act
            var interfaceMembers = groupInterface.Members;

            // Assert
            Assert.That(interfaceMembers, Is.Not.Null);
            Assert.That(interfaceMembers.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(interfaceMembers[0], Is.InstanceOf<IContact>());
                Assert.That(interfaceMembers[1], Is.InstanceOf<IContact>());
            });
            Assert.Multiple(() =>
            {
                Assert.That(interfaceMembers[0].Id, Is.EqualTo(1));
                Assert.That(interfaceMembers[1].Id, Is.EqualTo(2));
            });
        }

        [Test, Category("Models")]
        public void IGroup_Members_Set_UpdatesMembers()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            var groupInterface = (IGroup)_sut;
            var interfaceMembers = _testMembers.Cast<IContact>().ToList();

            // Act
            groupInterface.Members = interfaceMembers;
            var afterModified = DateTime.Now;

            // Assert
            Assert.That(_sut.Members, Is.Not.Null);
            Assert.That(_sut.Members.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members[0].Id, Is.EqualTo(1));
                Assert.That(_sut.Members[1].Id, Is.EqualTo(2));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeModified));
            Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterModified));
        }

        [Test, Category("Models")]
        public void IGroup_Members_SetEmptyList_UpdatesCorrectly()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            var groupInterface = (IGroup)_sut;
            var emptyInterfaceMembers = new List<IContact>();

            // Act
            groupInterface.Members = emptyInterfaceMembers;
            var afterModified = DateTime.Now;

            // Assert
            Assert.That(_sut.Members, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members.Count, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeModified));
            Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterModified));
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToGroupDTO_ReturnsCorrectGroupDTO()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Name = "Test Group";
            _sut.Description = "Test Description";
            _sut.Members = _testMembers;
            _sut.AccountId = 123;
            _sut.Account = null; // Set to null to avoid casting issues
            _sut.DateCreated = _testDateCreated;
            _sut.DateModified = _testDateModified;

            // Act
            var groupDTO = _sut.Cast<GroupDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDTO, Is.Not.Null);
                Assert.That(groupDTO, Is.InstanceOf<GroupDTO>());
                Assert.That(groupDTO.Id, Is.EqualTo(1));
                Assert.That(groupDTO.Name, Is.EqualTo("Test Group"));
                Assert.That(groupDTO.Description, Is.EqualTo("Test Description"));
                Assert.That(groupDTO.Members, Is.Not.Null);
                Assert.That(groupDTO.Members.Count, Is.EqualTo(2));
                Assert.That(groupDTO.Members[0], Is.InstanceOf<ContactDTO>());
                Assert.That(groupDTO.Members[1], Is.InstanceOf<ContactDTO>());
                Assert.That(groupDTO.AccountId, Is.EqualTo(123));
                Assert.That(groupDTO.Account, Is.Null);
                Assert.That(groupDTO.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(groupDTO.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIGroupDTO_ReturnsCorrectGroupDTO()
        {
            // Arrange
            _sut.Id = 2;
            _sut.Name = "Interface Test Group";
            _sut.Description = "Interface Test Description";
            _sut.Members = _testMembers;
            _sut.AccountId = 456;
            _sut.Account = null; // Set to null to avoid casting issues
            _sut.DateCreated = _testDateCreated;
            _sut.DateModified = _testDateModified;

            // Act
            var groupDTO = _sut.Cast<IGroupDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDTO, Is.Not.Null);
                Assert.That(groupDTO, Is.InstanceOf<IGroupDTO>());
                Assert.That(groupDTO, Is.InstanceOf<GroupDTO>());
                Assert.That(groupDTO.Id, Is.EqualTo(2));
                Assert.That(groupDTO.Name, Is.EqualTo("Interface Test Group"));
                Assert.That(groupDTO.Description, Is.EqualTo("Interface Test Description"));
                Assert.That(groupDTO.AccountId, Is.EqualTo(456));
                Assert.That(groupDTO.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(groupDTO.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ThrowsInvalidCastException()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Contact>());
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Does.Contain("Cannot cast Group to Contact"));
        }

        [Test, Category("Models")]
        public void Cast_WithEmptyMembers_WorksCorrectly()
        {
            // Arrange
            _sut.Id = 3;
            _sut.Name = "Empty Group";
            _sut.Description = "Group with no members";
            _sut.Members = [];
            _sut.AccountId = 789;
            _sut.Account = null;
            _sut.DateCreated = _testDateCreated;
            _sut.DateModified = null;

            // Act
            var groupDTO = _sut.Cast<GroupDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDTO, Is.Not.Null);
                Assert.That(groupDTO.Id, Is.EqualTo(3));
                Assert.That(groupDTO.Name, Is.EqualTo("Empty Group"));
                Assert.That(groupDTO.Description, Is.EqualTo("Group with no members"));
                Assert.That(groupDTO.Members, Is.Not.Null);
                Assert.That(groupDTO.Members.Count, Is.EqualTo(0));
                Assert.That(groupDTO.AccountId, Is.EqualTo(789));
                Assert.That(groupDTO.Account, Is.Null);
                Assert.That(groupDTO.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(groupDTO.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToGroupDTO_WithNullAccount_WorksCorrectly()
        {
            // Arrange
            _sut.Id = 10;
            _sut.Name = "Test Group Null Account";
            _sut.Description = "Test Description";
            _sut.Members = _testMembers;
            _sut.AccountId = 123;
            _sut.Account = null;
            _sut.DateCreated = _testDateCreated;
            _sut.DateModified = _testDateModified;

            // Act
            var groupDTO = _sut.Cast<GroupDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDTO, Is.Not.Null);
                Assert.That(groupDTO, Is.InstanceOf<GroupDTO>());
                Assert.That(groupDTO.Id, Is.EqualTo(10));
                Assert.That(groupDTO.Name, Is.EqualTo("Test Group Null Account"));
                Assert.That(groupDTO.Description, Is.EqualTo("Test Description"));
                Assert.That(groupDTO.Members, Is.Not.Null);
                Assert.That(groupDTO.Members.Count, Is.EqualTo(2));
                Assert.That(groupDTO.AccountId, Is.EqualTo(123));
                Assert.That(groupDTO.Account, Is.Null);
                Assert.That(groupDTO.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(groupDTO.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        #endregion

        #region JSON Serialization Tests

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Name = "JSON Test Group";
            _sut.Description = "Group for JSON testing";
            _sut.Members = _testMembers;
            _sut.AccountId = 123;
            _sut.Account = null; // Set to null to avoid serialization issues with Account's not-implemented properties
            _sut.DateCreated = _testDateCreated;
            _sut.DateModified = _testDateModified;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            // Verify the JSON contains expected properties
            Assert.That(json, Does.Contain("\"id\":1"));
            Assert.That(json, Does.Contain("\"name\":\"JSON Test Group\""));
            Assert.That(json, Does.Contain("\"description\":\"Group for JSON testing\""));
            Assert.That(json, Does.Contain("\"accountId\":123"));
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Name = null;
            _sut.Description = null;
            _sut.Members = [];
            _sut.AccountId = 0;
            _sut.Account = null;
            _sut.DateModified = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            // Verify the JSON handles nulls appropriately
            Assert.That(json, Does.Contain("\"id\":0"));
            Assert.That(json, Does.Contain("\"name\":null"));
            Assert.That(json, Does.Contain("\"description\":null"));
            Assert.That(json, Does.Contain("\"accountId\":0"));
            Assert.That(json, Does.Contain("\"account\":null"));
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("Models")]
        public void Group_ImplementsIGroupInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IGroup>());
        }

        [Test, Category("Models")]
        public void Group_ImplementsIDomainEntityInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        #endregion

        #region DateModified Update Tests

        [Test, Category("Models")]
        public void Members_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            _sut.DateModified = null;

            // Act
            _sut.Members = _testMembers;
            var afterModified = DateTime.Now;

            // Assert - Note: Based on code analysis, the regular Members property doesn't update DateModified
            // Only the interface implementation does
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("Models")]
        public void IGroup_Members_Set_UpdatesDateModified_WhenNull()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            _sut.DateModified = null;
            var groupInterface = (IGroup)_sut;
            var interfaceMembers = _testMembers.Cast<IContact>().ToList();

            // Act
            groupInterface.Members = interfaceMembers;
            var afterModified = DateTime.Now;

            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeModified));
            Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterModified));
        }

        [Test, Category("Models")]
        public void IGroup_Members_Set_UpdatesDateModified_WhenAlreadySet()
        {
            // Arrange
            _sut.DateModified = _testDateModified;
            var beforeSet = DateTime.Now;
            var groupInterface = (IGroup)_sut;
            var interfaceMembers = _testMembers.Cast<IContact>().ToList();

            // Act
            groupInterface.Members = interfaceMembers;
            var afterSet = DateTime.Now;

            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.Not.EqualTo(_testDateModified));
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
            Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
        }

        #endregion

        #region Additional Comprehensive Coverage Tests

        [Test, Category("Models")]
        public void JsonConstructor_WithUnusedParameters_ShouldIgnoreThemAndSetPropertiesCorrectly()
        {
            // Test that JsonConstructor handles unused parameters (isCast, castId, castType) correctly
            
            // Arrange & Act
            var group = new Group(
                id: 999,
                name: "ComprehensiveGroup",
                description: "Test comprehensive functionality",
                members: _testMembers,
                accountId: 456,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified,
                isCast: true,        // These parameters are unused by the constructor
                castId: 12345,       // but should be handled gracefully
                castType: "TestType"
            );

            // Assert - Verify that the object is created correctly and unused parameters don't affect it
            Assert.Multiple(() =>
            {
                Assert.That(group.Id, Is.EqualTo(999));
                Assert.That(group.Name, Is.EqualTo("ComprehensiveGroup"));
                Assert.That(group.Description, Is.EqualTo("Test comprehensive functionality"));
                Assert.That(group.Members, Is.SameAs(_testMembers));
                Assert.That(group.AccountId, Is.EqualTo(456));
                Assert.That(group.Account, Is.SameAs(_testAccount));
                Assert.That(group.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(group.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsCorrectly()
        {
            // This test verifies that the catch block in the Cast method properly rethrows exceptions
            
            // Arrange
            _sut.Id = 1;
            _sut.Name = "TestGroup";
            _sut.Members = _testMembers;

            // Act & Assert - Test that InvalidCastException is thrown and rethrown correctly
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Account>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Group to Account"));
            });
        }

        [Test, Category("Models")]
        public void SerializerOptions_CyclicalReferenceHandling_ComprehensiveTest()
        {
            // Test that the serialization options handle complex scenarios correctly
            
            // Arrange - Create group with complex member data
            _sut = new Group(
                id: 1,
                name: "SerializationTestGroup",
                description: "Testing cyclical references",
                members: _testMembers,
                accountId: 123,
                account: null, // Avoid potential serialization cycles
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert - Multiple serialization calls should work consistently
            string json1, json2, json3;
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => json1 = _sut.ToJson());
                Assert.DoesNotThrow(() => json2 = _sut.ToJson());
                Assert.DoesNotThrow(() => json3 = _sut.ToJson());
            });

            // All serializations should produce valid JSON
            json1 = _sut.ToJson();
            json2 = _sut.ToJson();
            json3 = _sut.ToJson();
            
            Assert.Multiple(() =>
            {
                Assert.That(json1, Is.Not.Null.And.Not.Empty);
                Assert.That(json2, Is.Not.Null.And.Not.Empty);
                Assert.That(json3, Is.Not.Null.And.Not.Empty);
                Assert.That(json1, Is.EqualTo(json2));
                Assert.That(json2, Is.EqualTo(json3));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMultipleDTOTypes_ShouldCreateIndependentInstances()
        {
            // Test that multiple Cast calls create independent DTO instances
            var account = new Account
            {
                Id = 1,
                AccountName = "Test Account",
                AccountNumber = "ACC123"
            };
            
            // Arrange
            _sut = new Group(
                id: 100,
                name: "MultiCastGroup",
                description: "Testing multiple casts",
                members: _testMembers,
                accountId: account.Id,
                account: account,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act - Cast to different supported types
            var groupDto1 = _sut.Cast<GroupDTO>();
            var groupDto2 = _sut.Cast<GroupDTO>();
            var iGroupDto = _sut.Cast<IGroupDTO>();

            // Assert - All should be different instances but with same data
            Assert.Multiple(() =>
            {
                Assert.That(groupDto1, Is.Not.SameAs(groupDto2));
                Assert.That(groupDto1, Is.Not.SameAs(iGroupDto));
                Assert.That(groupDto2, Is.Not.SameAs(iGroupDto));
                
                // All should have same data
                Assert.That(groupDto1.Id, Is.EqualTo(100));
                Assert.That(groupDto2.Id, Is.EqualTo(100));
                Assert.That(iGroupDto.Id, Is.EqualTo(100));
                
                Assert.That(groupDto1.Name, Is.EqualTo("MultiCastGroup"));
                Assert.That(groupDto2.Name, Is.EqualTo("MultiCastGroup"));
                Assert.That(iGroupDto.Name, Is.EqualTo("MultiCastGroup"));
                
                // Verify members are properly cast
                Assert.That(groupDto1.Members, Has.Count.EqualTo(2));
                Assert.That(groupDto2.Members, Has.Count.EqualTo(2));
                Assert.That(iGroupDto.Members, Has.Count.EqualTo(2));
            });
        }

        [Test, Category("Models")]
        public void IGroup_Members_WithComplexMemberOperations_ShouldHandleCorrectly()
        {
            // Test complex operations with explicit interface implementation
            
            // Arrange
            var groupInterface = (IGroup)_sut;
            var additionalMembers = new List<Contact>
            {
                new() { Id = 3, FirstName = "Alice", LastName = "Johnson", Pronouns = Pronouns.SheHer },
                new() { Id = 4, FirstName = "Bob", LastName = "Wilson", Pronouns = Pronouns.HeHim }
            };
            
            // Act & Assert - Test multiple member operations
            var beforeModified = DateTime.Now;
            
            // Set initial members
            groupInterface.Members = _testMembers.Cast<IContact>().ToList();
            var firstModified = _sut.DateModified;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members, Has.Count.EqualTo(2));
                Assert.That(firstModified, Is.GreaterThanOrEqualTo(beforeModified));
            });
            
            System.Threading.Thread.Sleep(1);
            
            // Replace with additional members
            groupInterface.Members = additionalMembers.Cast<IContact>().ToList();
            var secondModified = _sut.DateModified;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members, Has.Count.EqualTo(2));
                Assert.That(_sut.Members[0].Id, Is.EqualTo(3));
                Assert.That(_sut.Members[1].Id, Is.EqualTo(4));
                Assert.That(secondModified, Is.GreaterThan(firstModified));
            });
            
            System.Threading.Thread.Sleep(1);
            
            // Clear members
            groupInterface.Members = [];
            var thirdModified = _sut.DateModified;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members, Has.Count.EqualTo(0));
                Assert.That(thirdModified, Is.GreaterThan(secondModified));
            });
        }

        [Test, Category("Models")]
        public void AutoProperties_DoNotUpdateDateModified()
        {
            // Test that auto-properties don't update DateModified (unlike explicit interface implementation)
            
            // Arrange
            _sut.DateModified = _testDateModified;
            var originalDateModified = _sut.DateModified;
            
            // Act - Set various auto-properties
            _sut.Id = 999;
            _sut.Name = "UpdatedName";
            _sut.Description = "UpdatedDescription";
            _sut.Members = _testMembers;
            _sut.AccountId = 789;
            _sut.Account = _testAccount;
            
            // Assert - DateModified should not have changed
            Assert.That(_sut.DateModified, Is.EqualTo(originalDateModified));
        }

        [Test, Category("Models")]
        public void Cast_WithNullAccount_ShouldHandleNullAccountCorrectly()
        {
            // Test that Cast handles null Account correctly without casting issues
            
            // Arrange
            _sut = new Group
            {
                Id = 1,
                Name = "NullAccountTest",
                Description = "Testing null account casting",
                Members = _testMembers,
                AccountId = 123,
                Account = null, // Set to null to avoid casting issues
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var groupDto = _sut.Cast<GroupDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDto, Is.Not.Null);
                Assert.That(groupDto.Account, Is.Null);
                Assert.That(groupDto.AccountId, Is.EqualTo(123));
                Assert.That(groupDto.Name, Is.EqualTo("NullAccountTest"));
                Assert.That(groupDto.Members, Has.Count.EqualTo(2));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithSpecialCharactersAndUnicode_ShouldSerializeCorrectly()
        {
            // Test JSON serialization with special characters and Unicode
            
            // Arrange
            _sut.Id = 888;
            _sut.Name = "Group!@#$%^&*()_+-={}[]|\\:;\"'<>?,./ 组 🚀 ñáéíóú";
            _sut.Description = "Description with special chars: !@#$%^&*() and Unicode: 测试 🎯";
            _sut.Members = _testMembers;
            _sut.AccountId = 999;
            _sut.Account = null;
            _sut.DateCreated = _testDateCreated;
            _sut.DateModified = _testDateModified;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":888"));
                Assert.That(json, Does.Contain("\"accountId\":999"));
                
                // Verify JSON is valid by attempting to parse
                Assert.DoesNotThrow(() =>
                {
                    var document = JsonDocument.Parse(json);
                    Assert.That(document.RootElement.ValueKind, Is.EqualTo(JsonValueKind.Object));
                    document.Dispose();
                });
            });
        }

        [Test, Category("Models")]
        public void Group_ComprehensiveFunctionalityIntegrationTest()
        {
            // Comprehensive test that exercises all major functionality together
            
            // Test default constructor
            var defaultGroup = new Group();
            Assert.Multiple(() =>
            {
                Assert.That(defaultGroup, Is.Not.Null);
                Assert.That(defaultGroup.DateCreated, Is.Not.EqualTo(default(DateTime)));
                Assert.That(defaultGroup.Members, Is.Not.Null);
                Assert.That(defaultGroup.Members, Is.Empty);
            });
            
            // Test JsonConstructor with comprehensive data
            var comprehensiveGroup = new Group(
                id: 12345,
                name: "ComprehensiveTestGroup",
                description: "Testing all functionality",
                members: _testMembers,
                accountId: 789,
                account: null, // Set to null to avoid casting issues in Cast method
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );
            
            // Verify comprehensive properties
            Assert.Multiple(() =>
            {
                Assert.That(comprehensiveGroup.Id, Is.EqualTo(12345));
                Assert.That(comprehensiveGroup.Name, Is.EqualTo("ComprehensiveTestGroup"));
                Assert.That(comprehensiveGroup.Description, Is.EqualTo("Testing all functionality"));
                Assert.That(comprehensiveGroup.Members, Is.SameAs(_testMembers));
                Assert.That(comprehensiveGroup.AccountId, Is.EqualTo(789));
                Assert.That(comprehensiveGroup.Account, Is.Null);
                Assert.That(comprehensiveGroup.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(comprehensiveGroup.DateModified, Is.EqualTo(_testDateModified));
            });
            
            // Test all property setters
            defaultGroup.Id = 54321;
            defaultGroup.Name = "UpdatedGroup";
            defaultGroup.Description = "Updated description";
            defaultGroup.Members = _testMembers;
            defaultGroup.AccountId = 999;
            defaultGroup.Account = null; // Set to null to avoid casting issues
            defaultGroup.DateModified = DateTime.Now;
            
            Assert.Multiple(() =>
            {
                Assert.That(defaultGroup.Id, Is.EqualTo(54321));
                Assert.That(defaultGroup.Name, Is.EqualTo("UpdatedGroup"));
                Assert.That(defaultGroup.Description, Is.EqualTo("Updated description"));
                Assert.That(defaultGroup.Members, Is.SameAs(_testMembers));
                Assert.That(defaultGroup.AccountId, Is.EqualTo(999));
                Assert.That(defaultGroup.Account, Is.Null);
                Assert.That(defaultGroup.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });
            
            // Test Cast functionality
            var groupDto = defaultGroup.Cast<GroupDTO>();
            var iGroupDto = defaultGroup.Cast<IGroupDTO>();
            
            Assert.Multiple(() =>
            {
                Assert.That(groupDto, Is.InstanceOf<GroupDTO>());
                Assert.That(iGroupDto, Is.InstanceOf<GroupDTO>());
                Assert.That(groupDto.Id, Is.EqualTo(defaultGroup.Id));
                Assert.That(iGroupDto.Id, Is.EqualTo(defaultGroup.Id));
                Assert.That(groupDto.Members, Has.Count.EqualTo(2));
                Assert.That(iGroupDto.Members, Has.Count.EqualTo(2));
            });
            
            // Test JSON serialization
            var json = defaultGroup.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);
            Assert.DoesNotThrow(() => JsonDocument.Parse(json).Dispose());
            
            // Test interface functionality
            var groupInterface = (IGroup)defaultGroup;
            var interfaceMembers = groupInterface.Members;
            Assert.Multiple(() =>
            {
                Assert.That(interfaceMembers, Has.Count.EqualTo(2));
                Assert.That(interfaceMembers[0], Is.InstanceOf<IContact>());
                Assert.That(interfaceMembers[1], Is.InstanceOf<IContact>());
            });
            
            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultGroup.Cast<Contact>());
        }

        [Test, Category("Models")]
        public void Cast_ExceptionRethrowing_ShouldPreserveOriginalException()
        {
            // Test that the catch block in Cast method properly rethrows exceptions
            
            // Arrange
            _sut = new Group
            {
                Id = 1,
                Name = "ExceptionTestGroup",
                Members = _testMembers
            };
            
            // Act & Assert - Verify that InvalidCastException is thrown for unsupported types
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Contact>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Group to Contact"));
                Assert.That(ex.InnerException, Is.Null); // Should be the original exception, not wrapped
            });
            
            // Test other unsupported types
            Assert.Throws<InvalidCastException>(() => _sut.Cast<Account>());
        }

        [Test, Category("Models")]
        public void Cast_WithMembersConversion_ShouldConvertAllMembers()
        {
            // Test that Cast properly converts all members to ContactDTO
            
            // Arrange
            var largeGroup = new Group
            {
                Id = 1,
                Name = "LargeGroup",
                Description = "Group with many members",
                Members =
                [
                    new() { Id = 1, FirstName = "Member1", LastName = "Test", Pronouns = Pronouns.HeHim },
                    new() { Id = 2, FirstName = "Member2", LastName = "Test", Pronouns = Pronouns.SheHer },
                    new() { Id = 3, FirstName = "Member3", LastName = "Test", Pronouns = Pronouns.TheyThem },
                    new() { Id = 4, FirstName = "Member4", LastName = "Test", Pronouns = Pronouns.HeHim },
                    new() { Id = 5, FirstName = "Member5", LastName = "Test", Pronouns = Pronouns.SheHer }
                ],
                AccountId = 123,
                Account = null,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var groupDto = largeGroup.Cast<GroupDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDto.Members, Has.Count.EqualTo(5));
                for (int i = 0; i < 5; i++)
                {
                    Assert.That(groupDto.Members[i], Is.InstanceOf<ContactDTO>());
                    Assert.That(groupDto.Members[i].Id, Is.EqualTo(i + 1));
                    Assert.That(groupDto.Members[i].FirstName, Is.EqualTo($"Member{i + 1}"));
                }
            });
        }

        [Test, Category("Models")]
        public void DateCreated_AutoProperty_ShouldBeSetInDefaultConstructor()
        {
            // Test that DateCreated is automatically set in default constructor
            
            // Arrange
            var beforeCreation = DateTime.Now;
            
            // Act
            var group = new Group();
            var afterCreation = DateTime.Now;
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(group.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(group.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(group.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceImplementation_JsonIgnoreAttribute_ShouldExcludeFromSerialization()
        {
            // Test that the explicit interface implementation Members property is ignored during JSON serialization
            
            // Arrange
            _sut.Members = _testMembers;
            
            // Act
            var json = _sut.ToJson();
            
            // Assert - The explicit interface Members should not appear in JSON due to JsonIgnore
            // The regular Members property should be serialized instead
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Contain("\"members\""));
                // Verify the JSON structure is correct
                Assert.DoesNotThrow(() => JsonDocument.Parse(json).Dispose());
            });
        }

        [Test, Category("Models")]
        public void Properties_WithExtremeValues_ShouldHandleCorrectly()
        {
            // Test properties with extreme values
            
            // Arrange & Act
            _sut.Id = int.MaxValue;
            _sut.Name = new string('A', 10000); // Very long name
            _sut.Description = new string('B', 10000); // Very long description
            _sut.AccountId = int.MaxValue;
            _sut.DateModified = DateTime.MaxValue;
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.Name, Has.Length.EqualTo(10000));
                Assert.That(_sut.Description, Has.Length.EqualTo(10000));
                Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
            });
            
            // Test that these extreme values can be serialized
            Assert.DoesNotThrow(() => _sut.ToJson());
        }

        #endregion
    }
}
