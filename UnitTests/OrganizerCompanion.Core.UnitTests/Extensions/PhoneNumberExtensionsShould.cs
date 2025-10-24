using NUnit.Framework;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class PhoneNumberExtensionsShould
    {
        private PhoneNumber _domainPhoneNumber = null!;
        private MockTypePhoneNumber _typePhoneNumber = null!;

        [SetUp]
        public void SetUp()
        {
            _domainPhoneNumber = new PhoneNumber(1, "+1-555-123-4567", OrganizerCompanion.Core.Enums.Types.Work, OrganizerCompanion.Core.Enums.Countries.UnitedStates, null, DateTime.Now, DateTime.Now);

            _typePhoneNumber = new MockTypePhoneNumber
            {
                Phone = "+1-555-987-6543",
                Type = OrganizerCompanion.Core.Enums.Types.Home,
                Country = OrganizerCompanion.Core.Enums.Countries.Canada
            };
        }

        [Test, Category("Extensions")]
        public void AsTypePhoneNumber_WithValidDomainPhoneNumber_ShouldReturnTypePhoneNumber()
        {
            // Act
            var result = _domainPhoneNumber.AsTypePhoneNumber();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Phone, Is.EqualTo("+1-555-123-4567"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
            });
        }

        [Test, Category("Extensions")]
        public void AsDomainPhoneNumber_WithDomainPhoneNumberAsTypePhoneNumber_ShouldReturnDomainPhoneNumber()
        {
            // Arrange
            Interfaces.Type.IPhoneNumber typePhoneFromDomain = _domainPhoneNumber;

            // Act
            var result = typePhoneFromDomain.AsDomainPhoneNumber();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_domainPhoneNumber));
            Assert.That(result!.Phone, Is.EqualTo("+1-555-123-4567"));
        }

        [Test, Category("Extensions")]
        public void AsDomainPhoneNumber_WithPureTypePhoneNumber_ShouldReturnNull()
        {
            // Arrange
            Interfaces.Type.IPhoneNumber pureTypePhoneNumber = _typePhoneNumber;

            // Act
            var result = pureTypePhoneNumber.AsDomainPhoneNumber();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Extensions")]
        public void AsTypePhoneNumbers_WithListOfDomainPhoneNumbers_ShouldConvertToTypePhoneNumbers()
        {
            // Arrange
            var domainPhoneNumber2 = new PhoneNumber(2, "+1-555-111-2222", OrganizerCompanion.Core.Enums.Types.Home, OrganizerCompanion.Core.Enums.Countries.Canada, null, DateTime.Now, DateTime.Now);
            var domainPhoneNumbers = new List<IPhoneNumber?> { _domainPhoneNumber, domainPhoneNumber2, null };

            // Act
            var result = domainPhoneNumbers.AsTypePhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0]?.Phone, Is.EqualTo("+1-555-123-4567"));
                Assert.That(result[0]?.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
                Assert.That(result[1]?.Phone, Is.EqualTo("+1-555-111-2222"));
                Assert.That(result[1]?.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Canada));
                Assert.That(result[2], Is.Null);
            });
        }

        [Test, Category("Extensions")]
        public void AsTypePhoneNumbers_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<IPhoneNumber?>();

            // Act
            var result = emptyList.AsTypePhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainPhoneNumbers_WithListContainingDomainPhoneNumbers_ShouldReturnOnlyDomainPhoneNumbers()
        {
            // Arrange
            var domainPhoneNumber2 = new PhoneNumber(3, "+1-555-333-4444", OrganizerCompanion.Core.Enums.Types.Mobile, OrganizerCompanion.Core.Enums.Countries.Mexico, null, DateTime.Now, DateTime.Now);
            var typePhoneNumbers = new List<Interfaces.Type.IPhoneNumber?>
            {
                _domainPhoneNumber,         // This is a domain phone number (should be included)
                _typePhoneNumber,           // This is a pure type phone number (should be filtered out)
                domainPhoneNumber2,         // This is a domain phone number (should be included)
                null                        // Null values are filtered out by OfType
            };

            // Act
            var result = typePhoneNumbers.AsDomainPhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(result[0]?.Phone, Is.EqualTo("+1-555-123-4567"));
                Assert.That(result[0]?.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
                Assert.That(result[1]?.Phone, Is.EqualTo("+1-555-333-4444"));
                Assert.That(result[1]?.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Mexico));
            });
        }

        [Test, Category("Extensions")]
        public void AsDomainPhoneNumbers_WithOnlyPureTypePhoneNumbers_ShouldReturnEmptyList()
        {
            // Arrange
            var typePhoneNumber2 = new MockTypePhoneNumber { Phone = "+1-555-777-8888", Type = OrganizerCompanion.Core.Enums.Types.Billing };
            var typePhoneNumbers = new List<Interfaces.Type.IPhoneNumber?> { _typePhoneNumber, typePhoneNumber2 };

            // Act
            var result = typePhoneNumbers.AsDomainPhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainPhoneNumbers_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<Interfaces.Type.IPhoneNumber?>();

            // Act
            var result = emptyList.AsDomainPhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainPhoneNumbers_WithNullsOnly_ShouldReturnEmptyList()
        {
            // Arrange
            var listWithNulls = new List<Interfaces.Type.IPhoneNumber?> { null, null };

            // Act
            var result = listWithNulls.AsDomainPhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsTypePhoneNumber_WithNullPhone_ShouldReturnTypePhoneNumber()
        {
            // Arrange
            var domainPhoneWithNull = new PhoneNumber(4, null, OrganizerCompanion.Core.Enums.Types.Other, OrganizerCompanion.Core.Enums.Countries.UnitedStates, null, DateTime.Now, DateTime.Now);

            // Act
            var result = domainPhoneWithNull.AsTypePhoneNumber();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Phone, Is.Null);
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
            });
        }

        [Test, Category("Extensions")]
        public void AsTypePhoneNumbers_WithNullPhones_ShouldHandleNullValues()
        {
            // Arrange
            var domainPhoneWithNull = new PhoneNumber(
                5, 
                null, 
                OrganizerCompanion.Core.Enums.Types.Fax, 
                OrganizerCompanion.Core.Enums.Countries.UnitedStates, 
                null, 
                DateTime.Now, 
                DateTime.Now);
            var domainPhoneNumbers = new List<IPhoneNumber?> { _domainPhoneNumber, domainPhoneWithNull, null };

            // Act
            var result = domainPhoneNumbers.AsTypePhoneNumbers();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.Multiple(() =>
            {
                Assert.That(result[0]?.Phone, Is.EqualTo("+1-555-123-4567"));
                Assert.That(result[1]?.Phone, Is.Null);
                Assert.That(result[1]?.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
                Assert.That(result[2], Is.Null);
            });
        }

        // Mock class to simulate a pure Type.IPhoneNumber implementation
        private class MockTypePhoneNumber : Interfaces.Type.IPhoneNumber
        {
            public string? Phone { get; set; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
            public OrganizerCompanion.Core.Enums.Countries? Country { get; set; }
        }
    }
}
