using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class UserShould
    {
        private User _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new User();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreatePersonWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new User();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.FirstName, Is.Null);
                Assert.That(_sut.MiddleName, Is.Null);
                Assert.That(_sut.LastName, Is.Null);
                Assert.That(_sut.FullName, Is.Null);
                Assert.That(_sut.UserName, Is.Null);
                Assert.That(_sut.Pronouns, Is.Null);
                Assert.That(_sut.BirthDate, Is.Null);
                Assert.That(_sut.DeceasedDate, Is.Null);
                Assert.That(_sut.JoinedDate, Is.Null);
                Assert.That(_sut.Emails, Is.Not.Null.And.Empty);
                Assert.That(_sut.PhoneNumbers, Is.Not.Null.And.Empty);
                Assert.That(_sut.Addresses, Is.Not.Null.And.Empty);
                Assert.That(_sut.IsActive, Is.Null);
                Assert.That(_sut.IsDeceased, Is.Null);
                Assert.That(_sut.IsAdmin, Is.Null);
                Assert.That(_sut.IsSuperUser, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreatePersonWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var firstName = "John";
            var middleName = "Michael";
            var lastName = "Doe";
            var userName = "johndoe";
            var pronouns = Pronouns.HeHim;
            var birthDate = DateTime.Now.AddYears(-30);
            var deceasedDate = DateTime.Now.AddYears(-1);
            var joinDate = DateTime.Now.AddMonths(-6);
            var emails = new List<IEmail?> { null };
            var phoneNumbers = new List<IPhoneNumber?> { null };
            var addresses = new List<IAddress?> { null };
            var isActive = true;
            var isDeceased = false;
            var isAdmin = true;
            var isSuperUser = false;
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var person = new User(id, firstName, middleName, lastName, userName, pronouns, birthDate, deceasedDate, joinDate,
                emails, phoneNumbers, addresses, isActive, isDeceased, isAdmin, isSuperUser, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(person.Id, Is.EqualTo(id));
                Assert.That(person.FirstName, Is.EqualTo(firstName));
                Assert.That(person.MiddleName, Is.EqualTo(middleName));
                Assert.That(person.LastName, Is.EqualTo(lastName));
                Assert.That(person.FullName, Is.EqualTo($"{firstName} {middleName} {lastName}"));
                Assert.That(person.Pronouns, Is.EqualTo(pronouns));
                Assert.That(person.BirthDate, Is.EqualTo(birthDate));
                Assert.That(person.DeceasedDate, Is.EqualTo(deceasedDate));
                Assert.That(person.JoinedDate, Is.EqualTo(joinDate));
                Assert.That(person.Emails, Is.EqualTo(emails));
                Assert.That(person.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(person.Addresses, Is.EqualTo(addresses));
                Assert.That(person.IsActive, Is.EqualTo(isActive));
                Assert.That(person.IsDeceased, Is.EqualTo(isDeceased));
                Assert.That(person.IsAdmin, Is.EqualTo(isAdmin));
                Assert.That(person.IsSuperUser, Is.EqualTo(isSuperUser));
                Assert.That(person.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(person.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullLists_ShouldInitializeEmptyLists()
        {
            // Act
            var person = new User(1, "John", null, "Doe", null, null, null, null, null,
                [], [], [], null, null, null, null, DateTime.Now, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(person.Emails, Is.Not.Null.And.Empty);
                Assert.That(person.PhoneNumbers, Is.Not.Null.And.Empty);
                Assert.That(person.Addresses, Is.Not.Null.And.Empty);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_ShouldCreatePersonWithProvidedValues()
        {
            // Arrange
            var firstName = "Jane";
            var middleName = "Elizabeth";
            var lastName = "Smith";
            var userName = "johndoe";
            var pronouns = Pronouns.SheHer;
            var birthDate = DateTime.Now.AddYears(-25);
            var joinDate = DateTime.Now.AddMonths(-3);
            var emails = new List<IEmail?> { null };
            var phoneNumbers = new List<IPhoneNumber?> { null };
            var addresses = new List<IAddress?> { null };
            var isActive = true;
            var isDeceased = false;
            var isAdmin = false;
            var dateCreated = DateTime.Now.AddDays(-1);

            // Act
            var person = new User(firstName, middleName, lastName, userName, pronouns, birthDate, joinDate,
                emails, phoneNumbers, addresses, isActive, isDeceased, isAdmin, dateCreated);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(person.FirstName, Is.EqualTo(firstName));
                Assert.That(person.MiddleName, Is.EqualTo(middleName));
                Assert.That(person.LastName, Is.EqualTo(lastName));
                Assert.That(person.Pronouns, Is.EqualTo(pronouns));
                Assert.That(person.BirthDate, Is.EqualTo(birthDate));
                Assert.That(person.JoinedDate, Is.EqualTo(joinDate));
                Assert.That(person.Emails, Is.EqualTo(emails));
                Assert.That(person.PhoneNumbers, Is.EqualTo(phoneNumbers));
                Assert.That(person.Addresses, Is.EqualTo(addresses));
                Assert.That(person.IsActive, Is.EqualTo(isActive));
                Assert.That(person.IsDeceased, Is.EqualTo(isDeceased));
                Assert.That(person.IsAdmin, Is.EqualTo(isAdmin));
                Assert.That(person.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(person.IsSuperUser, Is.Null); // Not set in this constructor
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
        public void FirstName_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newFirstName = "Alice";
            var beforeSet = DateTime.Now;

            // Act
            _sut.FirstName = newFirstName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo(newFirstName));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void FirstName_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.FirstName = "Initial";
            var beforeSet = DateTime.Now;

            // Act
            _sut.FirstName = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void MiddleName_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newMiddleName = "Rose";
            var beforeSet = DateTime.Now;

            // Act
            _sut.MiddleName = newMiddleName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.MiddleName, Is.EqualTo(newMiddleName));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void MiddleName_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.MiddleName = "Initial";
            var beforeSet = DateTime.Now;

            // Act
            _sut.MiddleName = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.MiddleName, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void LastName_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newLastName = "Johnson";
            var beforeSet = DateTime.Now;

            // Act
            _sut.LastName = newLastName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LastName, Is.EqualTo(newLastName));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void LastName_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.LastName = "Initial";
            var beforeSet = DateTime.Now;

            // Act
            _sut.LastName = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LastName, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void FullName_WithFirstAndLastName_ShouldReturnFormattedName()
        {
            // Arrange
            _sut.FirstName = "John";
            _sut.LastName = "Doe";

            // Act
            var fullName = _sut.FullName;

            // Assert
            Assert.That(fullName, Is.EqualTo("John Doe"));
        }

        [Test, Category("Models")]
        public void FullName_WithAllNames_ShouldReturnFormattedName()
        {
            // Arrange
            _sut.FirstName = "John";
            _sut.MiddleName = "Michael";
            _sut.LastName = "Doe";

            // Act
            var fullName = _sut.FullName;

            // Assert
            Assert.That(fullName, Is.EqualTo("John Michael Doe"));
        }

        [Test, Category("Models")]
        public void FullName_WithAllNullNames_ShouldReturnNull()
        {
            // Act
            var fullName = _sut.FullName;

            // Assert
            Assert.That(fullName, Is.Null);
        }

        [Test, Category("Models")]
        public void FullName_WithOnlyMiddleName_ShouldReturnFormattedName()
        {
            // Arrange
            _sut.MiddleName = "Michael";

            // Act
            var fullName = _sut.FullName;

            // Assert
            Assert.That(fullName, Is.EqualTo(" Michael "));
        }

        [Test, Category("Models")]
        public void Pronouns_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newPronouns = Pronouns.TheyThem;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Pronouns = newPronouns;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Pronouns, Is.EqualTo(newPronouns));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void BirthDate_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newBirthDate = DateTime.Now.AddYears(-25);
            var beforeSet = DateTime.Now;

            // Act
            _sut.BirthDate = newBirthDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.BirthDate, Is.EqualTo(newBirthDate));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void JoinDate_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newJoinDate = DateTime.Now.AddMonths(-3);
            var beforeSet = DateTime.Now;

            // Act
            _sut.JoinedDate = newJoinDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.JoinedDate, Is.EqualTo(newJoinDate));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Emails_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newEmails = new List<IEmail?> { null };
            var beforeSet = DateTime.Now;

            // Act
            _sut.Emails = newEmails;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Is.EqualTo(newEmails));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void PhoneNumbers_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newPhoneNumbers = new List<IPhoneNumber?> { null };
            var beforeSet = DateTime.Now;

            // Act
            _sut.PhoneNumbers = newPhoneNumbers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Is.EqualTo(newPhoneNumbers));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Addresses_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newAddresses = new List<IAddress?> { null };
            var beforeSet = DateTime.Now;

            // Act
            _sut.Addresses = newAddresses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Is.EqualTo(newAddresses));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsActive_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newIsActive = true;
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsActive = newIsActive;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsActive, Is.EqualTo(newIsActive));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsDeceased_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newIsDeceased = true;
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsDeceased = newIsDeceased;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsDeceased, Is.EqualTo(newIsDeceased));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsAdmin_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newIsAdmin = true;
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsAdmin = newIsAdmin;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsAdmin, Is.EqualTo(newIsAdmin));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsSuperUser_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newIsSuperUser = true;
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsSuperUser = newIsSuperUser;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsSuperUser, Is.EqualTo(newIsSuperUser));
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
            var person = new User();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(person.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(person.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-10);

            // Act
            var person = new User(1, "John", null, "Doe", null, null, null, null, null,
                [], [], [], null, null, null, null, specificDate, null);

            // Assert
            Assert.That(person.DateCreated, Is.EqualTo(specificDate));
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
            Assert.Throws<NotImplementedException>(() => _sut.Cast<User>());
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.IsActive = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"firstName\":\"John\""));
                Assert.That(json, Does.Contain("\"lastName\":\"Doe\""));
                Assert.That(json, Does.Contain("\"isActive\":true"));
                Assert.That(json, Does.Contain("\"emails\":[]"));
                Assert.That(json, Does.Contain("\"phoneNumbers\":[]"));
                Assert.That(json, Does.Contain("\"addresses\":[]"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldReturnValidJsonString()
        {
            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":0"));
                Assert.That(json, Does.Contain("\"firstName\":null"));
                Assert.That(json, Does.Contain("\"lastName\":null"));
                Assert.That(json, Does.Contain("\"emails\":[]"));
                Assert.That(json, Does.Contain("\"phoneNumbers\":[]"));
                Assert.That(json, Does.Contain("\"addresses\":[]"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("123"));
                Assert.That(result, Does.Contain("John Doe"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullFullName_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 456;

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
        public void Person_WithAllPronounsValues_ShouldBeAllowed()
        {
            // Test all enum values
            foreach (Pronouns pronoun in Enum.GetValues<Pronouns>())
            {
                // Act
                _sut.Pronouns = pronoun;

                // Assert
                Assert.That(_sut.Pronouns, Is.EqualTo(pronoun), $"Failed for pronoun: {pronoun}");
            }
        }

        [Test, Category("Models")]
        public void Person_WithEmptyStringNames_ShouldBeAllowed()
        {
            // Act
            _sut.FirstName = string.Empty;
            _sut.MiddleName = string.Empty;
            _sut.LastName = string.Empty;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo(string.Empty));
                Assert.That(_sut.MiddleName, Is.EqualTo(string.Empty));
                Assert.That(_sut.LastName, Is.EqualTo(string.Empty));
            });
        }

        [Test, Category("Models")]
        public void Person_WithVeryLongNames_ShouldBeAllowed()
        {
            // Arrange
            var longName = new string('A', 1000);

            // Act
            _sut.FirstName = longName;
            _sut.MiddleName = longName;
            _sut.LastName = longName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo(longName));
                Assert.That(_sut.MiddleName, Is.EqualTo(longName));
                Assert.That(_sut.LastName, Is.EqualTo(longName));
            });
        }

        [Test, Category("Models")]
        public void Person_MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.FirstName = "John";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.LastName = "Doe";
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.Pronouns = Pronouns.HeHim;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.DeceasedDate = DateTime.Now.AddYears(-1);
            var fifthModified = _sut.DateModified;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));

            System.Threading.Thread.Sleep(1);
            _sut.IsActive = true;
            var sixthModified = _sut.DateModified;
            Assert.That(sixthModified, Is.GreaterThan(fifthModified));
        }

        [Test, Category("Models")]
        public void Person_WithZeroId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Person_WithMaxIntId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Person_WithNegativeId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = -1;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void Person_BooleanProperties_ShouldHandleAllValues()
        {
            // Test true, false, and null for all boolean properties
            var boolValues = new bool?[] { true, false, null };

            foreach (var value in boolValues)
            {
                _sut.IsActive = value;
                Assert.That(_sut.IsActive, Is.EqualTo(value), $"Failed for IsActive with value: {value}");

                _sut.IsDeceased = value;
                Assert.That(_sut.IsDeceased, Is.EqualTo(value), $"Failed for IsDeceased with value: {value}");

                _sut.IsAdmin = value;
                Assert.That(_sut.IsAdmin, Is.EqualTo(value), $"Failed for IsAdmin with value: {value}");

                _sut.IsSuperUser = value;
                Assert.That(_sut.IsSuperUser, Is.EqualTo(value), $"Failed for IsSuperUser with value: {value}");
            }
        }

        [Test, Category("Models")]
        public void Person_SpecialCharactersInNames_ShouldBeAllowed()
        {
            // Arrange
            var specialName = "O'Connor-Smith";

            // Act
            _sut.FirstName = specialName;
            _sut.MiddleName = specialName;
            _sut.LastName = specialName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo(specialName));
                Assert.That(_sut.MiddleName, Is.EqualTo(specialName));
                Assert.That(_sut.LastName, Is.EqualTo(specialName));
            });
        }

        [Test, Category("Models")]
        public void Person_UnicodeCharactersInNames_ShouldBeAllowed()
        {
            // Arrange
            var unicodeName = "José María Azñár";

            // Act
            _sut.FirstName = unicodeName;
            _sut.MiddleName = unicodeName;
            _sut.LastName = unicodeName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo(unicodeName));
                Assert.That(_sut.MiddleName, Is.EqualTo(unicodeName));
                Assert.That(_sut.LastName, Is.EqualTo(unicodeName));
            });
        }

        [Test, Category("Models")]
        public void Person_SerializerOptions_ShouldHandleCyclicalReferences()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";

            // Act & Assert - This should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = _sut.ToJson();
                Assert.That(json, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test, Category("Models")]
        public void Person_DateProperties_ShouldHandleMinMaxValues()
        {
            // Arrange
            var minDate = DateTime.MinValue;
            var maxDate = DateTime.MaxValue;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                _sut.BirthDate = minDate;
                Assert.That(_sut.BirthDate, Is.EqualTo(minDate));

                _sut.BirthDate = maxDate;
                Assert.That(_sut.BirthDate, Is.EqualTo(maxDate));

                _sut.JoinedDate = minDate;
                Assert.That(_sut.JoinedDate, Is.EqualTo(minDate));

                _sut.JoinedDate = maxDate;
                Assert.That(_sut.JoinedDate, Is.EqualTo(maxDate));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceProperties_Emails_ShouldWorkCorrectly()
        {
            // Arrange
            var typePerson = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var typeEmails = new List<OrganizerCompanion.Core.Interfaces.Type.IEmail?> { null };

            // Act
            typePerson.Emails = typeEmails;

            // Assert
            Assert.That(typePerson.Emails, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceProperties_PhoneNumbers_ShouldWorkCorrectly()
        {
            // Arrange
            var typePerson = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var typePhoneNumbers = new List<OrganizerCompanion.Core.Interfaces.Type.IPhoneNumber?> { null };

            // Act
            typePerson.PhoneNumbers = typePhoneNumbers;

            // Assert
            Assert.That(typePerson.PhoneNumbers, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceProperties_Addresses_ShouldWorkCorrectly()
        {
            // Arrange
            var typePerson = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;
            var typeAddresses = new List<OrganizerCompanion.Core.Interfaces.Type.IAddress?> { null };

            // Act
            typePerson.Addresses = typeAddresses;

            // Assert
            Assert.That(typePerson.Addresses, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceProperties_WithNullValues_ShouldInitializeEmptyLists()
        {
            // Arrange
            var typePerson = (OrganizerCompanion.Core.Interfaces.Type.IPerson)_sut;

            // Act
            typePerson.Emails = null!;
            typePerson.PhoneNumbers = null!;
            typePerson.Addresses = null!;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(typePerson.Emails, Is.Not.Null.And.Empty);
                Assert.That(typePerson.PhoneNumbers, Is.Not.Null.And.Empty);
                Assert.That(typePerson.Addresses, Is.Not.Null.And.Empty);
            });
        }

        [Test, Category("Models")]
        public void DeceasedDate_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newDeceasedDate = DateTime.Now.AddYears(-1);
            var beforeSet = DateTime.Now;

            // Act
            _sut.DeceasedDate = newDeceasedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DeceasedDate, Is.EqualTo(newDeceasedDate));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void DeceasedDate_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.DeceasedDate = DateTime.Now.AddYears(-1);
            var beforeSet = DateTime.Now;

            // Act
            _sut.DeceasedDate = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DeceasedDate, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void DeceasedDate_WithMinMaxValues_ShouldBeAllowed()
        {
            // Arrange
            var minDate = DateTime.MinValue;
            var maxDate = DateTime.MaxValue;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                _sut.DeceasedDate = minDate;
                Assert.That(_sut.DeceasedDate, Is.EqualTo(minDate));

                _sut.DeceasedDate = maxDate;
                Assert.That(_sut.DeceasedDate, Is.EqualTo(maxDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithDeceasedDate_ShouldSetValue()
        {
            // Arrange
            var deceasedDate = DateTime.Now.AddYears(-2);

            // Act
            var person = new User(1, "John", null, "Doe", null, null, null, deceasedDate, null,
                [], [], [], null, null, null, null, DateTime.Now, null);

            // Assert
            Assert.That(person.DeceasedDate, Is.EqualTo(deceasedDate));
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullDeceasedDate_ShouldSetNull()
        {
            // Act
            var person = new User(1, "John", null, "Doe", null, null, null, null, null,
                [], [], [], null, null, null, null, DateTime.Now, null);

            // Assert
            Assert.That(person.DeceasedDate, Is.Null);
        }

        [Test, Category("Models")]
        public void ToJson_WithDeceasedDate_ShouldIncludeInJson()
        {
            // Arrange
            var deceasedDate = DateTime.Now.AddYears(-1);
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.DeceasedDate = deceasedDate;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"deceasedDate\""));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullDeceasedDate_ShouldNotIncludeInJson()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.DeceasedDate = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                // Due to JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), null DeceasedDate should not appear in JSON
                Assert.That(json, Does.Not.Contain("\"deceasedDate\""));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void DeceasedDate_MultipleChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var firstDate = DateTime.Now.AddYears(-3);
            var secondDate = DateTime.Now.AddYears(-2);
            var thirdDate = DateTime.Now.AddYears(-1);

            // Act & Assert
            System.Threading.Thread.Sleep(1);
            _sut.DeceasedDate = firstDate;
            var firstModified = _sut.DateModified;

            System.Threading.Thread.Sleep(1);
            _sut.DeceasedDate = secondDate;
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.DeceasedDate = thirdDate;
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.DeceasedDate = null;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));
        }

        [Test, Category("Models")]
        public void DeceasedDate_WithSpecificDateScenarios_ShouldHandleCorrectly()
        {
            // Test various date scenarios
            var scenarios = new[]
            {
                DateTime.Today, // Today
                DateTime.Now.AddDays(-1), // Yesterday
                DateTime.Now.AddMonths(-1), // Last month
                DateTime.Now.AddYears(-1), // Last year
                new DateTime(2000, 1, 1), // Specific date
                new DateTime(1950, 12, 31) // Older date
            };

            foreach (var date in scenarios)
            {
                // Act
                _sut.DeceasedDate = date;

                // Assert
                Assert.That(_sut.DeceasedDate, Is.EqualTo(date), 
                    $"Failed to set DeceasedDate to {date}");
            }
        }

        [Test, Category("Models")]
        public void UserName_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newUserName = "johndoe123";
            var beforeSet = DateTime.Now;

            // Act
            _sut.UserName = newUserName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.EqualTo(newUserName));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void UserName_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.UserName = "initialUser";
            var beforeSet = DateTime.Now;

            // Act
            _sut.UserName = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void UserName_WhenSetToEmptyString_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.UserName = string.Empty;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.EqualTo(string.Empty));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void UserName_WithSpecialCharacters_ShouldBeAllowed()
        {
            // Arrange
            var specialUserName = "user@domain.com";

            // Act
            _sut.UserName = specialUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(specialUserName));
        }

        [Test, Category("Models")]
        public void UserName_WithVeryLongString_ShouldBeAllowed()
        {
            // Arrange
            var longUserName = new string('u', 500);

            // Act
            _sut.UserName = longUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(longUserName));
        }

        [Test, Category("Models")]
        public void UserName_MultipleChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1);
            _sut.UserName = "user1";
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.UserName = "user2";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.UserName = null;
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));
        }

        [Test, Category("Models")]
        public void ToJson_WithUserName_ShouldIncludeInJson()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.UserName = "johndoe";
            _sut.IsActive = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"firstName\":\"John\""));
                Assert.That(json, Does.Contain("\"lastName\":\"Doe\""));
                Assert.That(json, Does.Contain("\"userName\":\"johndoe\""));
                Assert.That(json, Does.Contain("\"isActive\":true"));
                Assert.That(json, Does.Contain("\"emails\":[]"));
                Assert.That(json, Does.Contain("\"phoneNumbers\":[]"));
                Assert.That(json, Does.Contain("\"addresses\":[]"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullUserName_ShouldNotIncludeInJson()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.UserName = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                // Due to JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), null UserName should not appear in JSON
                Assert.That(json, Does.Not.Contain("\"userName\""));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithEmptyUserName_ShouldIncludeInJson()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.UserName = string.Empty;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"userName\":\"\""));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }
    }
}
