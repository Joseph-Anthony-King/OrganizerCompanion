using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class ContactDTOShould
    {
        private ContactDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ContactDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateContactDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new ContactDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.FirstName, Is.Null);
                Assert.That(_sut.MiddleName, Is.Null);
                Assert.That(_sut.LastName, Is.Null);
                Assert.That(_sut.FullName, Is.Null);
                Assert.That(_sut.Pronouns, Is.Null);
                Assert.That(_sut.BirthDate, Is.Null);
                Assert.That(_sut.DeceasedDate, Is.Null);
                Assert.That(_sut.JoinedDate, Is.Null);
                Assert.That(_sut.Emails, Is.Not.Null);
                Assert.That(_sut.Emails, Is.Empty);
                Assert.That(_sut.PhoneNumbers, Is.Not.Null);
                Assert.That(_sut.PhoneNumbers, Is.Empty);
                Assert.That(_sut.Addresses, Is.Not.Null);
                Assert.That(_sut.Addresses, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedId = 123;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void FirstName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedFirstName = "John";

            // Act
            _sut.FirstName = expectedFirstName;

            // Assert
            Assert.That(_sut.FirstName, Is.EqualTo(expectedFirstName));
        }

        [Test, Category("DataTransferObjects")]
        public void FirstName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.FirstName = null;

            // Assert
            Assert.That(_sut.FirstName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void MiddleName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedMiddleName = "Michael";

            // Act
            _sut.MiddleName = expectedMiddleName;

            // Assert
            Assert.That(_sut.MiddleName, Is.EqualTo(expectedMiddleName));
        }

        [Test, Category("DataTransferObjects")]
        public void MiddleName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.MiddleName = null;

            // Assert
            Assert.That(_sut.MiddleName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LastName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedLastName = "Doe";

            // Act
            _sut.LastName = expectedLastName;

            // Assert
            Assert.That(_sut.LastName, Is.EqualTo(expectedLastName));
        }

        [Test, Category("DataTransferObjects")]
        public void LastName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.LastName = null;

            // Assert
            Assert.That(_sut.LastName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FullName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedFullName = "John Michael Doe";

            // Act
            _sut.FullName = expectedFullName;

            // Assert
            Assert.That(_sut.FullName, Is.EqualTo(expectedFullName));
        }

        [Test, Category("DataTransferObjects")]
        public void FullName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.FullName = null;

            // Assert
            Assert.That(_sut.FullName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldGetAndSetValue()
        {
            // Arrange
            OrganizerCompanion.Core.Enums.Pronouns expectedPronouns = OrganizerCompanion.Core.Enums.Pronouns.TheyThem;

            // Act
            _sut.Pronouns = expectedPronouns;

            // Assert
            Assert.That(_sut.Pronouns, Is.EqualTo(expectedPronouns));
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Pronouns = null;

            // Assert
            Assert.That(_sut.Pronouns, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldAcceptAllEnumValues()
        {
            // Arrange
            var enumValues = new[] { 
                OrganizerCompanion.Core.Enums.Pronouns.HeHim,
                OrganizerCompanion.Core.Enums.Pronouns.SheHer,
                OrganizerCompanion.Core.Enums.Pronouns.TheyThem,
                OrganizerCompanion.Core.Enums.Pronouns.XeXir,
                OrganizerCompanion.Core.Enums.Pronouns.ZeZir,
                OrganizerCompanion.Core.Enums.Pronouns.EyEm,
                OrganizerCompanion.Core.Enums.Pronouns.FaeFaer,
                OrganizerCompanion.Core.Enums.Pronouns.VeVer,
                OrganizerCompanion.Core.Enums.Pronouns.PerPer,
                OrganizerCompanion.Core.Enums.Pronouns.PreferNotToSay,
                OrganizerCompanion.Core.Enums.Pronouns.Other
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var enumValue in enumValues)
                {
                    _sut.Pronouns = enumValue;
                    Assert.That(_sut.Pronouns, Is.EqualTo(enumValue));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void BirthDate_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedBirthDate = new(1990, 5, 15);

            // Act
            _sut.BirthDate = expectedBirthDate;

            // Assert
            Assert.That(_sut.BirthDate, Is.EqualTo(expectedBirthDate));
        }

        [Test, Category("DataTransferObjects")]
        public void BirthDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.BirthDate = null;

            // Assert
            Assert.That(_sut.BirthDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDeceasedDate = new(2023, 12, 25);

            // Act
            _sut.DeceasedDate = expectedDeceasedDate;

            // Assert
            Assert.That(_sut.DeceasedDate, Is.EqualTo(expectedDeceasedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DeceasedDate = null;

            // Assert
            Assert.That(_sut.DeceasedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void JoinedDate_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedJoinedDate = new(2020, 1, 1);

            // Act
            _sut.JoinedDate = expectedJoinedDate;

            // Assert
            Assert.That(_sut.JoinedDate, Is.EqualTo(expectedJoinedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void JoinedDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.JoinedDate = null;

            // Assert
            Assert.That(_sut.JoinedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Emails_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedEmails = new List<EmailDTO>
            {
                new() { Id = 1, EmailAddress = "john@example.com" },
                new() { Id = 2, EmailAddress = "john.doe@work.com" }
            };

            // Act
            _sut.Emails = expectedEmails;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Is.EqualTo(expectedEmails));
                Assert.That(_sut.Emails.Count, Is.EqualTo(2));
                Assert.That(_sut.Emails[0].EmailAddress, Is.EqualTo("john@example.com"));
                Assert.That(_sut.Emails[1].EmailAddress, Is.EqualTo("john.doe@work.com"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Emails_ShouldAcceptEmptyList()
        {
            // Arrange
            var emptyEmails = new List<EmailDTO>();

            // Act
            _sut.Emails = emptyEmails;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Is.Not.Null);
                Assert.That(_sut.Emails, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumbers_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedPhoneNumbers = new List<PhoneNumberDTO>
            {
                new() { Id = 1, Phone = "555-1234" },
                new() { Id = 2, Phone = "555-5678" }
            };

            // Act
            _sut.PhoneNumbers = expectedPhoneNumbers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Is.EqualTo(expectedPhoneNumbers));
                Assert.That(_sut.PhoneNumbers.Count, Is.EqualTo(2));
                Assert.That(_sut.PhoneNumbers[0].Phone, Is.EqualTo("555-1234"));
                Assert.That(_sut.PhoneNumbers[1].Phone, Is.EqualTo("555-5678"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumbers_ShouldAcceptEmptyList()
        {
            // Arrange
            var emptyPhoneNumbers = new List<PhoneNumberDTO>();

            // Act
            _sut.PhoneNumbers = emptyPhoneNumbers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Is.Not.Null);
                Assert.That(_sut.PhoneNumbers, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Addresses_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedAddresses = new List<IAddressDTO>
            {
                new MockAddressDTO { LinkedEntityId = 10 },
                new MockAddressDTO { LinkedEntityId = 20 }
            };

            // Act
            _sut.Addresses = expectedAddresses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Is.EqualTo(expectedAddresses));
                Assert.That(_sut.Addresses.Count, Is.EqualTo(2));
                Assert.That(_sut.Addresses[0].LinkedEntityId, Is.EqualTo(10));
                Assert.That(_sut.Addresses[1].LinkedEntityId, Is.EqualTo(20));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Addresses_ShouldAcceptEmptyList()
        {
            // Arrange
            var emptyAddresses = new List<IAddressDTO>();

            // Act
            _sut.Addresses = emptyAddresses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Is.Not.Null);
                Assert.That(_sut.Addresses, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceEmails_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = personInterface.Emails; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceEmails_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var emails = new List<OrganizerCompanion.Core.Interfaces.Type.IEmail>();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { personInterface.Emails = emails; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfacePhoneNumbers_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = personInterface.PhoneNumbers; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfacePhoneNumbers_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var phoneNumbers = new List<OrganizerCompanion.Core.Interfaces.Type.IPhoneNumber>();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { personInterface.PhoneNumbers = phoneNumbers; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAddresses_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = personInterface.Addresses; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAddresses_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var addresses = new List<OrganizerCompanion.Core.Interfaces.Type.IAddress>();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { personInterface.Addresses = addresses; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.IsCast = true; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastId = 123; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastType = "TestType"; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.DateCreated; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.DateModified; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.DateModified = DateTime.Now; });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.ToJson(); });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FirstName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.FirstName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void MiddleName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.MiddleName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LastName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.LastName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FullName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.FullName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.Pronouns));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void BirthDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.BirthDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void JoinedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.JoinedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Emails_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.Emails));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumbers_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.PhoneNumbers));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Addresses_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.Addresses));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldNotHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.DeceasedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_ShouldImplementIContactDTO()
        {
            // Arrange & Act
            var contactDTO = new ContactDTO();

            // Assert
            Assert.That(contactDTO, Is.InstanceOf<IContactDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var contactDTO = new ContactDTO();

            // Assert
            Assert.That(contactDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_ShouldImplementIPerson()
        {
            // Arrange & Act
            var contactDTO = new ContactDTO();

            // Assert
            Assert.That(contactDTO, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Type.IPerson>());
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange & Act
            var contactDTO = new ContactDTO
            {
                Id = 999,
                FirstName = "Jane",
                MiddleName = "Marie",
                LastName = "Smith",
                FullName = "Jane Marie Smith",
                Pronouns = OrganizerCompanion.Core.Enums.Pronouns.SheHer,
                BirthDate = new DateTime(1985, 6, 15),
                DeceasedDate = null,
                JoinedDate = new DateTime(2020, 1, 1),
                Emails = new List<EmailDTO> { new() { Id = 1, EmailAddress = "jane@example.com" } },
                PhoneNumbers = new List<PhoneNumberDTO> { new() { Id = 1, Phone = "555-1234" } },
                Addresses = new List<IAddressDTO> { new MockAddressDTO { LinkedEntityId = 1 } }
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contactDTO.Id, Is.EqualTo(999));
                Assert.That(contactDTO.FirstName, Is.EqualTo("Jane"));
                Assert.That(contactDTO.MiddleName, Is.EqualTo("Marie"));
                Assert.That(contactDTO.LastName, Is.EqualTo("Smith"));
                Assert.That(contactDTO.FullName, Is.EqualTo("Jane Marie Smith"));
                Assert.That(contactDTO.Pronouns, Is.EqualTo(OrganizerCompanion.Core.Enums.Pronouns.SheHer));
                Assert.That(contactDTO.BirthDate, Is.EqualTo(new DateTime(1985, 6, 15)));
                Assert.That(contactDTO.DeceasedDate, Is.Null);
                Assert.That(contactDTO.JoinedDate, Is.EqualTo(new DateTime(2020, 1, 1)));
                Assert.That(contactDTO.Emails.Count, Is.EqualTo(1));
                Assert.That(contactDTO.PhoneNumbers.Count, Is.EqualTo(1));
                Assert.That(contactDTO.Addresses.Count, Is.EqualTo(1));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { nameof(ContactDTO.Id), "id" },
                { nameof(ContactDTO.FirstName), "firstName" },
                { nameof(ContactDTO.MiddleName), "middleName" },
                { nameof(ContactDTO.LastName), "lastName" },
                { nameof(ContactDTO.FullName), "fullName" },
                { nameof(ContactDTO.Pronouns), "userName" }, // Note: JSON name is "userName" not "pronouns"
                { nameof(ContactDTO.BirthDate), "birthDate" },
                { nameof(ContactDTO.DeceasedDate), "deceasedDate" },
                { nameof(ContactDTO.JoinedDate), "joinedDate" },
                { nameof(ContactDTO.Emails), "emails" },
                { nameof(ContactDTO.PhoneNumbers), "phoneNumbers" },
                { nameof(ContactDTO.Addresses), "addresses" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var kvp in properties)
                {
                    var property = typeof(ContactDTO).GetProperty(kvp.Key);
                    var jsonAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                    Assert.That(jsonAttribute, Is.Not.Null, $"Property {kvp.Key} should have JsonPropertyName attribute");
                    Assert.That(jsonAttribute?.Name, Is.EqualTo(kvp.Value), $"Property {kvp.Key} should have JsonPropertyName '{kvp.Value}'");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldHaveJsonIgnoreWhenWritingNullAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.DeceasedDate));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttribute, Is.Not.Null);
                Assert.That(jsonIgnoreAttribute?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Interface_Properties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var properties = new[]
            {
                nameof(ContactDTO.IsCast),
                nameof(ContactDTO.CastId),
                nameof(ContactDTO.CastType),
                nameof(ContactDTO.DateCreated),
                nameof(ContactDTO.DateModified)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var propertyName in properties)
                {
                    var property = typeof(ContactDTO).GetProperty(propertyName);
                    var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                    Assert.That(jsonIgnoreAttribute, Is.Not.Null, $"Property {propertyName} should have JsonIgnore attribute");
                }
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; }
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        private class MockAddressDTO : IAddressDTO
        {
            public int LinkedEntityId { get; set; }
            public IDomainEntity? LinkedEntity { get; set; }
            public string? LinkedEntityType { get; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
        }
        #endregion
    }
}
