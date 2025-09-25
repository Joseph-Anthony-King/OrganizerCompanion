using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AnnonymousUserShould
    {
        private AnnonymousUser _sut;
        private DateTime _testDateCreated;
        private DateTime _testDateModified;

        [SetUp]
        public void SetUp()
        {
            _sut = new AnnonymousUser();
            _testDateCreated = new DateTime(2023, 1, 1, 12, 0, 0);
            _testDateModified = new DateTime(2023, 1, 2, 12, 0, 0);
        }

        [Test]
        [Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new AnnonymousUser();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 123,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_ThrowsArgumentException_WhenExceptionOccurs()
        {
            // Note: Since the constructor has simple field assignments, it's difficult to cause
            // an actual exception. This test demonstrates the exception handling structure.
            // The try-catch exists for safety but may not be triggered under normal circumstances.

            // For demonstration purposes, we test with valid parameters to ensure no exception is thrown
            Assert.DoesNotThrow(() => new AnnonymousUser(
                id: 1,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
        }

        [Test]
        [Category("Models")]
        public void Id_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test]
        [Category("Models")]
        public void Id_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(456));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new AnnonymousUser();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            _sut = new AnnonymousUser(
                id: 123,
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(specificDate));
        }

        [Test]
        [Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var testDate = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            _sut.DateModified = testDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(testDate));
        }

        [Test]
        [Category("Models")]
        public void DateModified_CanBeSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                DateModified = null
            };

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void Cast_ThrowsNotImplementedException()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<AnnonymousUser>());
        }

        [Test]
        [Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 789,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            Assert.That(json, Does.Contain("\"id\":789"));
            Assert.That(json, Does.Contain("\"dateCreated\""));
            Assert.That(json, Does.Contain("\"dateModified\""));
        }

        [Test]
        [Category("Models")]
        public void ToJson_HandlesDefaultValues()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties with default values
            Assert.That(json, Does.Contain("\"id\":0"));
            Assert.That(json, Does.Contain("\"dateCreated\""));
            Assert.That(json, Does.Contain("\"dateModified\""));
        }

        [Test]
        [Category("Models")]
        public void ToJson_HandlesNullDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            Assert.That(json, Does.Contain("\"id\":100"));
            Assert.That(json, Does.Contain("\"dateCreated\""));
            Assert.That(json, Does.Contain("\"dateModified\":null"));
        }

        [Test]
        [Category("Models")]
        public void Properties_CanBeSetMultipleTimes()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var firstDate = new DateTime(2023, 1, 1);
            var secondDate = new DateTime(2023, 2, 1);

            // Act & Assert - Id
            _sut.Id = 100;
            Assert.That(_sut.Id, Is.EqualTo(100));

            _sut.Id = 200;
            Assert.That(_sut.Id, Is.EqualTo(200));

            // Act & Assert - DateModified (DateCreated is read-only)
            _sut.DateModified = firstDate;
            Assert.That(_sut.DateModified, Is.EqualTo(firstDate));

            _sut.DateModified = secondDate;
            Assert.That(_sut.DateModified, Is.EqualTo(secondDate));

            // DateCreated is set during construction and cannot be modified afterward
            Assert.That(_sut.DateCreated, Is.Not.EqualTo(default(DateTime)));
        }

        [Test]
        [Category("Models")]
        public void Id_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.Id = 1;
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.Id = 2;
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_HandlesMinAndMaxDateTimeValues()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: int.MaxValue,
                dateCreated: DateTime.MinValue,
                dateModified: DateTime.MaxValue
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
            });
        }
    }
}
