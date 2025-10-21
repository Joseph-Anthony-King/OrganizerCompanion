using NUnit.Framework;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class EmailExtensionsShould
    {
        private Email _sut = null!;
        private MockTypeEmail _typeEmail = null!;

        [SetUp]
        public void SetUp()
        {
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true)
            {
                Id = 1
            };

            _typeEmail = new MockTypeEmail
            {
                EmailAddress = "type@example.com",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };
        }

        [Test, Category("Extensions")]
        public void AsTypeEmail_WithValidDomainEmail_ShouldReturnTypeEmail()
    {
      // Act
      var result = _sut.AsTypeEmail();

            // Assert
            Assert.That(result, Is.Not.Null);
      Assert.Multiple(() =>
      {
        Assert.That(result.EmailAddress, Is.EqualTo("test@example.com"));
        Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainEmail_WithDomainEmailAsTypeEmail_ShouldReturnDomainEmail()
        {
            // Arrange
            Interfaces.Type.IEmail typeEmailFromDomain = _sut;

            // Act
            var result = typeEmailFromDomain.AsDomainEmail();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(_sut));
            Assert.That(result!.EmailAddress, Is.EqualTo("test@example.com"));
        }

        [Test, Category("Extensions")]
        public void AsDomainEmail_WithPureTypeEmail_ShouldReturnNull()
        {
            // Arrange
            Interfaces.Type.IEmail pureTypeEmail = _typeEmail;

            // Act
            var result = pureTypeEmail.AsDomainEmail();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Extensions")]
        public void ToDomainEmail_WithTypeEmail_ShouldCreateNewDomainEmail()
        {
            // Act
            var result = _typeEmail.ToDomainEmail();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Email>());
            Assert.That(result.EmailAddress, Is.EqualTo("type@example.com"));
        }

        [Test, Category("Extensions")]
        public void ToDomainEmail_WithNullEmailAddress_ShouldCreateDomainEmailWithNull()
        {
            // Arrange
            _typeEmail.EmailAddress = null;

            // Act
            var result = _typeEmail.ToDomainEmail();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EmailAddress, Is.Null);
        }

        [Test, Category("Extensions")]
        public void AsTypeEmails_WithListOfDomainEmails_ShouldConvertToTypeEmails()
    {
      // Arrange
      var domainEmail2 = new Email("test2@example.com", OrganizerCompanion.Core.Enums.Types.Home);
            var domainEmails = new List<IEmail?> { _sut, domainEmail2, null };

            // Act
            var result = domainEmails.AsTypeEmails();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(3));
      Assert.Multiple(() =>
      {
        Assert.That(result[0]?.EmailAddress, Is.EqualTo("test@example.com"));
        Assert.That(result[1]?.EmailAddress, Is.EqualTo("test2@example.com"));
        Assert.That(result[2], Is.Null);
      });
    }

    [Test, Category("Extensions")]
        public void AsTypeEmails_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<IEmail?>();

            // Act
            var result = emptyList.AsTypeEmails();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainEmails_WithListContainingDomainEmails_ShouldReturnOnlyDomainEmails()
    {
      // Arrange
      var domainEmail2 = new Email("test2@example.com", OrganizerCompanion.Core.Enums.Types.Mobil);
            var typeEmails = new List<Interfaces.Type.IEmail?> 
            { 
                _sut,      // This is a domain email (should be included)
                _typeEmail,        // This is a pure type email (should be filtered out)
                domainEmail2,      // This is a domain email (should be included)
                null               // Null values are filtered out by OfType
            };

            // Act
            var result = typeEmails.AsDomainEmails();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(2));
      Assert.Multiple(() =>
      {
        Assert.That(result[0]?.EmailAddress, Is.EqualTo("test@example.com"));
        Assert.That(result[1]?.EmailAddress, Is.EqualTo("test2@example.com"));
      });
    }

    [Test, Category("Extensions")]
        public void AsDomainEmails_WithOnlyPureTypeEmails_ShouldReturnEmptyList()
        {
            // Arrange
            var typeEmail2 = new MockTypeEmail { EmailAddress = "type2@example.com", Type = OrganizerCompanion.Core.Enums.Types.Billing };
            var typeEmails = new List<Interfaces.Type.IEmail?> { _typeEmail, typeEmail2 };

            // Act
            var result = typeEmails.AsDomainEmails();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainEmails_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyList = new List<Interfaces.Type.IEmail?>();

            // Act
            var result = emptyList.AsDomainEmails();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Extensions")]
        public void AsDomainEmails_WithNullsOnly_ShouldReturnEmptyList()
        {
            // Arrange
            var listWithNulls = new List<Interfaces.Type.IEmail?> { null, null };

            // Act
            var result = listWithNulls.AsDomainEmails();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        // Mock class to simulate a pure Type.IEmail implementation
        private class MockTypeEmail : Interfaces.Type.IEmail
        {
            public string? EmailAddress { get; set; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
        }
    }
}
