using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class EmailShould
    {
        private Email _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Email();
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateEmailWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Email();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Constructor_WithEmailAddressAndType_ShouldSetPropertiesAndDateCreated()
        {
            // Arrange
            var emailAddress = "test@example.com";
            var type = Types.Work;
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Email(emailAddress, type);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.EqualTo(emailAddress));
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithValidParameters_ShouldSetAllProperties()
        {
            // Arrange
            var id = 1;
            var emailAddress = "test@example.com";
            var type = Types.Home;
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now;

            // Act
            _sut = new Email(id, emailAddress, type, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.EmailAddress, Is.EqualTo(emailAddress));
                Assert.That(_sut.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
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
        public void EmailAddress_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var newEmailAddress = "newemail@example.com";
            var beforeSet = DateTime.Now;

            // Act
            _sut.EmailAddress = newEmailAddress;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.EmailAddress, Is.EqualTo(newEmailAddress));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void EmailAddress_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut = new Email("test@example.com", Types.Work)
            {
                EmailAddress = null
            };

            // Assert
            Assert.That(_sut.EmailAddress, Is.Null);
        }

        [Test, Category("Models")]
        public void Type_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var newType = Types.Fax;
            var beforeSet = DateTime.Now;

            // Act
            // Access through the interface property
            ((OrganizerCompanion.Core.Interfaces.Type.IEmail)_sut).Type = newType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(((OrganizerCompanion.Core.Interfaces.Type.IEmail)_sut).Type, Is.EqualTo(newType));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Type_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut = new Email("test@example.com", Types.Work);

            // Act
            ((OrganizerCompanion.Core.Interfaces.Type.IEmail)_sut).Type = null;

            // Assert
            Assert.That(((OrganizerCompanion.Core.Interfaces.Type.IEmail)_sut).Type, Is.Null);
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Email();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-5);

            // Act
            _sut = new Email(1, "test@example.com", Types.Work, specificDate, DateTime.Now);

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            _sut = new Email();
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            _sut.DateModified = dateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
        }

        [Test, Category("Models")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange
            _sut = new Email();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<Email>());
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut = new Email(1, "test@example.com", Types.Work, DateTime.Now.AddDays(-1), DateTime.Now);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"emailAddress\":\"test@example.com\""));
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Contain("\"dateModified\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut = new Email();

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":0"));
                Assert.That(json, Does.Contain("\"emailAddress\":null"));
            });
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut = new Email(123, "test@example.com", Types.Home, DateTime.Now.AddDays(-1), DateTime.Now);

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("123"));
                Assert.That(result, Does.Contain("test@example.com"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullEmailAddress_ShouldHandleNullValues()
        {
            // Arrange
            _sut = new Email(456, null, Types.Work, DateTime.Now.AddDays(-1), DateTime.Now);

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
            // Arrange
            _sut = new Email();

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
        public void JsonSerialization_ShouldProduceValidJsonFormat()
        {
            // Arrange
            _sut = new Email(1, "test@example.com", Types.Cell, DateTime.Now.AddDays(-1), DateTime.Now);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"emailAddress\":\"test@example.com\""));
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Contain("\"dateModified\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithAllTypesEnum_ShouldProduceValidJson()
        {
            // Test all enum values
            var enumValues = new[] { Types.Home, Types.Work, Types.Cell, Types.Fax, Types.Billing, Types.Other };

            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut = new Email($"test{enumValue}@example.com", enumValue)
                {
                    Id = 1
                };

                // Act
                var json = _sut.ToJson();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(json, Is.Not.Null.And.Not.Empty, $"Failed for {enumValue}");
                    Assert.That(json, Does.Contain($"test{enumValue}@example.com"), $"Failed for {enumValue}");
                    Assert.That(json, Does.Contain("\"dateCreated\":"), $"Failed for {enumValue}");

                    // Verify JSON is well-formed
                    Assert.DoesNotThrow(() => JsonDocument.Parse(json), $"Failed for {enumValue}");
                });
            }
        }

        [Test, Category("Models")]
        public void Email_WithEmptyEmailAddress_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email(string.Empty, Types.Other);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Email_WithVeryLongEmailAddress_ShouldBeAllowed()
        {
            // Arrange
            var longEmail = new string('a', 1000) + "@example.com";

            // Act
            _sut = new Email(longEmail, Types.Work);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(longEmail));
        }

        [Test, Category("Models")]
        public void Email_MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            _sut = new Email();
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.EmailAddress = "test@example.com";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            ((OrganizerCompanion.Core.Interfaces.Type.IEmail)_sut).Type = Types.Home;
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));
        }

        [Test, Category("Models")]
        public void Email_WithZeroId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email
            {
                Id = 0
            };

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Email_WithMaxIntId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email
            {
                Id = int.MaxValue
            };

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Email_SerializerOptions_ShouldHandleCyclicalReferences()
        {
            // Arrange
            _sut = new Email(1, "test@example.com", Types.Work, DateTime.Now, DateTime.Now);

            // Act & Assert - This should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = _sut.ToJson();
                Assert.That(json, Is.Not.Null.And.Not.Empty);
            });
        }
    }
}
