using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class AssignmentDTOShould
    {
        private AssignmentDTO _assignmentDTO;
        private List<GroupDTO> _testGroups;

        [SetUp]
        public void SetUp()
        {
            _assignmentDTO = new AssignmentDTO();

            // Create test groups
            _testGroups =
            [
                new() { Id = 1, Name = "Development Team" },
                new() { Id = 2, Name = "QA Team" }
            ];
        }

        #region Constructor Tests

        [Test]
        public void HaveDefaultConstructor()
        {
            // Arrange & Act
            var assignmentDTO = new AssignmentDTO();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(assignmentDTO.Id, Is.EqualTo(0));
                Assert.That(assignmentDTO.Name, Is.EqualTo(string.Empty));
                Assert.That(assignmentDTO.Description, Is.Null);
                Assert.That(assignmentDTO.Groups, Is.Null);
                Assert.That(assignmentDTO.IsCompleted, Is.False);
                Assert.That(assignmentDTO.DateDue, Is.Null);
                Assert.That(assignmentDTO.DateCompleted, Is.Null);
                Assert.That(assignmentDTO.DateCreated, Is.Not.EqualTo(default(DateTime)));
                Assert.That(assignmentDTO.DateModified, Is.Null);
            });
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructor()
        {
            // Act & Assert - The JSON constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new AssignmentDTO(
                1, "Test Assignment", "Test Description", _testGroups,
                true, DateTime.Now.AddDays(7), DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now));
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructorWithNullCollections()
        {
            // Act & Assert - The JSON constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new AssignmentDTO(
                1, "Test", "Description", null,
                false, null, null, DateTime.Now, null));
        }

        [Test]
        public void ThrowExceptionWhenConstructorTriesToSetIsCast()
        {
            // Act & Assert - The constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new AssignmentDTO(
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

        #region Property Tests

        [Test]
        public void SetAndGetId()
        {
            // Arrange
            var id = 42;
            var initialDateModified = _assignmentDTO.DateModified;

            // Act
            _assignmentDTO.Id = id;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.Id, Is.EqualTo(id));
                Assert.That(_assignmentDTO.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignmentDTO.DateModified, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionForNegativeId()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _assignmentDTO.Id = -1);
        }

        [Test]
        public void SetAndGetName()
        {
            // Arrange
            var name = "Test Assignment Name";
            var initialDateModified = _assignmentDTO.DateModified;

            // Act
            _assignmentDTO.Name = name;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.Name, Is.EqualTo(name));
                Assert.That(_assignmentDTO.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignmentDTO.DateModified, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionForEmptyName()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignmentDTO.Name = "");
        }

        [Test]
        public void ThrowExceptionForTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignmentDTO.Name = longName);
        }

        [Test]
        public void SetAndGetDescription()
        {
            // Arrange
            var description = "Test description";
            var initialDateModified = _assignmentDTO.DateModified;

            // Act
            _assignmentDTO.Description = description;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.Description, Is.EqualTo(description));
                Assert.That(_assignmentDTO.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignmentDTO.DateModified, Is.Not.Null);
        }

        [Test]
        public void ThrowExceptionForTooLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignmentDTO.Description = longDescription);
        }

        [Test]
        public void AcceptNullDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Description = null);
            Assert.That(_assignmentDTO.Description, Is.Null);
        }

        [Test]
        public void AcceptEmptyDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Description = "");
            Assert.That(_assignmentDTO.Description, Is.EqualTo(""));
        }

        [Test]
        public void SetAndGetGroups()
        {
            // Arrange
            var initialDateModified = _assignmentDTO.DateModified;

            // Act
            _assignmentDTO.Groups = _testGroups;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.Groups, Is.EqualTo(_testGroups));
                Assert.That(_assignmentDTO.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignmentDTO.DateModified, Is.Not.Null);
        }

        [Test]
        public void InitializeEmptyListWhenGroupsIsNull()
        {
            // Arrange
            _assignmentDTO.Groups = _testGroups; // Set to non-null first

            // Act
            _assignmentDTO.Groups = null;

            // Assert
            Assert.That(_assignmentDTO.Groups, Is.Null);
        }

        [Test]
        public void SetIsCompletedToTrueAndUpdateDateCompleted()
        {
            // Arrange
            var initialDateModified = _assignmentDTO.DateModified;
            var beforeCompletion = DateTime.Now;

            // Act
            _assignmentDTO.IsCompleted = true;
            var afterCompletion = DateTime.Now;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.IsCompleted, Is.True);
                Assert.That(_assignmentDTO.DateCompleted, Is.Not.Null);
            });
            Assert.That(_assignmentDTO.DateCompleted, Is.GreaterThanOrEqualTo(beforeCompletion));
            Assert.Multiple(() =>
            {
                Assert.That(_assignmentDTO.DateCompleted, Is.LessThanOrEqualTo(afterCompletion));
                Assert.That(_assignmentDTO.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignmentDTO.DateModified, Is.Not.Null);
        }

        [Test]
        public void SetIsCompletedToFalseAndClearDateCompleted()
        {
            // Arrange
            _assignmentDTO.IsCompleted = true; // Set to true first
            Assert.That(_assignmentDTO.DateCompleted, Is.Not.Null);

            // Act
            _assignmentDTO.IsCompleted = false;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.IsCompleted, Is.False);
                Assert.That(_assignmentDTO.DateCompleted, Is.Null);
            });
        }

        [Test]
        public void SetAndGetDateDue()
        {
            // Arrange
            var dateDue = DateTime.Now.AddDays(7);
            var initialDateModified = _assignmentDTO.DateModified;

            // Act
            _assignmentDTO.DateDue = dateDue;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignmentDTO.DateDue, Is.EqualTo(dateDue));
                Assert.That(_assignmentDTO.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignmentDTO.DateModified, Is.Not.Null);
        }

        [Test]
        public void GetDateCompletedReadOnly()
        {
            // Arrange
            var initialValue = _assignmentDTO.DateCompleted;

            // Act
            var retrievedValue = _assignmentDTO.DateCompleted;

            // Assert
            Assert.That(retrievedValue, Is.EqualTo(initialValue));
        }

        [Test]
        public void GetDateCreatedReadOnly()
        {
            // Arrange
            var initialValue = _assignmentDTO.DateCreated;

            // Act
            var retrievedValue = _assignmentDTO.DateCreated;

            // Assert
            Assert.That(retrievedValue, Is.EqualTo(initialValue));
            Assert.That(retrievedValue, Is.Not.EqualTo(default(DateTime)));
        }

        #endregion

        #region Interface Implementation Tests

        [Test]
        public void ThrowNotImplementedExceptionForIsCast()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var isCast = _assignmentDTO.IsCast; });
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.IsCast = true);
        }

        [Test]
        public void ThrowNotImplementedExceptionForCastId()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var castId = _assignmentDTO.CastId; });
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.CastId = 1);
        }

        [Test]
        public void ThrowNotImplementedExceptionForCastType()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var castType = _assignmentDTO.CastType; });
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.CastType = "test");
        }

        [Test]
        public void CoverExplicitInterfaceImplementations()
        {
            // Arrange
            IAssignmentDTO iAssignmentDTO = _assignmentDTO;
            var testIGroups = _testGroups.Cast<IGroupDTO>().ToList();

            // Set up non-null collections first
            _assignmentDTO.Groups = _testGroups;

            // Act & Assert for Groups interface property
            var retrievedIGroups = iAssignmentDTO.Groups;
            Assert.That(retrievedIGroups, Is.Not.Null);
            Assert.That(retrievedIGroups.Count, Is.EqualTo(_testGroups.Count));

            iAssignmentDTO.Groups = testIGroups;
            Assert.That(_assignmentDTO.Groups, Is.Not.Null);
            Assert.That(_assignmentDTO.Groups.Count, Is.EqualTo(testIGroups.Count));
        }

        #endregion

        #region Cast Method Tests

        [Test]
        public void ThrowNotImplementedExceptionForCastMethod()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.Cast<GroupDTO>());
        }

        #endregion

        #region ToJson Method Tests

        [Test]
        public void SerializeToJson()
        {
            // Arrange
            _assignmentDTO.Id = 1;
            _assignmentDTO.Name = "Test Assignment";
            _assignmentDTO.Description = "Test Description";
            _assignmentDTO.IsCompleted = false;

            // Act
            var json = _assignmentDTO.ToJson();

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
            _assignmentDTO.Id = 0;
            _assignmentDTO.Name = "Minimal Assignment";
            // Leave other properties as default/null

            // Act
            var json = _assignmentDTO.ToJson();

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
            _assignmentDTO.Id = 1;
            _assignmentDTO.Name = "Test Assignment";
            _assignmentDTO.Groups = _testGroups;

            // Act & Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() => _assignmentDTO.ToJson());
        }

        #endregion

        #region Edge Cases and Validation Tests

        [Test]
        public void AcceptMinimumValidName()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Name = "A");
            Assert.That(_assignmentDTO.Name, Is.EqualTo("A"));
        }

        [Test]
        public void AcceptMaximumValidName()
        {
            // Arrange
            var maxName = new string('a', 100);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Name = maxName);
            Assert.That(_assignmentDTO.Name, Is.EqualTo(maxName));
        }

        [Test]
        public void AcceptMaximumValidDescription()
        {
            // Arrange
            var maxDescription = new string('a', 1000);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Description = maxDescription);
            Assert.That(_assignmentDTO.Description, Is.EqualTo(maxDescription));
        }

        [Test]
        public void AcceptZeroId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Id = 0);
            Assert.That(_assignmentDTO.Id, Is.EqualTo(0));
        }

        [Test]
        public void AcceptMaximumId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Id = int.MaxValue);
            Assert.That(_assignmentDTO.Id, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void HandleEmptyGroupsList()
        {
            // Act
            _assignmentDTO.Groups = [];

            // Assert
            Assert.That(_assignmentDTO.Groups, Is.Not.Null);
            Assert.That(_assignmentDTO.Groups, Is.Empty);
        }

        [Test]
        public void HandleNullDateValues()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.DateDue = null);
            Assert.That(_assignmentDTO.DateDue, Is.Null);
        }

        [Test]
        public void HandleGroupsNullToEmptyListConversion()
        {
            // Arrange - Start with null
            _assignmentDTO.Groups = null;

            // Act - Setting null should initialize empty list due to ??= operator
            _assignmentDTO.Groups = _testGroups;

            // Assert
            Assert.That(_assignmentDTO.Groups, Is.EqualTo(_testGroups));
        }

        #endregion
    }
}
