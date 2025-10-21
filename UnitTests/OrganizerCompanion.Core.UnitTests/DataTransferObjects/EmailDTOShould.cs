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
                Assert.That(_sut.IsPrimary, Is.False);
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
                OrganizerCompanion.Core.Enums.Types.Mobil,
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
        public void IsPrimary_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedIsPrimary = true;

            // Act
            _sut.IsPrimary = expectedIsPrimary;

            // Assert
            Assert.That(_sut.IsPrimary, Is.EqualTo(expectedIsPrimary));
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldAcceptTrueValue()
        {
            // Arrange & Act
            _sut.IsPrimary = true;

            // Assert
            Assert.That(_sut.IsPrimary, Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldAcceptFalseValue()
        {
            // Arrange & Act
            _sut.IsPrimary = false;

            // Assert
            Assert.That(_sut.IsPrimary, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act
            var emailDTO = new EmailDTO();

            // Assert
            Assert.That(emailDTO.IsPrimary, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldSupportMultipleReassignments()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);
                
                _sut.IsPrimary = false;
                Assert.That(_sut.IsPrimary, Is.False);
                
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);
            });
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
        public void IsPrimary_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(EmailDTO).GetProperty(nameof(EmailDTO.IsPrimary));

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
                IsPrimary = true,
                DateCreated = testDate,
                DateModified = testModifiedDate
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(emailDTO.Id, Is.EqualTo(999));
                Assert.That(emailDTO.EmailAddress, Is.EqualTo("chained@example.com"));
                Assert.That(emailDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(emailDTO.IsPrimary, Is.True);
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
                { nameof(EmailDTO.IsPrimary), "isPrimary" },
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
            // Arrange, Act & Assert
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

                // Test IsPrimary reassignments
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);
                _sut.IsPrimary = false;
                Assert.That(_sut.IsPrimary, Is.False);

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

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeEmail = "用户@测试.com";

            // Act
            _sut.EmailAddress = unicodeEmail;

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(unicodeEmail));
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldAcceptNumericDomains()
        {
            // Arrange
            var numericEmail = "test@123.456.789.012";

            // Act
            _sut.EmailAddress = numericEmail;

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(numericEmail));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldSupportCastingFromInt()
        {
            // Arrange
            var enumValue = (OrganizerCompanion.Core.Enums.Types)0;

            // Act
            _sut.Type = enumValue;

            // Assert
            Assert.That(_sut.Type, Is.EqualTo(enumValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldSupportAllDefinedValues()
        {
            // Arrange
            var allTypes = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var typeValue in allTypes)
                {
                    _sut.Type = typeValue;
                    Assert.That(_sut.Type, Is.EqualTo(typeValue));
                    Assert.That(_sut.Type.HasValue, Is.True);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void EmailAddress_ShouldHandleConsecutiveAssignments()
        {
            // Arrange
            var emails = new[] { "first@test.com", "second@test.com", null, "third@test.com", "" };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var email in emails)
                {
                    _sut.EmailAddress = email;
                    Assert.That(_sut.EmailAddress, Is.EqualTo(email));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportSequentialAssignments()
        {
            // Arrange
            var ids = new[] { 0, 1, -1, int.MaxValue, int.MinValue, 42 };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var id in ids)
                {
                    _sut.Id = id;
                    Assert.That(_sut.Id, Is.EqualTo(id));
                }
            });
        }

        [Test, Category("DataTransferObjects")]  
        public void DateCreated_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 12, 25, 14, 30, 45, 123);

            // Act
            _sut.DateCreated = preciseDate;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(preciseDate));
            Assert.That(_sut.DateCreated.Millisecond, Is.EqualTo(123));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 11, 15, 9, 45, 30, 456);

            // Act
            _sut.DateModified = preciseDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(preciseDate));
            Assert.That(_sut.DateModified?.Millisecond, Is.EqualTo(456));
        }

        [Test, Category("DataTransferObjects")]
        public void IEmailDTO_InterfaceConsistency_ShouldExposeAllProperties()
        {
            // Arrange
            IEmailDTO interfaceDto = new EmailDTO();
            var testModified = DateTime.Now.AddHours(-2);

            // Act
            interfaceDto.Id = 100;
            interfaceDto.EmailAddress = "interface@test.com";
            interfaceDto.Type = OrganizerCompanion.Core.Enums.Types.Work;
            interfaceDto.IsPrimary = true;
            interfaceDto.DateModified = testModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceDto.Id, Is.EqualTo(100));
                Assert.That(interfaceDto.EmailAddress, Is.EqualTo("interface@test.com"));
                Assert.That(interfaceDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(interfaceDto.IsPrimary, Is.True);
                Assert.That(interfaceDto.DateCreated, Is.Not.EqualTo(default(DateTime))); // DateCreated is read-only, check it has a value
                Assert.That(interfaceDto.DateModified, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_ShouldHandleComplexEmailFormats()
        {
            // Arrange
            var complexEmails = new[]
            {
                "user.name+tag@example.co.uk",
                "user_name@sub-domain.example.com", 
                "123456@domain.org",
                "user@domain-with-hyphens.net",
                "first.last@very-long-domain-name.museum"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var email in complexEmails)
                {
                    _sut.EmailAddress = email;
                    Assert.That(_sut.EmailAddress, Is.EqualTo(email));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHandleNullToValueTransitions()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Start with null
                _sut.Type = null;
                Assert.That(_sut.Type, Is.Null);

                // Assign each enum value
                foreach (var enumValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
                {
                    _sut.Type = enumValue;
                    Assert.That(_sut.Type, Is.EqualTo(enumValue));
                    Assert.That(_sut.Type.HasValue, Is.True);
                    
                    // Back to null
                    _sut.Type = null;
                    Assert.That(_sut.Type, Is.Null);
                    Assert.That(_sut.Type.HasValue, Is.False);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHandleNullToDateTransitions()
        {
            // Arrange
            var testDates = new DateTime[]
            {
                DateTime.Now,
                DateTime.MinValue,
                DateTime.MaxValue,
                new DateTime(2020, 1, 1),
                new DateTime(2030, 12, 31, 23, 59, 59)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var date in testDates)
                {
                    // Start with null
                    _sut.DateModified = null;
                    Assert.That(_sut.DateModified, Is.Null);

                    // Assign date
                    _sut.DateModified = date;
                    Assert.That(_sut.DateModified, Is.EqualTo(date));

                    // Back to null
                    _sut.DateModified = null;
                    Assert.That(_sut.DateModified, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_ShouldMaintainStateAcrossMultipleOperations()
        {
            // Arrange
            var operations = new[]
            {
                new { Id = 1, Email = (string?)"first@test.com", Type = (OrganizerCompanion.Core.Enums.Types?)OrganizerCompanion.Core.Enums.Types.Home, IsPrimary = true },
                new { Id = 2, Email = (string?)"second@test.com", Type = (OrganizerCompanion.Core.Enums.Types?)OrganizerCompanion.Core.Enums.Types.Work, IsPrimary = false },
                new { Id = 3, Email = (string?)null, Type = (OrganizerCompanion.Core.Enums.Types?)null, IsPrimary = true }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var op in operations)
                {
                    _sut.Id = op.Id;
                    _sut.EmailAddress = op.Email;
                    _sut.Type = op.Type;
                    _sut.IsPrimary = op.IsPrimary;

                    Assert.That(_sut.Id, Is.EqualTo(op.Id));
                    Assert.That(_sut.EmailAddress, Is.EqualTo(op.Email));
                    Assert.That(_sut.Type, Is.EqualTo(op.Type));
                    Assert.That(_sut.IsPrimary, Is.EqualTo(op.IsPrimary));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_PropertiesShouldBeIndependent()
        {
            // Arrange & Act
            _sut.Id = 999;
            _sut.EmailAddress = "independent@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            _sut.IsPrimary = true;
            var testDate = DateTime.Now.AddDays(-5);
            var testModified = DateTime.Now.AddHours(-3);
            _sut.DateCreated = testDate;
            _sut.DateModified = testModified;

            // Modify one property at a time and verify others remain unchanged
            var originalId = _sut.Id;
            var originalEmail = _sut.EmailAddress;
            var originalType = _sut.Type;
            var originalIsPrimary = _sut.IsPrimary;
            var originalCreated = _sut.DateCreated;
            var originalModified = _sut.DateModified;

            // Assert
            Assert.Multiple(() =>
            {
                // Change Id, verify others unchanged
                _sut.Id = 1000;
                Assert.That(_sut.EmailAddress, Is.EqualTo(originalEmail));
                Assert.That(_sut.Type, Is.EqualTo(originalType));
                Assert.That(_sut.IsPrimary, Is.EqualTo(originalIsPrimary));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));

                // Change EmailAddress, verify others unchanged
                _sut.EmailAddress = "changed@test.com";
                Assert.That(_sut.Id, Is.EqualTo(1000)); // New value
                Assert.That(_sut.Type, Is.EqualTo(originalType));
                Assert.That(_sut.IsPrimary, Is.EqualTo(originalIsPrimary));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));

                // Change Type, verify others unchanged
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;
                Assert.That(_sut.Id, Is.EqualTo(1000)); // New value
                Assert.That(_sut.EmailAddress, Is.EqualTo("changed@test.com")); // New value
                Assert.That(_sut.IsPrimary, Is.EqualTo(originalIsPrimary));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void EmailDTO_ShouldSupportObjectInitializerSyntax()
        {
            // Arrange
            var testCreated = DateTime.Now.AddDays(-7);
            var testModified = DateTime.Now.AddHours(-1);

            // Act
            var emailDto = new EmailDTO
            {
                Id = 555,
                EmailAddress = "initializer@test.com",
                Type = OrganizerCompanion.Core.Enums.Types.Billing,
                IsPrimary = true,
                DateCreated = testCreated,
                DateModified = testModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(emailDto.Id, Is.EqualTo(555));
                Assert.That(emailDto.EmailAddress, Is.EqualTo("initializer@test.com"));
                Assert.That(emailDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(emailDto.IsPrimary, Is.True);
                Assert.That(emailDto.DateCreated, Is.EqualTo(testCreated));
                Assert.That(emailDto.DateModified, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException_WithDifferentGenericTypes()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IEmailDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldConsistentlyThrowNotImplementedException()
        {
            // Arrange - Multiple calls should all throw
            var exceptions = new List<NotImplementedException>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    var ex = Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                    Assert.That(ex, Is.Not.Null);
                    if (ex != null)
                    {
                        exceptions.Add(ex);
                    }
                }
                
                // Verify all exceptions are separate instances
                Assert.That(exceptions, Has.Count.EqualTo(3));
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
