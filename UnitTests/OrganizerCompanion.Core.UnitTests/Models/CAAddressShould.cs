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
                Assert.That(caAddress.IsPrimary, Is.False);
                Assert.That(caAddress.LinkedEntityId, Is.Null);
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
            var isPrimary = true;
            var linkedEntity = new MockDomainEntity();
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
                isPrimary: isPrimary,
                linkedEntity: linkedEntity,
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
                Assert.That(caAddress.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(caAddress.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(caAddress.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(caAddress.LinkedEntityType, Is.EqualTo(linkedEntity.GetType().Name));
                Assert.That(caAddress.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(caAddress.DateModified, Is.EqualTo(dateModified));
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
            var beforeCreation = DateTime.Now;

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
            var afterCreation = DateTime.Now;

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
                Assert.That(caAddress.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(caAddress.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(caAddress.LinkedEntityType, Is.EqualTo(linkedEntity.GetType().Name));
                Assert.That(caAddress.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(caAddress.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(caAddress.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithNullLinkedEntity_SetsPropertiesCorrectly()
        {
            // Arrange
            var street1 = "789 Main Avenue";
            var street2 = "Suite 101";
            var city = "Vancouver";
            var province = CAProvinces.BritishColumbia.ToStateModel();
            var zipCode = "V6B 4N8";
            var country = "Canada";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var isPrimary = true;
            var beforeCreation = DateTime.Now;

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
                linkedEntity: null
            );
            var afterCreation = DateTime.Now;

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
                Assert.That(caAddress.LinkedEntity, Is.Null);
                Assert.That(caAddress.LinkedEntityId, Is.Null);
                Assert.That(caAddress.LinkedEntityType, Is.Null);
                Assert.That(caAddress.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(caAddress.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(caAddress.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithNullStringValues_AcceptsNullValues()
        {
            // Arrange
            var province = CAProvinces.Manitoba.ToStateModel();
            var type = OrganizerCompanion.Core.Enums.Types.Billing;
            var linkedEntity = new MockDomainEntity();

            // Act
            var caAddress = new CAAddress(
                street1: null!,
                street2: null!,
                city: null!,
                province: province,
                zipCode: null!,
                country: null!,
                type: type,
                isPrimary: false,
                linkedEntity: linkedEntity
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Street1, Is.Null);
                Assert.That(caAddress.Street2, Is.Null);
                Assert.That(caAddress.City, Is.Null);
                Assert.That(caAddress.Province, Is.EqualTo(province));
                Assert.That(caAddress.ZipCode, Is.Null);
                Assert.That(caAddress.Country, Is.Null);
                Assert.That(caAddress.Type, Is.EqualTo(type));
                Assert.That(caAddress.IsPrimary, Is.False);
                Assert.That(caAddress.LinkedEntity, Is.EqualTo(linkedEntity));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithNullProvince_AcceptsNullValues()
        {
            // Arrange
            var street1 = "Test Street";
            var city = "Test City";
            var zipCode = "T1T 1T1";
            var country = "Canada";

            // Act
            var caAddress = new CAAddress(
                street1: street1,
                street2: null!,
                city: city,
                province: null!,
                zipCode: zipCode,
                country: country,
                type: OrganizerCompanion.Core.Enums.Types.Home,
                isPrimary: true,
                linkedEntity: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Street1, Is.EqualTo(street1));
                Assert.That(caAddress.Street2, Is.Null);
                Assert.That(caAddress.City, Is.EqualTo(city));
                Assert.That(caAddress.Province, Is.Null);
                Assert.That(caAddress.ZipCode, Is.EqualTo(zipCode));
                Assert.That(caAddress.Country, Is.EqualTo(country));
                Assert.That(caAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(caAddress.IsPrimary, Is.True);
                Assert.That(caAddress.LinkedEntity, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithAllProvinces_SetsProvinceCorrectly()
        {
            // Arrange & Act & Assert - Test with various Canadian provinces
            var provinces = new[]
            {
                CAProvinces.Alberta.ToStateModel(),
                CAProvinces.BritishColumbia.ToStateModel(),
                CAProvinces.Manitoba.ToStateModel(),
                CAProvinces.NewBrunswick.ToStateModel(),
                CAProvinces.NewfoundlandAndLabrador.ToStateModel(),
                CAProvinces.NorthwestTerritories.ToStateModel(),
                CAProvinces.NovaScotia.ToStateModel(),
                CAProvinces.Nunavut.ToStateModel(),
                CAProvinces.Ontario.ToStateModel(),
                CAProvinces.PrinceEdwardIsland.ToStateModel(),
                CAProvinces.Quebec.ToStateModel(),
                CAProvinces.Saskatchewan.ToStateModel(),
                CAProvinces.Yukon.ToStateModel()
            };

            foreach (var province in provinces)
            {
                var caAddress = new CAAddress(
                    street1: "Test Street",
                    street2: "Test Unit",
                    city: "Test City",
                    province: province,
                    zipCode: "T1T 1T1",
                    country: "Canada",
                    type: OrganizerCompanion.Core.Enums.Types.Home,
                    isPrimary: false,
                    linkedEntity: null
                );

                Assert.That(caAddress.Province, Is.EqualTo(province),
                    $"Province should be set correctly for {province.Name}");
            }
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
        public void IsPrimary_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.IsPrimary = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldGetAndSetCorrectly()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test setting to true
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);

                // Test setting to false
                _sut.IsPrimary = false;
                Assert.That(_sut.IsPrimary, Is.False);
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldDefaultToFalse()
        {
            // Arrange & Act
            var newAddress = new CAAddress();

            // Assert
            Assert.That(newAddress.IsPrimary, Is.False);
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldHandleBooleanToggling()
        {
            // Arrange
            Assert.That(_sut.IsPrimary, Is.False, "Should start as false");

            // Act & Assert - Toggle multiple times
            _sut.IsPrimary = !_sut.IsPrimary;
            Assert.That(_sut.IsPrimary, Is.True, "Should be true after first toggle");

            _sut.IsPrimary = !_sut.IsPrimary;
            Assert.That(_sut.IsPrimary, Is.False, "Should be false after second toggle");

            _sut.IsPrimary = !_sut.IsPrimary;
            Assert.That(_sut.IsPrimary, Is.True, "Should be true after third toggle");
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldMaintainValueAfterOtherPropertyChanges()
        {
            // Arrange
            _sut.IsPrimary = true;

            // Act - Change other properties
            _sut.Id = 999;
            _sut.Street1 = "Test Street";
            _sut.City = "Test City";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Assert
            Assert.That(_sut.IsPrimary, Is.True,
                "IsPrimary should maintain its value when other properties change");
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
            _sut.IsPrimary = true;
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

                Assert.That(root.TryGetProperty("isPrimary", out var isPrimaryProperty), Is.True);
                Assert.That(isPrimaryProperty.GetBoolean(), Is.True);

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
            _sut.IsPrimary = false;
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

                Assert.That(root.TryGetProperty("isPrimary", out var isPrimaryProperty), Is.True);
                Assert.That(isPrimaryProperty.GetBoolean(), Is.False);

                Assert.That(root.TryGetProperty("linkedEntityId", out var linkedEntityIdProperty), Is.True);
                Assert.That(linkedEntityIdProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));

                Assert.That(root.TryGetProperty("linkedEntity", out var linkedEntityProperty), Is.True);
                Assert.That(linkedEntityProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));

                Assert.That(root.TryGetProperty("linkedEntityType", out var linkedEntityTypeProperty), Is.True);
                Assert.That(linkedEntityTypeProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));

                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }

        [Test, Category("Models")]
        public void ToJson_ShouldSerializeBooleanValuesCorrectly()
        {
            // Arrange, Act & Assert for true
            _sut.IsPrimary = true;
            var jsonTrue = _sut.ToJson();
            var documentTrue = JsonDocument.Parse(jsonTrue);
            Assert.Multiple(() =>
            {
                Assert.That(documentTrue.RootElement.TryGetProperty("isPrimary", out var isPrimaryTrueProperty), Is.True);
                Assert.That(isPrimaryTrueProperty.GetBoolean(), Is.True);
            });

            // Arrange, Act & Assert for false
            _sut.IsPrimary = false;
            var jsonFalse = _sut.ToJson();
            var documentFalse = JsonDocument.Parse(jsonFalse);
            Assert.That(documentFalse.RootElement.TryGetProperty("isPrimary", out var isPrimaryFalseProperty), Is.True);
            Assert.That(isPrimaryFalseProperty.GetBoolean(), Is.False);
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
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
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
                isPrimary: true,
                linkedEntity: null,
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
            _sut.IsPrimary = false;

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
                Assert.That(result.IsPrimary, Is.EqualTo(_sut.IsPrimary));
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
            _sut.IsPrimary = false;
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
                Assert.That(result.IsPrimary, Is.False);
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
                Assert.That(exception.Message, Does.Contain("Cannot cast CAAddress to type MockDomainEntity."));
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
                Assert.That(exception.Message, Does.Contain("Cannot cast CAAddress to type AnotherMockEntity."));
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
                isPrimary: true,
                linkedEntity: null,
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

        [Test, Category("Models")]
        public void JsonConstructor_WithUnusedParameters_ShouldIgnoreThemAndSetPropertiesCorrectly()
        {
            // Arrange & Act - Test that unused parameters (isCast, castId, castType) are ignored
            var linkedEntity = new MockDomainEntity();
            var testDate = DateTime.Now;
            var province = CAProvinces.Ontario.ToStateModel();

            var caAddress = new CAAddress(
                id: 999,
                street1: "Test Street",
                street2: "Test Unit",
                city: "Test City",
                province: province,
                zipCode: "M1M 1M1",
                country: "Canada",
                type: OrganizerCompanion.Core.Enums.Types.Home,
                isPrimary: true,
                linkedEntity: linkedEntity,
                dateCreated: testDate,
                dateModified: testDate
            );

            // Assert - Verify that the object is created correctly and unused parameters don't affect it
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(999));
                Assert.That(caAddress.Street1, Is.EqualTo("Test Street"));
                Assert.That(caAddress.Street2, Is.EqualTo("Test Unit"));
                Assert.That(caAddress.City, Is.EqualTo("Test City"));
                Assert.That(caAddress.Province, Is.EqualTo(province));
                Assert.That(caAddress.ZipCode, Is.EqualTo("M1M 1M1"));
                Assert.That(caAddress.Country, Is.EqualTo("Canada"));
                Assert.That(caAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(caAddress.DateCreated, Is.EqualTo(testDate));
                Assert.That(caAddress.DateModified, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsCorrectly()
        {
            // This test verifies that the catch block in the Cast method properly rethrows exceptions.
            // We test this by attempting to cast to an unsupported type, which triggers the exception handling path.

            // Arrange
            _sut.Id = 1;
            _sut.Street1 = "Test Street";

            // Act & Assert - Test that InvalidCastException is thrown and rethrown correctly
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast CAAddress to type MockDomainEntity"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithProvinceHavingOnlyName_ShouldUseNameForDisplay()
        {
            // Arrange - Create a province mock that has Name but null Abbreviation
            var mockProvince = new MockNationalSubdivision
            {
                Name = "Full Province Name",
                Abbreviation = null
            };

            _sut.Id = 100;
            _sut.Street1 = "Name Test Street";
            _sut.City = "Name Test City";
            _sut.Province = mockProvince;
            _sut.ZipCode = "N1N 1N1";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:100"));
                Assert.That(result, Does.Contain("Street1:Name Test Street"));
                Assert.That(result, Does.Contain("City:Name Test City"));
                Assert.That(result, Does.Contain("Province:Full Province Name"));
                Assert.That(result, Does.Contain("Zip:N1N 1N1"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithProvinceHavingBothNameAndAbbreviation_ShouldUseAbbreviation()
        {
            // Arrange - Test that abbreviation takes precedence over name
            var mockProvince = new MockNationalSubdivision
            {
                Name = "Full Province Name",
                Abbreviation = "FPN"
            };

            _sut.Id = 200;
            _sut.Street1 = "Abbrev Test Street";
            _sut.City = "Abbrev Test City";
            _sut.Province = mockProvince;
            _sut.ZipCode = "A1A 1A1";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:200"));
                Assert.That(result, Does.Contain("Street1:Abbrev Test Street"));
                Assert.That(result, Does.Contain("City:Abbrev Test City"));
                Assert.That(result, Does.Contain("Province:FPN"));
                Assert.That(result, Does.Not.Contain("Full Province Name"));
                Assert.That(result, Does.Contain("Zip:A1A 1A1"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithProvinceHavingEmptyStrings_ShouldUseEmptyString()
        {
            // Arrange - Test edge case where province has empty strings instead of null
            var mockProvince = new MockNationalSubdivision
            {
                Name = "",
                Abbreviation = ""
            };

            _sut.Id = 300;
            _sut.Street1 = "Empty Test Street";
            _sut.City = "Empty Test City";
            _sut.Province = mockProvince;
            _sut.ZipCode = "E1E 1E1";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:300"));
                Assert.That(result, Does.Contain("Street1:Empty Test Street"));
                Assert.That(result, Does.Contain("City:Empty Test City"));
                Assert.That(result, Does.Contain("Province:"));  // Empty abbreviation is used
                Assert.That(result, Does.Contain("Zip:E1E 1E1"));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesLinkedEntityTypeCorrectly()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_WhenSetToNull_ClearsLinkedEntityType()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            _sut.LinkedEntity = mockEntity; // First set to non-null
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity")); // Confirm it's set

            var originalDateModified = _sut.DateModified;

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void CAAddress_ComprehensiveFunctionalityTest()
        {
            // This comprehensive test verifies all major functionality works together correctly

            // Arrange - Test default constructor
            var linkedEntity = new MockDomainEntity();
            var linkedEntityType = linkedEntity.GetType().Name;
            var defaultAddress = new CAAddress();
            Assert.That(defaultAddress, Is.Not.Null);

            // Test JsonConstructor with all parameters
            var testDate = DateTime.Now;
            var province = CAProvinces.Manitoba.ToStateModel();

            var parameterizedAddress = new CAAddress(
                id: 12345,
                street1: "Comprehensive Test Street",
                street2: "Suite 987",
                city: "Winnipeg",
                province: province,
                zipCode: "R3C 0V8",
                country: "Canada",
                type: OrganizerCompanion.Core.Enums.Types.Billing,
                isPrimary: false,
                linkedEntity: linkedEntity,
                dateCreated: testDate,
                dateModified: testDate
            );

            // Test all property setters and getters
            defaultAddress.Id = 54321;
            defaultAddress.Street1 = "Setter Test Street";
            defaultAddress.Street2 = "Unit 123";
            defaultAddress.City = "Test City";
            defaultAddress.Province = CAProvinces.NovaScotia.ToStateModel();
            defaultAddress.ZipCode = "B1B 1B1";
            defaultAddress.Country = "Canada";
            defaultAddress.Type = OrganizerCompanion.Core.Enums.Types.Other;
            defaultAddress.LinkedEntity = new MockDomainEntity();
            defaultAddress.DateModified = testDate;

            // Act & Assert - Verify all properties are set correctly
            Assert.Multiple(() =>
            {
                // Test parameterized constructor results
                Assert.That(parameterizedAddress.Id, Is.EqualTo(12345));
                Assert.That(parameterizedAddress.Street1, Is.EqualTo("Comprehensive Test Street"));
                Assert.That(parameterizedAddress.Street2, Is.EqualTo("Suite 987"));
                Assert.That(parameterizedAddress.City, Is.EqualTo("Winnipeg"));
                Assert.That(parameterizedAddress.Province, Is.EqualTo(province));
                Assert.That(parameterizedAddress.ZipCode, Is.EqualTo("R3C 0V8"));
                Assert.That(parameterizedAddress.Country, Is.EqualTo("Canada"));
                Assert.That(parameterizedAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(parameterizedAddress.DateCreated, Is.EqualTo(testDate));
                Assert.That(parameterizedAddress.DateModified, Is.EqualTo(testDate));

                // Test property setter results
                Assert.That(defaultAddress.Id, Is.EqualTo(54321));
                Assert.That(defaultAddress.Street1, Is.EqualTo("Setter Test Street"));
                Assert.That(defaultAddress.Street2, Is.EqualTo("Unit 123"));
                Assert.That(defaultAddress.City, Is.EqualTo("Test City"));
                Assert.That(defaultAddress.Province?.Name, Is.EqualTo("Nova Scotia"));
                Assert.That(defaultAddress.ZipCode, Is.EqualTo("B1B 1B1"));
                Assert.That(defaultAddress.Country, Is.EqualTo("Canada"));
                Assert.That(defaultAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(defaultAddress.LinkedEntityId, Is.EqualTo(1)); // MockDomainEntity has Id = 1
                Assert.That(defaultAddress.LinkedEntity, Is.Not.Null);
                Assert.That(defaultAddress.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(defaultAddress.DateModified, Is.EqualTo(testDate));
                Assert.That(defaultAddress.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });

            // Test Cast functionality
            var caAddressDto = defaultAddress.Cast<CAAddressDTO>();
            var iCaAddressDto = defaultAddress.Cast<ICAAddressDTO>();

            Assert.Multiple(() =>
            {
                Assert.That(caAddressDto, Is.InstanceOf<CAAddressDTO>());
                Assert.That(iCaAddressDto, Is.InstanceOf<CAAddressDTO>());
                Assert.That(caAddressDto.Id, Is.EqualTo(defaultAddress.Id));
                Assert.That(iCaAddressDto.Id, Is.EqualTo(defaultAddress.Id));
            });

            // Test JSON serialization
            var json = defaultAddress.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);

            // Test ToString functionality
            var stringResult = defaultAddress.ToString();
            Assert.That(stringResult, Is.Not.Null.And.Not.Empty);

            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultAddress.Cast<AnotherMockEntity>());
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithMinimalValidParameters_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act - Test constructor with minimal valid parameters (using empty strings where needed)
            var linkedEntity = new MockDomainEntity();
            var linkedEntityType = linkedEntity.GetType().Name;
            var testDate = DateTime.Now;
            var province = CAProvinces.Ontario.ToStateModel();

            var caAddress = new CAAddress(
                id: 777,
                street1: "",
                street2: "",
                city: "",
                province: province,
                zipCode: "",
                country: "",
                type: OrganizerCompanion.Core.Enums.Types.Other,
                isPrimary: false,
                linkedEntity: linkedEntity,
                dateCreated: testDate,
                dateModified: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(777));
                Assert.That(caAddress.Street1, Is.EqualTo(""));
                Assert.That(caAddress.Street2, Is.EqualTo(""));
                Assert.That(caAddress.City, Is.EqualTo(""));
                Assert.That(caAddress.Province, Is.EqualTo(province));
                Assert.That(caAddress.ZipCode, Is.EqualTo(""));
                Assert.That(caAddress.Country, Is.EqualTo(""));
                Assert.That(caAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(caAddress.DateCreated, Is.EqualTo(testDate));
                Assert.That(caAddress.DateModified, Is.Null);
                Assert.That(caAddress.LinkedEntityId, Is.EqualTo(linkedEntity.Id)); // Use actual linkedEntity.Id (which is 1)
                Assert.That(caAddress.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(caAddress.LinkedEntityType, Is.EqualTo(linkedEntityType));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullStreetAndCity_ShouldHandleNullsCorrectly()
        {
            // Arrange - Test ToString with null street and city values
            _sut.Id = 999;
            _sut.Street1 = null;
            _sut.City = null;
            _sut.Province = CAProvinces.PrinceEdwardIsland.ToStateModel();
            _sut.ZipCode = null;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:999"));
                Assert.That(result, Does.Contain("Street1:"));  // Should show empty after colon
                Assert.That(result, Does.Contain("City:"));     // Should show empty after colon
                Assert.That(result, Does.Contain("Province:PE"));
                Assert.That(result, Does.Contain("Zip:"));      // Should show empty after colon
            });
        }

        [Test, Category("Models")]
        public void DefaultConstructor_Country_ShouldDefaultToCanada()
        {
            // Arrange & Act
            var defaultAddress = new CAAddress();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(defaultAddress.Country, Is.EqualTo(Countries.Canada.GetName()));
                Assert.That(defaultAddress.Country, Is.EqualTo("Canada"));
            });
        }

        [Test, Category("Models")]
        public void Properties_SetToSameValue_ShouldStillUpdateDateModified()
        {
            // Arrange
            _sut.Street1 = "Test Street";
            var firstDateModified = _sut.DateModified;

            // Small delay to ensure different timestamp
            System.Threading.Thread.Sleep(1);

            // Act - Set to the same value
            _sut.Street1 = "Test Street";
            var secondDateModified = _sut.DateModified;

            // Assert - DateModified should still be updated even when setting the same value
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo("Test Street"));
                Assert.That(secondDateModified, Is.GreaterThan(firstDateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToCAAddressDTO_PreservesAllFieldsIncludingDates()
        {
            // Arrange - Set up a complete CAAddress with all fields populated
            var linkedEntity = new MockDomainEntity();
            var specificDateCreated = new DateTime(2023, 6, 15, 9, 30, 0);
            var specificDateModified = new DateTime(2023, 6, 16, 14, 45, 30);
            var province = CAProvinces.Yukon.ToStateModel();

            var completeAddress = new CAAddress(
                id: 555,
                street1: "555 Complete Street",
                street2: "Unit 555",
                city: "Whitehorse",
                province: province,
                zipCode: "Y1A 1A1",
                country: "Canada",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: false, 
                linkedEntity: linkedEntity,
                dateCreated: specificDateCreated,
                dateModified: specificDateModified
            );

            // Act
            var dto = completeAddress.Cast<CAAddressDTO>();

            // Assert - Verify ALL fields are preserved correctly
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(555));
                Assert.That(dto.Street1, Is.EqualTo("555 Complete Street"));
                Assert.That(dto.Street2, Is.EqualTo("Unit 555"));
                Assert.That(dto.City, Is.EqualTo("Whitehorse"));
                Assert.That(dto.Province, Is.EqualTo(province));
                Assert.That(dto.Province?.Name, Is.EqualTo("Yukon"));
                Assert.That(dto.Province?.Abbreviation, Is.EqualTo("YT"));
                Assert.That(dto.ZipCode, Is.EqualTo("Y1A 1A1"));
                Assert.That(dto.Country, Is.EqualTo("Canada"));
                Assert.That(dto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(dto.DateCreated, Is.EqualTo(specificDateCreated));
                Assert.That(dto.DateModified, Is.EqualTo(specificDateModified));
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_WithValidDTO_SetsAllPropertiesCorrectly()
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
                DateCreated = DateTime.Now.AddDays(-5),
                DateModified = DateTime.Now.AddDays(-2)
            };
            var linkedEntity = new MockDomainEntity { Id = 555 };

            // Act
            var caAddress = new CAAddress(dto, linkedEntity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(123), "Id should match DTO");
                Assert.That(caAddress.Street1, Is.EqualTo("456 Elm Street"), "Street1 should match DTO");
                Assert.That(caAddress.Street2, Is.EqualTo("Suite 789"), "Street2 should match DTO");
                Assert.That(caAddress.City, Is.EqualTo("Montreal"), "City should match DTO");
                Assert.That(caAddress.Province?.Name, Is.EqualTo(CAProvinces.Quebec.ToStateModel().Name), "Province name should match DTO");
                Assert.That(caAddress.Province?.Abbreviation, Is.EqualTo(CAProvinces.Quebec.ToStateModel().Abbreviation), "Province abbreviation should match DTO");
                Assert.That(caAddress.ZipCode, Is.EqualTo("H3B 2Y7"), "ZipCode should match DTO");
                Assert.That(caAddress.Country, Is.EqualTo("Canada"), "Country should match DTO");
                Assert.That(caAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work), "Type should match DTO");
                Assert.That(caAddress.IsPrimary, Is.True, "IsPrimary should match DTO");
                Assert.That(caAddress.LinkedEntity, Is.EqualTo(linkedEntity), "LinkedEntity should be set correctly");
                Assert.That(caAddress.LinkedEntityId, Is.EqualTo(555), "LinkedEntityId should match linked entity");
                Assert.That(caAddress.LinkedEntityType, Is.EqualTo("MockDomainEntity"), "LinkedEntityType should be correct");
                Assert.That(caAddress.DateCreated, Is.EqualTo(dto.DateCreated), "DateCreated should match DTO");
                Assert.That(caAddress.DateModified, Is.EqualTo(dto.DateModified), "DateModified should match DTO");
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_WithNullLinkedEntity_SetsPropertiesCorrectly()
        {
            // Arrange
            var dto = new CAAddressDTO
            {
                Id = 789,
                Street1 = "789 Pine Avenue",
                Street2 = "Apartment 101",
                City = "Winnipeg",
                Province = CAProvinces.Manitoba.ToStateModel(),
                ZipCode = "R3C 4A5",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Home,
                IsPrimary = false,
                DateCreated = DateTime.Now.AddDays(-3),
                DateModified = DateTime.Now.AddHours(-6)
            };

            // Act
            var caAddress = new CAAddress(dto, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(789), "Id should match DTO");
                Assert.That(caAddress.Street1, Is.EqualTo("789 Pine Avenue"), "Street1 should match DTO");
                Assert.That(caAddress.Street2, Is.EqualTo("Apartment 101"), "Street2 should match DTO");
                Assert.That(caAddress.City, Is.EqualTo("Winnipeg"), "City should match DTO");
                Assert.That(caAddress.Province?.Name, Is.EqualTo(CAProvinces.Manitoba.ToStateModel().Name), "Province name should match DTO");
                Assert.That(caAddress.Province?.Abbreviation, Is.EqualTo(CAProvinces.Manitoba.ToStateModel().Abbreviation), "Province abbreviation should match DTO");
                Assert.That(caAddress.ZipCode, Is.EqualTo("R3C 4A5"), "ZipCode should match DTO");
                Assert.That(caAddress.Country, Is.EqualTo("Canada"), "Country should match DTO");
                Assert.That(caAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home), "Type should match DTO");
                Assert.That(caAddress.IsPrimary, Is.False, "IsPrimary should match DTO");
                Assert.That(caAddress.LinkedEntity, Is.Null, "LinkedEntity should be null");
                Assert.That(caAddress.LinkedEntityId, Is.Null, "LinkedEntityId should be null when LinkedEntity is null");
                Assert.That(caAddress.LinkedEntityType, Is.Null, "LinkedEntityType should be null when LinkedEntity is null");
                Assert.That(caAddress.DateCreated, Is.EqualTo(dto.DateCreated), "DateCreated should match DTO");
                Assert.That(caAddress.DateModified, Is.EqualTo(dto.DateModified), "DateModified should match DTO");
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_WithDefaultLinkedEntity_SetsLinkedEntityToNull()
        {
            // Arrange
            var dto = new CAAddressDTO
            {
                Id = 456,
                Street1 = "123 Default Street",
                City = "Calgary",
                Province = CAProvinces.Alberta.ToStateModel(),
                ZipCode = "T2P 1M7",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Billing,
                IsPrimary = true
            };

            // Act - Using default parameter for linkedEntity (should be null)
            var caAddress = new CAAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(456), "Id should match DTO");
                Assert.That(caAddress.Street1, Is.EqualTo("123 Default Street"), "Street1 should match DTO");
                Assert.That(caAddress.City, Is.EqualTo("Calgary"), "City should match DTO");
                Assert.That(caAddress.LinkedEntity, Is.Null, "LinkedEntity should be null when using default parameter");
                Assert.That(caAddress.LinkedEntityId, Is.Null, "LinkedEntityId should be null");
                Assert.That(caAddress.LinkedEntityType, Is.Null, "LinkedEntityType should be null");
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_WithNullPropertyValues_HandlesNullsCorrectly()
        {
            // Arrange
            var dto = new CAAddressDTO
            {
                Id = 111,
                Street1 = null,
                Street2 = null,
                City = null,
                Province = null,
                ZipCode = null,
                Country = null,
                Type = null,
                IsPrimary = false,
                DateCreated = DateTime.Now.AddDays(-1),
                DateModified = null
            };
            var linkedEntity = new MockDomainEntity { Id = 999 };

            // Act
            var caAddress = new CAAddress(dto, linkedEntity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(111), "Id should match DTO");
                Assert.That(caAddress.Street1, Is.Null, "Street1 should be null");
                Assert.That(caAddress.Street2, Is.Null, "Street2 should be null");
                Assert.That(caAddress.City, Is.Null, "City should be null");
                Assert.That(caAddress.Province, Is.Null, "Province should be null");
                Assert.That(caAddress.ZipCode, Is.Null, "ZipCode should be null");
                Assert.That(caAddress.Country, Is.Null, "Country should be null");
                Assert.That(caAddress.Type, Is.Null, "Type should be null");
                Assert.That(caAddress.IsPrimary, Is.False, "IsPrimary should match DTO");
                Assert.That(caAddress.LinkedEntity, Is.EqualTo(linkedEntity), "LinkedEntity should be set");
                Assert.That(caAddress.DateCreated, Is.EqualTo(dto.DateCreated), "DateCreated should match DTO");
                Assert.That(caAddress.DateModified, Is.Null, "DateModified should be null when DTO DateModified is null");
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_WithZeroId_SetsIdCorrectly()
        {
            // Arrange
            var dto = new CAAddressDTO
            {
                Id = 0,
                Street1 = "Zero ID Street",
                City = "Halifax",
                Province = CAProvinces.NovaScotia.ToStateModel(),
                ZipCode = "B3H 3C3",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Shipping,
                IsPrimary = false
            };

            // Act
            var caAddress = new CAAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.Id, Is.EqualTo(0), "Zero Id should be accepted");
                Assert.That(caAddress.Street1, Is.EqualTo("Zero ID Street"), "Street1 should match DTO");
                Assert.That(caAddress.City, Is.EqualTo("Halifax"), "City should match DTO");
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_WithAllProvinces_HandlesEachProvinceCorrectly()
        {
            // Arrange & Act & Assert - Test with various Canadian provinces
            var provinces = new[]
            {
                CAProvinces.Alberta.ToStateModel(),
                CAProvinces.BritishColumbia.ToStateModel(),
                CAProvinces.Manitoba.ToStateModel(),
                CAProvinces.NewBrunswick.ToStateModel(),
                CAProvinces.NewfoundlandAndLabrador.ToStateModel(),
                CAProvinces.NorthwestTerritories.ToStateModel(),
                CAProvinces.NovaScotia.ToStateModel(),
                CAProvinces.Nunavut.ToStateModel(),
                CAProvinces.Ontario.ToStateModel(),
                CAProvinces.PrinceEdwardIsland.ToStateModel(),
                CAProvinces.Quebec.ToStateModel(),
                CAProvinces.Saskatchewan.ToStateModel(),
                CAProvinces.Yukon.ToStateModel()
            };

            for (int i = 0; i < provinces.Length; i++)
            {
                var dto = new CAAddressDTO
                {
                    Id = i + 1,
                    Street1 = $"Test Street {i + 1}",
                    City = $"Test City {i + 1}",
                    Province = provinces[i],
                    ZipCode = "T1T 1T1",
                    Country = "Canada",
                    Type = OrganizerCompanion.Core.Enums.Types.Home,
                    IsPrimary = i == 0
                };

                var caAddress = new CAAddress(dto);

                Assert.That(caAddress.Province, Is.EqualTo(provinces[i]), 
                    $"Province should be set correctly for {provinces[i]?.Name ?? "null"}");
            }
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_PreservesDatePrecision()
        {
            // Arrange
            var specificCreationDate = new DateTime(2023, 5, 15, 14, 30, 45, 123);
            var specificModificationDate = new DateTime(2023, 6, 20, 9, 15, 30, 456);

            var dto = new CAAddressDTO
            {
                Id = 777,
                Street1 = "Precision Test Street",
                City = "Victoria",
                Province = CAProvinces.BritishColumbia.ToStateModel(),
                ZipCode = "V8W 1P6",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Other,
                IsPrimary = true,
                DateCreated = specificCreationDate,
                DateModified = specificModificationDate
            };

            // Act
            var caAddress = new CAAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress.DateCreated, Is.EqualTo(specificCreationDate), 
                    "DateCreated should preserve exact precision from DTO");
                Assert.That(caAddress.DateModified, Is.EqualTo(specificModificationDate), 
                    "DateModified should preserve exact precision from DTO");
                Assert.That(caAddress.DateCreated.Millisecond, Is.EqualTo(123), 
                    "DateCreated milliseconds should be preserved");
                Assert.That(caAddress.DateModified?.Millisecond, Is.EqualTo(456), 
                    "DateModified milliseconds should be preserved");
            });
        }

        [Test, Category("Models")]
        public void CAAddressDTOConstructor_ImplementsIDomainEntityInterface()
        {
            // Arrange
            var dto = new CAAddressDTO
            {
                Id = 888,
                Street1 = "Interface Test Street",
                City = "Edmonton",
                Province = CAProvinces.Alberta.ToStateModel(),
                ZipCode = "T5J 3S4",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                IsPrimary = false
            };

            // Act
            var caAddress = new CAAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddress, Is.InstanceOf<IDomainEntity>(), 
                    "CAAddress should implement IDomainEntity interface");
                Assert.That(caAddress, Is.InstanceOf<Interfaces.Domain.ICAAddress>(), 
                    "CAAddress should implement ICAAddress interface");
                Assert.That(caAddress.ToJson(), Is.Not.Null.And.Not.Empty, 
                    "ToJson method should work correctly");
                Assert.DoesNotThrow(() => caAddress.Cast<CAAddressDTO>(), 
                    "Cast method should work correctly");
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