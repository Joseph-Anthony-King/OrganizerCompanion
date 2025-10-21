using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class USAddressExtensionsShould
    {
        private USAddress _sut = null!;
        private MockTypeUSAddress _typeUSAddress = null!;

        [SetUp]
        public void SetUp()
        {
            _sut = new USAddress()
            {
                Id = 1,
                Street1 = "123 Main St",
                City = "Anytown",
                State = USStates.California.ToStateModel(),
                ZipCode = "12345",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };

            _typeUSAddress = new MockTypeUSAddress
            {
                Street1 = "456 Oak Ave",
                City = "Springfield",
                State = USStates.Texas.ToStateModel(),
                ZipCode = "54321",
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };
        }

        [Test, Category("Extensions")]
        public void AsTypeUSAddress_WithValidDomainUSAddress_ShouldReturnTypeUSAddress()
    {
      // Act
      var result = _sut.AsTypeUSAddress();

            // Assert
            Assert.That(result, Is.Not.Null);
      Assert.Multiple(() =>
      {
        Assert.That(result.Street1, Is.EqualTo("123 Main St"));
        Assert.That(result.City, Is.EqualTo("Anytown"));
        Assert.That(result.ZipCode, Is.EqualTo("12345"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainUSAddress_WithDomainUSAddressAsTypeUSAddress_ShouldReturnDomainUSAddress()
    {
      // Arrange
      Interfaces.Type.IUSAddress typeUSAddressFromDomain = _sut;

            // Act
            var result = typeUSAddressFromDomain.AsDomainUSAddress();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_sut));
      Assert.Multiple(() =>
      {
        Assert.That(result!.Street1, Is.EqualTo("123 Main St"));
        Assert.That(result.City, Is.EqualTo("Anytown"));
        Assert.That(result.ZipCode, Is.EqualTo("12345"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainUSAddress_WithPureTypeUSAddress_ShouldReturnNull()
        {
            // Arrange
            Interfaces.Type.IUSAddress pureTypeUSAddress = _typeUSAddress;

            // Act
            var result = pureTypeUSAddress.AsDomainUSAddress();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Extensions")]
        public void AsTypeUSAddresses_WithListOfDomainUSAddresses_ShouldConvertToTypeUSAddresses()
    {
      // Arrange
      var domainUSAddress2 = new USAddress()
            {
                Street1 = "789 Pine St",
                City = "Somewhere", 
                State = USStates.NewYork.ToStateModel(),
                ZipCode = "98765",
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };
            var domainUSAddresses = new List<IUSAddress?> { _sut, domainUSAddress2, null };

            // Act
            var result = domainUSAddresses.AsTypeUSAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
      Assert.Multiple(() =>
      {
        Assert.That(result[0]?.Street1, Is.EqualTo("123 Main St"));
        Assert.That(result[0]?.City, Is.EqualTo("Anytown"));
        Assert.That(result[1]?.Street1, Is.EqualTo("789 Pine St"));
        Assert.That(result[1]?.City, Is.EqualTo("Somewhere"));
        Assert.That(result[2], Is.Null);
      });
    }

    [Test, Category("Extensions")]
        public void AsTypeUSAddresses_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<IUSAddress?>();

            // Act
            var result = emptyList.AsTypeUSAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainUSAddresses_WithListContainingDomainUSAddresses_ShouldReturnOnlyDomainUSAddresses()
    {
      // Arrange
      var domainUSAddress2 = new USAddress()
            {
                Street1 = "321 Elm St",
                City = "Elsewhere",
                State = USStates.Florida.ToStateModel(),
                ZipCode = "11111",
                Type = OrganizerCompanion.Core.Enums.Types.Billing
            };
            var typeUSAddresses = new List<Interfaces.Type.IUSAddress?> 
            { 
                _sut,              // This is a domain USAddress (should be included)
                _typeUSAddress,    // This is a pure type USAddress (should be filtered out)
                domainUSAddress2,  // This is a domain USAddress (should be included)
                null               // Null values are filtered out by OfType
            };

            // Act
            var result = typeUSAddresses.AsDomainUSAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
      Assert.Multiple(() =>
      {
        Assert.That(result[0]?.Street1, Is.EqualTo("123 Main St"));
        Assert.That(result[0]?.City, Is.EqualTo("Anytown"));
        Assert.That(result[1]?.Street1, Is.EqualTo("321 Elm St"));
        Assert.That(result[1]?.City, Is.EqualTo("Elsewhere"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainUSAddresses_WithOnlyPureTypeUSAddresses_ShouldReturnEmptyList()
        {
            // Arrange
            var typeUSAddress2 = new MockTypeUSAddress 
            { 
                Street1 = "999 Cherry Lane", 
                City = "Nowhere", 
                State = USStates.Oregon.ToStateModel(),
                ZipCode = "99999" 
            };
            var typeUSAddresses = new List<Interfaces.Type.IUSAddress?> { _typeUSAddress, typeUSAddress2 };

            // Act
            var result = typeUSAddresses.AsDomainUSAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainUSAddresses_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<Interfaces.Type.IUSAddress?>();

            // Act
            var result = emptyList.AsDomainUSAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainUSAddresses_WithNullsOnly_ShouldReturnEmptyList()
        {
            // Arrange
            var listWithNulls = new List<Interfaces.Type.IUSAddress?> { null, null };

            // Act
            var result = listWithNulls.AsDomainUSAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsTypeUSAddress_WithUSAddressWithAllProperties_ShouldPreserveAllProperties()
        {
            // Arrange
            var completeUSAddress = new USAddress()
            {
                Id = 1,
                Street1 = "123 Main St",
                Street2 = "Apt 2B",
                City = "Anytown",
                State = USStates.California.ToStateModel(),
                ZipCode = "12345",
                Country = "United States",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };

            // Act
            var result = completeUSAddress.AsTypeUSAddress();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Street1, Is.EqualTo("123 Main St"));
                Assert.That(result.Street2, Is.EqualTo("Apt 2B"));
                Assert.That(result.City, Is.EqualTo("Anytown"));
                Assert.That(result.State?.Name, Is.EqualTo("California"));
                Assert.That(result.ZipCode, Is.EqualTo("12345"));
                Assert.That(result.Country, Is.EqualTo("United States"));
            });
        }

        [Test, Category("Extensions")]
        public void AsDomainUSAddress_WithCompleteTypeUSAddress_ShouldHandleAllProperties()
        {
            // Arrange
            var completeUSAddress = new USAddress()
            {
                Id = 2,
                Street1 = "789 Oak Ave",
                Street2 = "Suite 100",
                City = "Springfield",
                State = USStates.Texas.ToStateModel(),
                ZipCode = "54321",
                Country = "USA",
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };
            Interfaces.Type.IUSAddress typeUSAddress = completeUSAddress;

            // Act
            var result = typeUSAddress.AsDomainUSAddress();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result!.Street1, Is.EqualTo("789 Oak Ave"));
                Assert.That(result.Street2, Is.EqualTo("Suite 100"));
                Assert.That(result.City, Is.EqualTo("Springfield"));
                Assert.That(result.State?.Name, Is.EqualTo("Texas"));
                Assert.That(result.ZipCode, Is.EqualTo("54321"));
                Assert.That(result.Country, Is.EqualTo("USA"));
            });
        }

        [Test, Category("Extensions")]
        public void AsTypeUSAddresses_WithMixedAddressProperties_ShouldHandleVariousConfigurations()
        {
            // Arrange
            var address1 = new USAddress()
            {
                Street1 = "123 First St",
                City = "City1",
                State = USStates.California.ToStateModel(),
                ZipCode = "11111",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };
            var address2 = new USAddress()
            {
                Street1 = "456 Second Ave",
                Street2 = "Floor 5",
                City = "City2",
                State = USStates.NewYork.ToStateModel(),
                ZipCode = "22222",
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };
            var domainUSAddresses = new List<IUSAddress?> { address1, address2 };

            // Act
            var result = domainUSAddresses.AsTypeUSAddresses();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(2));
                
                // First address
                Assert.That(result[0]?.Street1, Is.EqualTo("123 First St"));
                Assert.That(result[0]?.City, Is.EqualTo("City1"));
                Assert.That(result[0]?.Street2, Is.Null);
                
                // Second address
                Assert.That(result[1]?.Street1, Is.EqualTo("456 Second Ave"));
                Assert.That(result[1]?.Street2, Is.EqualTo("Floor 5"));
                Assert.That(result[1]?.City, Is.EqualTo("City2"));
            });
        }

        // Mock class to simulate a pure Type.IUSAddress implementation
        private class MockTypeUSAddress : Interfaces.Type.IUSAddress
        {
            public string? Street1 { get; set; }
            public string? Street2 { get; set; }
            public string? City { get; set; }
            public Interfaces.Type.INationalSubdivision? State { get; set; }
            public string? ZipCode { get; set; }
            public string? Country { get; set; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
            public bool IsPrimary { get; set; }
        }
    }
}
