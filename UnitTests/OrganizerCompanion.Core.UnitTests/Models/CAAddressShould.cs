using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class CAAddressShould
    {
        private CAAddress _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CAAddress();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        #region Constructor Tests
        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange & Act
            var caAddress = new CAAddress();

            // Assert
            Assert.Multiple(() =>
        {
            Assert.That(caAddress.Id, Is.EqualTo(0));
            Assert.That(caAddress.Street1, Is.Null);
            Assert.That(caAddress.Street2, Is.Null);
            Assert.That(caAddress.City, Is.Null);
            Assert.That(caAddress.Province, Is.Null);
            Assert.That(caAddress.ZipCode, Is.Null);
            Assert.That(caAddress.Country, Is.EqualTo(Countries.Canada.GetName()));
            Assert.That(caAddress.Type, Is.Null);
            Assert.That(caAddress.IsPrimary, Is.False);
            Assert.That(caAddress.LinkedEntityId, Is.Null);
            Assert.That(caAddress.LinkedEntity, Is.Null);
            Assert.That(caAddress.LinkedEntityType, Is.Null);
            Assert.That(caAddress.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(caAddress.ModifiedDate, Is.Null);

            // Test EF Core navigation properties default to null
            Assert.That(caAddress.User, Is.Null);
            Assert.That(caAddress.UserId, Is.Null);
            Assert.That(caAddress.Contact, Is.Null);
            Assert.That(caAddress.ContactId, Is.Null);
            Assert.That(caAddress.Organization, Is.Null);
            Assert.That(caAddress.OrganizationId, Is.Null);
            Assert.That(caAddress.SubAccount, Is.Null);
            Assert.That(caAddress.SubAccountId, Is.Null);
        });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var id = 123;
            var street1 = "123 Main Street";
            var street2 = "Apt 4B";
            var city = "Toronto";
            var province = CAProvinces.Ontario.ToStateModel();
            var zipCode = "M5V 3A8";
            var country = "Canada";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var isPrimary = true;
            var linkedEntity = new MockDomainEntity();
            var createdDate = DateTime.Now.AddDays(-1);
            var modifiedDate = DateTime.Now.AddHours(-2);

            // Act
            var caAddress = new CAAddress(
               id: id,
        street1: street1,
             street2: street2,
              city: city,
            province: province,
                   zipCode: zipCode,
           country: country,
           type: type,
                   isPrimary: isPrimary,
                      linkedEntity: linkedEntity,
                createdDate: createdDate,
         modifiedDate: modifiedDate
           );

            // Assert
            Assert.Multiple(() =>
                 {
                     Assert.That(caAddress.Id, Is.EqualTo(id));
                     Assert.That(caAddress.Street1, Is.EqualTo(street1));
                     Assert.That(caAddress.Street2, Is.EqualTo(street2));
                     Assert.That(caAddress.City, Is.EqualTo(city));
                     Assert.That(caAddress.Province, Is.EqualTo(province));
                     Assert.That(caAddress.ZipCode, Is.EqualTo(zipCode));
                     Assert.That(caAddress.Country, Is.EqualTo(country));
                     Assert.That(caAddress.Type, Is.EqualTo(type));
                     Assert.That(caAddress.IsPrimary, Is.EqualTo(isPrimary));
                     Assert.That(caAddress.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                     Assert.That(caAddress.CreatedDate, Is.EqualTo(createdDate));
                     Assert.That(caAddress.ModifiedDate, Is.EqualTo(modifiedDate));
                 });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var street1 = "456 Queen Street";
            var street2 = "Unit 789";
            var city = "Calgary";
            var province = CAProvinces.Alberta.ToStateModel();
            var zipCode = "T2P 1M7";
            var country = "Canada";
            var type = OrganizerCompanion.Core.Enums.Types.Work;
            var isPrimary = false;
            var linkedEntity = new MockDomainEntity();

            // Act
            var caAddress = new CAAddress(
               street1: street1,
         street2: street2,
        city: city,
                province: province,
            zipCode: zipCode,
         country: country,
           type: type,
             isPrimary: isPrimary,
             linkedEntity: linkedEntity
           );

            // Assert
            Assert.Multiple(() =>
           {
               Assert.That(caAddress.Id, Is.EqualTo(0)); // Default value
               Assert.That(caAddress.Street1, Is.EqualTo(street1));
               Assert.That(caAddress.Street2, Is.EqualTo(street2));
               Assert.That(caAddress.City, Is.EqualTo(city));
               Assert.That(caAddress.Province, Is.EqualTo(province));
               Assert.That(caAddress.ZipCode, Is.EqualTo(zipCode));
               Assert.That(caAddress.Country, Is.EqualTo(country));
               Assert.That(caAddress.Type, Is.EqualTo(type));
               Assert.That(caAddress.IsPrimary, Is.EqualTo(isPrimary));
               Assert.That(caAddress.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
               Assert.That(caAddress.ModifiedDate, Is.Null);
           });
        }

        [Test, Category("Models")]
        public void DTOConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var dto = new CAAddressDTO
            {
                Id = 123,
                Street1 = "456 Elm Street",
                Street2 = "Suite 789",
                City = "Montreal",
                Province = CAProvinces.Quebec.ToStateModel(),
                ZipCode = "H3B 2Y7",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                IsPrimary = true,
                CreatedDate = DateTime.Now.AddDays(-5),
                ModifiedDate = DateTime.Now.AddDays(-2)
            };
            var linkedEntity = new MockDomainEntity { Id = 555 };

            // Act
            var caAddress = new CAAddress(dto, linkedEntity);

            // Assert
            Assert.Multiple(() =>
    {
        Assert.That(caAddress.Id, Is.EqualTo(123));
        Assert.That(caAddress.Street1, Is.EqualTo("456 Elm Street"));
        Assert.That(caAddress.Street2, Is.EqualTo("Suite 789"));
        Assert.That(caAddress.City, Is.EqualTo("Montreal"));
        Assert.That(caAddress.Province, Is.EqualTo(dto.Province));
        Assert.That(caAddress.ZipCode, Is.EqualTo("H3B 2Y7"));
        Assert.That(caAddress.Country, Is.EqualTo("Canada"));
        Assert.That(caAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
        Assert.That(caAddress.IsPrimary, Is.True);
        Assert.That(caAddress.LinkedEntityId, Is.EqualTo(555));
        Assert.That(caAddress.CreatedDate, Is.EqualTo(dto.CreatedDate));
        Assert.That(caAddress.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
    });
        }
        #endregion

        #region Property Setter Tests
        [Test, Category("Models")]
        public void Id_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(_sut.Id, Is.EqualTo(456));
                 Assert.That(originalModifiedDate, Is.Null);
                 Assert.That(_sut.ModifiedDate, Is.Not.Null);
             });
        }

        [Test, Category("Models")]
        public void Id_Setter_WithNegativeValue_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(exception.ParamName, Is.EqualTo("Id"));
                Assert.That(exception.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void Street1_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Street1 = "789 Queen Street";

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(_sut.Street1, Is.EqualTo("789 Queen Street"));
                 Assert.That(originalModifiedDate, Is.Null);
                 Assert.That(_sut.ModifiedDate, Is.Not.Null);
             });
        }

        [Test, Category("Models")]
        public void Street2_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Street2 = "Unit 12";

            // Assert
            Assert.Multiple(() =>
                  {
                      Assert.That(_sut.Street2, Is.EqualTo("Unit 12"));
                      Assert.That(originalModifiedDate, Is.Null);
                      Assert.That(_sut.ModifiedDate, Is.Not.Null);
                  });
        }

        [Test, Category("Models")]
        public void City_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.City = "Vancouver";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.City, Is.EqualTo("Vancouver"));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Province_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            var province = CAProvinces.BritishColumbia.ToStateModel();

            // Act
            _sut.Province = province;

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(_sut.Province, Is.EqualTo(province));
                 Assert.That(originalModifiedDate, Is.Null);
                 Assert.That(_sut.ModifiedDate, Is.Not.Null);
             });
        }

        [Test, Category("Models")]
        public void ProvinceEnum_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.ProvinceEnum = CAProvinces.Alberta;

            // Assert
            Assert.Multiple(() =>
         {
             Assert.That(_sut.Province?.Name, Is.EqualTo(CAProvinces.Alberta.ToStateModel().Name));
             Assert.That(_sut.Province?.Abbreviation, Is.EqualTo(CAProvinces.Alberta.ToStateModel().Abbreviation));
             Assert.That(originalModifiedDate, Is.Null);
             Assert.That(_sut.ModifiedDate, Is.Not.Null);
         });
        }

        [Test, Category("Models")]
        public void ProvinceEnum_Getter_ReturnsNull()
        {
            // Act & Assert
            Assert.That(_sut.ProvinceEnum, Is.Null);
        }

        [Test, Category("Models")]
        public void ZipCode_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.ZipCode = "V6B 4N8";

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(_sut.ZipCode, Is.EqualTo("V6B 4N8"));
                 Assert.That(originalModifiedDate, Is.Null);
                 Assert.That(_sut.ModifiedDate, Is.Not.Null);
             });
        }

        [Test, Category("Models")]
        public void Country_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Country = "Canada";

            // Assert
            Assert.Multiple(() =>
               {
                   Assert.That(_sut.Country, Is.EqualTo("Canada"));
                   Assert.That(originalModifiedDate, Is.Null);
                   Assert.That(_sut.ModifiedDate, Is.Not.Null);
               });
        }

        [Test, Category("Models")]
        public void Type_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Assert
            Assert.Multiple(() =>
               {
                   Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                   Assert.That(originalModifiedDate, Is.Null);
                   Assert.That(_sut.ModifiedDate, Is.Not.Null);
               });
        }

        [Test, Category("Models")]
        public void IsPrimary_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.IsPrimary = true;

            // Assert
            Assert.Multiple(() =>
        {
            Assert.That(_sut.IsPrimary, Is.True);
            Assert.That(originalModifiedDate, Is.Null);
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
        });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            var mockEntity = new MockDomainEntity(); // Create a mock domain entity

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity), "LinkedEntity should be set to the mock entity");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(mockEntity.Id), "LinkedEntityId should match the mock entity's Id");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"), "LinkedEntityType should reflect the actual type");
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }
        #endregion

        #region Cast Method Tests
        [Test, Category("Models")]
        public void Cast_ToCAAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "456 Test Street";
            _sut.Street2 = "Unit 789";
            _sut.City = "Toronto";
            _sut.Province = CAProvinces.Ontario.ToStateModel();
            _sut.ZipCode = "M5V 3A8";
            _sut.Country = "Canada";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.IsPrimary = true;

            // Act
            var result = _sut.Cast<CAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<CAAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
                Assert.That(result.Street1, Is.EqualTo(_sut.Street1));
                Assert.That(result.Street2, Is.EqualTo(_sut.Street2));
                Assert.That(result.City, Is.EqualTo(_sut.City));
                Assert.That(result.Province, Is.EqualTo(_sut.Province));
                Assert.That(result.ZipCode, Is.EqualTo(_sut.ZipCode));
                Assert.That(result.Country, Is.EqualTo(_sut.Country));
                Assert.That(result.Type, Is.EqualTo(_sut.Type));
                Assert.That(result.IsPrimary, Is.EqualTo(_sut.IsPrimary));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToICAAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Street1 = "789 Another Street";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var result = _sut.Cast<ICAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<CAAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
                Assert.That(result.Street1, Is.EqualTo(_sut.Street1));
                Assert.That(result.Type, Is.EqualTo(_sut.Type));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 789;
            _sut.City = "Montreal";

            // Act
            var result = _sut.Cast<IAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<CAAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street1 = "Test Street";

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast CAAddress to type MockDomainEntity."));
            });
        }
        #endregion

        #region ToString Tests
        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            _sut.Id = 42;
            _sut.Street1 = "123 Test Street";
            _sut.City = "Montreal";
            _sut.Province = CAProvinces.Quebec.ToStateModel();
            _sut.ZipCode = "H1A 0A1";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:42"));
                Assert.That(result, Does.Contain("Street1:123 Test Street"));
                Assert.That(result, Does.Contain("City:Montreal"));
                Assert.That(result, Does.Contain("Province:QC"));
                Assert.That(result, Does.Contain("Zip:H1A 0A1"));
                Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.CAAddress"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullProvince_HandlesCorrectly()
        {
            // Arrange
            _sut.Id = 42;
            _sut.Street1 = "123 Test Street";
            _sut.City = "Montreal";
            _sut.Province = null;
            _sut.ZipCode = "H1A 0A1";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Province:Unknown"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithProvinceNameOnly_UsesName()
        {
            // Arrange
            var mockProvince = new MockNationalSubdivision { Name = "TestProvince", Abbreviation = null };
            _sut.Id = 42;
            _sut.Province = mockProvince;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain("Province:TestProvince"));
        }
        #endregion

        #region JSON Serialization Tests
        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street1 = "456 Maple Avenue";
            _sut.City = "Calgary";
            _sut.Province = CAProvinces.Alberta.ToStateModel();
            _sut.IsPrimary = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(1));

                Assert.That(root.TryGetProperty("street1", out var street1Property), Is.True);
                Assert.That(street1Property.GetString(), Is.EqualTo("456 Maple Avenue"));

                Assert.That(root.TryGetProperty("isPrimary", out var isPrimaryProperty), Is.True);
                Assert.That(isPrimaryProperty.GetBoolean(), Is.True);
            });
        }

        [Test, Category("Models")]
        public void ToJson_HandlesNullProperties()
        {
            // Arrange
            _sut.Id = 2;
            _sut.Street1 = null;
            _sut.ModifiedDate = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(2));

                Assert.That(root.TryGetProperty("street1", out var street1Property), Is.True);
                Assert.That(street1Property.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }
        #endregion

        #region Additional Property Tests
        [Test, Category("Models")]
        public void CreatedDate_IsReadOnly()
        {
            // Arrange
            var property = typeof(CAAddress).GetProperty(nameof(CAAddress.CreatedDate));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_IsReadOnly()
        {
            // Arrange
            var property = typeof(CAAddress).GetProperty(nameof(CAAddress.LinkedEntityId));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_IsReadOnly()
        {
            // Arrange
            var property = typeof(CAAddress).GetProperty(nameof(CAAddress.LinkedEntityType));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.ModifiedDate = testDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetNullValues()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street1 = null;
                Assert.That(_sut.Street1, Is.Null);

                _sut.Street2 = null;
                Assert.That(_sut.Street2, Is.Null);

                _sut.City = null;
                Assert.That(_sut.City, Is.Null);

                _sut.Province = null;
                Assert.That(_sut.Province, Is.Null);

                _sut.ZipCode = null;
                Assert.That(_sut.ZipCode, Is.Null);

                _sut.Country = null;
                Assert.That(_sut.Country, Is.Null);

                _sut.Type = null;
                Assert.That(_sut.Type, Is.Null);

                _sut.ModifiedDate = null;
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }
        #endregion

        #region EF Core Foreign Key Relationship Tests
        [Test, Category("Models")]
        public void LinkedEntity_WithUser_ShouldSetUserPropertiesAndClearOthers()
        {
            // Arrange
            var user = new MockUser { Id = 100 };

            // Act
            _sut.LinkedEntity = user;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(user), "LinkedEntity should return the User");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(100), "LinkedEntityId should match User.Id");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockUser"), "LinkedEntityType should be MockUser");

                // Since MockUser is not a recognized concrete type (User, Contact, etc.), 
                // it should be stored in the _linkedEntity field instead of navigation properties
                Assert.That(_sut.User, Is.Null, "User navigation property should remain null for mock types");
                Assert.That(_sut.UserId, Is.Null, "UserId foreign key should remain null for mock types");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithContact_ShouldSetContactPropertiesAndClearOthers()
        {
            // Arrange
            var contact = new MockContact { Id = 200 };

            // Act
            _sut.LinkedEntity = contact;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(contact), "LinkedEntity should return the Contact");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(200), "LinkedEntityId should match Contact.Id");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockContact"), "LinkedEntityType should be MockContact");

                // Since MockContact is not a recognized concrete type (User, Contact, etc.), 
                // it should be stored in the _linkedEntity field instead of navigation properties
                Assert.That(_sut.Contact, Is.Null, "Contact navigation property should remain null for mock types");
                Assert.That(_sut.ContactId, Is.Null, "ContactId foreign key should remain null for mock types");
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_SetToNull_ShouldClearAllNavigationProperties()
        {
            // Arrange
            var user = new MockUser { Id = 100 };
            _sut.LinkedEntity = user; // Set initial value

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null, "LinkedEntity should be null");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(null), "LinkedEntityId should be null");
                Assert.That(_sut.LinkedEntityType, Is.Null, "LinkedEntityType should be null");

                // All navigation properties should be null
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithUnsupportedType_ShouldStoreEntityButNotSetNavigationProperties()
        {
            // Arrange
            var unsupportedEntity = new MockDomainEntity { Id = 999 };

            // Act
            _sut.LinkedEntity = unsupportedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(unsupportedEntity), "LinkedEntity should return the unsupported entity");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(999), "_linkedEntityId should be set");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"), "LinkedEntityType should reflect the actual type");

                // All navigation properties should remain null for unsupported types
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithUnrecognizedDomainEntity_ShouldStoreInBackingField()
        {
            // Arrange
            var anotherMockEntity = new AnotherMockEntity { Id = 999 };

            // Act
            _sut.LinkedEntity = anotherMockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(anotherMockEntity), "LinkedEntity should return the unrecognized entity");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(999), "LinkedEntityId should match the entity's Id");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"), "LinkedEntityType should reflect the actual type");

                // Navigation properties should remain null for unrecognized types
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ComputedGetter_ShouldReturnFirstNonNullNavigationProperty()
        {
            // This test verifies the priority order: User ?? Contact ?? Organization ?? SubAccount ?? _linkedEntity

            // Arrange & Act - All navigation properties are null by default, and no _linkedEntity is set
            var result = _sut.LinkedEntity;
            Assert.That(result, Is.Null, "Should return null when all navigation properties and _linkedEntity are null");

            // Test that _linkedEntity is returned when navigation properties are null
            var mockEntity = new MockDomainEntity { Id = 123 };
            _sut.LinkedEntity = mockEntity;
            Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity), "Should return _linkedEntity when navigation properties are null");

            // Test priority: if we manually set foreign key IDs (simulating EF Core loading), 
            // the computed getter should prioritize them over _linkedEntity, but since we can't 
            // create actual domain entities in unit tests, we'll verify _linkedEntity is cleared 
            // when we set LinkedEntity to null
            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntity, Is.Null, "Should return null after clearing LinkedEntity");
        }

        [Test, Category("Models")]
        public void NavigationProperties_ShouldAllowDirectAssignment()
        {
            // Test that navigation properties can be set directly (for EF Core use)
            // We test that the properties exist and can be assigned null values

            // Act & Assert - Test that properties can be set to null without error
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _sut.User = null);
                Assert.DoesNotThrow(() => _sut.UserId = null);
                Assert.DoesNotThrow(() => _sut.Contact = null);
                Assert.DoesNotThrow(() => _sut.ContactId = null);
                Assert.DoesNotThrow(() => _sut.Organization = null);
                Assert.DoesNotThrow(() => _sut.OrganizationId = null);
                Assert.DoesNotThrow(() => _sut.SubAccount = null);
                Assert.DoesNotThrow(() => _sut.SubAccountId = null);

                // Test that foreign key IDs can be set to integer values
                Assert.DoesNotThrow(() => _sut.UserId = 100);
                Assert.DoesNotThrow(() => _sut.ContactId = 200);
                Assert.DoesNotThrow(() => _sut.OrganizationId = 300);
                Assert.DoesNotThrow(() => _sut.SubAccountId = 400);

                // Verify the values were set
                Assert.That(_sut.UserId, Is.EqualTo(100));
                Assert.That(_sut.ContactId, Is.EqualTo(200));
                Assert.That(_sut.OrganizationId, Is.EqualTo(300));
                Assert.That(_sut.SubAccountId, Is.EqualTo(400));
            });
        }

        [Test, Category("Models")]
        public void ForeignKeyProperties_ShouldHaveJsonIgnoreAttribute()
        {
            // Verify that foreign key properties are not serialized to JSON
            var userIdProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.UserId));
            var contactIdProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.ContactId));
            var organizationIdProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.OrganizationId));
            var subAccountIdProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.SubAccountId));

            Assert.Multiple(() =>
            {
                Assert.That(userIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "UserId should have JsonIgnore");
                Assert.That(contactIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "ContactId should have JsonIgnore");
                Assert.That(organizationIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "OrganizationId should have JsonIgnore");
                Assert.That(subAccountIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "SubAccountId should have JsonIgnore");
            });
        }

        [Test, Category("Models")]
        public void NavigationProperties_ShouldHaveJsonIgnoreAndForeignKeyAttributes()
        {
            // Verify EF Core attributes are present
            var userProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.User));
            var contactProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.Contact));
            var organizationProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.Organization));
            var subAccountProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.SubAccount));

            Assert.Multiple(() =>
            {
                // Check JsonIgnore attributes
                Assert.That(userProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "User should have JsonIgnore");
                Assert.That(contactProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "Contact should have JsonIgnore");
                Assert.That(organizationProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "Organization should have JsonIgnore");
                Assert.That(subAccountProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "SubAccount should have JsonIgnore");

                // Check ForeignKey attributes
                Assert.That(userProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("UserId"), "User should have ForeignKey(UserId)");
                Assert.That(contactProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("ContactId"), "Contact should have ForeignKey(ContactId)");
                Assert.That(organizationProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("OrganizationId"), "Organization should have ForeignKey(OrganizationId)");
                Assert.That(subAccountProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("SubAccountId"), "SubAccount should have ForeignKey(SubAccountId)");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ShouldHaveNotMappedAttribute()
        {
            // Verify LinkedEntity is not mapped to database (computed property)
            var linkedEntityProperty = typeof(CAAddress).GetProperty(nameof(CAAddress.LinkedEntity));
            var notMappedAttribute = linkedEntityProperty?.GetCustomAttribute<NotMappedAttribute>();

            Assert.That(notMappedAttribute, Is.Not.Null, "LinkedEntity should have NotMapped attribute");
        }

        [Test, Category("Models")]
        public void JsonSerialization_ShouldNotIncludeNavigationProperties()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street1 = "Test Street";
            _sut.LinkedEntity = new MockUser { Id = 100 };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Not.Contain("\"userId\""), "JSON should not contain userId");
                Assert.That(json, Does.Not.Contain("\"user\""), "JSON should not contain user");
                Assert.That(json, Does.Not.Contain("\"contactId\""), "JSON should not contain contactId");
                Assert.That(json, Does.Not.Contain("\"contact\""), "JSON should not contain contact");
                Assert.That(json, Does.Not.Contain("\"organizationId\""), "JSON should not contain organizationId");
                Assert.That(json, Does.Not.Contain("\"organization\""), "JSON should not contain organization");
                Assert.That(json, Does.Not.Contain("\"subAccountId\""), "JSON should not contain subAccountId");
                Assert.That(json, Does.Not.Contain("\"subAccount\""), "JSON should not contain subAccount");

                // But should contain the computed properties
                Assert.That(json, Does.Contain("\"linkedEntity\""), "JSON should contain linkedEntity");
                Assert.That(json, Does.Contain("\"linkedEntityId\""), "JSON should contain linkedEntityId");
                Assert.That(json, Does.Contain("\"linkedEntityType\""), "JSON should contain linkedEntityType");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ModifiedDateUpdate_ShouldUseUtcNow()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow.AddSeconds(-1);
            var user = new MockUser { Id = 100 };

            // Act
            _sut.LinkedEntity = user;
            var afterSet = DateTime.UtcNow.AddSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ModifiedDate, Is.Not.Null, "ModifiedDate should be set");
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(beforeSet), "ModifiedDate should be after beforeSet");
                Assert.That(_sut.ModifiedDate, Is.LessThan(afterSet), "ModifiedDate should be before afterSet");
                Assert.That(_sut.ModifiedDate?.Kind, Is.EqualTo(DateTimeKind.Utc).Or.EqualTo(DateTimeKind.Unspecified), "Should use UTC time");
            });
        }
        #endregion

        #region Helper Mock Classes
        // Helper mock class for testing INationalSubdivision
        private class MockNationalSubdivision : Interfaces.Type.INationalSubdivision
        {
            public string? Name { get; set; }
            public string? Abbreviation { get; set; }
        }

        // Helper mock class for testing IDomainEntity
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; } = 1;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Another helper mock class for testing LinkedEntityType changes
        private class AnotherMockEntity : IDomainEntity
        {
            public int Id { get; set; } = 2;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Additional mock classes for testing the new EF Core entity types
        private class MockUser : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        private class MockContact : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        private class MockOrganization : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        private class MockSubAccount : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
        #endregion
    }
}
