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
                Assert.That(_sut.UserName, Is.Null);
                Assert.That(_sut.IsActive, Is.Null);
                Assert.That(_sut.IsDeceased, Is.Null);
                Assert.That(_sut.IsAdmin, Is.Null);
                Assert.That(_sut.IsSuperUser, Is.Null);
                Assert.That(_sut.JoinedDate, Is.Null);
                Assert.That(_sut.Emails, Is.Not.Null);
                Assert.That(_sut.Emails, Is.Empty);
                Assert.That(_sut.PhoneNumbers, Is.Not.Null);
                Assert.That(_sut.PhoneNumbers, Is.Empty);
                Assert.That(_sut.Addresses, Is.Not.Null);
                Assert.That(_sut.Addresses, Is.Empty);
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
                Assert.That(_sut.DateModified, Is.Null);
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
        public void UserName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedUserName = "john.doe";

            // Act
            _sut.UserName = expectedUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(expectedUserName));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.UserName = null;

            // Assert
            Assert.That(_sut.UserName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsActive_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedIsActive = true;

            // Act
            _sut.IsActive = expectedIsActive;

            // Assert
            Assert.That(_sut.IsActive, Is.EqualTo(expectedIsActive));
        }

        [Test, Category("DataTransferObjects")]
        public void IsActive_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.IsActive = null;

            // Assert
            Assert.That(_sut.IsActive, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsDeceased_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedIsDeceased = true;

            // Act
            _sut.IsDeceased = expectedIsDeceased;

            // Assert
            Assert.That(_sut.IsDeceased, Is.EqualTo(expectedIsDeceased));
        }

        [Test, Category("DataTransferObjects")]
        public void IsDeceased_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.IsDeceased = null;

            // Assert
            Assert.That(_sut.IsDeceased, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsAdmin_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedIsAdmin = true;

            // Act
            _sut.IsAdmin = expectedIsAdmin;

            // Assert
            Assert.That(_sut.IsAdmin, Is.EqualTo(expectedIsAdmin));
        }

        [Test, Category("DataTransferObjects")]
        public void IsAdmin_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.IsAdmin = null;

            // Assert
            Assert.That(_sut.IsAdmin, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsSuperUser_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedIsSuperUser = true;

            // Act
            _sut.IsSuperUser = expectedIsSuperUser;

            // Assert
            Assert.That(_sut.IsSuperUser, Is.EqualTo(expectedIsSuperUser));
        }

        [Test, Category("DataTransferObjects")]
        public void IsSuperUser_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.IsSuperUser = null;

            // Assert
            Assert.That(_sut.IsSuperUser, Is.Null);
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
                Assert.That(_sut.Emails, Has.Count.EqualTo(2));
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
                Assert.That(_sut.PhoneNumbers, Has.Count.EqualTo(2));
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
                Assert.That(_sut.Addresses, Has.Count.EqualTo(2));
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
            Assert.DoesNotThrow(() => { var _ = personInterface.Emails; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceEmails_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var emails = new List<OrganizerCompanion.Core.Interfaces.Type.IEmail>();

            // Act & Assert
            Assert.DoesNotThrow(() => { personInterface.Emails = emails; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfacePhoneNumbers_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;

            // Act & Assert
            Assert.DoesNotThrow(() => { var _ = personInterface.PhoneNumbers; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfacePhoneNumbers_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var phoneNumbers = new List<OrganizerCompanion.Core.Interfaces.Type.IPhoneNumber>();

            // Act & Assert
            Assert.DoesNotThrow(() => { personInterface.PhoneNumbers = phoneNumbers; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAddresses_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;

            // Act & Assert
            Assert.DoesNotThrow(() => { var _ = personInterface.Addresses; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAddresses_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var personInterface = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var addresses = new List<OrganizerCompanion.Core.Interfaces.Type.IAddress>();

            // Act & Assert
            Assert.DoesNotThrow(() => { personInterface.Addresses = addresses; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDateCreated = new(2023, 10, 8, 14, 30, 0);

            // Act
            _sut.DateCreated = expectedDateCreated;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(expectedDateCreated));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveDefaultValueOfNow()
        {
            // Arrange & Act
            _sut = new ContactDTO();

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDateModified = new(2023, 10, 8, 15, 45, 0);

            // Act
            _sut.DateModified = expectedDateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedDateModified));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
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
        public void Id_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.Id));

            // Act
            var rangeAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));
            });
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
        public void UserName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.UserName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsActive_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.IsActive));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsAdmin_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.IsAdmin));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.DateModified));

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
        public void IsDeceased_ShouldNotHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.IsDeceased));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsSuperUser_ShouldNotHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.IsSuperUser));

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
                UserName = "jane.smith",
                IsActive = true,
                IsDeceased = false,
                IsAdmin = false,
                IsSuperUser = null,
                JoinedDate = new DateTime(2020, 1, 1),
                Emails = [new() { Id = 1, EmailAddress = "jane@example.com" }],
                PhoneNumbers = [new() { Id = 1, Phone = "555-1234" }],
                Addresses = [new MockAddressDTO { LinkedEntityId = 1 }],
                DateCreated = new DateTime(2023, 10, 8, 10, 0, 0),
                DateModified = new DateTime(2023, 10, 8, 15, 30, 0)
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
                Assert.That(contactDTO.UserName, Is.EqualTo("jane.smith"));
                Assert.That(contactDTO.IsActive, Is.True);
                Assert.That(contactDTO.IsDeceased, Is.False);
                Assert.That(contactDTO.IsAdmin, Is.False);
                Assert.That(contactDTO.IsSuperUser, Is.Null);
                Assert.That(contactDTO.JoinedDate, Is.EqualTo(new DateTime(2020, 1, 1)));
                Assert.That(contactDTO.Emails, Has.Count.EqualTo(1));
                Assert.That(contactDTO.PhoneNumbers, Has.Count.EqualTo(1));
                Assert.That(contactDTO.Addresses, Has.Count.EqualTo(1));
                Assert.That(contactDTO.DateCreated, Is.EqualTo(new DateTime(2023, 10, 8, 10, 0, 0)));
                Assert.That(contactDTO.DateModified, Is.EqualTo(new DateTime(2023, 10, 8, 15, 30, 0)));
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
                { nameof(ContactDTO.Pronouns), "pronouns" }, // Note: JSON name is "userName" not "pronouns"
                { nameof(ContactDTO.BirthDate), "birthDate" },
                { nameof(ContactDTO.DeceasedDate), "deceasedDate" },
                { nameof(ContactDTO.UserName), "userName" }, // Note: Also has "userName" JSON name
                { nameof(ContactDTO.IsActive), "isActive" },
                { nameof(ContactDTO.IsDeceased), "isDeceased" },
                { nameof(ContactDTO.IsAdmin), "isAdmin" },
                { nameof(ContactDTO.IsSuperUser), "isSuperUser" },
                { nameof(ContactDTO.JoinedDate), "joinedDate" },
                { nameof(ContactDTO.Emails), "emails" },
                { nameof(ContactDTO.PhoneNumbers), "phoneNumbers" },
                { nameof(ContactDTO.Addresses), "addresses" },
                { nameof(ContactDTO.DateCreated), "dateCreated" },
                { nameof(ContactDTO.DateModified), "dateModified" }
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
        public void IsDeceased_ShouldHaveJsonIgnoreWhenWritingNullAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.IsDeceased));

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
        public void IsSuperUser_ShouldHaveJsonIgnoreWhenWritingNullAttribute()
        {
            // Arrange
            var property = typeof(ContactDTO).GetProperty(nameof(ContactDTO.IsSuperUser));

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
        public void ContactDTO_AsInterface_ShouldRetainAllProperties()
        {
            // Arrange
            var testId = 100;
            var testFirstName = "Interface";
            var testMiddleName = "Test";
            var testLastName = "User";
            var testFullName = "Interface Test User";
            var testPronouns = OrganizerCompanion.Core.Enums.Pronouns.TheyThem;
            var testBirthDate = new DateTime(1990, 1, 1);
            var testDeceasedDate = new DateTime(2023, 12, 31);
            var testUserName = "interface.test";
            var testIsActive = true;
            var testIsDeceased = false;
            var testIsAdmin = true;
            var testIsSuperUser = false;
            var testJoinedDate = new DateTime(2020, 1, 1);
            var testDateCreated = new DateTime(2023, 10, 1, 8, 0, 0);
            var testDateModified = new DateTime(2023, 10, 2, 10, 0, 0);

            _sut.Id = testId;
            _sut.FirstName = testFirstName;
            _sut.MiddleName = testMiddleName;
            _sut.LastName = testLastName;
            _sut.FullName = testFullName;
            _sut.Pronouns = testPronouns;
            _sut.BirthDate = testBirthDate;
            _sut.DeceasedDate = testDeceasedDate;
            _sut.UserName = testUserName;
            _sut.IsActive = testIsActive;
            _sut.IsDeceased = testIsDeceased;
            _sut.IsAdmin = testIsAdmin;
            _sut.IsSuperUser = testIsSuperUser;
            _sut.JoinedDate = testJoinedDate;
            _sut.DateCreated = testDateCreated;
            _sut.DateModified = testDateModified;

            // Act
            var interfaceInstance = (IContactDTO)_sut;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceInstance.Id, Is.EqualTo(testId));
                Assert.That(interfaceInstance.UserName, Is.EqualTo(testUserName));
                Assert.That(interfaceInstance.IsActive, Is.EqualTo(testIsActive));
                Assert.That(interfaceInstance.IsDeceased, Is.EqualTo(testIsDeceased));
                Assert.That(interfaceInstance.IsAdmin, Is.EqualTo(testIsAdmin));
                Assert.That(interfaceInstance.IsSuperUser, Is.EqualTo(testIsSuperUser));
                Assert.That(interfaceInstance.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(interfaceInstance.DateModified, Is.EqualTo(testDateModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_InterfaceMethods_ShouldWork()
        {
            // Arrange
            var domainInterface = (IDomainEntity)_sut;
            var contactInterface = (IContactDTO)_sut;

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test IDomainEntity interface methods
                Assert.Throws<NotImplementedException>(() => domainInterface.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => domainInterface.ToJson());

                // Test property access through interface
                Assert.DoesNotThrow(() => contactInterface.Id = 500);
                Assert.DoesNotThrow(() => contactInterface.UserName = "interface.user");
                Assert.DoesNotThrow(() => contactInterface.IsActive = true);
                Assert.DoesNotThrow(() => contactInterface.IsDeceased = false);
                Assert.DoesNotThrow(() => contactInterface.IsAdmin = true);
                Assert.DoesNotThrow(() => contactInterface.IsSuperUser = false);

                // Verify changes through interface are reflected in concrete type
                Assert.That(_sut.Id, Is.EqualTo(500));
                Assert.That(_sut.UserName, Is.EqualTo("interface.user"));
                Assert.That(_sut.IsActive, Is.EqualTo(true));
                Assert.That(_sut.IsDeceased, Is.EqualTo(false));
                Assert.That(_sut.IsAdmin, Is.EqualTo(true));
                Assert.That(_sut.IsSuperUser, Is.EqualTo(false));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldAllowLargeLists()
        {
            // Arrange
            var largeEmailList = new List<EmailDTO>();
            var largePhoneList = new List<PhoneNumberDTO>();
            var largeAddressList = new List<IAddressDTO>();

            for (int i = 0; i < 50; i++)
            {
                largeEmailList.Add(new EmailDTO { Id = i, EmailAddress = $"email{i}@test.com" });
                largePhoneList.Add(new PhoneNumberDTO { Id = i, Phone = $"555-{i:D4}" });
                largeAddressList.Add(new MockAddressDTO { LinkedEntityId = i });
            }

            // Act
            _sut.Emails = largeEmailList;
            _sut.PhoneNumbers = largePhoneList;
            _sut.Addresses = largeAddressList;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Has.Count.EqualTo(50));
                Assert.That(_sut.PhoneNumbers, Has.Count.EqualTo(50));
                Assert.That(_sut.Addresses, Has.Count.EqualTo(50));
                Assert.That(_sut.Emails[25].EmailAddress, Is.EqualTo("email25@test.com"));
                Assert.That(_sut.PhoneNumbers[25].Phone, Is.EqualTo("555-0025"));
                Assert.That(_sut.Addresses[25].LinkedEntityId, Is.EqualTo(25));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldAllowNullAssignment()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => { _sut.Emails = null!; });
                Assert.DoesNotThrow(() => { _sut.PhoneNumbers = null!; });
                Assert.DoesNotThrow(() => { _sut.Addresses = null!; });
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptEmptyStrings()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _sut.FirstName = "");
                Assert.DoesNotThrow(() => _sut.MiddleName = "");
                Assert.DoesNotThrow(() => _sut.LastName = "");
                Assert.DoesNotThrow(() => _sut.FullName = "");
                Assert.DoesNotThrow(() => _sut.UserName = "");

                Assert.That(_sut.FirstName, Is.EqualTo(""));
                Assert.That(_sut.MiddleName, Is.EqualTo(""));
                Assert.That(_sut.LastName, Is.EqualTo(""));
                Assert.That(_sut.FullName, Is.EqualTo(""));
                Assert.That(_sut.UserName, Is.EqualTo(""));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptLongStrings()
        {
            // Arrange
            var longString = new string('A', 1000);

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.FirstName = longString;
                Assert.That(_sut.FirstName, Is.EqualTo(longString));

                _sut.MiddleName = longString;
                Assert.That(_sut.MiddleName, Is.EqualTo(longString));

                _sut.LastName = longString;
                Assert.That(_sut.LastName, Is.EqualTo(longString));

                _sut.FullName = longString;
                Assert.That(_sut.FullName, Is.EqualTo(longString));

                _sut.UserName = longString;
                Assert.That(_sut.UserName, Is.EqualTo(longString));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.FirstName = specialChars;
                Assert.That(_sut.FirstName, Is.EqualTo(specialChars));

                _sut.MiddleName = specialChars;
                Assert.That(_sut.MiddleName, Is.EqualTo(specialChars));

                _sut.LastName = specialChars;
                Assert.That(_sut.LastName, Is.EqualTo(specialChars));

                _sut.FullName = specialChars;
                Assert.That(_sut.FullName, Is.EqualTo(specialChars));

                _sut.UserName = specialChars;
                Assert.That(_sut.UserName, Is.EqualTo(specialChars));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeString = "José María Åström 测试用户 🙋‍♀️👨‍💼";

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.FirstName = unicodeString;
                Assert.That(_sut.FirstName, Is.EqualTo(unicodeString));

                _sut.MiddleName = unicodeString;
                Assert.That(_sut.MiddleName, Is.EqualTo(unicodeString));

                _sut.LastName = unicodeString;
                Assert.That(_sut.LastName, Is.EqualTo(unicodeString));

                _sut.FullName = unicodeString;
                Assert.That(_sut.FullName, Is.EqualTo(unicodeString));

                _sut.UserName = unicodeString;
                Assert.That(_sut.UserName, Is.EqualTo(unicodeString));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateTimeProperties_ShouldAcceptBoundaryValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test BirthDate with boundary values
                _sut.BirthDate = DateTime.MinValue;
                Assert.That(_sut.BirthDate, Is.EqualTo(DateTime.MinValue));

                _sut.BirthDate = DateTime.MaxValue;
                Assert.That(_sut.BirthDate, Is.EqualTo(DateTime.MaxValue));

                _sut.BirthDate = null;
                Assert.That(_sut.BirthDate, Is.Null);

                // Test DeceasedDate with boundary values
                _sut.DeceasedDate = DateTime.MinValue;
                Assert.That(_sut.DeceasedDate, Is.EqualTo(DateTime.MinValue));

                _sut.DeceasedDate = DateTime.MaxValue;
                Assert.That(_sut.DeceasedDate, Is.EqualTo(DateTime.MaxValue));

                _sut.DeceasedDate = null;
                Assert.That(_sut.DeceasedDate, Is.Null);

                // Test JoinedDate with boundary values
                _sut.JoinedDate = DateTime.MinValue;
                Assert.That(_sut.JoinedDate, Is.EqualTo(DateTime.MinValue));

                _sut.JoinedDate = DateTime.MaxValue;
                Assert.That(_sut.JoinedDate, Is.EqualTo(DateTime.MaxValue));

                _sut.JoinedDate = null;
                Assert.That(_sut.JoinedDate, Is.Null);

                // Test DateCreated with boundary values
                _sut.DateCreated = DateTime.MinValue;
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));

                _sut.DateCreated = DateTime.MaxValue;
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MaxValue));

                // Test DateModified with boundary values
                _sut.DateModified = DateTime.MinValue;
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MinValue));

                _sut.DateModified = DateTime.MaxValue;
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));

                _sut.DateModified = null;
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void BooleanProperties_ShouldAcceptAllValidValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test IsActive
                _sut.IsActive = true;
                Assert.That(_sut.IsActive, Is.True);
                _sut.IsActive = false;
                Assert.That(_sut.IsActive, Is.False);
                _sut.IsActive = null;
                Assert.That(_sut.IsActive, Is.Null);

                // Test IsDeceased
                _sut.IsDeceased = true;
                Assert.That(_sut.IsDeceased, Is.True);
                _sut.IsDeceased = false;
                Assert.That(_sut.IsDeceased, Is.False);
                _sut.IsDeceased = null;
                Assert.That(_sut.IsDeceased, Is.Null);

                // Test IsAdmin
                _sut.IsAdmin = true;
                Assert.That(_sut.IsAdmin, Is.True);
                _sut.IsAdmin = false;
                Assert.That(_sut.IsAdmin, Is.False);
                _sut.IsAdmin = null;
                Assert.That(_sut.IsAdmin, Is.Null);

                // Test IsSuperUser
                _sut.IsSuperUser = true;
                Assert.That(_sut.IsSuperUser, Is.True);
                _sut.IsSuperUser = false;
                Assert.That(_sut.IsSuperUser, Is.False);
                _sut.IsSuperUser = null;
                Assert.That(_sut.IsSuperUser, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptBoundaryValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test minimum value (0)
                _sut.Id = 0;
                Assert.That(_sut.Id, Is.EqualTo(0));

                // Test maximum value
                _sut.Id = int.MaxValue;
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

                // Test mid-range value
                _sut.Id = 1_000_000;
                Assert.That(_sut.Id, Is.EqualTo(1_000_000));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldAcceptAllEnumValuesByName()
        {
            // Arrange
            var enumNames = Enum.GetNames(typeof(OrganizerCompanion.Core.Enums.Pronouns));

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var enumName in enumNames)
                {
                    var enumValue = Enum.Parse<OrganizerCompanion.Core.Enums.Pronouns>(enumName);
                    _sut.Pronouns = enumValue;
                    Assert.That(_sut.Pronouns, Is.EqualTo(enumValue), $"Enum value {enumName} should be settable");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllPropertiesSet_ShouldMaintainCorrectTypes()
        {
            // Arrange
            var testDate = new DateTime(2023, 11, 15, 14, 30, 45);

            // Act
            _sut.Id = 12345;
            _sut.FirstName = "TypeTest";
            _sut.MiddleName = "Type";
            _sut.LastName = "User";
            _sut.FullName = "TypeTest Type User";
            _sut.Pronouns = OrganizerCompanion.Core.Enums.Pronouns.HeHim;
            _sut.BirthDate = testDate;
            _sut.DeceasedDate = testDate.AddYears(50);
            _sut.UserName = "typetest.user";
            _sut.IsActive = true;
            _sut.IsDeceased = false;
            _sut.IsAdmin = true;
            _sut.IsSuperUser = false;
            _sut.JoinedDate = testDate.AddYears(-5);
            _sut.Emails = [new() { Id = 1, EmailAddress = "test@type.com" }];
            _sut.PhoneNumbers = [new() { Id = 1, Phone = "555-TYPE" }];
            _sut.Addresses = [new MockAddressDTO { LinkedEntityId = 1 }];
            _sut.DateCreated = testDate;
            _sut.DateModified = testDate.AddHours(2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.TypeOf<int>());
                Assert.That(_sut.FirstName, Is.TypeOf<string>());
                Assert.That(_sut.MiddleName, Is.TypeOf<string>());
                Assert.That(_sut.LastName, Is.TypeOf<string>());
                Assert.That(_sut.FullName, Is.TypeOf<string>());
                Assert.That(_sut.Pronouns, Is.TypeOf<OrganizerCompanion.Core.Enums.Pronouns>());
                Assert.That(_sut.BirthDate, Is.TypeOf<DateTime>(), "BirthDate should be DateTime when set to non-null value");
                Assert.That(_sut.DeceasedDate, Is.TypeOf<DateTime>(), "DeceasedDate should be DateTime when set to non-null value");
                Assert.That(_sut.UserName, Is.TypeOf<string>());
                Assert.That(_sut.IsActive, Is.TypeOf<bool>(), "IsActive should be bool when set to non-null value");
                Assert.That(_sut.IsDeceased, Is.TypeOf<bool>(), "IsDeceased should be bool when set to non-null value");
                Assert.That(_sut.IsAdmin, Is.TypeOf<bool>(), "IsAdmin should be bool when set to non-null value");
                Assert.That(_sut.IsSuperUser, Is.TypeOf<bool>(), "IsSuperUser should be bool when set to non-null value");
                Assert.That(_sut.JoinedDate, Is.TypeOf<DateTime>(), "JoinedDate should be DateTime when set to non-null value");
                Assert.That(_sut.Emails, Is.TypeOf<List<EmailDTO>>());
                Assert.That(_sut.PhoneNumbers, Is.TypeOf<List<PhoneNumberDTO>>());
                Assert.That(_sut.Addresses, Is.TypeOf<List<IAddressDTO>>());
                Assert.That(_sut.DateCreated, Is.TypeOf<DateTime>());
                Assert.That(_sut.DateModified, Is.TypeOf<DateTime>(), "DateModified should be DateTime when set to non-null value");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_DefaultValuesVerification()
        {
            // Arrange & Act
            var freshDto = new ContactDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(freshDto.Id, Is.EqualTo(0), "Id should default to 0");
                Assert.That(freshDto.FirstName, Is.Null, "FirstName should default to null");
                Assert.That(freshDto.MiddleName, Is.Null, "MiddleName should default to null");
                Assert.That(freshDto.LastName, Is.Null, "LastName should default to null");
                Assert.That(freshDto.FullName, Is.Null, "FullName should default to null");
                Assert.That(freshDto.Pronouns, Is.Null, "Pronouns should default to null");
                Assert.That(freshDto.BirthDate, Is.Null, "BirthDate should default to null");
                Assert.That(freshDto.DeceasedDate, Is.Null, "DeceasedDate should default to null");
                Assert.That(freshDto.UserName, Is.Null, "UserName should default to null");
                Assert.That(freshDto.IsActive, Is.Null, "IsActive should default to null");
                Assert.That(freshDto.IsDeceased, Is.Null, "IsDeceased should default to null");
                Assert.That(freshDto.IsAdmin, Is.Null, "IsAdmin should default to null");
                Assert.That(freshDto.IsSuperUser, Is.Null, "IsSuperUser should default to null");
                Assert.That(freshDto.JoinedDate, Is.Null, "JoinedDate should default to null");
                Assert.That(freshDto.Emails, Is.Not.Null, "Emails should not be null");
                Assert.That(freshDto.Emails, Is.Empty, "Emails should be empty list");
                Assert.That(freshDto.PhoneNumbers, Is.Not.Null, "PhoneNumbers should not be null");
                Assert.That(freshDto.PhoneNumbers, Is.Empty, "PhoneNumbers should be empty list");
                Assert.That(freshDto.Addresses, Is.Not.Null, "Addresses should not be null");
                Assert.That(freshDto.Addresses, Is.Empty, "Addresses should be empty list");
                Assert.That(freshDto.DateCreated, Is.TypeOf<DateTime>(), "DateCreated should be DateTime");
                Assert.That(freshDto.DateModified, Is.Null, "DateModified should default to null");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_WithDifferentTypes_ShouldAlwaysThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<ContactDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => ((IDomainEntity)_sut).Cast<IContactDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_RepeatedCalls_ShouldAlwaysThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                Assert.Throws<NotImplementedException>(() => ((IDomainEntity)_sut).ToJson());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_PropertyChaining_ShouldWorkCorrectly()
        {
      // Arrange & Act
      var chainedDto = new ContactDTO
      {
        Id = 1,
        FirstName = "Chain",
        LastName = "User",
        IsActive = true,
        IsAdmin = false
      };

      // Assert
      Assert.Multiple(() =>
            {
                Assert.That(chainedDto.Id, Is.EqualTo(1));
                Assert.That(chainedDto.FirstName, Is.EqualTo("Chain"));
                Assert.That(chainedDto.LastName, Is.EqualTo("User"));
                Assert.That(chainedDto.IsActive, Is.True);
                Assert.That(chainedDto.IsAdmin, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldAllowModificationAfterInitialization()
        {
            // Arrange
            var initialEmails = new List<EmailDTO>
            {
                new() { Id = 1, EmailAddress = "initial@test.com" }
            };
            var initialPhones = new List<PhoneNumberDTO>
            {
                new() { Id = 1, Phone = "555-INIT" }
            };
            var initialAddresses = new List<IAddressDTO>
            {
                new MockAddressDTO { LinkedEntityId = 1 }
            };

            _sut.Emails = initialEmails;
            _sut.PhoneNumbers = initialPhones;
            _sut.Addresses = initialAddresses;

            var newEmails = new List<EmailDTO>
            {
                new() { Id = 2, EmailAddress = "new@test.com" },
                new() { Id = 3, EmailAddress = "another@test.com" }
            };
            var newPhones = new List<PhoneNumberDTO>
            {
                new() { Id = 2, Phone = "555-NEW1" },
                new() { Id = 3, Phone = "555-NEW2" }
            };
            var newAddresses = new List<IAddressDTO>
            {
                new MockAddressDTO { LinkedEntityId = 2 },
                new MockAddressDTO { LinkedEntityId = 3 }
            };

            // Act
            _sut.Emails = newEmails;
            _sut.PhoneNumbers = newPhones;
            _sut.Addresses = newAddresses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Is.EqualTo(newEmails));
                Assert.That(_sut.Emails, Has.Count.EqualTo(2));
                Assert.That(_sut.Emails, Is.Not.EqualTo(initialEmails));

                Assert.That(_sut.PhoneNumbers, Is.EqualTo(newPhones));
                Assert.That(_sut.PhoneNumbers, Has.Count.EqualTo(2));
                Assert.That(_sut.PhoneNumbers, Is.Not.EqualTo(initialPhones));

                Assert.That(_sut.Addresses, Is.EqualTo(newAddresses));
                Assert.That(_sut.Addresses, Has.Count.EqualTo(2));
                Assert.That(_sut.Addresses, Is.Not.EqualTo(initialAddresses));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ContactDTO_ComprehensiveAttributeValidation()
        {
            // Arrange
            var type = typeof(ContactDTO);

            // Act & Assert - Verify all expected attributes are present
            Assert.Multiple(() =>
            {
                // Verify Required attributes on expected properties
                var requiredPropertyNames = new[] { "Id", "FirstName", "MiddleName", "LastName", "FullName", "Pronouns", 
                    "BirthDate", "UserName", "IsActive", "IsAdmin", "JoinedDate", "Emails", "PhoneNumbers", "Addresses", 
                    "DateCreated", "DateModified" };
                foreach (var propName in requiredPropertyNames)
                {
                    var property = type.GetProperty(propName);
                    Assert.That(property?.GetCustomAttribute<RequiredAttribute>(), Is.Not.Null, $"{propName} should have Required attribute");
                }

                // Verify properties that should NOT have Required attribute
                var nonRequiredPropertyNames = new[] { "DeceasedDate", "IsDeceased", "IsSuperUser" };
                foreach (var propName in nonRequiredPropertyNames)
                {
                    var property = type.GetProperty(propName);
                    Assert.That(property?.GetCustomAttribute<RequiredAttribute>(), Is.Null, $"{propName} should NOT have Required attribute");
                }

                // Verify Id has Range attribute
                var idProperty = type.GetProperty("Id");
                var idRangeAttr = idProperty?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                Assert.That(idRangeAttr?.Minimum, Is.EqualTo(0));
                Assert.That(idRangeAttr?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(idRangeAttr?.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));

                // Verify JsonIgnore conditions on conditional properties
                var deceasedDateProp = type.GetProperty("DeceasedDate");
                var deceasedDateIgnore = deceasedDateProp?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                Assert.That(deceasedDateIgnore?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));

                var isDeceasedProp = type.GetProperty("IsDeceased");
                var isDeceasedIgnore = isDeceasedProp?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                Assert.That(isDeceasedIgnore?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));

                var isSuperUserProp = type.GetProperty("IsSuperUser");
                var isSuperUserIgnore = isSuperUserProp?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                Assert.That(isSuperUserIgnore?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CustomValidationAttributes_ShouldBePresent()
        {
            // Arrange
            var type = typeof(ContactDTO);

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Verify EmailsValidatorAttribute attribute on Emails property
                var emailsProperty = type.GetProperty("Emails");
                var emailsValidator = emailsProperty?.GetCustomAttribute<OrganizerCompanion.Core.Validation.Attributes.EmailsValidatorAttribute>();
                Assert.That(emailsValidator, Is.Not.Null, "Emails property should have EmailsValidatorAttribute attribute");

                // Verify PhoneNumbersValidator attribute on PhoneNumbers property
                var phoneNumbersProperty = type.GetProperty("PhoneNumbers");
                var phoneNumbersValidator = phoneNumbersProperty?.GetCustomAttribute<OrganizerCompanion.Core.Validation.Attributes.PhoneNumbersValidator>();
                Assert.That(phoneNumbersValidator, Is.Not.Null, "PhoneNumbers property should have PhoneNumbersValidator attribute");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void NullableDateTime_PropertyCombinations_ShouldWork()
        {
            // Arrange & Act - Test various combinations of nullable DateTime properties
            _sut.BirthDate = new DateTime(1990, 1, 1);
            _sut.DeceasedDate = null;
            _sut.JoinedDate = new DateTime(2020, 1, 1);
            _sut.DateModified = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.BirthDate, Is.Not.Null);
                Assert.That(_sut.DeceasedDate, Is.Null);
                Assert.That(_sut.JoinedDate, Is.Not.Null);
                Assert.That(_sut.DateModified, Is.Null);
                
                // Test setting all to null
                _sut.BirthDate = null;
                _sut.JoinedDate = null;
                Assert.That(_sut.BirthDate, Is.Null);
                Assert.That(_sut.JoinedDate, Is.Null);
                
                // Test setting all to values
                var testDate = DateTime.Now;
                _sut.BirthDate = testDate;
                _sut.DeceasedDate = testDate.AddYears(80);
                _sut.JoinedDate = testDate.AddYears(-10);
                _sut.DateModified = testDate;
                
                Assert.That(_sut.BirthDate, Is.EqualTo(testDate));
                Assert.That(_sut.DeceasedDate, Is.EqualTo(testDate.AddYears(80)));
                Assert.That(_sut.JoinedDate, Is.EqualTo(testDate.AddYears(-10)));
                Assert.That(_sut.DateModified, Is.EqualTo(testDate));
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
            public int Id { get; set; }
            public int LinkedEntityId { get; set; }
            public IDomainEntity? LinkedEntity { get; set; }
            public string? LinkedEntityType { get; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
            bool OrganizerCompanion.Core.Interfaces.Type.IAddress.IsPrimary { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }

            public T Cast<T>() where T : IDomainEntity
            {
                throw new NotImplementedException();
            }

            public string ToJson()
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
