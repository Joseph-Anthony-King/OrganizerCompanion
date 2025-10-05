using System.Text.Json;
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

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var caAddress = new CAAddress();
            var afterCreation = DateTime.Now;

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
                Assert.That(caAddress.LinkedEntityId, Is.EqualTo(0));
                Assert.That(caAddress.LinkedEntity, Is.Null);
                Assert.That(caAddress.LinkedEntityType, Is.Null);
                Assert.That(caAddress.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(caAddress.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(caAddress.DateModified, Is.EqualTo(default(DateTime)));
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
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

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
                dateCreated: dateCreated,
                dateModified: dateModified
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
                Assert.That(caAddress.LinkedEntityId, Is.EqualTo(0));
                Assert.That(caAddress.LinkedEntity, Is.Null);
                Assert.That(caAddress.LinkedEntityType, Is.Null);
                Assert.That(caAddress.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(caAddress.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(456));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Street1_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Street1 = "789 Queen Street";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo("789 Queen Street"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Street2_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Street2 = "Unit 12";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street2, Is.EqualTo("Unit 12"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void City_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.City = "Vancouver";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.City, Is.EqualTo("Vancouver"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Province_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;
            var province = CAProvinces.BritishColumbia.ToStateModel();

            // Act
            _sut.Province = province;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Province, Is.EqualTo(province));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ZipCode_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.ZipCode = "V6B 4N8";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ZipCode, Is.EqualTo("V6B 4N8"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Country_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Country = "Canada";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Country, Is.EqualTo("Canada"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Type_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.LinkedEntityId = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(456));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;
            var mockEntity = new MockDomainEntity(); // Create a mock domain entity

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var caAddress = new CAAddress();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(caAddress.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.DateModified = testDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void IsCast_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("Models")]
        public void IsCast_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("Models")]
        public void CastId_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("Models")]
        public void CastType_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "SomeType");
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street1 = "456 Maple Avenue";
            _sut.Street2 = "Suite 200";
            _sut.City = "Calgary";
            _sut.Province = CAProvinces.Alberta.ToStateModel();
            _sut.ZipCode = "T2P 1M7";
            _sut.Country = "Canada";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.DateModified = new DateTime(2023, 1, 1, 12, 0, 0);

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
                
                Assert.That(root.TryGetProperty("city", out var cityProperty), Is.True);
                Assert.That(cityProperty.GetString(), Is.EqualTo("Calgary"));
                
                Assert.That(root.TryGetProperty("zipCode", out var zipCodeProperty), Is.True);
                Assert.That(zipCodeProperty.GetString(), Is.EqualTo("T2P 1M7"));
                
                Assert.That(root.TryGetProperty("country", out var countryProperty), Is.True);
                Assert.That(countryProperty.GetString(), Is.EqualTo("Canada"));
                
                Assert.That(root.TryGetProperty("dateCreated", out var dateCreatedProperty), Is.True);
                Assert.That(dateCreatedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
            });
        }

        [Test, Category("Models")]
        public void ToJson_HandlesNullProperties()
        {
            // Arrange
            _sut.Id = 2;
            _sut.Street1 = null;
            _sut.Street2 = null;
            _sut.City = null;
            _sut.Province = null;
            _sut.ZipCode = null;
            _sut.Type = null;
            _sut.DateModified = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
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
                
                Assert.That(root.TryGetProperty("linkedEntityId", out var linkedEntityIdProperty), Is.True);
                Assert.That(linkedEntityIdProperty.GetInt32(), Is.EqualTo(0));
                
                Assert.That(root.TryGetProperty("linkedEntity", out var linkedEntityProperty), Is.True);
                Assert.That(linkedEntityProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
                
                Assert.That(root.TryGetProperty("linkedEntityType", out var linkedEntityTypeProperty), Is.True);
                Assert.That(linkedEntityTypeProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }

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
        public void ToString_HandlesNullProvince()
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
                Assert.That(result, Does.Contain("Id:42"));
                Assert.That(result, Does.Contain("Street1:123 Test Street"));
                Assert.That(result, Does.Contain("City:Montreal"));
                Assert.That(result, Does.Contain("Province:Unknown"));
                Assert.That(result, Does.Contain("Zip:H1A 0A1"));
            });
        }

        [Test, Category("Models")]
        public void ToString_UsesProvinceNameWhenAbbreviationIsNull()
        {
            // Arrange
            var mockProvince = new MockNationalSubdivision { Name = "TestProvince", Abbreviation = null };
            _sut.Id = 42;
            _sut.Street1 = "123 Test Street";
            _sut.City = "TestCity";
            _sut.Province = mockProvince;
            _sut.ZipCode = "T1T 1T1";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Province:TestProvince"));
            });
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetNullValues()
        {
            // Act & Assert
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

            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntity, Is.Null);

            _sut.DateModified = null;
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("Models")]
        public void Properties_MaintainConsistencyAfterMultipleChanges()
        {
            // Arrange
            const int id = 999;
            const string street1 = "999 Final Street";
            const string street2 = "Floor 99";
            const string city = "TestCity";
            var province = CAProvinces.Saskatchewan.ToStateModel();
            const string zipCode = "S7K 0J5";
            const string country = "Canada";
            const int linkedEntityId = 123;
            var linkedEntity = new MockDomainEntity();
            var dateModified = new DateTime(2023, 12, 25, 15, 30, 0);

            // Act
            _sut.Id = id;
            _sut.Street1 = street1;
            _sut.Street2 = street2;
            _sut.City = city;
            _sut.Province = province;
            _sut.ZipCode = zipCode;
            _sut.Country = country;
            _sut.Type = Enums.Types.Billing;
            _sut.LinkedEntityId = linkedEntityId;
            _sut.LinkedEntity = linkedEntity;
            _sut.DateModified = dateModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.Street1, Is.EqualTo(street1));
                Assert.That(_sut.Street2, Is.EqualTo(street2));
                Assert.That(_sut.City, Is.EqualTo(city));
                Assert.That(_sut.Province, Is.EqualTo(province));
                Assert.That(_sut.ZipCode, Is.EqualTo(zipCode));
                Assert.That(_sut.Country, Is.EqualTo(country));
                Assert.That(_sut.Type, Is.EqualTo(Enums.Types.Billing));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(_sut.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithValidProvince_SetsPropertiesCorrectly()
        {
            // Arrange
            var province = CAProvinces.Alberta.ToStateModel();
            var testDate = DateTime.Now;

            // Act
            var caAddress = new CAAddress(
                id: 1,
                street1: "Test Street",
                street2: "Test Unit",
                city: "Test City",
                province: province,
                zipCode: "T1T 1T1",
                country: "Canada",
                type: Enums.Types.Home,
                dateCreated: testDate,
                dateModified: testDate
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Province, Is.EqualTo(province));
                Assert.That(caAddress.Province?.Name, Is.EqualTo(province.Name));
                Assert.That(caAddress.Province?.Abbreviation, Is.EqualTo(province.Abbreviation));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithSerializerOptions_HandlesCircularReferences()
        {
            // Arrange
            _sut.Id = 100;
            _sut.Street1 = "Test Circular Street";
            _sut.City = "Test City";
            _sut.Province = CAProvinces.Ontario.ToStateModel();

            // Act
            var json = _sut.ToJson();

            // Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenLinkedEntityIsNull_ShouldReturnNull()
        {
            // Arrange
            _sut.LinkedEntity = null;

            // Act
            var result = _sut.LinkedEntityType;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenLinkedEntityIsSet_ShouldReturnTypeName()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            _sut.LinkedEntity = mockEntity;

            // Act
            var result = _sut.LinkedEntityType;

            // Assert
            Assert.That(result, Is.EqualTo("MockDomainEntity"));
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenLinkedEntityChanges_ShouldUpdateTypeName()
        {
            // Arrange
            var mockEntity1 = new MockDomainEntity();
            var mockEntity2 = new AnotherMockEntity();
            
            _sut.LinkedEntity = mockEntity1;
            var firstType = _sut.LinkedEntityType;

            // Act
            _sut.LinkedEntity = mockEntity2;
            var secondType = _sut.LinkedEntityType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(firstType, Is.EqualTo("MockDomainEntity"));
                Assert.That(secondType, Is.EqualTo("AnotherMockEntity"));
                Assert.That(firstType, Is.Not.EqualTo(secondType));
            });
        }

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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToICAAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Street1 = "789 Another Street";
            _sut.Street2 = "Apartment 101";
            _sut.City = "Vancouver";
            _sut.Province = CAProvinces.BritishColumbia.ToStateModel();
            _sut.ZipCode = "V6B 4N8";
            _sut.Country = "Canada";
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
                Assert.That(result.Street2, Is.EqualTo(_sut.Street2));
                Assert.That(result.City, Is.EqualTo(_sut.City));
                Assert.That(result.Province, Is.EqualTo(_sut.Province));
                Assert.That(result.ZipCode, Is.EqualTo(_sut.ZipCode));
                Assert.That(result.Country, Is.EqualTo(_sut.Country));
                Assert.That(result.Type, Is.EqualTo(_sut.Type));
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToCAAddressDTO_WithNullProperties_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 999;
            _sut.Street1 = null;
            _sut.Street2 = null;
            _sut.City = null;
            _sut.Province = null;
            _sut.ZipCode = null;
            _sut.Country = null;
            _sut.Type = null;
            _sut.DateModified = null;

            // Act
            var result = _sut.Cast<CAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.Street1, Is.Null);
                Assert.That(result.Street2, Is.Null);
                Assert.That(result.City, Is.Null);
                Assert.That(result.Province, Is.Null);
                Assert.That(result.ZipCode, Is.Null);
                Assert.That(result.Country, Is.Null);
                Assert.That(result.Type, Is.Null);
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.Null);
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
                Assert.That(exception.Message, Does.Contain("Error casting CAAddress to type MockDomainEntity: Cannot cast CAAddress to type MockDomainEntity, casting is not supported for this type"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToCAAddressDTO_WhenExceptionOccurs_ShouldWrapInInvalidCastException()
        {
            // This test verifies that any exceptions during casting are properly wrapped
            // Since the current implementation is straightforward, we test the exception handling path
            // by using a scenario that would cause the cast to fail due to the generic constraint

            // Arrange
            _sut.Id = 1;

            // Act & Assert
            // Test with a type that doesn't implement IDomainEntity (this should fail at compile time,
            // but we test with a type that does implement it but isn't supported)
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Error casting CAAddress to type AnotherMockEntity: Cannot cast CAAddress to type AnotherMockEntity, casting is not supported for this type"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToCAAddressDTO_WithAllProperties_ShouldPreserveAllData()
        {
            // Arrange - Set up CAAddress with comprehensive data
            var province = CAProvinces.Quebec.ToStateModel();
            var dateCreated = DateTime.Now.AddDays(-5);
            var dateModified = DateTime.Now.AddHours(-2);

            var fullAddress = new CAAddress(
                id: 777,
                street1: "123 Complete Street",
                street2: "Suite 456",
                city: "Montreal",
                province: province,
                zipCode: "H1A 0A1",
                country: "Canada",
                type: OrganizerCompanion.Core.Enums.Types.Billing,
                dateCreated: dateCreated,
                dateModified: dateModified
            );

            // Act
            var result = fullAddress.Cast<CAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(777));
                Assert.That(result.Street1, Is.EqualTo("123 Complete Street"));
                Assert.That(result.Street2, Is.EqualTo("Suite 456"));
                Assert.That(result.City, Is.EqualTo("Montreal"));
                Assert.That(result.Province, Is.EqualTo(province));
                Assert.That(result.Province?.Name, Is.EqualTo("Quebec"));
                Assert.That(result.Province?.Abbreviation, Is.EqualTo("QC"));
                Assert.That(result.ZipCode, Is.EqualTo("H1A 0A1"));
                Assert.That(result.Country, Is.EqualTo("Canada"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(result.DateCreated, Is.EqualTo(fullAddress.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(fullAddress.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToICAAddressDTO_ShouldReturnCAAddressDTOInstance()
        {
            // Arrange
            _sut.Id = 888;
            _sut.Street1 = "Interface Test Street";
            _sut.City = "Calgary";
            _sut.Province = CAProvinces.Alberta.ToStateModel();

            // Act
            var result = _sut.Cast<ICAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<CAAddressDTO>()); // Should be concrete CAAddressDTO
                Assert.That(result.Id, Is.EqualTo(888));
                Assert.That(result.Street1, Is.EqualTo("Interface Test Street"));
                Assert.That(result.City, Is.EqualTo("Calgary"));
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_MultipleCallsToSameType_ShouldReturnDifferentInstances()
        {
            // Arrange
            _sut.Id = 555;
            _sut.Street1 = "Consistency Test Street";

            // Act
            var result1 = _sut.Cast<CAAddressDTO>();
            var result2 = _sut.Cast<CAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.Null);
                Assert.That(result2, Is.Not.Null);
                Assert.That(result1, Is.Not.SameAs(result2)); // Different instances
                Assert.That(result1.Id, Is.EqualTo(result2.Id)); // Same data
                Assert.That(result1.Street1, Is.EqualTo(result2.Street1)); // Same data
                Assert.That(result1.DateCreated, Is.EqualTo(result2.DateCreated)); // Same DateCreated
                Assert.That(result1.DateModified, Is.EqualTo(result2.DateModified)); // Same DateModified
            });
        }

        [Test, Category("Models")]
        public void Cast_ToCAAddressDTO_WithSpecificDateModified_ShouldPreserveDateModified()
        {
            // Arrange
            var specificDateModified = new DateTime(2024, 3, 15, 14, 30, 45);
            _sut.Id = 321;
            _sut.Street1 = "Date Test Street";
            _sut.City = "Test City";
            _sut.DateModified = specificDateModified;

            // Act
            var result = _sut.Cast<CAAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(321));
                Assert.That(result.Street1, Is.EqualTo("Date Test Street"));
                Assert.That(result.City, Is.EqualTo("Test City"));
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(specificDateModified));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

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
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

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
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
    }
}