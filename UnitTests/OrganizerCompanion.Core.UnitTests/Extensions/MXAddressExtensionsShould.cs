using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class MXAddressExtensionsShould
    {
        private MXAddress _sut = null!;
        private MockTypeMXAddress _typeMXAddress = null!;

        [SetUp]
        public void SetUp()
        {
            _sut = new MXAddress()
            {
                Id = 1,
                Street = "Calle Principal 123",
                Neighborhood = "Centro",
                PostalCode = "01000",
                City = "Ciudad de México",
                State = MXStates.CiudadDeMéxico.ToStateModel(),
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };

            _typeMXAddress = new MockTypeMXAddress
            {
                Street = "Avenida Reforma 456",
                Neighborhood = "Roma Norte",
                PostalCode = "06700",
                City = "Guadalajara",
                State = MXStates.Jalisco.ToStateModel(),
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };
        }

        [Test, Category("Extensions")]
        public void AsTypeMXAddress_WithValidDomainMXAddress_ShouldReturnTypeMXAddress()
    {
      // Act
      var result = _sut.AsTypeMXAddress();

            // Assert
            Assert.That(result, Is.Not.Null);
      Assert.Multiple(() =>
      {
        Assert.That(result.Street, Is.EqualTo("Calle Principal 123"));
        Assert.That(result.Neighborhood, Is.EqualTo("Centro"));
        Assert.That(result.PostalCode, Is.EqualTo("01000"));
        Assert.That(result.City, Is.EqualTo("Ciudad de México"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainMXAddress_WithDomainMXAddressAsTypeMXAddress_ShouldReturnDomainMXAddress()
    {
      // Arrange
      Interfaces.Type.IMXAddress typeMXAddressFromDomain = _sut;

            // Act
            var result = typeMXAddressFromDomain.AsDomainMXAddress();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_sut));
      Assert.Multiple(() =>
      {
        Assert.That(result!.Street, Is.EqualTo("Calle Principal 123"));
        Assert.That(result.Neighborhood, Is.EqualTo("Centro"));
        Assert.That(result.PostalCode, Is.EqualTo("01000"));
        Assert.That(result.City, Is.EqualTo("Ciudad de México"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainMXAddress_WithPureTypeMXAddress_ShouldReturnNull()
        {
            // Arrange
            Interfaces.Type.IMXAddress pureTypeMXAddress = _typeMXAddress;

            // Act
            var result = pureTypeMXAddress.AsDomainMXAddress();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Extensions")]
        public void AsTypeMXAddresses_WithListOfDomainMXAddresses_ShouldConvertToTypeMXAddresses()
    {
      // Arrange
      var domainMXAddress2 = new MXAddress()
            {
                Street = "Calle Juárez 789",
                Neighborhood = "Zona Centro",
                PostalCode = "44100",
                City = "Guadalajara", 
                State = MXStates.Jalisco.ToStateModel(),
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };
            var domainMXAddresses = new List<IMXAddress?> { _sut, domainMXAddress2, null };

            // Act
            var result = domainMXAddresses.AsTypeMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
      Assert.Multiple(() =>
      {
        Assert.That(result[0]?.Street, Is.EqualTo("Calle Principal 123"));
        Assert.That(result[0]?.Neighborhood, Is.EqualTo("Centro"));
        Assert.That(result[0]?.City, Is.EqualTo("Ciudad de México"));
        Assert.That(result[1]?.Street, Is.EqualTo("Calle Juárez 789"));
        Assert.That(result[1]?.Neighborhood, Is.EqualTo("Zona Centro"));
        Assert.That(result[1]?.City, Is.EqualTo("Guadalajara"));
        Assert.That(result[2], Is.Null);
      });
    }

    [Test, Category("Extensions")]
        public void AsTypeMXAddresses_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<IMXAddress?>();

            // Act
            var result = emptyList.AsTypeMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainMXAddresses_WithListContainingDomainMXAddresses_ShouldReturnOnlyDomainMXAddresses()
    {
      // Arrange
      var domainMXAddress2 = new MXAddress()
            {
                Street = "Avenida Insurgentes 321",
                Neighborhood = "Condesa",
                PostalCode = "06140",
                City = "Ciudad de México",
                State = MXStates.CiudadDeMéxico.ToStateModel(),
                Type = OrganizerCompanion.Core.Enums.Types.Billing
            };
            var typeMXAddresses = new List<Interfaces.Type.IMXAddress?> 
            { 
                _sut,              // This is a domain MXAddress (should be included)
                _typeMXAddress,    // This is a pure type MXAddress (should be filtered out)
                domainMXAddress2,  // This is a domain MXAddress (should be included)
                null               // Null values are filtered out by OfType
            };

            // Act
            var result = typeMXAddresses.AsDomainMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
      Assert.Multiple(() =>
      {
        Assert.That(result[0]?.Street, Is.EqualTo("Calle Principal 123"));
        Assert.That(result[0]?.Neighborhood, Is.EqualTo("Centro"));
        Assert.That(result[0]?.City, Is.EqualTo("Ciudad de México"));
        Assert.That(result[1]?.Street, Is.EqualTo("Avenida Insurgentes 321"));
        Assert.That(result[1]?.Neighborhood, Is.EqualTo("Condesa"));
        Assert.That(result[1]?.City, Is.EqualTo("Ciudad de México"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainMXAddresses_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<Interfaces.Type.IMXAddress?>();

            // Act
            var result = emptyList.AsDomainMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainMXAddresses_WithListOfOnlyPureTypeMXAddresses_ShouldReturnEmptyList()
        {
            // Arrange
            var typeMXAddresses = new List<Interfaces.Type.IMXAddress?> 
            { 
                _typeMXAddress,
                new MockTypeMXAddress 
                { 
                    Street = "Another Street",
                    City = "Another City"
                }
            };

            // Act
            var result = typeMXAddresses.AsDomainMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsTypeMXAddresses_WithMixedNullAndValidAddresses_ShouldPreserveOrder()
        {
            // Arrange
            var address1 = new MXAddress()
            {
                Street = "Calle Primera 123",
                City = "Ciudad1"
            };
            var address2 = new MXAddress()
            {
                Street = "Calle Segunda 456",
                Neighborhood = "Barrio Segundo",
                City = "Ciudad2"
            };
            var addresses = new List<IMXAddress?> { address1, null, address2 };

            // Act
            var result = addresses.AsTypeMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.Multiple(() =>
            {
                // First address
                Assert.That(result[0]?.Street, Is.EqualTo("Calle Primera 123"));
                Assert.That(result[0]?.City, Is.EqualTo("Ciudad1"));
                Assert.That(result[0]?.Neighborhood, Is.Null);
                
                // Null entry
                Assert.That(result[1], Is.Null);
                
                // Second address
                Assert.That(result[2]?.Street, Is.EqualTo("Calle Segunda 456"));
                Assert.That(result[2]?.Neighborhood, Is.EqualTo("Barrio Segundo"));
                Assert.That(result[2]?.City, Is.EqualTo("Ciudad2"));
            });
        }

        [Test, Category("Extensions")]
        public void AsDomainMXAddresses_WithMixedDomainAndTypeAddresses_ShouldReturnOnlyDomainAddresses()
        {
            // Arrange
            var domainAddress1 = new MXAddress()
            {
                Street = "Calle Primera 123",
                City = "Ciudad1"
            };
            var domainAddress2 = new MXAddress()
            {
                Street = "Calle Segunda 456",
                Neighborhood = "Barrio Segundo",
                City = "Ciudad2"
            };
            var addresses = new List<Interfaces.Type.IMXAddress?> 
            { 
                domainAddress1, 
                _typeMXAddress, 
                domainAddress2 
            };

            // Act
            var result = addresses.AsDomainMXAddresses();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                // First address
                Assert.That(result[0]?.Street, Is.EqualTo("Calle Primera 123"));
                Assert.That(result[0]?.City, Is.EqualTo("Ciudad1"));
                Assert.That(result[0]?.Neighborhood, Is.Null);
                
                // Second address
                Assert.That(result[1]?.Street, Is.EqualTo("Calle Segunda 456"));
                Assert.That(result[1]?.Neighborhood, Is.EqualTo("Barrio Segundo"));
                Assert.That(result[1]?.City, Is.EqualTo("Ciudad2"));
            });
        }

        // Mock class to simulate a pure Type.IMXAddress implementation
        private class MockTypeMXAddress : Interfaces.Type.IMXAddress
        {
            public string? Street { get; set; }
            public string? Neighborhood { get; set; }
            public string? PostalCode { get; set; }
            public string? City { get; set; }
            public Interfaces.Type.INationalSubdivision? State { get; set; }
            public bool IsPrimary { get; set; }
            public string? Country { get; set; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
        }
    }
}
