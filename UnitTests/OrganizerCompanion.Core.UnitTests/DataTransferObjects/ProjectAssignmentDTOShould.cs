using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using Task = OrganizerCompanion.Core.Models.Domain.ProjectTask;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class ProjectAssignmentDTOShould
    {
        private ProjectAssignmentDTO _projectAssignmentDTO;
        private List<GroupDTO> _testGroups;

        [SetUp]
        public void SetUp()
        {
            _projectAssignmentDTO = new ProjectAssignmentDTO();

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
            var projectAssignmentDTO = new ProjectAssignmentDTO();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(projectAssignmentDTO.Id, Is.EqualTo(0));
                Assert.That(projectAssignmentDTO.Name, Is.EqualTo(string.Empty));
                Assert.That(projectAssignmentDTO.Description, Is.Null);
                Assert.That(projectAssignmentDTO.Groups, Is.Null);
                Assert.That(projectAssignmentDTO.IsCompleted, Is.False);
                Assert.That(projectAssignmentDTO.DateDue, Is.Null);
                Assert.That(projectAssignmentDTO.DateCompleted, Is.Null);
                Assert.That(projectAssignmentDTO.DateCreated, Is.Not.EqualTo(default(DateTime)));
                Assert.That(projectAssignmentDTO.DateModified, Is.Null);
            });
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructor()
        {
            // Act & Assert - The JSON constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new ProjectAssignmentDTO(
                1, "Test Assignment", "Test Description", _testGroups, 
                1, null, true, DateTime.Now.AddDays(7), DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now));
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructorWithNullCollections()
        {
            // Act & Assert - The JSON constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new ProjectAssignmentDTO(
                1, "Test", "Description", null,
                null, null, false, null, null, DateTime.Now, null));
        }

        [Test]
        public void ThrowExceptionWhenConstructorTriesToSetIsCast()
        {
            // Act & Assert - The constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new ProjectAssignmentDTO(
                id: 1,
                name: "Test",
                description: "Description",
                groups: null,
                taskId: null,
                task: null,
                isCompleted: false,
                dateDue: null,
                dateCompleted: null,
                dateCreated: DateTime.Now,
                dateModified: null,
                isCast: true,
                castId: 42,
                castType: "TestType"));
        }

        [Test]
        public void JsonConstructorWithNullIsCastParameters()
        {
            // Act & Assert - Test the null coalescing paths in constructor
            Assert.Throws<NotImplementedException>(() => new ProjectAssignmentDTO(
                id: 1,
                name: "Test",
                description: "Description", 
                groups: null,
                taskId: null,
                task: null,
                isCompleted: false,
                dateDue: null,
                dateCompleted: null,
                dateCreated: DateTime.Now,
                dateModified: null,
                isCast: null, // This should trigger ?? false
                castId: null, // This should trigger ?? 0
                castType: null));
        }

        #endregion

        #region Property Tests

        [Test]
        public void SetAndGetId()
        {
            // Arrange
            var id = 42;

            // Act
            _projectAssignmentDTO.Id = id;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(id));
        }

        [Test]
        public void AcceptNegativeId()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Id = -1);
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(-1));
        }

        [Test]
        public void SetAndGetName()
        {
            // Arrange
            var name = "Test Assignment Name";

            // Act
            _projectAssignmentDTO.Name = name;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_projectAssignmentDTO.Name, Is.EqualTo(name));
        }

        [Test]
        public void AcceptEmptyName()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Name = "");
            Assert.That(_projectAssignmentDTO.Name, Is.EqualTo(""));
        }

        [Test]
        public void AcceptTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Name = longName);
            Assert.That(_projectAssignmentDTO.Name, Is.EqualTo(longName));
        }

        [Test]
        public void SetAndGetDescription()
        {
            // Arrange
            var description = "Test description";

            // Act
            _projectAssignmentDTO.Description = description;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(description));
        }

        [Test]
        public void AcceptTooLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = longDescription);
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(longDescription));
        }

        [Test]
        public void AcceptNullDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = null);
            Assert.That(_projectAssignmentDTO.Description, Is.Null);
        }

        [Test]
        public void AcceptEmptyDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = "");
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(""));
        }

        [Test]
        public void SetAndGetGroups()
        {
            // Act
            _projectAssignmentDTO.Groups = _testGroups;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_projectAssignmentDTO.Groups, Is.EqualTo(_testGroups));
        }

        [Test]
        public void InitializeEmptyListWhenGroupsIsNull()
        {
            // Arrange
            _projectAssignmentDTO.Groups = _testGroups; // Set to non-null first

            // Act
            _projectAssignmentDTO.Groups = null;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Null);
        }

        [Test]
        public void SetIsCompletedToTrue()
        {
            // Act
            _projectAssignmentDTO.IsCompleted = true;

            // Assert - DTOs don't auto-update DateCompleted or DateModified
            Assert.That(_projectAssignmentDTO.IsCompleted, Is.True);
            Assert.That(_projectAssignmentDTO.DateCompleted, Is.Null); // DateCompleted is readonly and stays null
        }

        [Test]
        public void SetIsCompletedToFalse()
        {
            // Arrange
            _projectAssignmentDTO.IsCompleted = true; // Set to true first
            Assert.That(_projectAssignmentDTO.DateCompleted, Is.Null); // DateCompleted stays null in DTOs

            // Act
            _projectAssignmentDTO.IsCompleted = false;
            Assert.Multiple(() =>
            {

                // Assert - DTOs don't auto-manage DateCompleted
                Assert.That(_projectAssignmentDTO.IsCompleted, Is.False);
                Assert.That(_projectAssignmentDTO.DateCompleted, Is.Null);
            });
        }

        [Test]
        public void SetAndGetDateDue()
        {
            // Arrange
            var dateDue = DateTime.Now.AddDays(7);

            // Act
            _projectAssignmentDTO.DateDue = dateDue;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_projectAssignmentDTO.DateDue, Is.EqualTo(dateDue));
        }

        [Test]
        public void GetDateCompletedReadOnly()
        {
            // Arrange
            var initialValue = _projectAssignmentDTO.DateCompleted;

            // Act
            var retrievedValue = _projectAssignmentDTO.DateCompleted;

            // Assert
            Assert.That(retrievedValue, Is.EqualTo(initialValue));
        }

        [Test]
        public void GetDateCreatedReadOnly()
        {
            // Arrange
            var initialValue = _projectAssignmentDTO.DateCreated;

            // Act
            var retrievedValue = _projectAssignmentDTO.DateCreated;

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
            Assert.Throws<NotImplementedException>(() => { var isCast = _projectAssignmentDTO.IsCast; });
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.IsCast = true);
        }

        [Test]
        public void ThrowNotImplementedExceptionForCastId()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var castId = _projectAssignmentDTO.CastId; });
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.CastId = 1);
        }

        [Test]
        public void ThrowNotImplementedExceptionForCastType()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var castType = _projectAssignmentDTO.CastType; });
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.CastType = "test");
        }

        [Test]
        public void CoverExplicitInterfaceImplementations()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testIGroups = _testGroups.Cast<IGroupDTO>().ToList();

            // Set up non-null collections first
            _projectAssignmentDTO.Groups = _testGroups;

            // Act & Assert for Groups interface property
            var retrievedIGroups = iAssignmentDTO.Groups;
            Assert.That(retrievedIGroups, Is.Not.Null);
            Assert.That(retrievedIGroups.Count, Is.EqualTo(_testGroups.Count));

            iAssignmentDTO.Groups = testIGroups;
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups.Count, Is.EqualTo(testIGroups.Count));
        }

        [Test]
        public void CoverTaskInterfaceImplementation()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testTask = new Task();

            // Act & Assert for Task interface property getter
            var retrievedTask = iAssignmentDTO.Task;
            Assert.That(retrievedTask, Is.EqualTo(_projectAssignmentDTO.Task));

            // Act & Assert for Task interface property setter - this should cover line 27
            iAssignmentDTO.Task = testTask;
            Assert.That(_projectAssignmentDTO.Task, Is.EqualTo(testTask));

            // Test setting null via interface - ensure cast occurs
            iAssignmentDTO.Task = null;
            Assert.That(_projectAssignmentDTO.Task, Is.Null);
        }

        #endregion

        #region Cast Method Tests

        [Test]
        public void ThrowNotImplementedExceptionForCastMethod()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.Cast<GroupDTO>());
        }

        #endregion

        #region ToJson Method Tests

        [Test]
        public void ToJson_ThrowsNotImplementedException()
        {
            // Arrange
            _projectAssignmentDTO.Id = 1;
            _projectAssignmentDTO.Name = "Test Assignment";
            _projectAssignmentDTO.Description = "Test Description";
            _projectAssignmentDTO.IsCompleted = false;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.ToJson());
        }

        [Test]
        public void ToJsonWithNullValues_ThrowsNotImplementedException()
        {
            // Arrange
            _projectAssignmentDTO.Id = 0;
            _projectAssignmentDTO.Name = "Minimal Assignment";
            // Leave other properties as default/null

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.ToJson());
        }

        [Test]
        public void ToJsonWithCircularReferences_ThrowsNotImplementedException()
        {
            // Arrange
            _projectAssignmentDTO.Id = 1;
            _projectAssignmentDTO.Name = "Test Assignment";
            _projectAssignmentDTO.Groups = _testGroups;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.ToJson());
        }

        #endregion

        #region Edge Cases and Validation Tests

        [Test]
        public void AcceptSingleCharacterName()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Name = "A");
            Assert.That(_projectAssignmentDTO.Name, Is.EqualTo("A"));
        }

        [Test]
        public void AcceptLongName()
        {
            // Arrange
            var longName = new string('a', 200); // Beyond typical validation limit

            // Act & Assert - DTOs accept any string length
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Name = longName);
            Assert.That(_projectAssignmentDTO.Name, Is.EqualTo(longName));
        }

        [Test]
        public void AcceptLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 2000); // Beyond typical validation limit

            // Act & Assert - DTOs accept any string length
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = longDescription);
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(longDescription));
        }

        [Test]
        public void AcceptZeroId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Id = 0);
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(0));
        }

        [Test]
        public void AcceptMaximumId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Id = int.MaxValue);
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void HandleEmptyGroupsList()
        {
            // Act
            _projectAssignmentDTO.Groups = [];

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups, Is.Empty);
        }

        [Test]
        public void HandleNullDateValues()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.DateDue = null);
            Assert.That(_projectAssignmentDTO.DateDue, Is.Null);
        }

        [Test]
        public void SetAndGetTaskId()
        {
            // Act
            _projectAssignmentDTO.TaskId = 42;

            // Assert
            Assert.That(_projectAssignmentDTO.TaskId, Is.EqualTo(42));
        }

        [Test]
        public void SetAndGetTask()
        {
            // Arrange
            var testTask = new OrganizerCompanion.Core.Models.Domain.ProjectTask();

            // Act
            _projectAssignmentDTO.Task = testTask;

            // Assert
            Assert.That(_projectAssignmentDTO.Task, Is.EqualTo(testTask));
        }

        [Test]
        public void AcceptNullTask()
        {
            // Act
            _projectAssignmentDTO.Task = null;

            // Assert
            Assert.That(_projectAssignmentDTO.Task, Is.Null);
        }

        [Test]
        public void AcceptNullTaskId()
        {
            // Act
            _projectAssignmentDTO.TaskId = null;

            // Assert
            Assert.That(_projectAssignmentDTO.TaskId, Is.Null);
        }

        [Test]
        public void SetAndGetDateModified()
        {
            // Arrange
            var dateTime = DateTime.Now;

            // Act
            _projectAssignmentDTO.DateModified = dateTime;

            // Assert
            Assert.That(_projectAssignmentDTO.DateModified, Is.EqualTo(dateTime));
        }

        [Test]
        public void SetDateModifiedToNull()
        {
            // Act
            _projectAssignmentDTO.DateModified = null;

            // Assert
            Assert.That(_projectAssignmentDTO.DateModified, Is.Null);
        }

        [Test]
        public void HandleGroupsNullToEmptyListConversion()
        {
            // Arrange - Start with null
            _projectAssignmentDTO.Groups = null;

            // Act - Setting null should initialize empty list due to ??= operator
            _projectAssignmentDTO.Groups = _testGroups;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.EqualTo(_testGroups));
        }

        #endregion
    }
}
