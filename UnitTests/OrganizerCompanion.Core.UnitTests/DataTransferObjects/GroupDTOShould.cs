using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class GroupDTOShould
    {
        private GroupDTO _sut;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);
        private List<ContactDTO> _testMembers;
        private AccountDTO _testAccount;

        [SetUp]
        public void SetUp()
        {
            _testAccount = new AccountDTO
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

            _sut = new GroupDTO();
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var groupDTO = new GroupDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDTO.Id, Is.EqualTo(0));
                Assert.That(groupDTO.GroupName, Is.Null);
                Assert.That(groupDTO.Description, Is.Null);
                Assert.That(groupDTO.Members, Is.Not.Null);
                Assert.That(groupDTO.Members.Count, Is.EqualTo(0));
                Assert.That(groupDTO.AccountId, Is.EqualTo(0));
                Assert.That(groupDTO.Account, Is.Null);
                Assert.That(groupDTO.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(groupDTO.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(groupDTO.DateModified, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("DataTransferObjects")]
        public void Id_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Id = 42;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(42));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.GroupName = "My Group";

            // Assert
            Assert.That(_sut.GroupName, Is.EqualTo("My Group"));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.GroupName = null;

            // Assert
            Assert.That(_sut.GroupName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Description_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Description = "Group Description";

            // Assert
            Assert.That(_sut.Description, Is.EqualTo("Group Description"));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Description = null;

            // Assert
            Assert.That(_sut.Description, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Members_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Members = _testMembers;

            // Assert
            Assert.That(_sut.Members, Is.SameAs(_testMembers));
            Assert.That(_sut.Members.Count, Is.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Members_SetEmptyList_WorksCorrectly()
        {
            // Arrange
            var emptyList = new List<ContactDTO>();

            // Act
            _sut.Members = emptyList;

            // Assert
            Assert.That(_sut.Members, Is.SameAs(emptyList));
            Assert.That(_sut.Members.Count, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.AccountId = 999;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(999));
        }

        [Test, Category("DataTransferObjects")]
        public void Account_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Account = _testAccount;

            // Assert
            Assert.That(_sut.Account, Is.SameAs(_testAccount));
        }

        [Test, Category("DataTransferObjects")]
        public void Account_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Account = null;

            // Assert
            Assert.That(_sut.Account, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.DateCreated = _testDateCreated;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(_testDateCreated));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_GetSet_WorksCorrectly()
        {
            // Arrange & Act
            _sut.DateModified = _testDateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(_testDateModified));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        #endregion

        #region Explicit Interface Implementation Tests - Members

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_Get_ReturnsCorrectCastedList()
        {
            // Arrange
            _sut.Members = _testMembers;
            var groupInterface = (IGroupDTO)_sut;

            // Act
            var interfaceMembers = groupInterface.Members;

            // Assert
            Assert.That(interfaceMembers, Is.Not.Null);
            Assert.That(interfaceMembers.Count, Is.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(interfaceMembers[0], Is.InstanceOf<IContactDTO>());
                Assert.That(interfaceMembers[1], Is.InstanceOf<IContactDTO>());
            });
            Assert.Multiple(() =>
            {
                Assert.That(interfaceMembers[0].Id, Is.EqualTo(1));
                Assert.That(interfaceMembers[1].Id, Is.EqualTo(2));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_Set_UpdatesMembers()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            var groupInterface = (IGroupDTO)_sut;
            var interfaceMembers = _testMembers.Cast<IContactDTO>().ToList();

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

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_SetEmptyList_UpdatesCorrectly()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            var groupInterface = (IGroupDTO)_sut;
            var emptyInterfaceMembers = new List<IContactDTO>();

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

        #region Explicit Interface Implementation Tests - Account

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Account_Get_ReturnsCorrectValue()
        {
            // Arrange
            _sut.Account = _testAccount;
            var groupInterface = (IGroupDTO)_sut;

            // Act
            var interfaceAccount = groupInterface.Account;

            // Assert
            Assert.That(interfaceAccount, Is.SameAs(_testAccount));
            Assert.That(interfaceAccount, Is.InstanceOf<IAccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Account_GetNull_ReturnsNull()
        {
            // Arrange
            _sut.Account = null;
            var groupInterface = (IGroupDTO)_sut;

            // Act
            var interfaceAccount = groupInterface.Account;

            // Assert
            Assert.That(interfaceAccount, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Account_Set_UpdatesAccount()
        {
            // Arrange
            var groupInterface = (IGroupDTO)_sut;

            // Act
            groupInterface.Account = _testAccount;

            // Assert
            Assert.That(_sut.Account, Is.SameAs(_testAccount));
            Assert.That(_sut.Account, Is.InstanceOf<AccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Account_SetNull_SetsAccountToNull()
        {
            // Arrange
            var groupInterface = (IGroupDTO)_sut;
            _sut.Account = _testAccount; // Start with a non-null account

            // Act
            groupInterface.Account = null;

            // Assert
            Assert.That(_sut.Account, Is.Null);
        }

        #endregion

        #region Not Implemented Methods Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _sut.Cast<ContactDTO>());
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _sut.ToJson());
            Assert.That(ex, Is.Not.Null);
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void GroupDTO_ImplementsIGroupDTOInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IGroupDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void GroupDTO_ImplementsIDomainEntityInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        #endregion

        #region DateModified Update Tests

        [Test, Category("DataTransferObjects")]
        public void Members_Set_DoesNotUpdateDateModified()
        {
            // Arrange
            _sut.DateModified = null;

            // Act
            _sut.Members = _testMembers;

            // Assert - Note: Regular Members property doesn't update DateModified, only interface implementation does
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_Set_UpdatesDateModified_WhenNull()
        {
            // Arrange
            var beforeModified = DateTime.Now;
            _sut.DateModified = null;
            var groupInterface = (IGroupDTO)_sut;
            var interfaceMembers = _testMembers.Cast<IContactDTO>().ToList();

            // Act
            groupInterface.Members = interfaceMembers;
            var afterModified = DateTime.Now;

            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeModified));
            Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterModified));
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_Set_UpdatesDateModified_WhenAlreadySet()
        {
            // Arrange
            _sut.DateModified = _testDateModified;
            var beforeSet = DateTime.Now;
            var groupInterface = (IGroupDTO)_sut;
            var interfaceMembers = _testMembers.Cast<IContactDTO>().ToList();

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

        #region JSON Attribute Behavior Tests

        [Test, Category("DataTransferObjects")]
        public void Description_HasRequiredAttribute()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.Description));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null, "Description property should have Required attribute");
        }

        [Test, Category("DataTransferObjects")]
        public void Description_HasCorrectJsonPropertyName()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.Description));

            // Act
            var jsonAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.That(jsonAttribute, Is.Not.Null);
            Assert.That(jsonAttribute.Name, Is.EqualTo("description"));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_HasJsonIgnoreWhenWritingDefault()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.AccountId));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false)
                .FirstOrDefault() as JsonIgnoreAttribute;

            // Assert
            Assert.That(jsonIgnoreAttribute, Is.Not.Null);
            Assert.That(jsonIgnoreAttribute.Condition, Is.EqualTo(JsonIgnoreCondition.WhenWritingDefault));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_DoesNotHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.AccountId));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

            // Assert
            Assert.That(requiredAttribute, Is.Null, "AccountId property should not have Required attribute");
        }

        [Test, Category("DataTransferObjects")]
        public void Name_HasRequiredAttribute()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.GroupName));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null, "Name property should have Required attribute");
        }

        [Test, Category("DataTransferObjects")]
        public void Members_HasRequiredAttribute()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.Members));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null, "Members property should have Required attribute");
        }

        [Test, Category("DataTransferObjects")]
        public void Id_HasCorrectRangeAttribute()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.Id));

            // Act
            var rangeAttribute = property?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)
                .FirstOrDefault() as System.ComponentModel.DataAnnotations.RangeAttribute;

            // Assert
            Assert.That(rangeAttribute, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_HasCorrectRangeAttribute()
        {
            // Arrange
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.AccountId));

            // Act
            var rangeAttribute = property?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false)
                .FirstOrDefault() as System.ComponentModel.DataAnnotations.RangeAttribute;

            // Assert
            Assert.That(rangeAttribute, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute.ErrorMessage, Is.EqualTo("Account Id must be a non-negative number."));
            });
        }

        #endregion

        #region Additional Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.Id = int.MinValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.AccountId = int.MinValue;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.AccountId = int.MaxValue;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.GroupName = "";

            // Assert
            Assert.That(_sut.GroupName, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_ShouldAcceptWhitespace()
        {
            // Arrange & Act
            _sut.GroupName = "   ";

            // Assert
            Assert.That(_sut.GroupName, Is.EqualTo("   "));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeName = "测试组 (Test Group) - Группа Тест 🚀";

            // Act
            _sut.GroupName = unicodeName;

            // Assert
            Assert.That(_sut.GroupName, Is.EqualTo(unicodeName));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_ShouldAcceptVeryLongString()
        {
            // Arrange
            var longName = new string('G', 10000) + " Group";

            // Act
            _sut.GroupName = longName;

            // Assert
            Assert.That(_sut.GroupName, Is.EqualTo(longName));
            Assert.That(_sut.GroupName?.Length, Is.EqualTo(10006));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.Description = "";

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldAcceptMultilineText()
        {
            // Arrange
            var multilineDescription = "Line 1\nLine 2\r\nLine 3\tTabbed";

            // Act
            _sut.Description = multilineDescription;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(multilineDescription));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialDescription = "Group with special chars: !@#$%^&*()_+-=[]{}|;':\",./<>?";

            // Act
            _sut.Description = specialDescription;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(specialDescription));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 12, 25, 14, 30, 45, 123);

            // Act
            _sut.DateCreated = preciseDate;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(preciseDate));
            Assert.That(_sut.DateCreated.Millisecond, Is.EqualTo(123));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 11, 15, 9, 45, 30, 456);

            // Act
            _sut.DateModified = preciseDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(preciseDate));
            Assert.That(_sut.DateModified?.Millisecond, Is.EqualTo(456));
        }

        [Test, Category("DataTransferObjects")]
        public void Members_ShouldHandleLargeCollections()
        {
            // Arrange
            var largeCollection = new List<ContactDTO>();
            for (int i = 1; i <= 1000; i++)
            {
                largeCollection.Add(new ContactDTO
                {
                    Id = i,
                    FirstName = $"Contact{i}",
                    LastName = "Test"
                });
            }

            // Act
            _sut.Members = largeCollection;

            // Assert
            Assert.That(_sut.Members, Is.SameAs(largeCollection));
            Assert.That(_sut.Members.Count, Is.EqualTo(1000));
        }

        [Test, Category("DataTransferObjects")]
        public void Members_ShouldAcceptNullContactsInList()
        {
            // Arrange
            var membersWithNull = new List<ContactDTO>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe" },
                null!,
                new() { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            // Act
            _sut.Members = membersWithNull;

            // Assert
            Assert.That(_sut.Members, Is.SameAs(membersWithNull));
            Assert.That(_sut.Members.Count, Is.EqualTo(3));
            Assert.That(_sut.Members[1], Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_ShouldHandleEmptyToNonEmptyTransition()
        {
            // Arrange
            var groupInterface = (IGroupDTO)_sut;
            var emptyMembers = new List<IContactDTO>();
            var nonEmptyMembers = _testMembers.Cast<IContactDTO>().ToList();

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Start with empty
                groupInterface.Members = emptyMembers;
                Assert.That(_sut.Members.Count, Is.EqualTo(0));

                // Add members
                groupInterface.Members = nonEmptyMembers;
                Assert.That(_sut.Members.Count, Is.EqualTo(2));

                // Back to empty
                groupInterface.Members = emptyMembers;
                Assert.That(_sut.Members.Count, Is.EqualTo(0));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Account_ShouldHandleNullToNonNullTransition()
        {
            // Arrange
            var groupInterface = (IGroupDTO)_sut;

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Start with null
                groupInterface.Account = null;
                Assert.That(_sut.Account, Is.Null);

                // Set to non-null
                groupInterface.Account = _testAccount;
                Assert.That(_sut.Account, Is.SameAs(_testAccount));

                // Back to null
                groupInterface.Account = null;
                Assert.That(_sut.Account, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void GroupDTO_ShouldSupportObjectInitializerSyntax()
        {
            // Arrange
            var testCreated = DateTime.Now.AddDays(-7);
            var testModified = DateTime.Now.AddHours(-1);

            // Act
            var groupDto = new GroupDTO
            {
                Id = 555,
                GroupName = "Initializer Group",
                Description = "Group created with initializer",
                Members = _testMembers,
                AccountId = 123,
                Account = _testAccount,
                DateCreated = testCreated,
                DateModified = testModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(groupDto.Id, Is.EqualTo(555));
                Assert.That(groupDto.GroupName, Is.EqualTo("Initializer Group"));
                Assert.That(groupDto.Description, Is.EqualTo("Group created with initializer"));
                Assert.That(groupDto.Members, Is.SameAs(_testMembers));
                Assert.That(groupDto.AccountId, Is.EqualTo(123));
                Assert.That(groupDto.Account, Is.SameAs(_testAccount));
                Assert.That(groupDto.DateCreated, Is.EqualTo(testCreated));
                Assert.That(groupDto.DateModified, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void GroupDTO_PropertiesShouldBeIndependent()
        {
            // Arrange & Act
            _sut.Id = 999;
            _sut.GroupName = "Independent Group";
            _sut.Description = "Independent Description";
            _sut.Members = _testMembers;
            _sut.AccountId = 456;
            _sut.Account = _testAccount;
            var testDate = DateTime.Now.AddDays(-5);
            var testModified = DateTime.Now.AddHours(-3);
            _sut.DateCreated = testDate;
            _sut.DateModified = testModified;

            // Store original values
            var originalId = _sut.Id;
            var originalName = _sut.GroupName;
            var originalDescription = _sut.Description;
            var originalMembers = _sut.Members;
            var originalAccountId = _sut.AccountId;
            var originalAccount = _sut.Account;
            var originalCreated = _sut.DateCreated;
            var originalModified = _sut.DateModified;

            // Assert
            Assert.Multiple(() =>
            {
                // Change Id, verify others unchanged
                _sut.Id = 1000;
                Assert.That(_sut.GroupName, Is.EqualTo(originalName));
                Assert.That(_sut.Description, Is.EqualTo(originalDescription));
                Assert.That(_sut.Members, Is.SameAs(originalMembers));
                Assert.That(_sut.AccountId, Is.EqualTo(originalAccountId));
                Assert.That(_sut.Account, Is.SameAs(originalAccount));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));

                // Change Name, verify others unchanged
                _sut.GroupName = "Changed Name";
                Assert.That(_sut.Id, Is.EqualTo(1000)); // New value
                Assert.That(_sut.Description, Is.EqualTo(originalDescription));
                Assert.That(_sut.Members, Is.SameAs(originalMembers));
                Assert.That(_sut.AccountId, Is.EqualTo(originalAccountId));
                Assert.That(_sut.Account, Is.SameAs(originalAccount));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException_WithDifferentGenericTypes()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<ContactDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IGroupDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<AccountDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldConsistentlyThrowNotImplementedException()
        {
            // Arrange - Multiple calls should all throw
            var exceptions = new List<NotImplementedException>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    var ex = Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                    Assert.That(ex, Is.Not.Null);
                    if (ex != null)
                    {
                        exceptions.Add(ex);
                    }
                }
                
                // Verify all exceptions are separate instances
                Assert.That(exceptions, Has.Count.EqualTo(3));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_Set_ShouldUpdateDateModifiedMultipleTimes()
        {
            // Arrange
            var groupInterface = (IGroupDTO)_sut;
            var firstMembers = new List<IContactDTO> { _testMembers[0] };
            var secondMembers = new List<IContactDTO> { _testMembers[1] };

            // Act & Assert
            Assert.Multiple(() =>
            {
                // First assignment
                groupInterface.Members = firstMembers;
                var firstModified = _sut.DateModified;
                Assert.That(firstModified, Is.Not.Null);

                // Wait a moment to ensure different timestamp
                System.Threading.Thread.Sleep(1);

                // Second assignment 
                groupInterface.Members = secondMembers;
                var secondModified = _sut.DateModified;
                Assert.That(secondModified, Is.Not.Null);
                Assert.That(secondModified, Is.GreaterThan(firstModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void GroupDTO_ShouldMaintainStateAcrossMultipleOperations()
        {
            // Arrange
            var operations = new[]
            {
                new { Id = 1, Name = (string?)"Group One", Description = (string?)"First group" },
                new { Id = 2, Name = (string?)"Group Two", Description = (string?)null },
                new { Id = 3, Name = (string?)null, Description = (string?)"Third group" },
                new { Id = 4, Name = (string?)"", Description = (string?)"" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var op in operations)
                {
                    _sut.Id = op.Id;
                    _sut.GroupName = op.Name;
                    _sut.Description = op.Description;

                    Assert.That(_sut.Id, Is.EqualTo(op.Id));
                    Assert.That(_sut.GroupName, Is.EqualTo(op.Name));
                    Assert.That(_sut.Description, Is.EqualTo(op.Description));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHandleNullToDateTransitions()
        {
            // Arrange
            var testDates = new DateTime[]
            {
                DateTime.Now,
                DateTime.MinValue,
                DateTime.MaxValue,
                new DateTime(2020, 1, 1),
                new DateTime(2030, 12, 31, 23, 59, 59)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var date in testDates)
                {
                    // Start with null
                    _sut.DateModified = null;
                    Assert.That(_sut.DateModified, Is.Null);

                    // Assign date
                    _sut.DateModified = date;
                    Assert.That(_sut.DateModified, Is.EqualTo(date));

                    // Back to null
                    _sut.DateModified = null;
                    Assert.That(_sut.DateModified, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Members_ShouldSupportConsecutiveAssignments()
        {
            // Arrange
            var emptyList = new List<ContactDTO>();
            var singleMemberList = new List<ContactDTO> { _testMembers[0] };
            var fullList = _testMembers;

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Empty -> Single -> Full -> Empty
                _sut.Members = emptyList;
                Assert.That(_sut.Members, Is.SameAs(emptyList));

                _sut.Members = singleMemberList;
                Assert.That(_sut.Members, Is.SameAs(singleMemberList));

                _sut.Members = fullList;
                Assert.That(_sut.Members, Is.SameAs(fullList));

                _sut.Members = emptyList;
                Assert.That(_sut.Members, Is.SameAs(emptyList));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void GroupDTO_ShouldHandleComplexScenarios()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;
            _sut.GroupName = "Complex Group with 特殊字符 and emojis 🌟⭐";
            _sut.Description = "Multi-line\ndescription with\ttabs and special chars !@#$%";
            _sut.AccountId = int.MaxValue;
            _sut.DateCreated = DateTime.MaxValue.AddMilliseconds(-1);
            _sut.DateModified = DateTime.MinValue.AddMilliseconds(1);

            // Create complex members list
            var complexMembers = new List<ContactDTO>();
            for (int i = 1; i <= 100; i++)
            {
                complexMembers.Add(new ContactDTO
                {
                    Id = i,
                    FirstName = $"Contact_{i}_测试",
                    LastName = $"LastName_{i}_🚀"
                });
            }
            _sut.Members = complexMembers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.GroupName, Contains.Substring("Complex Group"));
                Assert.That(_sut.GroupName, Contains.Substring("特殊字符"));
                Assert.That(_sut.GroupName, Contains.Substring("🌟⭐"));
                Assert.That(_sut.Description, Contains.Substring("Multi-line"));
                Assert.That(_sut.Description, Contains.Substring("!@#$%"));
                Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.Members.Count, Is.EqualTo(100));
                Assert.That(_sut.DateCreated, Is.LessThan(DateTime.MaxValue));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.MinValue));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IGroupDTO_Members_ShouldPreserveCastingBehavior()
        {
            // Arrange
            _sut.Members = _testMembers;
            var groupInterface = (IGroupDTO)_sut;

            // Act
            var interfaceMembers = groupInterface.Members;
            var backToInterface = interfaceMembers.Cast<ContactDTO>().ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceMembers.Count, Is.EqualTo(_testMembers.Count));
                Assert.That(backToInterface.Count, Is.EqualTo(_testMembers.Count));
                
                for (int i = 0; i < _testMembers.Count; i++)
                {
                    Assert.That(interfaceMembers[i].Id, Is.EqualTo(_testMembers[i].Id));
                    Assert.That(backToInterface[i].Id, Is.EqualTo(_testMembers[i].Id));
                }
            });
        }

        #endregion
    }
}
