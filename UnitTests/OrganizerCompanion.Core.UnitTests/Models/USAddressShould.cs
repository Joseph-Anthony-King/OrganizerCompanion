using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;
using DataAnnotationsRange = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    /// <summary>
    /// Unit tests for USAddress class to achieve 100% code coverage.
    /// Tests all constructors, properties, methods, field behaviors, validation,
    /// JSON serialization, linked entity relationships, and edge cases.
    /// </summary>
    [TestFixture]
    internal class USAddressShould
    {
        private USAddress _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new USAddress();
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var address = new USAddress();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(0));
                Assert.That(address.Street1, Is.Null);
                Assert.That(address.Street2, Is.Null);
                Assert.That(address.City, Is.Null);
                Assert.That(address.State, Is.Null);
                Assert.That(address.ZipCode, Is.Null);
                Assert.That(address.Country, Is.EqualTo("United States"));
                Assert.That(address.Type, Is.Null);
                Assert.That(address.IsPrimary, Is.False);
                Assert.That(address.LinkedEntity, Is.Null);
                Assert.That(address.LinkedEntityId, Is.Null);
                Assert.That(address.LinkedEntityType, Is.Null);
                Assert.That(address.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(address.ModifiedDate, Is.Null);
                Assert.That(address.StateEnum, Is.Null);
                Assert.That(address.UserId, Is.Null);
                Assert.That(address.User, Is.Null);
                Assert.That(address.ContactId, Is.Null);
                Assert.That(address.Contact, Is.Null);
                Assert.That(address.OrganizationId, Is.Null);
                Assert.That(address.Organization, Is.Null);
                Assert.That(address.SubAccountId, Is.Null);
                Assert.That(address.SubAccount, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldInitializeWithAllParameters()
        {
            // Arrange
            var id = 123;
            var street1 = "123 Main St";
            var street2 = "Apt 4B";
            var city = "Springfield";
            var state = USStates.Illinois.ToStateModel();
            var zipCode = "62701";
            var country = "USA";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var isPrimary = true;
            var linkedEntity = new MockDomainEntity { Id = 456 };
            var createdDate = DateTime.UtcNow.AddDays(-30);
            var modifiedDate = DateTime.UtcNow.AddDays(-1);

            // Act
            var address = new USAddress(
                id, street1, street2, city, state, zipCode, country,
                type, isPrimary, linkedEntity, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(id));
                Assert.That(address.Street1, Is.EqualTo(street1));
                Assert.That(address.Street2, Is.EqualTo(street2));
                Assert.That(address.City, Is.EqualTo(city));
                Assert.That(address.State, Is.EqualTo(state));
                Assert.That(address.ZipCode, Is.EqualTo(zipCode));
                Assert.That(address.Country, Is.EqualTo(country));
                Assert.That(address.Type, Is.EqualTo(type));
                Assert.That(address.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(address.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(address.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(address.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(address.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithoutId_ShouldInitializeAllParametersExceptId()
        {
            // Arrange
            var street1 = "456 Oak Ave";
            var street2 = "Suite 100";
            var city = "Chicago";
            var state = USStates.Illinois.ToStateModel();
            var zipCode = "60601";
            var country = "United States";
            var type = OrganizerCompanion.Core.Enums.Types.Work;
            var isPrimary = false;
            var linkedEntity = new MockDomainEntity { Id = 789 };
            var createdDate = DateTime.UtcNow.AddDays(-15);
            var modifiedDate = DateTime.UtcNow.AddHours(-2);

            // Act
            var address = new USAddress(
                street1, street2, city, state, zipCode, country,
                type, isPrimary, linkedEntity, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(0)); // Default value
                Assert.That(address.Street1, Is.EqualTo(street1));
                Assert.That(address.Street2, Is.EqualTo(street2));
                Assert.That(address.City, Is.EqualTo(city));
                Assert.That(address.State, Is.EqualTo(state));
                Assert.That(address.ZipCode, Is.EqualTo(zipCode));
                Assert.That(address.Country, Is.EqualTo(country));
                Assert.That(address.Type, Is.EqualTo(type));
                Assert.That(address.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(address.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(address.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(address.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(address.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_ShouldInitializeFromUSAddressDTO()
        {
            // Arrange
            var dto = new USAddressDTO
            {
                Id = 999,
                Street1 = "789 Pine St",
                Street2 = "Floor 2",
                City = "New York",
                State = USStates.NewYork.ToStateModel(),
                ZipCode = "10001",
                Country = "USA",
                Type = OrganizerCompanion.Core.Enums.Types.Billing,
                IsPrimary = true,
                CreatedDate = DateTime.UtcNow.AddDays(-10),
                ModifiedDate = DateTime.UtcNow.AddMinutes(-30)
            };
            var linkedEntity = new MockDomainEntity { Id = 111 };

            // Act
            var address = new USAddress(dto, linkedEntity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(dto.Id));
                Assert.That(address.Street1, Is.EqualTo(dto.Street1));
                Assert.That(address.Street2, Is.EqualTo(dto.Street2));
                Assert.That(address.City, Is.EqualTo(dto.City));
                Assert.That(address.State, Is.EqualTo(dto.State));
                Assert.That(address.ZipCode, Is.EqualTo(dto.ZipCode));
                Assert.That(address.Country, Is.EqualTo(dto.Country));
                Assert.That(address.Type, Is.EqualTo(dto.Type));
                Assert.That(address.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(address.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(address.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(address.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(address.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullLinkedEntity_ShouldWork()
        {
            // Arrange
            var dto = new USAddressDTO
            {
                Id = 555,
                Street1 = "Test Street",
                City = "Test City",
                Type = OrganizerCompanion.Core.Enums.Types.Other
            };

            // Act
            var address = new USAddress(dto, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.LinkedEntity, Is.Null);
                Assert.That(address.LinkedEntityId, Is.Null);
                Assert.That(address.Id, Is.EqualTo(dto.Id));
                Assert.That(address.Street1, Is.EqualTo(dto.Street1));
                Assert.That(address.City, Is.EqualTo(dto.City));
            });
        }

        #endregion

        #region Property Tests - Basic Properties

        [Test, Category("Models")]
        public void Id_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedId = 12345;
            var initialModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(expectedId));
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Id_WithNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(ex.ParamName, Is.EqualTo("Id"));
                Assert.That(ex.Message, Contains.Substring("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void Street1_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedStreet = "123 Main Street";
            var initialModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Street1 = expectedStreet;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo(expectedStreet));
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Street2_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedStreet2 = "Apt 4B";

            // Act
            _sut.Street2 = expectedStreet2;

            // Assert
            Assert.That(_sut.Street2, Is.EqualTo(expectedStreet2));
        }

        [Test, Category("Models")]
        public void City_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedCity = "Springfield";

            // Act
            _sut.City = expectedCity;

            // Assert
            Assert.That(_sut.City, Is.EqualTo(expectedCity));
        }

        [Test, Category("Models")]
        public void State_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedState = USStates.California.ToStateModel();

            // Act
            _sut.State = expectedState;

            // Assert
            Assert.That(_sut.State, Is.EqualTo(expectedState));
        }

        [Test, Category("Models")]
        public void ZipCode_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedZipCode = "12345-6789";

            // Act
            _sut.ZipCode = expectedZipCode;

            // Assert
            Assert.That(_sut.ZipCode, Is.EqualTo(expectedZipCode));
        }

        [Test, Category("Models")]
        public void Country_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedCountry = "USA";

            // Act
            _sut.Country = expectedCountry;

            // Assert
            Assert.That(_sut.Country, Is.EqualTo(expectedCountry));
        }

        [Test, Category("Models")]
        public void Type_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedType = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            _sut.Type = expectedType;

            // Assert
            Assert.That(_sut.Type, Is.EqualTo(expectedType));
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldGetAndSetCorrectly()
        {
            // Act & Assert
            _sut.IsPrimary = true;
            Assert.That(_sut.IsPrimary, Is.True);

            _sut.IsPrimary = false;
            Assert.That(_sut.IsPrimary, Is.False);
        }

        #endregion

        #region StateEnum Property Tests

        [Test, Category("Models")]
        public void StateEnum_Get_ShouldReturnNull()
        {
            // Arrange
            _sut.State = USStates.Texas.ToStateModel();

            // Act & Assert
            Assert.That(_sut.StateEnum, Is.Null);
        }

        [Test, Category("Models")]
        public void StateEnum_SetWithValidEnum_ShouldSetStateAndUpdateModifiedDate()
        {
            // Arrange
            var initialModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.StateEnum = USStates.Florida;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Not.Null);
                Assert.That(_sut.State?.Name, Is.EqualTo("Florida"));
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void StateEnum_SetWithNull_ShouldSetStateToNull()
        {
            // Arrange
            _sut.State = USStates.California.ToStateModel();

            // Act
            _sut.StateEnum = null;

            // Assert
            Assert.That(_sut.State, Is.Null);
        }

        #endregion

        #region LinkedEntity Property Tests

        [Test, Category("Models")]
        public void LinkedEntity_Get_WithUser_ShouldReturnUser()
        {
            // Arrange
            var user = new MockUser { Id = 123 };
            _sut.User = user;
            _sut.UserId = user.Id;

            // Act & Assert
            Assert.That(_sut.LinkedEntity, Is.EqualTo(user));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Get_WithContact_ShouldReturnContact()
        {
            // Arrange
            var contact = new MockContact { Id = 456 };
            _sut.Contact = contact;
            _sut.ContactId = contact.Id;

            // Act & Assert
            Assert.That(_sut.LinkedEntity, Is.EqualTo(contact));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Get_WithOrganization_ShouldReturnOrganization()
        {
            // Arrange
            var organization = new MockOrganization { Id = 789 };
            _sut.Organization = organization;
            _sut.OrganizationId = organization.Id;

            // Act & Assert
            Assert.That(_sut.LinkedEntity, Is.EqualTo(organization));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Get_WithSubAccount_ShouldReturnSubAccount()
        {
            // Arrange
            var subAccount = new MockSubAccount { Id = 999 };
            _sut.SubAccount = subAccount;
            _sut.SubAccountId = subAccount.Id;

            // Act & Assert
            Assert.That(_sut.LinkedEntity, Is.EqualTo(subAccount));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Get_WithGenericEntity_ShouldReturnGenericEntity()
        {
            // Arrange
            var genericEntity = new MockDomainEntity { Id = 555 };
            var address = new USAddress();

            // Use constructor to set the private _linkedEntity field
            address = new USAddress(
                1, "Street", null, "City", null, "12345", "USA",
                OrganizerCompanion.Core.Enums.Types.Home, false, genericEntity, DateTime.UtcNow, null);

            // Act & Assert
            Assert.That(address.LinkedEntity, Is.EqualTo(genericEntity));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_WithUser_ShouldSetUserAndClearOthers()
        {
            // Arrange
            var user = new MockUser { Id = 123 };
            var contact = new MockContact { Id = 456 };
            _sut.Contact = contact; // Set something else first
            _sut.ContactId = contact.Id;

            // Act
            _sut.LinkedEntity = user;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.User, Is.EqualTo(user));
                Assert.That(_sut.UserId, Is.EqualTo(user.Id));
                Assert.That(_sut.Contact, Is.Null);
                Assert.That(_sut.ContactId, Is.Null);
                Assert.That(_sut.Organization, Is.Null);
                Assert.That(_sut.OrganizationId, Is.Null);
                Assert.That(_sut.SubAccount, Is.Null);
                Assert.That(_sut.SubAccountId, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(user.Id));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_WithContact_ShouldSetContactAndClearOthers()
        {
            // Arrange
            var contact = new MockContact { Id = 789 };

            // Act
            _sut.LinkedEntity = contact;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Contact, Is.EqualTo(contact));
                Assert.That(_sut.ContactId, Is.EqualTo(contact.Id));
                Assert.That(_sut.User, Is.Null);
                Assert.That(_sut.UserId, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(contact.Id));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_WithOrganization_ShouldSetOrganizationAndClearOthers()
        {
            // Arrange
            var organization = new MockOrganization { Id = 999 };

            // Act
            _sut.LinkedEntity = organization;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Organization, Is.EqualTo(organization));
                Assert.That(_sut.OrganizationId, Is.EqualTo(organization.Id));
                Assert.That(_sut.User, Is.Null);
                Assert.That(_sut.UserId, Is.Null);
                Assert.That(_sut.Contact, Is.Null);
                Assert.That(_sut.ContactId, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(organization.Id));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_WithSubAccount_ShouldSetSubAccountAndClearOthers()
        {
            // Arrange
            var subAccount = new MockSubAccount { Id = 111 };

            // Act
            _sut.LinkedEntity = subAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.SubAccount, Is.EqualTo(subAccount));
                Assert.That(_sut.SubAccountId, Is.EqualTo(subAccount.Id));
                Assert.That(_sut.User, Is.Null);
                Assert.That(_sut.UserId, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(subAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_WithNull_ShouldClearAllLinkedEntities()
        {
            // Arrange
            var user = new MockUser { Id = 123 };
            _sut.LinkedEntity = user;

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.User, Is.Null);
                Assert.That(_sut.UserId, Is.Null);
                Assert.That(_sut.Contact, Is.Null);
                Assert.That(_sut.ContactId, Is.Null);
                Assert.That(_sut.Organization, Is.Null);
                Assert.That(_sut.OrganizationId, Is.Null);
                Assert.That(_sut.SubAccount, Is.Null);
                Assert.That(_sut.SubAccountId, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_WithGenericDomainEntity_ShouldStoreInPrivateField()
        {
            // Arrange
            var genericEntity = new MockDomainEntity { Id = 777 };

            // Act
            _sut.LinkedEntity = genericEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(genericEntity));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(genericEntity.Id));
                Assert.That(_sut.User, Is.Null);
                Assert.That(_sut.Contact, Is.Null);
                Assert.That(_sut.Organization, Is.Null);
                Assert.That(_sut.SubAccount, Is.Null);
            });
        }

        #endregion

        #region LinkedEntityType Property Tests

        [Test, Category("Models")]
        public void LinkedEntityType_WithNoLinkedEntity_ShouldReturnNull()
        {
            // Act & Assert
            Assert.That(_sut.LinkedEntityType, Is.Null);
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WithGenericLinkedEntity_ShouldReturnTypeName()
        {
            // Arrange
            var genericEntity = new MockDomainEntity { Id = 888 };
            _sut.LinkedEntity = genericEntity;

            // Act & Assert
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
        }

        #endregion

        #region DateTime Property Tests

        [Test, Category("Models")]
        public void CreatedDate_ShouldBeReadOnly()
        {
            // Arrange
            var initialCreatedDate = _sut.CreatedDate;

            // Act - Try to modify other properties
            _sut.Id = 123;
            _sut.Street1 = "Test";

            // Assert - CreatedDate should not change
            Assert.That(_sut.CreatedDate, Is.EqualTo(initialCreatedDate));
        }

        [Test, Category("Models")]
        public void ModifiedDate_ShouldUpdateWhenPropertiesChange()
        {
            // Arrange
            var initialModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Street1 = "New Street";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToUSAddressDTO_ShouldReturnCorrectDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "Test Street";
            _sut.City = "Test City";
            _sut.State = USStates.California.ToStateModel();
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var dto = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(_sut.Id));
                Assert.That(dto.Street1, Is.EqualTo(_sut.Street1));
                Assert.That(dto.City, Is.EqualTo(_sut.City));
                Assert.That(dto.State, Is.EqualTo(_sut.State));
                Assert.That(dto.Type, Is.EqualTo(_sut.Type));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIUSAddressDTO_ShouldReturnCorrectDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.ZipCode = "12345";

            // Act
            var dto = _sut.Cast<IUSAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(_sut.Id));
                Assert.That(dto.ZipCode, Is.EqualTo(_sut.ZipCode));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToInvalidType_ShouldThrowInvalidCastException()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.That(ex.Message, Contains.Substring("Cannot cast USAddress to type MockDomainEntity"));
        }

        #endregion

        #region ToJson Method Tests

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 789;
            _sut.Street1 = "JSON Test Street";
            _sut.City = "JSON City";
            _sut.IsPrimary = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Contains.Substring("\"id\":789"));
                Assert.That(json, Contains.Substring("\"street1\":\"JSON Test Street\""));
                Assert.That(json, Contains.Substring("\"city\":\"JSON City\""));
                Assert.That(json, Contains.Substring("\"isPrimary\":true"));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithCircularReference_ShouldHandleGracefully()
        {
            // Arrange
            var user = new MockUser { Id = 123 };
            _sut.User = user;
            _sut.LinkedEntity = user;

            // Act & Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() => _sut.ToJson());
        }

        #endregion

        #region ToString Method Tests

        [Test, Category("Models")]
        public void ToString_WithAllProperties_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 999;
            _sut.Street1 = "ToString Street";
            _sut.City = "ToString City";
            _sut.State = USStates.Texas.ToStateModel();
            _sut.ZipCode = "75001";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Contains.Substring("Id:999"));
                Assert.That(result, Contains.Substring("Street1:ToString Street"));
                Assert.That(result, Contains.Substring("City:ToString City"));
                Assert.That(result, Contains.Substring("State:TX")); // Should show abbreviation
                Assert.That(result, Contains.Substring("Zip:75001"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithStateNameOnly_ShouldShowStateName()
        {
            // Arrange
            var stateWithoutAbbreviation = new MockNationalSubdivision
            {
                Name = "Unknown State",
                Abbreviation = null
            };
            _sut.State = stateWithoutAbbreviation;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Contains.Substring("State:Unknown State"));
        }

        [Test, Category("Models")]
        public void ToString_WithNullState_ShouldShowUnknown()
        {
            // Arrange
            _sut.State = null;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Contains.Substring("State:Unknown"));
        }

        #endregion

        #region Validation Tests

        [Test, Category("Models")]
        public void IdProperty_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(USAddress).GetProperty(nameof(USAddress.Id));

            // Act
            var rangeAttribute = property?.GetCustomAttributes(typeof(DataAnnotationsRange), false)
                .FirstOrDefault() as DataAnnotationsRange;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityIdProperty_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(USAddress).GetProperty(nameof(USAddress.LinkedEntityId));

            // Act
            var rangeAttribute = property?.GetCustomAttributes(typeof(DataAnnotationsRange), false)
                .FirstOrDefault() as DataAnnotationsRange;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute.Maximum, Is.EqualTo(int.MaxValue));
            });
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("Models")]
        public void USAddress_ShouldImplementDomainIUSAddress()
        {
            Assert.That(_sut, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Domain.IUSAddress>());
        }

        [Test, Category("Models")]
        public void USAddress_ShouldImplementIDomainEntity()
        {
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        #endregion

        #region Edge Cases and Complex Scenarios

        [Test, Category("Models")]
        public void AllStringProperties_ShouldAcceptNull()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _sut.Street1 = null);
                Assert.DoesNotThrow(() => _sut.Street2 = null);
                Assert.DoesNotThrow(() => _sut.City = null);
                Assert.DoesNotThrow(() => _sut.ZipCode = null);
                Assert.DoesNotThrow(() => _sut.Country = null);

                Assert.That(_sut.Street1, Is.Null);
                Assert.That(_sut.Street2, Is.Null);
                Assert.That(_sut.City, Is.Null);
                Assert.That(_sut.ZipCode, Is.Null);
                Assert.That(_sut.Country, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void AllStringProperties_ShouldAcceptEmptyStrings()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street1 = "";
                _sut.Street2 = "";
                _sut.City = "";
                _sut.ZipCode = "";
                _sut.Country = "";

                Assert.That(_sut.Street1, Is.EqualTo(""));
                Assert.That(_sut.Street2, Is.EqualTo(""));
                Assert.That(_sut.City, Is.EqualTo(""));
                Assert.That(_sut.ZipCode, Is.EqualTo(""));
                Assert.That(_sut.Country, Is.EqualTo(""));
            });
        }

        [Test, Category("Models")]
        public void AllStringProperties_ShouldHandleUnicodeCharacters()
        {
            // Arrange
            var unicodeStreet = "123 Ünicöde Strëet";
            var unicodeCity = "Tökýø";
            var unicodeZip = "１２３４５";

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street1 = unicodeStreet;
                _sut.City = unicodeCity;
                _sut.ZipCode = unicodeZip;

                Assert.That(_sut.Street1, Is.EqualTo(unicodeStreet));
                Assert.That(_sut.City, Is.EqualTo(unicodeCity));
                Assert.That(_sut.ZipCode, Is.EqualTo(unicodeZip));
            });
        }

        [Test, Category("Models")]
        public void AllEnumProperties_ShouldAcceptAllValidValues()
        {
            // Act & Assert
            foreach (OrganizerCompanion.Core.Enums.Types type in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
            {
                _sut.Type = type;
                Assert.That(_sut.Type, Is.EqualTo(type));
            }

            foreach (USStates state in Enum.GetValues<USStates>())
            {
                _sut.StateEnum = state;
                Assert.That(_sut.State?.Name, Is.EqualTo(state.GetName()));
            }
        }

        [Test, Category("Models")]
        public void CompleteAddressScenario_ShouldWorkCorrectly()
        {
            // Arrange
            var user = new MockUser { Id = 12345 };
            var createdDate = DateTime.UtcNow.AddMonths(-6);

            // Act - Create a complete address
            _sut.Id = 99999;
            _sut.Street1 = "1600 Amphitheatre Parkway";
            _sut.Street2 = "Building 43";
            _sut.City = "Mountain View";
            _sut.StateEnum = USStates.California;
            _sut.ZipCode = "94043-1351";
            _sut.Country = "United States";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.IsPrimary = true;
            _sut.LinkedEntity = user;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(99999));
                Assert.That(_sut.Street1, Is.EqualTo("1600 Amphitheatre Parkway"));
                Assert.That(_sut.Street2, Is.EqualTo("Building 43"));
                Assert.That(_sut.City, Is.EqualTo("Mountain View"));
                Assert.That(_sut.State?.Name, Is.EqualTo("California"));
                Assert.That(_sut.ZipCode, Is.EqualTo("94043-1351"));
                Assert.That(_sut.Country, Is.EqualTo("United States"));
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(_sut.LinkedEntity, Is.EqualTo(user));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(user.Id));
                Assert.That(_sut.User, Is.EqualTo(user));
                Assert.That(_sut.UserId, Is.EqualTo(user.Id));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void MultipleLinkedEntityChanges_ShouldWorkCorrectly()
        {
            // Arrange
            var user = new MockUser { Id = 111 };
            var contact = new MockContact { Id = 222 };
            var organization = new MockOrganization { Id = 333 };

            // Act & Assert - Test multiple linked entity changes
            _sut.LinkedEntity = user;
            Assert.That(_sut.LinkedEntity, Is.EqualTo(user));
            Assert.That(_sut.User, Is.EqualTo(user));
            Assert.That(_sut.Contact, Is.Null);

            _sut.LinkedEntity = contact;
            Assert.That(_sut.LinkedEntity, Is.EqualTo(contact));
            Assert.That(_sut.Contact, Is.EqualTo(contact));
            Assert.That(_sut.User, Is.Null);

            _sut.LinkedEntity = organization;
            Assert.That(_sut.LinkedEntity, Is.EqualTo(organization));
            Assert.That(_sut.Organization, Is.EqualTo(organization));
            Assert.That(_sut.Contact, Is.Null);
        }

        [Test, Category("Models")]
        public void PropertyModifiedDateUpdates_ShouldBeConsistent()
        {
            // Arrange
            var properties = new Action[]
            {
                () => _sut.Id = 1,
                () => _sut.Street1 = "Test",
                () => _sut.Street2 = "Test2",
                () => _sut.City = "TestCity",
                () => _sut.State = USStates.Alaska.ToStateModel(),
                () => _sut.ZipCode = "99501",
                () => _sut.Country = "USA",
                () => _sut.Type = OrganizerCompanion.Core.Enums.Types.Other,
                () => _sut.IsPrimary = true,
                () => _sut.StateEnum = USStates.Hawaii,
                () => _sut.LinkedEntity = new MockUser { Id = 999 }
            };

            // Act & Assert
            foreach (var setProperty in properties)
            {
                var initialModifiedDate = _sut.ModifiedDate;
                System.Threading.Thread.Sleep(1); // Ensure time difference
                setProperty();
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(initialModifiedDate),
                    $"ModifiedDate should update when setting property");
            }
        }

        #endregion

        #region Mock Classes for Testing

        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? ModifiedDate { get; set; }

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        private class MockUser : User
        {
            public MockUser() : base() { }
        }

        private class MockContact : Contact
        {
            public MockContact() : base() { }
        }

        private class MockOrganization : Organization
        {
            public MockOrganization() : base() { }
        }

        private class MockSubAccount : SubAccount
        {
            public MockSubAccount() : base() { }
        }

        private class MockNationalSubdivision : INationalSubdivision
        {
            public string? Name { get; set; }
            public string? Abbreviation { get; set; }
        }

        #endregion
    }
}
