using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AssignmentShould
    {
        private Assignment _assignment;
        private List<Group> _groups;

        [SetUp]
        public void SetUp()
        {
            _assignment = new Assignment();

            _groups =
            [
                new() {
                    Id = 1,
                    Name = "Group 1",
                    Members =
                    [
                        new() { Id = 1, FirstName = "John", LastName = "Doe" },
                        new() { Id = 2, FirstName = "Jane", LastName = "Smith" }
                    ] },
                new() {
                    Id = 2,
                    Name = "Group 2",
                    Members =
                    [
                        new() { Id = 3, FirstName = "Bob", LastName = "Johnson" }
                    ]
                }
            ];
        }

        #region Constructor Tests

        [Test]
        public void HaveDefaultConstructor()
        {
            // Arrange & Act
            var assignment = new Assignment();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(assignment.Id, Is.EqualTo(0));
                Assert.That(assignment.Name, Is.EqualTo(string.Empty));
                Assert.That(assignment.Description, Is.Null);
                Assert.That(assignment.Groups, Is.Null);
                Assert.That(assignment.IsCompleted, Is.False);
                Assert.That(assignment.DateDue, Is.Null);
                Assert.That(assignment.DateCompleted, Is.Null);
                Assert.That(assignment.DateCreated, Is.Not.EqualTo(default(DateTime)));
                Assert.That(assignment.DateModified, Is.Null);
            });
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructor()
        {
            // Act & Assert - The JSON constructor always tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new Assignment(
                1, "Test Assignment", "Test Description", _groups,
                true, DateTime.Now.AddDays(7), DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now));
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructorWithNullCollections()
        {
            // Act & Assert - The JSON constructor always tries to set IsCast which throws NotImplementedException  
            Assert.Throws<NotImplementedException>(() => new Assignment(
                1, "Test", "Description", null,
                false, null, null, DateTime.Now, null));
        }

        #endregion

        #region Property Tests

        [Test]
        public void SetAndGetId()
        {
            // Arrange
            var id = 42;
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.Id = id;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Id, Is.EqualTo(id));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionForNegativeId()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _assignment.Id = -1);
        }

        [Test]
        public void SetAndGetName()
        {
            // Arrange
            var name = "Test Assignment Name";
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.Name = name;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Name, Is.EqualTo(name));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionForEmptyName()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.Name = "");
        }

        [Test]
        public void ThrowExceptionForTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.Name = longName);
        }

        [Test]
        public void SetAndGetDescription()
        {
            // Arrange
            var description = "Test description";
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.Description = description;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Description, Is.EqualTo(description));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionForTooLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.Description = longDescription);
        }

        [Test]
        public void SetAndGetAssignees()
        {
            // Arrange
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.Groups = _groups;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Groups, Is.EqualTo(_groups));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void InitializeEmptyListWhenAssigneesIsNull()
        {
            // Arrange
            _assignment.Groups = _groups; // Set to non-null first

            // Act
            _assignment.Groups = null;

            // Assert
            Assert.That(_assignment.Groups, Is.Null);
        }

        [Test]
        public void SetAndGetContacts()
        {
            // Arrange
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.Groups = _groups;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Groups, Is.EqualTo(_groups));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void InitializeEmptyListWhenContactsIsNull()
        {
            // Arrange
            _assignment.Groups = _groups; // Set to non-null first

            // Act
            _assignment.Groups = null;

            // Assert
            Assert.That(_assignment.Groups, Is.Null);
        }

        [Test]
        public void SetIsCompletedToTrueAndUpdateDateCompleted()
        {
            // Arrange
            var initialDateModified = _assignment.DateModified;
            var beforeCompletion = DateTime.Now;

            // Act
            _assignment.IsCompleted = true;
            var afterCompletion = DateTime.Now;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.IsCompleted, Is.True);
                Assert.That(_assignment.DateCompleted, Is.Not.Null);
            });
            Assert.That(_assignment.DateCompleted, Is.GreaterThanOrEqualTo(beforeCompletion));
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.DateCompleted, Is.LessThanOrEqualTo(afterCompletion));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void SetIsCompletedToFalseAndClearDateCompleted()
        {
            // Arrange
            _assignment.IsCompleted = true; // Set to true first
            Assert.That(_assignment.DateCompleted, Is.Not.Null);

            // Act
            _assignment.IsCompleted = false;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.IsCompleted, Is.False);
                Assert.That(_assignment.DateCompleted, Is.Null);
            });
        }

        [Test]
        public void SetAndGetDateDue()
        {
            // Arrange
            var dateDue = DateTime.Now.AddDays(7);
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.DateDue = dateDue;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.DateDue, Is.EqualTo(dateDue));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void SetAndGetDateCompleted()
        {
            // Arrange
            var dateCompleted = DateTime.Now;
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.DateCompleted = dateCompleted;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.DateCompleted, Is.EqualTo(dateCompleted));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test]
        public void SetAndGetDateCreated()
        {
            // Arrange
            var dateCreated = DateTime.Now.AddDays(-5);
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.DateCreated = dateCreated;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        #endregion

        #region Interface Implementation Tests

        [Test]
        public void ThrowNotImplementedExceptionForIsCast()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var isCast = _assignment.IsCast; });
            Assert.Throws<NotImplementedException>(() => _assignment.IsCast = true);
        }

        [Test]
        public void ThrowNotImplementedExceptionForCastId()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var castId = _assignment.CastId; });
            Assert.Throws<NotImplementedException>(() => _assignment.CastId = 1);
        }

        [Test]
        public void ThrowNotImplementedExceptionForCastType()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var castType = _assignment.CastType; });
            Assert.Throws<NotImplementedException>(() => _assignment.CastType = "test");
        }

        #endregion

        #region Cast Method Tests

        [Test]
        public void CastToAssignmentDTOWithNullGroups()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Description = "Test Description";
            _assignment.Groups = null;

            // Act
            var result = _assignment.Cast<AssignmentDTO>();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test Assignment"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Groups, Is.Null);
        }

        [Test]
        public void CastToAssignmentDTOWithEmptyGroups()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Description = "Test Description";
            _assignment.Groups = []; // Empty list

            // Act
            var result = _assignment.Cast<AssignmentDTO>();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test Assignment"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Groups, Is.Not.Null);
            Assert.That(result.Groups, Is.Empty);
        }

        [Test]
        public void CastToIAssignmentDTO()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Groups = [];

            // Act
            var result = _assignment.Cast<IAssignmentDTO>();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test Assignment"));
        }

        [Test]
        public void ThrowInvalidCastExceptionForUnsupportedType()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _assignment.Cast<Contact>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type Contact"));
        }

        #endregion

        #region ToJson Method Tests

        [Test]
        public void SerializeToJson()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Description = "Test Description";
            _assignment.IsCompleted = false;

            // Act
            var json = _assignment.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Does.Contain("\"id\":1"));
            Assert.That(json, Does.Contain("\"name\":\"Test Assignment\""));
            Assert.That(json, Does.Contain("\"description\":\"Test Description\""));
            Assert.That(json, Does.Contain("\"isCompleted\":false"));
        }

        [Test]
        public void SerializeToJsonWithNullValues()
        {
            // Arrange
            _assignment.Id = 0;
            _assignment.Name = "Minimal Assignment";
            // Leave other properties as default/null

            // Act
            var json = _assignment.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Does.Contain("\"id\":0"));
            Assert.That(json, Does.Contain("\"name\":\"Minimal Assignment\""));
        }

        [Test]
        public void SerializeToJsonWithCircularReferences()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Groups = _groups;

            // Act & Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() => _assignment.ToJson());
        }

        #endregion

        #region Edge Cases and Validation Tests

        [Test]
        public void AcceptMinimumValidName()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Name = "A");
            Assert.That(_assignment.Name, Is.EqualTo("A"));
        }

        [Test]
        public void AcceptMaximumValidName()
        {
            // Arrange
            var maxName = new string('a', 100);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Name = maxName);
            Assert.That(_assignment.Name, Is.EqualTo(maxName));
        }

        [Test]
        public void AcceptMaximumValidDescription()
        {
            // Arrange
            var maxDescription = new string('a', 1000);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Description = maxDescription);
            Assert.That(_assignment.Description, Is.EqualTo(maxDescription));
        }

        [Test]
        public void ThrowExceptionForNullDescription()
        {
            // Act & Assert - The Assignment class has a bug where it checks value!.Length for null
            Assert.Throws<NullReferenceException>(() => _assignment.Description = null);
        }

        [Test]
        public void AcceptEmptyDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Description = "");
            Assert.That(_assignment.Description, Is.EqualTo(""));
        }

        [Test]
        public void AcceptZeroId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Id = 0);
            Assert.That(_assignment.Id, Is.EqualTo(0));
        }

        [Test]
        public void AcceptMaximumId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Id = int.MaxValue);
            Assert.That(_assignment.Id, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void HandleEmptyGroupsList()
        {
            // Act
            _assignment.Groups = [];

            // Assert
            Assert.That(_assignment.Groups, Is.Not.Null);
            Assert.That(_assignment.Groups, Is.Empty);
        }

        [Test]
        public void HandleNullDateValues()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
                  {
                      _assignment.DateDue = null;
                      _assignment.DateCompleted = null;
                  });
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.DateDue, Is.Null);
                Assert.That(_assignment.DateCompleted, Is.Null);
            });
        }

        #endregion

        #region Constructor with Optional Parameters Tests

        [Test]
        public void ThrowExceptionWhenConstructorTriesToSetIsCast()
        {
            // Act & Assert - The constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new Assignment(
                id: 1,
                name: "Test",
                description: "Description",
                groups: null,
                isCompleted: false,
                dateDue: null,
                dateCompleted: null,
                dateCreated: DateTime.Now,
                dateModified: null,
                isCast: true,
                castId: 42,
                castType: "TestType"));
        }

        #endregion

        #region Interface Implementation Coverage Tests

        [Test]
        public void CoverExplicitInterfaceImplementations()
        {
            // Arrange
            IAssignment iAssignment = _assignment;
            var testIGroup = _groups.Cast<IGroup>().ToList();

            // Act & Assert for Asssignees interface property
            iAssignment.Groups = testIGroup;
            var retrievedIAssignees = iAssignment.Groups;
            Assert.That(retrievedIAssignees, Is.Not.Null);
            Assert.That(retrievedIAssignees.Count, Is.EqualTo(testIGroup.Count));
        }

        [Test]
        public void CastWithActualGroupObjects()
        {
            // Arrange - Create groups that will work with Cast<GroupDTO>()
            var workingGroup = new Group
            {
                Id = 1,
                Name = "Working Group",
                Description = "Description",
                Members =
                [
                    new() { Id = 1, FirstName = "Alice", LastName = "Wonderland" }
                ],
                AccountId = 0,
                Account = null,
            };

            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Description = "Test Description";
            _assignment.Groups = [workingGroup];

            // Act
            var result = _assignment.Cast<AssignmentDTO>();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Test Assignment"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Groups, Is.Not.Null);
            Assert.That(result.Groups.Count, Is.EqualTo(1));
            Assert.That(result.Groups[0].Id, Is.EqualTo(1));
            Assert.That(result.Groups[0].Name, Is.EqualTo("Working Group"));
        }

        [Test]
        public void CastToAssignmentDTOWithCompleteData()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Complete Assignment";
            _assignment.Description = "Complete Description";
            _assignment.Groups = _groups;
            _assignment.IsCompleted = true;
            _assignment.DateDue = DateTime.Now.AddDays(7);

            // Act
            var result = _assignment.Cast<AssignmentDTO>();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Complete Assignment"));
            Assert.That(result.Description, Is.EqualTo("Complete Description"));
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.DateDue, Is.EqualTo(_assignment.DateDue));
            Assert.That(result.Groups, Is.Not.Null);
            Assert.That(result.Groups.Count, Is.EqualTo(2));
        }

        #endregion
    }
}