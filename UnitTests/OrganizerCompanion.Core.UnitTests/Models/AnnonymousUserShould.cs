using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AnnonymousUserShould
    {
        private AnnonymousUser _sut;
        private DateTime _testCreatedDate;
        private DateTime _testModifiedDate;

        [SetUp]
        public void SetUp()
        {
            _sut = new AnnonymousUser();
            _testCreatedDate = new DateTime(2023, 1, 1, 12, 0, 0);
            _testModifiedDate = new DateTime(2023, 1, 2, 12, 0, 0);
        }

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange, Act, & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.UserName, Is.EqualTo(string.Empty));
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.IsCast, Is.Null);
                Assert.That(_sut.CastId, Is.Null);
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            var testAccount = new SubAccount();
            _sut = new AnnonymousUser(
                id: 123,
                userName: "TestUser",
                account: testAccount,
                isCast: true,
                castId: 456,
                castType: "Organization",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.UserName, Is.EqualTo("TestUser"));
                Assert.That(_sut.Account, Is.EqualTo(testAccount));
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(456));
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
                Assert.That(_sut.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_CreatesObjectSuccessfully()
        {
            // This test verifies that the JsonConstructor creates objects successfully
            // with various parameter combinations, including edge cases.

            // Test with valid parameters
            Assert.DoesNotThrow(() => new AnnonymousUser(
                id: 1,
                userName: "TestUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            ));
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_SetsPropertiesCorrectly()
        {
            // Arrange
            var userName = "TestUser";
            var account = new SubAccount { Id = 123 };

            // Act
            var anonymousUser = new AnnonymousUser(userName, account);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(0)); // Default value
                Assert.That(anonymousUser.UserName, Is.EqualTo(userName));
                Assert.That(anonymousUser.Account, Is.EqualTo(account));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(0)); // Not set by this constructor
                Assert.That(anonymousUser.IsCast, Is.Null); // Default value
                Assert.That(anonymousUser.CastId, Is.Null); // Default value
                Assert.That(anonymousUser.CastType, Is.Null); // Default value
                Assert.That(anonymousUser.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_WithNullUserName_AcceptsNull()
        {
            // Arrange
            var account = new SubAccount { Id = 456 };

            // Act & Assert - Constructor should accept null userName (validation happens in property setter)
            Assert.DoesNotThrow(() =>
            {
                var anonymousUser = new AnnonymousUser(null!, account);
                Assert.That(anonymousUser.UserName, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_WithNullAccount_AcceptsNull()
        {
            // Arrange
            var userName = "TestUser";

            // Act & Assert - Constructor should accept null account
            Assert.DoesNotThrow(() =>
            {
                var anonymousUser = new AnnonymousUser(userName, null!);
                Assert.That(anonymousUser.Account, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_WithBothNull_AcceptsNull()
        {
            // Act & Assert - Constructor should accept both null values
            Assert.DoesNotThrow(() =>
            {
                var anonymousUser = new AnnonymousUser(null!, null!);
                Assert.Multiple(() =>
                {
                    Assert.That(anonymousUser.UserName, Is.Null);
                    Assert.That(anonymousUser.Account, Is.Null);
                });
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_WithEmptyUserName_AcceptsEmpty()
        {
            // Arrange
            var account = new SubAccount { Id = 789 };

            // Act & Assert - Constructor should accept empty userName (validation happens in property setter)
            Assert.DoesNotThrow(() =>
            {
                var anonymousUser = new AnnonymousUser(string.Empty, account);
                Assert.That(anonymousUser.UserName, Is.EqualTo(string.Empty));
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_WithLongUserName_AcceptsLongString()
        {
            // Arrange
            var longUserName = new string('A', 150); // 150 characters (exceeds max)
            var account = new SubAccount { Id = 999 };

            // Act & Assert - Constructor should accept long userName (validation happens in property setter)
            Assert.DoesNotThrow(() =>
            {
                var anonymousUser = new AnnonymousUser(longUserName, account);
                Assert.That(anonymousUser.UserName, Is.EqualTo(longUserName));
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = 123,
                UserName = "DTOTestUser",
                AccountId = 456,
                IsCast = true,
                CastId = 789,
                CastType = "Organization",
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(123));
                Assert.That(anonymousUser.UserName, Is.EqualTo("DTOTestUser"));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(456));
                Assert.That(anonymousUser.Account, Is.Null); // Not set by this constructor
                Assert.That(anonymousUser.IsCast, Is.True);
                Assert.That(anonymousUser.CastId, Is.EqualTo(789));
                Assert.That(anonymousUser.CastType, Is.EqualTo("Organization"));
                Assert.That(anonymousUser.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(anonymousUser.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_WithNullDTO_ThrowsNullReferenceException()
        {
            // Arrange
            IAnnonymousUserDTO? dto = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new AnnonymousUser(dto!));
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_WithNullOptionalProperties_SetsToNull()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = 111,
                UserName = "NullPropsUser",
                AccountId = 222,
                IsCast = null, // Nullable bool
                CastId = null, // Nullable int
                CastType = null, // Nullable string
                CreatedDate = _testCreatedDate,
                ModifiedDate = null // Nullable DateTime
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(111));
                Assert.That(anonymousUser.UserName, Is.EqualTo("NullPropsUser"));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(222));
                Assert.That(anonymousUser.IsCast, Is.Null);
                Assert.That(anonymousUser.CastId, Is.Null);
                Assert.That(anonymousUser.CastType, Is.Null);
                Assert.That(anonymousUser.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(anonymousUser.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_WithZeroValues_AcceptsZeroValues()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = 0,
                UserName = "ZeroUser", // Cannot be empty due to validation
                AccountId = 0,
                IsCast = false,
                CastId = 0,
                CastType = "",
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(0));
                Assert.That(anonymousUser.UserName, Is.EqualTo("ZeroUser"));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(0));
                Assert.That(anonymousUser.IsCast, Is.False);
                Assert.That(anonymousUser.CastId, Is.EqualTo(0));
                Assert.That(anonymousUser.CastType, Is.EqualTo(""));
                Assert.That(anonymousUser.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(anonymousUser.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_WithMaxValues_HandlesLargeNumbers()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = int.MaxValue,
                UserName = "MaxValueUser",
                AccountId = int.MaxValue,
                IsCast = true,
                CastId = int.MaxValue,
                CastType = "MaxValueType",
                CreatedDate = DateTime.MaxValue,
                ModifiedDate = DateTime.MaxValue
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(int.MaxValue));
                Assert.That(anonymousUser.UserName, Is.EqualTo("MaxValueUser"));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(int.MaxValue));
                Assert.That(anonymousUser.IsCast, Is.True);
                Assert.That(anonymousUser.CastId, Is.EqualTo(int.MaxValue));
                Assert.That(anonymousUser.CastType, Is.EqualTo("MaxValueType"));
                Assert.That(anonymousUser.CreatedDate, Is.EqualTo(DateTime.MaxValue));
                Assert.That(anonymousUser.ModifiedDate, Is.EqualTo(DateTime.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_PropertiesCanBeModifiedAfterConstruction()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = 333,
                UserName = "ModifiableUser",
                AccountId = 444,
                IsCast = false,
                CastId = 555,
                CastType = "InitialType",
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto)
            {
                // Modify properties after construction
                Id = 999,
                UserName = "ModifiedUser",
                AccountId = 888,
                IsCast = true,
                CastId = 777,
                CastType = "ModifiedType"
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(999));
                Assert.That(anonymousUser.UserName, Is.EqualTo("ModifiedUser"));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(888));
                Assert.That(anonymousUser.IsCast, Is.True);
                Assert.That(anonymousUser.CastId, Is.EqualTo(777));
                Assert.That(anonymousUser.CastType, Is.EqualTo("ModifiedType"));
                Assert.That(anonymousUser.ModifiedDate, Is.Not.EqualTo(_testModifiedDate)); // Should be updated by setters
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_WithInterfaceType_WorksCorrectly()
        {
            // Arrange
            IAnnonymousUserDTO dto = new AnnonymousUserDTO
            {
                Id = 666,
                UserName = "InterfaceUser",
                AccountId = 777,
                IsCast = true,
                CastId = 888,
                CastType = "InterfaceType",
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.Id, Is.EqualTo(666));
                Assert.That(anonymousUser.UserName, Is.EqualTo("InterfaceUser"));
                Assert.That(anonymousUser.AccountId, Is.EqualTo(777));
                Assert.That(anonymousUser.IsCast, Is.True);
                Assert.That(anonymousUser.CastId, Is.EqualTo(888));
                Assert.That(anonymousUser.CastType, Is.EqualTo("InterfaceType"));
                Assert.That(anonymousUser.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(anonymousUser.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void AnnonymousUserDTOConstructor_OnlyMapsSpecificProperties_DoesNotSetAccount()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = 999,
                UserName = "PropertyMappingUser",
                AccountId = 123, // This sets AccountId but not Account object
                IsCast = false,
                CastId = 456,
                CastType = "TestType",
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var anonymousUser = new AnnonymousUser(dto);

            // Assert - Verify that AccountId is set but Account object is not
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUser.AccountId, Is.EqualTo(123)); // AccountId is set from DTO
                Assert.That(anonymousUser.Account, Is.Null); // Account object is not created/set
                Assert.That(anonymousUser.Id, Is.EqualTo(999));
                Assert.That(anonymousUser.UserName, Is.EqualTo("PropertyMappingUser"));
                Assert.That(anonymousUser.IsCast, Is.False);
                Assert.That(anonymousUser.CastId, Is.EqualTo(456));
                Assert.That(anonymousUser.CastType, Is.EqualTo("TestType"));
            });
        }

        [Test, Category("Models")]
        public void UserName_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.UserName, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void UserName_Setter_UpdatesValueAndModifiedDate()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.UserName = "TestUser";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.EqualTo("TestUser"));
            });
            Assert.That(originalModifiedDate, Is.Null);
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void UserName_Setter_ThrowsException_WhenNull()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = null!);
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_ThrowsException_WhenEmpty()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = "");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_ThrowsException_WhenWhitespace()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = "   ");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_ThrowsException_WhenTooLong()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var longUserName = new string('A', 101); // 101 characters

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = longUserName);
            Assert.That(ex.Message, Does.Contain("User Name cannot exceed 100 characters."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_AcceptsMaxLength()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var maxLengthUserName = new string('A', 100); // 100 characters

            // Act & Assert
            Assert.DoesNotThrow(() => _sut.UserName = maxLengthUserName);
            Assert.That(_sut.UserName, Is.EqualTo(maxLengthUserName));
        }

        [Test, Category("Models")]
        public void UserName_Setter_UpdatesModifiedDate_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalModifiedDate = _sut.ModifiedDate;
            _sut.UserName = "FirstName";
            var firstUpdateTime = _sut.ModifiedDate;

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.UserName = "SecondName";
            var secondUpdateTime = _sut.ModifiedDate;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test, Category("Models")]
        public void Id_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Id_Setter_UpdatesValueAndModifiedDate()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(456));
                Assert.That(originalModifiedDate, Is.Null);
            });
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void IsCast_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.IsCast, Is.Null);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.CastId, Is.Null);
        }

        [Test, Category("Models")]
        public void CastId_Setter_UpdatesValueAndModifiedDate()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.CastId = 789;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastId, Is.EqualTo(789));
                Assert.That(originalModifiedDate, Is.Null);
            });
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void CastId_Setter_CanSetToZero()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                CastId = 123
            };

            // Act
            _sut.CastId = 0;

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void CastType_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.CastType, Is.Null);
        }

        [Test, Category("Models")]
        public void CastType_Setter_UpdatesValueAndModifiedDate()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.CastType = "Organization";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
                Assert.That(originalModifiedDate, Is.Null);
            });
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void CastType_Setter_CanSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                CastType = "Person"
            };

            // Act
            _sut.CastType = null;

            // Assert
            Assert.That(_sut.CastType, Is.Null);
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsCreatedDateFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            _sut = new AnnonymousUser(
                id: 123,
                userName: "TestUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: specificDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetAndRetrieved()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var testDate = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            _sut.ModifiedDate = testDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                ModifiedDate = null
            };

            // Assert
            Assert.That(_sut.ModifiedDate, Is.Null);
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 789,
                userName: "JsonTestUser",
                account: new SubAccount(),
                isCast: true,
                castId: 456,
                castType: "Organization",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
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
            Assert.That(json, Does.Contain("\"userName\":\"JsonTestUser\""));
            Assert.That(json, Does.Contain("\"createdDate\""));
            Assert.That(json, Does.Contain("\"modifiedDate\""));
            Assert.That(json, Does.Contain("\"isCast\":true"));
            Assert.That(json, Does.Contain("\"castId\":456"));
            Assert.That(json, Does.Contain("\"castType\":\"Organization\""));
        }

        [Test, Category("Models")]
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
            Assert.That(json, Does.Contain("\"userName\":\"\""));
            Assert.That(json, Does.Contain("\"createdDate\""));
            Assert.That(json, Does.Contain("\"modifiedDate\""));
        }

        [Test, Category("Models")]
        public void ToJson_HandlesNullModifiedDate()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: "NullModifiedUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: _testCreatedDate,
                modifiedDate: null
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
            Assert.That(json, Does.Contain("\"userName\":\"NullModifiedUser\""));
            Assert.That(json, Does.Contain("\"createdDate\""));
            Assert.That(json, Does.Contain("\"modifiedDate\":null"));
        }

        [Test, Category("Models")]
        public void ToJson_WithCastProperties_IncludesConvertedFields()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: "CastTestUser",
                account: new SubAccount(),
                isCast: true,
                castId: 456,
                castType: "Organization",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
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

            // Verify JSON contains cast properties
            Assert.That(json, Does.Contain("\"isCast\":true"));
            Assert.That(json, Does.Contain("\"userName\":\"CastTestUser\""));
            Assert.That(json, Does.Contain("\"castId\":456"));
            Assert.That(json, Does.Contain("\"castType\":\"Organization\""));
        }

        [Test, Category("Models")]
        public void ToJson_WithNullCastProperties_HandlesCorrectly()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
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

            // Verify JSON handles null cast properties correctly (should be omitted due to JsonIgnore condition)
            Assert.That(json, Does.Contain("\"isCast\""));
            Assert.That(json, Does.Contain("\"userName\":\"Test User\""));
            Assert.That(json, Does.Contain("\"castId\""));
            Assert.That(json, Does.Not.Contain("\"castType\""));
        }

        [Test, Category("Models")]
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

            // Act & Assert - IsCast
            _sut.IsCast = true;
            Assert.That(_sut.IsCast, Is.True);

            _sut.IsCast = false;
            Assert.That(_sut.IsCast, Is.False);

            // Act & Assert - CastId
            _sut.CastId = 300;
            Assert.That(_sut.CastId, Is.EqualTo(300));

            _sut.CastId = 400;
            Assert.That(_sut.CastId, Is.EqualTo(400));

            // Act & Assert - CastType
            _sut.CastType = "Organization";
            Assert.That(_sut.CastType, Is.EqualTo("Organization"));

            _sut.CastType = "Person";
            Assert.That(_sut.CastType, Is.EqualTo("Person"));

            // Act & Assert - ModifiedDate (CreatedDate is read-only)
            _sut.ModifiedDate = firstDate;
            Assert.That(_sut.ModifiedDate, Is.EqualTo(firstDate));

            _sut.ModifiedDate = secondDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ModifiedDate, Is.EqualTo(secondDate));

                // CreatedDate is set during construction and cannot be modified afterward
                Assert.That(_sut.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_ThrowsException_WhenNegative()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.ParamName, Is.EqualTo("Id"));
                Assert.That(ex.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_ThrowsException_WhenMinValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = int.MinValue);
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.ParamName, Is.EqualTo("Id"));
                Assert.That(ex.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_AcceptsZero()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.DoesNotThrow(() => _sut.Id = 0);
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Id_Setter_AcceptsMaxValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.DoesNotThrow(() => _sut.Id = int.MaxValue);
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void JsonConstructor_IsCast_WithTrueValue_SetsTrueCorrectly()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 1,
                userName: "Test",
                account: new SubAccount(),
                isCast: true,
                castId: 100,
                castType: "Test",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(_sut.IsCast, Is.True);
        }

        [Test, Category("Models")]
        public void JsonConstructor_IsCast_WithNullValue_SetsFalse()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 1,
                userName: "Test",
                account: new SubAccount(),
                isCast: null,
                castId: 100,
                castType: "Test",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(_sut.IsCast, Is.False);
        }

        [Test, Category("Models")]
        public void JsonConstructor_CastId_WithNullValue_SetsZero()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 1,
                userName: "Test",
                account: new SubAccount(),
                isCast: false,
                castId: null,
                castType: "Test",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void JsonConstructor_CastId_WithValidValue_SetsCorrectly()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 1,
                userName: "Test",
                account: new SubAccount(),
                isCast: true,
                castId: 500,
                castType: "Test",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(500));
        }

        [Test, Category("Models")]
        public void JsonConstructor_CastId_WithZeroValue_SetsZero()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 1,
                userName: "Test",
                account: new SubAccount(),
                isCast: true,
                castId: 0,
                castType: "Test",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void UserName_Setter_AcceptsMinLength()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var minLengthName = "A"; // 1 character

            // Act & Assert
            Assert.DoesNotThrow(() => _sut.UserName = minLengthName);
            Assert.That(_sut.UserName, Is.EqualTo(minLengthName));
        }

        [Test, Category("Models")]
        public void UserName_Setter_WithSingleSpace_ThrowsException()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = " ");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_WithTabs_ThrowsException()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = "\t\t");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_WithNewlines_ThrowsException()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = "\n\r");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test, Category("Models")]
        public void UserName_Setter_WithExactly100Characters_Succeeds()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var userName = new string('B', 100);

            // Act & Assert
            Assert.DoesNotThrow(() => _sut.UserName = userName);
            Assert.That(_sut.UserName, Is.EqualTo(userName));
        }

        [Test, Category("Models")]
        public void UserName_Setter_WithExactly101Characters_ThrowsException()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var userName = new string('C', 101);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = userName);
            Assert.That(ex.Message, Does.Contain("User Name cannot exceed 100 characters."));
        }

        [Test, Category("Models")]
        public void ToJson_WithSerializerOptions_HandlesCircularReferences()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                Id = 999,
                UserName = "CircularTest",
                Account = new SubAccount()
            };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToOrganization_WithEmptyUserName_CreatesOrganizationWithEmptyName()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: string.Empty,
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Act
            var organization = _sut.Cast<Organization>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization, Is.Not.Null);
                Assert.That(organization.OrganizationName, Is.EqualTo(string.Empty));
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUser_WithEmptyUserName_CreatesUserWithEmptyName()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 200,
                userName: string.Empty,
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Act
            var user = _sut.Cast<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user, Is.Not.Null);
                Assert.That(user.UserName, Is.EqualTo(string.Empty));
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastType, Is.EqualTo("User"));
            });
        }

        [Test, Category("Models")]
        public void Cast_MultipleTimes_UpdatesCastPropertiesEachTime()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 300,
                userName: "MultiCastUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Act & Assert - First cast to Organization
            var org = _sut.Cast<Organization>();
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(org.Id));
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
            });

            // Act & Assert - Second cast to User
            var user = _sut.Cast<User>();
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(user.Id));
                Assert.That(_sut.CastType, Is.EqualTo("User"));
            });
        }

        [Test, Category("Models")]
        public void ImplementsIAnnonymousUser()
        {
            // Arrange & Act
            var user = new AnnonymousUser();

            // Assert
            Assert.That(user, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser>());
        }

        [Test, Category("Models")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Arrange & Act
            var user = new AnnonymousUser();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user.GetType(), Is.EqualTo(typeof(AnnonymousUser)));
                Assert.That(user.GetType().Name, Is.EqualTo("AnnonymousUser"));
                Assert.That(user.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.Domain"));
            });
        }

        [Test, Category("Models")]
        public void CreatedDate_PropertyInfo_IsReadOnly()
        {
            // Arrange & Act
            var property = typeof(AnnonymousUser).GetProperty(nameof(AnnonymousUser.CreatedDate));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.GetSetMethod(), Is.Null); // No public setter
            });
        }

        [Test, Category("Models")]
        public void AllProperties_GettersAndSetters_WorkCorrectly()
        {
            // Arrange
            var user = new AnnonymousUser();
            var testAccount = new SubAccount();
            var testDate = new DateTime(2025, 5, 15, 10, 30, 45);

            // Act & Assert - Test all property setters and getters
            user.Id = 12345;
            Assert.That(user.Id, Is.EqualTo(12345));

            user.UserName = "ComprehensiveTest";
            Assert.That(user.UserName, Is.EqualTo("ComprehensiveTest"));

            user.AccountId = 67890;
            Assert.That(user.AccountId, Is.EqualTo(67890));

            user.Account = testAccount;
            Assert.That(user.Account, Is.SameAs(testAccount));

            user.IsCast = true;
            Assert.That(user.IsCast, Is.True);

            user.CastId = 54321;
            Assert.That(user.CastId, Is.EqualTo(54321));

            user.CastType = "TestType";
            Assert.That(user.CastType, Is.EqualTo("TestType"));

            user.ModifiedDate = testDate;
            Assert.Multiple(() =>
            {
                Assert.That(user.ModifiedDate, Is.EqualTo(testDate));
                Assert.That(user.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithUserNameAndAccount_CreatesValidObject()
        {
            // Arrange
            var userName = "ConstructorTest";
            var account = new SubAccount { Id = 999 };

            // Act
            var user = new AnnonymousUser(userName, account);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user.UserName, Is.EqualTo(userName));
                Assert.That(user.Account, Is.SameAs(account));
                Assert.That(user.Id, Is.EqualTo(0));
                Assert.That(user.AccountId, Is.EqualTo(0));
                Assert.That(user.IsCast, Is.Null); // Default value
                Assert.That(user.CastId, Is.Null); // Default value
                Assert.That(user.CastType, Is.Null); // Default value
                Assert.That(user.CreatedDate, Is.Not.EqualTo(default(DateTime)));
                Assert.That(user.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_MapsAllProperties_Correctly()
        {
            // Arrange
            var dto = new AnnonymousUserDTO
            {
                Id = 888,
                UserName = "DTOMappingTest",
                AccountId = 777,
                IsCast = true,
                CastId = 666,
                CastType = "MappingTest",
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var user = new AnnonymousUser(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user.Id, Is.EqualTo(888));
                Assert.That(user.UserName, Is.EqualTo("DTOMappingTest"));
                Assert.That(user.AccountId, Is.EqualTo(777));
                Assert.That(user.Account, Is.Null); // Not set by this constructor
                Assert.That(user.IsCast, Is.True);
                Assert.That(user.CastId, Is.EqualTo(666));
                Assert.That(user.CastType, Is.EqualTo("MappingTest"));
                Assert.That(user.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(user.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_EdgeCases_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 1,
                userName: "EdgeCaseUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                createdDate: DateTime.MinValue,
                modifiedDate: DateTime.MaxValue
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(1));
                Assert.That(_sut.UserName, Is.EqualTo("EdgeCaseUser"));
                Assert.That(_sut.Account, Is.Not.Null);
                Assert.That(_sut.IsCast, Is.False);
                Assert.That(_sut.CastId, Is.EqualTo(0));
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.CreatedDate, Is.EqualTo(DateTime.MinValue));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(DateTime.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.UserName = "TestUser123";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:123"));
                Assert.That(result, Does.Contain("UserNameTestUser123"));
                Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.AnnonymousUser"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithDefaultValues_ReturnsCorrectFormat()
        {
            // Arrange - Use default values
            _sut = new AnnonymousUser();

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:0"));
                Assert.That(result, Does.Contain("UserName"));
                Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.AnnonymousUser"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithMaxValues_ReturnsCorrectFormat()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.UserName = new string('A', 100); // Max length

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain($"Id:{int.MaxValue}"));
                Assert.That(result, Does.Contain("UserName"));
                Assert.That(result!.Length, Is.GreaterThan(100)); // Should contain more than just username
            });
        }

        #region Additional Coverage Tests

        [Test, Category("Models")]
        public void AnnonymousUser_ComprehensiveCoverageVerification()
        {
            // This test verifies comprehensive coverage of all major functionality
            // in the AnnonymousUser class including edge cases and different scenarios.

            // Test default constructor
            var defaultUser = new AnnonymousUser();
            Assert.That(defaultUser, Is.Not.Null);

            // Test JsonConstructor with various parameter combinations
            var parameterizedUser = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                createdDate: DateTime.Now,
                modifiedDate: DateTime.Now,
                isCast: true,
                castId: 456,
                castType: "TestType"
            );
            Assert.That(parameterizedUser, Is.Not.Null);

            // Test all property setters and getters
            defaultUser.Id = 789;
            defaultUser.AccountId = 101112;
            defaultUser.Account = new SubAccount();
            defaultUser.IsCast = true;
            defaultUser.CastId = 131415;
            defaultUser.CastType = "CoverageTest";
            defaultUser.ModifiedDate = DateTime.Now;

            Assert.Multiple(() =>
            {
                Assert.That(defaultUser.Id, Is.EqualTo(789));
                Assert.That(defaultUser.AccountId, Is.EqualTo(101112));
                Assert.That(defaultUser.Account, Is.Not.Null);
                Assert.That(defaultUser.IsCast, Is.True);
                Assert.That(defaultUser.CastId, Is.EqualTo(131415));
                Assert.That(defaultUser.CastType, Is.EqualTo("CoverageTest"));
                Assert.That(defaultUser.ModifiedDate, Is.Not.Null);
                Assert.That(defaultUser.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            });

            // Test explicit interface implementation
            var interfaceRef = (OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser)defaultUser;
            var testAccount = new SubAccount();
            interfaceRef.Account = testAccount;
            Assert.That(interfaceRef.Account, Is.SameAs(testAccount));

            // Test Cast methods for all supported types
            var orgCast = defaultUser.Cast<Organization>();
            var userCast = defaultUser.Cast<User>();
            var dtoCast = defaultUser.Cast<AnnonymousUserDTO>();

            Assert.Multiple(() =>
            {
                Assert.That(orgCast, Is.InstanceOf<Organization>());
                Assert.That(userCast, Is.InstanceOf<User>());
                Assert.That(dtoCast, Is.InstanceOf<AnnonymousUserDTO>());
            });

            // Test ToJson functionality
            var json = defaultUser.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);

            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultUser.Cast<Account>());
        }

        #endregion
    }
}
