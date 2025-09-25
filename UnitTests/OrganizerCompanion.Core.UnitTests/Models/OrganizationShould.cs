using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class OrganizationShould
    {
        private Organization _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Organization();
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateOrganizationWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Organization();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.OrganizationName, Is.Null);
                Assert.That(_sut.Addresses, Is.Not.Null);
                Assert.That(_sut.Addresses, Is.Empty);
                Assert.That(_sut.Members, Is.Not.Null);
                Assert.That(_sut.Members, Is.Empty);
                Assert.That(_sut.PhoneNumbers, Is.Not.Null);
                Assert.That(_sut.PhoneNumbers, Is.Empty);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void OrganizationName_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newName = "ACME Corporation";
            var beforeSet = DateTime.Now;

            // Act
            _sut.OrganizationName = newName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.OrganizationName, Is.EqualTo(newName));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void OrganizationName_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.OrganizationName = "Test Org";

            // Act
            _sut.OrganizationName = null;

            // Assert
            Assert.That(_sut.OrganizationName, Is.Null);
        }

        [Test, Category("Models")]
        public void Addresses_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var addresses = new List<IAddress?> { null }; // Using null as we don't have concrete implementation
            var beforeSet = DateTime.Now;

            // Act
            _sut.Addresses = addresses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Is.EqualTo(addresses));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Addresses_WhenSetToNull_ShouldInitializeEmptyList()
        {
            // Arrange & Act
            _sut.Addresses = null!; // Using null! to bypass nullable warning

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Is.Not.Null);
                Assert.That(_sut.Addresses, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void Members_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var members = new List<IPerson?> { null }; // Using null as we don't have concrete implementation
            var beforeSet = DateTime.Now;

            // Act
            _sut.Members = members;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members, Is.EqualTo(members));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Members_WhenSetToNull_ShouldInitializeEmptyList()
        {
            // Arrange & Act
            _sut.Members = null!; // Using null! to bypass nullable warning

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Members, Is.Not.Null);
                Assert.That(_sut.Members, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void PhoneNumbers_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var phoneNumbers = new List<IPhoneNumber?> { null }; // Using null as we don't have concrete implementation
            var beforeSet = DateTime.Now;

            // Act
            _sut.PhoneNumbers = phoneNumbers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void PhoneNumbers_WhenSetToNull_ShouldInitializeEmptyList()
        {
            // Arrange & Act
            _sut.PhoneNumbers = null!; // Using null! to bypass nullable warning

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Is.Not.Null);
                Assert.That(_sut.PhoneNumbers, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var organization = new Organization();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(organization.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            _sut.DateModified = dateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
        }

        [Test, Category("Models")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<Organization>());
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.OrganizationName = "Test Org";
            _sut.DateModified = DateTime.Now;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"organizationName\":\"Test Org\""));
                Assert.That(json, Does.Contain("\"addresses\":[]"));
                Assert.That(json, Does.Contain("\"members\":[]"));
                Assert.That(json, Does.Contain("\"phoneNumbers\":[]"));
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Contain("\"dateModified\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldReturnValidJsonString()
        {
            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":0"));
                Assert.That(json, Does.Contain("\"organizationName\":null"));
                Assert.That(json, Does.Contain("\"addresses\":[]"));
                Assert.That(json, Does.Contain("\"members\":[]"));
                Assert.That(json, Does.Contain("\"phoneNumbers\":[]"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.OrganizationName = "ACME Corporation";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("123"));
                Assert.That(result, Does.Contain("ACME Corporation"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullOrganizationName_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 456;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("456"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithDefaultValues_ShouldReturnFormattedString()
        {
            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("0"));
            });
        }

        [Test, Category("Models")]
        public void Organization_WithEmptyOrganizationName_ShouldBeAllowed()
        {
            // Act
            _sut.OrganizationName = string.Empty;

            // Assert
            Assert.That(_sut.OrganizationName, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Organization_WithVeryLongOrganizationName_ShouldBeAllowed()
        {
            // Arrange
            var longName = new string('A', 1000);

            // Act
            _sut.OrganizationName = longName;

            // Assert
            Assert.That(_sut.OrganizationName, Is.EqualTo(longName));
        }

        [Test, Category("Models")]
        public void Organization_MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.OrganizationName = "Test Org";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.Addresses = [];
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.Members = [];
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.PhoneNumbers = [];
            var fifthModified = _sut.DateModified;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));
        }

        [Test, Category("Models")]
        public void Organization_WithZeroId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Organization_WithMaxIntId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Organization_SerializerOptions_ShouldHandleCyclicalReferences()
        {
            // Arrange
            _sut.Id = 1;
            _sut.OrganizationName = "Test Org";

            // Act & Assert - This should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = _sut.ToJson();
                Assert.That(json, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test, Category("Models")]
        public void Organization_ListProperties_ShouldBeIndependentInstances()
        {
            // Arrange
            var org1 = new Organization();
            var org2 = new Organization();

            // Act
            org1.Addresses.Add(null);
            org1.Members.Add(null);
            org1.PhoneNumbers.Add(null);

            // Assert - Ensure lists are independent instances
            Assert.Multiple(() =>
            {
                Assert.That(org2.Addresses, Is.Empty);
                Assert.That(org2.Members, Is.Empty);
                Assert.That(org2.PhoneNumbers, Is.Empty);

                Assert.That(org1.Addresses.Count, Is.EqualTo(1));
                Assert.That(org1.Members.Count, Is.EqualTo(1));
                Assert.That(org1.PhoneNumbers.Count, Is.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void Organization_JsonSerialization_ShouldProduceValidFormat()
        {
            // Arrange
            _sut.Id = 42;
            _sut.OrganizationName = "Sample Organization";
            _sut.DateModified = DateTime.Now;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);

                // Check that all expected properties are present
                var expectedProperties = new[] {
                    "id", "organizationName", "addresses", "members",
                    "phoneNumbers", "dateCreated", "dateModified"
                };

                foreach (var property in expectedProperties)
                {
                    Assert.That(json, Does.Contain($"\"{property}\":"), $"Property '{property}' not found in JSON");
                }

                // Verify JSON is well-formed
                var jsonDoc = JsonDocument.Parse(json);
                Assert.That(jsonDoc, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithValidParameters_ShouldCreateOrganizationCorrectly()
        {
            // Arrange
            var id = 123;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?> { null };
            var members = new List<IPerson?> { null };
            var phoneNumbers = new List<IPhoneNumber?> { null };
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.EqualTo(organizationName));
                Assert.That(organization.Addresses, Is.EqualTo(addresses));
                Assert.That(organization.Members, Is.EqualTo(members));
                Assert.That(organization.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullCollections_ShouldInitializeEmptyCollections()
        {
            // Arrange
            var id = 1;
            var organizationName = "Test Org";
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, organizationName, null!, null!, null!, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.EqualTo(organizationName));
                Assert.That(organization.Addresses, Is.Not.Null);
                Assert.That(organization.Addresses, Is.Empty);
                Assert.That(organization.Members, Is.Not.Null);
                Assert.That(organization.Members, Is.Empty);
                Assert.That(organization.PhoneNumbers, Is.Not.Null);
                Assert.That(organization.PhoneNumbers, Is.Empty);
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullOrganizationName_ShouldAcceptNull()
        {
            // Arrange
            var id = 1;
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, null, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.Null);
                Assert.That(organization.Addresses, Is.EqualTo(addresses));
                Assert.That(organization.Members, Is.EqualTo(members));
                Assert.That(organization.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNegativeId_ShouldThrowArgumentException()
        {
            // Arrange
            var negativeId = -1;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => 
                new Organization(negativeId, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified));
            
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Does.Contain("Error creating Organization object."));
                Assert.That(ex.InnerException, Is.InstanceOf<ArgumentOutOfRangeException>());
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithZeroId_ShouldAcceptZero()
        {
            // Arrange
            var id = 0;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.EqualTo(organizationName));
                Assert.That(organization.Addresses, Is.EqualTo(addresses));
                Assert.That(organization.Members, Is.EqualTo(members));
                Assert.That(organization.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithMaxIntId_ShouldAcceptMaxValue()
        {
            // Arrange
            var id = int.MaxValue;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.EqualTo(organizationName));
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullDateModified_ShouldAcceptNull()
        {
            // Arrange
            var id = 1;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            DateTime? dateModified = null;

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.EqualTo(organizationName));
                Assert.That(organization.Addresses, Is.EqualTo(addresses));
                Assert.That(organization.Members, Is.EqualTo(members));
                Assert.That(organization.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithEmptyCollections_ShouldPreserveEmptyCollections()
        {
            // Arrange
            var id = 1;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Addresses, Is.SameAs(addresses));
                Assert.That(organization.Members, Is.SameAs(members));
                Assert.That(organization.PhoneNumbers, Is.SameAs(phoneNumbers));
                Assert.That(organization.Addresses, Is.Empty);
                Assert.That(organization.Members, Is.Empty);
                Assert.That(organization.PhoneNumbers, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithPopulatedCollections_ShouldPreserveCollectionContents()
        {
            // Arrange
            var id = 1;
            var organizationName = "Test Organization";
            var addresses = new List<IAddress?> { null, null };
            var members = new List<IPerson?> { null, null, null };
            var phoneNumbers = new List<IPhoneNumber?> { null };
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Addresses, Is.SameAs(addresses));
                Assert.That(organization.Members, Is.SameAs(members));
                Assert.That(organization.PhoneNumbers, Is.SameAs(phoneNumbers));
                Assert.That(organization.Addresses, Has.Count.EqualTo(2));
                Assert.That(organization.Members.Count, Is.EqualTo(3));
                Assert.That(organization.PhoneNumbers, Has.Count.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithEmptyStringOrganizationName_ShouldAcceptEmptyString()
        {
            // Arrange
            var id = 1;
            var organizationName = string.Empty;
            var addresses = new List<IAddress?>();
            var members = new List<IPerson?>();
            var phoneNumbers = new List<IPhoneNumber?>();
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var organization = new Organization(id, organizationName, addresses, members, phoneNumbers, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization.Id, Is.EqualTo(id));
                Assert.That(organization.OrganizationName, Is.EqualTo(string.Empty));
                Assert.That(organization.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(dateModified));
            });
        }
    }
}
