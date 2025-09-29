using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class PasswordShould
    {
        private Password _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Password();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreatePasswordWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Password();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.PasswordValue, Is.Null);
                Assert.That(_sut.PasswordHint, Is.Null);
                Assert.That(_sut.PreviousPasswords, Is.Not.Null);
                Assert.That(_sut.PreviousPasswords, Is.Empty);
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreatePasswordWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var passwordValue = "SecurePassword123!";
            var passwordHint = "My favorite password";
            var previousPasswords = new List<string> { "OldPassword1", "OldPassword2" };
            var accountId = 456;
            IAccount? account = null; // Using null as we don't have concrete implementation
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var password = new Password(
                id, 
                passwordValue, 
                passwordHint, 
                previousPasswords, 
                accountId, 
                account, 
                dateCreated, 
                dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(password.Id, Is.EqualTo(id));
                Assert.That(password.PasswordValue, Is.EqualTo(passwordValue));
                Assert.That(password.PasswordHint, Is.EqualTo(passwordHint));
                Assert.That(password.PreviousPasswords, Is.Not.Null);
                Assert.That(password.PreviousPasswords.Count, Is.EqualTo(2));
                Assert.That(password.PreviousPasswords, Contains.Item("OldPassword1"));
                Assert.That(password.AccountId, Is.EqualTo(accountId));
                Assert.That(password.Account, Is.EqualTo(account));
                Assert.That(password.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(password.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_ShouldCreatePasswordWithAccountInfo()
        {
            // Arrange
            var passwordValue = "MyPassword123";
            var passwordHint = "Remember this one";
            IAccount? account = null; // Using null as we don't have concrete implementation
            var beforeCreation = DateTime.Now;

            // Act
            var password = new Password(passwordValue, passwordHint, account!);
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(password.PasswordValue, Is.EqualTo(passwordValue));
                Assert.That(password.PasswordHint, Is.EqualTo(passwordHint));
                Assert.That(password.PreviousPasswords, Is.Not.Null);
                Assert.That(password.PreviousPasswords, Is.Empty);
                Assert.That(password.Account, Is.EqualTo(account));
                Assert.That(password.AccountId, Is.EqualTo(0)); // Should be 0 when account is null
                Assert.That(password.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(password.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newId = 999;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = newId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(newId));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void PasswordValue_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newPassword = "NewSecurePassword456!";
            var beforeSet = DateTime.Now;

            // Act
            _sut.PasswordValue = newPassword;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PasswordValue, Is.EqualTo(newPassword));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void PasswordValue_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.PasswordValue = "InitialPassword";
            var beforeSet = DateTime.Now;

            // Act
            _sut.PasswordValue = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PasswordValue, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WhenPasswordValueIsSet_ShouldAddToHistory()
        {
            // Arrange
            var firstPassword = "FirstPassword123!";
            var secondPassword = "SecondPassword456!";

            // Act
            _sut.PasswordValue = firstPassword;
            _sut.PasswordValue = secondPassword;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(2));
                Assert.That(_sut.PreviousPasswords, Contains.Item(firstPassword));
                Assert.That(_sut.PreviousPasswords, Contains.Item(secondPassword));
                Assert.That(_sut.PreviousPasswords[0], Is.EqualTo(firstPassword));
                Assert.That(_sut.PreviousPasswords[1], Is.EqualTo(secondPassword));
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WhenPasswordValueSetToNull_ShouldAddNullToHistory()
        {
            // Arrange
            var initialPassword = "InitialPassword123!";

            // Act
            _sut.PasswordValue = initialPassword;
            _sut.PasswordValue = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(2));
                Assert.That(_sut.PreviousPasswords[0], Is.EqualTo(initialPassword));
                Assert.That(_sut.PreviousPasswords[1], Is.Null);
            });
        }

        [Test, Category("Models")]
        public void PasswordValue_WhenSetToSameAsPrevious_ShouldThrowArgumentException()
        {
            // Arrange
            var password = "RepeatedPassword123!";
            _sut.PasswordValue = password;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.PasswordValue = password);
            Assert.That(exception.Message, Does.Contain("New password cannot be the same as any previous passwords"));
        }

        [Test, Category("Models")]
        public void PasswordValue_WhenSetToSameAsEarlierPassword_ShouldThrowArgumentException()
        {
            // Arrange
            var firstPassword = "FirstPassword123!";
            var secondPassword = "SecondPassword456!";
            
            _sut.PasswordValue = firstPassword;
            _sut.PasswordValue = secondPassword;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.PasswordValue = firstPassword);
            Assert.That(exception.Message, Does.Contain("New password cannot be the same as any previous passwords"));
        }

        [Test, Category("Models")]
        public void PreviousPasswords_ShouldBeReadOnlyList()
        {
            // Arrange
            var password = "TestPassword123!";
            _sut.PasswordValue = password;

            // Act
            var previousPasswords = _sut.PreviousPasswords;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(previousPasswords, Is.Not.Null);
                Assert.That(previousPasswords, Has.Count.EqualTo(1));
                Assert.That(previousPasswords[0], Is.EqualTo(password));
                
                // Verify it's the same reference (not a copy)
                Assert.That(ReferenceEquals(previousPasswords, _sut.PreviousPasswords), Is.True);
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_MultiplePasswordChanges_ShouldMaintainOrder()
        {
            // Arrange
            var passwords = new[] { "Password1!", "Password2!", "Password3!", "Password4!" };

            // Act
            foreach (var password in passwords)
            {
                _sut.PasswordValue = password;
            }

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(4));
                for (int i = 0; i < passwords.Length; i++)
                {
                    Assert.That(_sut.PreviousPasswords[i], Is.EqualTo(passwords[i]), $"Password at index {i} should match");
                }
            });
        }

        [Test, Category("Models")]
        public void PasswordHint_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newHint = "Think of your pet's name";
            var beforeSet = DateTime.Now;

            // Act
            _sut.PasswordHint = newHint;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PasswordHint, Is.EqualTo(newHint));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void PasswordHint_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.PasswordHint = "Initial hint";
            var beforeSet = DateTime.Now;

            // Act
            _sut.PasswordHint = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PasswordHint, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void AccountId_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newAccountId = 789;
            var beforeSet = DateTime.Now;

            // Act
            _sut.AccountId = newAccountId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.AccountId, Is.EqualTo(newAccountId));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Account_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            IAccount? newAccount = null; // Using null as we don't have concrete implementation
            var beforeSet = DateTime.Now;

            // Act
            _sut.Account = newAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(newAccount));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var password = new Password();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(password.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(password.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-10);

            // Act
            var password = new Password(1, "test", "hint", [], 1, null, specificDate, null);

            // Assert
            Assert.That(password.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            _sut.DateModified = dateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
        }

        [Test, Category("Models")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<Password>());
        }

        [Test, Category("Models")]
        public void IsCast_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("Models")]
        public void IsCast_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("Models")]
        public void CastId_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("Models")]
        public void CastType_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "SomeType");
        }

        [Test, Category("Models")]
        public void Password_WithEmptyPasswordValue_ShouldBeAllowed()
        {
            // Act
            _sut.PasswordValue = string.Empty;

            // Assert
            Assert.That(_sut.PasswordValue, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Password_WithEmptyPasswordHint_ShouldBeAllowed()
        {
            // Act
            _sut.PasswordHint = string.Empty;

            // Assert
            Assert.That(_sut.PasswordHint, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Password_WithVeryLongPasswordValue_ShouldBeAllowed()
        {
            // Arrange
            var longPassword = new string('A', 1000);

            // Act
            _sut.PasswordValue = longPassword;

            // Assert
            Assert.That(_sut.PasswordValue, Is.EqualTo(longPassword));
        }

        [Test, Category("Models")]
        public void Password_WithVeryLongPasswordHint_ShouldBeAllowed()
        {
            // Arrange
            var longHint = new string('H', 1000);

            // Act
            _sut.PasswordHint = longHint;

            // Assert
            Assert.That(_sut.PasswordHint, Is.EqualTo(longHint));
        }

        [Test, Category("Models")]
        public void Password_MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.PasswordValue = "NewPassword";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.PasswordHint = "New Hint";
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.AccountId = 123;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.Account = null;
            var fifthModified = _sut.DateModified;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));
        }

        [Test, Category("Models")]
        public void Password_WithZeroId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Password_WithMaxIntId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Password_WithNegativeId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = -1;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void Password_WithZeroAccountId_ShouldBeAllowed()
        {
            // Act
            _sut.AccountId = 0;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Password_WithMaxIntAccountId_ShouldBeAllowed()
        {
            // Act
            _sut.AccountId = int.MaxValue;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Password_WithNegativeAccountId_ShouldBeAllowed()
        {
            // Act
            _sut.AccountId = -1;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void Password_SpecialCharactersInPasswordValue_ShouldBeAllowed()
        {
            // Arrange
            var specialPassword = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";

            // Act
            _sut.PasswordValue = specialPassword;

            // Assert
            Assert.That(_sut.PasswordValue, Is.EqualTo(specialPassword));
        }

        [Test, Category("Models")]
        public void Password_SpecialCharactersInPasswordHint_ShouldBeAllowed()
        {
            // Arrange
            var specialHint = "Hint with special chars: !@#$%^&*()";

            // Act
            _sut.PasswordHint = specialHint;

            // Assert
            Assert.That(_sut.PasswordHint, Is.EqualTo(specialHint));
        }

        [Test, Category("Models")]
        public void Password_UnicodeCharactersInPasswordValue_ShouldBeAllowed()
        {
            // Arrange
            var unicodePassword = "密码123ñáéíóú";

            // Act
            _sut.PasswordValue = unicodePassword;

            // Assert
            Assert.That(_sut.PasswordValue, Is.EqualTo(unicodePassword));
        }

        [Test, Category("Models")]
        public void Password_UnicodeCharactersInPasswordHint_ShouldBeAllowed()
        {
            // Arrange
            var unicodeHint = "Pista: contraseña en español";

            // Act
            _sut.PasswordHint = unicodeHint;

            // Assert
            Assert.That(_sut.PasswordHint, Is.EqualTo(unicodeHint));
        }

        [Test, Category("Models")]
        public void Password_WhitespaceInPasswordValue_ShouldBeAllowed()
        {
            // Arrange
            var passwordWithSpaces = "My Password With Spaces";

            // Act
            _sut.PasswordValue = passwordWithSpaces;

            // Assert
            Assert.That(_sut.PasswordValue, Is.EqualTo(passwordWithSpaces));
        }

        [Test, Category("Models")]
        public void Password_WhitespaceInPasswordHint_ShouldBeAllowed()
        {
            // Arrange
            var hintWithSpaces = "This is a hint with spaces";

            // Act
            _sut.PasswordHint = hintWithSpaces;

            // Assert
            Assert.That(_sut.PasswordHint, Is.EqualTo(hintWithSpaces));
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WhenPasswordSetToEmptyString_ShouldAddToHistory()
        {
            // Arrange
            var firstPassword = "ValidPassword123!";
            var emptyPassword = string.Empty;

            // Act
            _sut.PasswordValue = firstPassword;
            _sut.PasswordValue = emptyPassword;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(2));
                Assert.That(_sut.PreviousPasswords[0], Is.EqualTo(firstPassword));
                Assert.That(_sut.PreviousPasswords[1], Is.EqualTo(emptyPassword));
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WhenEmptyStringPasswordRepeated_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyPassword = string.Empty;
            _sut.PasswordValue = emptyPassword;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _sut.PasswordValue = emptyPassword);
            Assert.That(exception.Message, Does.Contain("New password cannot be the same as any previous passwords"));
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WithSpecialCharactersInPassword_ShouldTrackCorrectly()
        {
            // Arrange
            var specialPassword1 = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";
            var specialPassword2 = "Password with émojis: 🔒🔑";

            // Act
            _sut.PasswordValue = specialPassword1;
            _sut.PasswordValue = specialPassword2;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(2));
                Assert.That(_sut.PreviousPasswords[0], Is.EqualTo(specialPassword1));
                Assert.That(_sut.PreviousPasswords[1], Is.EqualTo(specialPassword2));
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WithVeryLongPasswords_ShouldTrackCorrectly()
        {
            // Arrange
            var longPassword1 = new string('A', 1000);
            var longPassword2 = new string('B', 1500);

            // Act
            _sut.PasswordValue = longPassword1;
            _sut.PasswordValue = longPassword2;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(2));
                Assert.That(_sut.PreviousPasswords[0], Is.EqualTo(longPassword1));
                Assert.That(_sut.PreviousPasswords[1], Is.EqualTo(longPassword2));
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_CaseSensitivePasswords_ShouldBeTreatedAsDifferent()
        {
            // Arrange
            var lowerCasePassword = "password123!";
            var upperCasePassword = "PASSWORD123!";
            var mixedCasePassword = "Password123!";

            // Act - All should be allowed as they are different
            _sut.PasswordValue = lowerCasePassword;
            _sut.PasswordValue = upperCasePassword;
            _sut.PasswordValue = mixedCasePassword;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PreviousPasswords, Has.Count.EqualTo(3));
                Assert.That(_sut.PreviousPasswords[0], Is.EqualTo(lowerCasePassword));
                Assert.That(_sut.PreviousPasswords[1], Is.EqualTo(upperCasePassword));
                Assert.That(_sut.PreviousPasswords[2], Is.EqualTo(mixedCasePassword));
            });
        }

        [Test, Category("Models")]
        public void PreviousPasswords_WhenCaseSensitivePasswordRepeated_ShouldThrowArgumentException()
        {
            // Arrange
            var password = "CaseSensitivePassword123!";
            _sut.PasswordValue = password;

            // Act & Assert - Exact same case should throw
            var exception = Assert.Throws<ArgumentException>(() => _sut.PasswordValue = password);
            Assert.That(exception.Message, Does.Contain("New password cannot be the same as any previous passwords"));
        }
    }
}
