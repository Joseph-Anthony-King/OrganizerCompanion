using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class EmailDTOShould
    {
        private EmailDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new EmailDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateEmailDTOWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new EmailDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.Null);
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
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
        public void Id_ShouldAcceptZeroValue()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptPositiveValues()
        {
            // Arrange
            int[] testValues = { 1, 100, 999, int.MaxValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (int value in testValues)
                {
                    _sut.Id = value;
                    Assert.That(_sut.Id, Is.EqualTo(value));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptNegativeValues()
        {
            // Arrange
            int negativeValue = -123;

            // Act
            _sut.Id = negativeValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(negativeValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportMinValue()
        {
            // Arrange & Act
            _sut.Id = int.MinValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedEmailAddress = "test@example.com";

            // Act
            _sut.EmailAddress = expectedEmailAddress;

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(expectedEmailAddress));
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.EmailAddress = null;

            // Assert
            Assert.That(_sut.EmailAddress, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.EmailAddress = "";

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptVariousEmailFormats()
        {
            // Arrange
            var emailFormats = new[]
            {
                "user@domain.com",
                "user.name@domain.com",
                "user+tag@domain.com",
                "user@sub.domain.com",
                "123@domain.com",
                "user@domain.co.uk"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var email in emailFormats)
                {
                    _sut.EmailAddress = email;
                    Assert.That(_sut.EmailAddress, Is.EqualTo(email));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldGetAndSetValue()
        {
            // Arrange
            OrganizerCompanion.Core.Enums.Types expectedType = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            _sut.Type = expectedType;

            // Assert
            Assert.That(_sut.Type, Is.EqualTo(expectedType));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Type = null;

            // Assert
            Assert.That(_sut.Type, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptAllEnumValues()
        {
            // Arrange
            var enumValues = new[] { 
                OrganizerCompanion.Core.Enums.Types.Home,
                OrganizerCompanion.Core.Enums.Types.Work,
                OrganizerCompanion.Core.Enums.Types.Cell,
                OrganizerCompanion.Core.Enums.Types.Fax,
                OrganizerCompanion.Core.Enums.Types.Billing,
                OrganizerCompanion.Core.Enums.Types.Other
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var enumValue in enumValues)
                {
                    _sut.Type = enumValue;
                    Assert.That(_sut.Type, Is.EqualTo(enumValue));
                }
            });
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
        public void DateCreated_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = DateTime.Now.AddDays(-1);

            // Act
            _sut.DateCreated = expectedDate;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_DefaultValue_ShouldBeCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var emailDTO = new EmailDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.That(emailDTO.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = DateTime.Now.AddHours(-2);

            // Act
            _sut.DateModified = expectedDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedDate));
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
        public void DateModified_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var emailDTO = new EmailDTO();

            // Assert
            Assert.That(emailDTO.DateModified, Is.Null);
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
            var property = typeof(EmailDTO).GetProperty(nameof(EmailDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(EmailDTO).GetProperty(nameof(EmailDTO.EmailAddress));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(EmailDTO).GetProperty(nameof(EmailDTO.Type));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(EmailDTO).GetProperty(nameof(EmailDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(EmailDTO).GetProperty(nameof(EmailDTO.DateModified));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_ShouldImplementIEmailDTO()
        {
            // Arrange & Act
            var emailDTO = new EmailDTO();

            // Assert
            Assert.That(emailDTO, Is.InstanceOf<IEmailDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var emailDTO = new EmailDTO();

            // Assert
            Assert.That(emailDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange
            var testDate = DateTime.Now.AddDays(-1);
            var testModifiedDate = DateTime.Now.AddHours(-2);

            // Act
            var emailDTO = new EmailDTO
            {
                Id = 999,
                EmailAddress = "chained@example.com",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                DateCreated = testDate,
                DateModified = testModifiedDate
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(emailDTO.Id, Is.EqualTo(999));
                Assert.That(emailDTO.EmailAddress, Is.EqualTo("chained@example.com"));
                Assert.That(emailDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(emailDTO.DateCreated, Is.EqualTo(testDate));
                Assert.That(emailDTO.DateModified, Is.EqualTo(testModifiedDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { nameof(EmailDTO.Id), "id" },
                { nameof(EmailDTO.EmailAddress), "emailAddress" },
                { nameof(EmailDTO.Type), "type" },
                { nameof(EmailDTO.DateCreated), "dateCreated" },
                { nameof(EmailDTO.DateModified), "dateModified" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var kvp in properties)
                {
                    var property = typeof(EmailDTO).GetProperty(kvp.Key);
                    var jsonAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                    Assert.That(jsonAttribute, Is.Not.Null, $"Property {kvp.Key} should have JsonPropertyName attribute");
                    Assert.That(jsonAttribute?.Name, Is.EqualTo(kvp.Value), $"Property {kvp.Key} should have JsonPropertyName '{kvp.Value}'");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Interface_Properties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var properties = new[]
            {
                nameof(EmailDTO.IsCast),
                nameof(EmailDTO.CastId),
                nameof(EmailDTO.CastType)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var propertyName in properties)
                {
                    var property = typeof(EmailDTO).GetProperty(propertyName);
                    var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                    Assert.That(jsonIgnoreAttribute, Is.Not.Null, $"Property {propertyName} should have JsonIgnore attribute");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptLongString()
        {
            // Arrange
            var longEmail = new string('a', 100) + "@" + new string('b', 100) + ".com";

            // Act
            _sut.EmailAddress = longEmail;

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(longEmail));
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialEmail = "user+test.email@domain-name.co.uk";

            // Act
            _sut.EmailAddress = specialEmail;

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(specialEmail));
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptWhitespace()
        {
            // Arrange
            var whitespaceEmail = " test@example.com ";

            // Act
            _sut.EmailAddress = whitespaceEmail;

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(whitespaceEmail));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldBeNullableEnum()
        {
            // Arrange & Act
            _sut.Type = null;
            var isNull = _sut.Type == null;

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(isNull, Is.True);
                Assert.That(_sut.Type.HasValue, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveValueWhenAssigned()
        {
            // Arrange & Act
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type.HasValue, Is.True);
                Assert.That(_sut.Type.Value, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Properties_ShouldAllowMultipleReassignments()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                // Test Id reassignments
                _sut.Id = 1;
                Assert.That(_sut.Id, Is.EqualTo(1));
                _sut.Id = 2;
                Assert.That(_sut.Id, Is.EqualTo(2));

                // Test EmailAddress reassignments
                _sut.EmailAddress = "first@example.com";
                Assert.That(_sut.EmailAddress, Is.EqualTo("first@example.com"));
                _sut.EmailAddress = "second@example.com";
                Assert.That(_sut.EmailAddress, Is.EqualTo("second@example.com"));

                // Test Type reassignments
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));

                // Test DateCreated reassignments
                var firstDate = DateTime.Now.AddDays(-1);
                var secondDate = DateTime.Now.AddDays(-2);
                _sut.DateCreated = firstDate;
                Assert.That(_sut.DateCreated, Is.EqualTo(firstDate));
                _sut.DateCreated = secondDate;
                Assert.That(_sut.DateCreated, Is.EqualTo(secondDate));

                // Test DateModified reassignments
                var firstModified = DateTime.Now.AddHours(-1);
                var secondModified = DateTime.Now.AddHours(-2);
                _sut.DateModified = firstModified;
                Assert.That(_sut.DateModified, Is.EqualTo(firstModified));
                _sut.DateModified = secondModified;
                Assert.That(_sut.DateModified, Is.EqualTo(secondModified));
                _sut.DateModified = null;
                Assert.That(_sut.DateModified, Is.Null);
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
        #endregion
    }
}
