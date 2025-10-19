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
                Assert.That(groupDTO.Name, Is.Null);
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
            _sut.Name = "My Group";

            // Assert
            Assert.That(_sut.Name, Is.EqualTo("My Group"));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_SetNull_WorksCorrectly()
        {
            // Arrange & Act
            _sut.Name = null;

            // Assert
            Assert.That(_sut.Name, Is.Null);
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

        #region IDomainEntity Not Implemented Properties Tests

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _ = _sut.IsCast);
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _ = _sut.CastId);
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _sut.CastId = 123);
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _ = _sut.CastType);
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ThrowsNotImplementedException()
        {
            // Act & Assert
            var ex = Assert.Throws<NotImplementedException>(() => _sut.CastType = "TestType");
            Assert.That(ex, Is.Not.Null);
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
            var property = typeof(GroupDTO).GetProperty(nameof(GroupDTO.Name));
            
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
  }
}
