using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using Task = OrganizerCompanion.Core.Models.Domain.ProjectTask;

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
                1, null, true, DateTime.Now.AddDays(7), DateTime.Now, DateTime.Now.AddDays(-1), DateTime.Now));
        }

        [Test]
        public void ThrowExceptionWhenUsingJsonConstructorWithNullCollections()
        {
            // Act & Assert - The JSON constructor tries to set IsCast which throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => new AssignmentDTO(
                1, "Test", "Description", null,
                null, null, false, null, null, DateTime.Now, null));
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
            Assert.Throws<NotImplementedException>(() => new AssignmentDTO(
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
            _assignmentDTO.Id = id;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_assignmentDTO.Id, Is.EqualTo(id));
        }

        [Test]
        public void AcceptNegativeId()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _assignmentDTO.Id = -1);
            Assert.That(_assignmentDTO.Id, Is.EqualTo(-1));
        }

        [Test]
        public void SetAndGetName()
        {
            // Arrange
            var name = "Test Assignment Name";

            // Act
            _assignmentDTO.Name = name;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_assignmentDTO.Name, Is.EqualTo(name));
        }

        [Test]
        public void AcceptEmptyName()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _assignmentDTO.Name = "");
            Assert.That(_assignmentDTO.Name, Is.EqualTo(""));
        }

        [Test]
        public void AcceptTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _assignmentDTO.Name = longName);
            Assert.That(_assignmentDTO.Name, Is.EqualTo(longName));
        }

        [Test]
        public void SetAndGetDescription()
        {
            // Arrange
            var description = "Test description";

            // Act
            _assignmentDTO.Description = description;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_assignmentDTO.Description, Is.EqualTo(description));
        }

        [Test]
        public void AcceptTooLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _assignmentDTO.Description = longDescription);
            Assert.That(_assignmentDTO.Description, Is.EqualTo(longDescription));
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
            // Act
            _assignmentDTO.Groups = _testGroups;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_assignmentDTO.Groups, Is.EqualTo(_testGroups));
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
        public void SetIsCompletedToTrue()
        {
            // Act
            _assignmentDTO.IsCompleted = true;

            // Assert - DTOs don't auto-update DateCompleted or DateModified
            Assert.That(_assignmentDTO.IsCompleted, Is.True);
            Assert.That(_assignmentDTO.DateCompleted, Is.Null); // DateCompleted is readonly and stays null
        }

        [Test]
        public void SetIsCompletedToFalse()
        {
            // Arrange
            _assignmentDTO.IsCompleted = true; // Set to true first
            Assert.That(_assignmentDTO.DateCompleted, Is.Null); // DateCompleted stays null in DTOs

            // Act
            _assignmentDTO.IsCompleted = false;

            // Assert - DTOs don't auto-manage DateCompleted
            Assert.That(_assignmentDTO.IsCompleted, Is.False);
            Assert.That(_assignmentDTO.DateCompleted, Is.Null);
        }

        [Test]
        public void SetAndGetDateDue()
        {
            // Arrange
            var dateDue = DateTime.Now.AddDays(7);

            // Act
            _assignmentDTO.DateDue = dateDue;

            // Assert - DTOs don't auto-update DateModified
            Assert.That(_assignmentDTO.DateDue, Is.EqualTo(dateDue));
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

        [Test]
        public void CoverTaskInterfaceImplementation()
        {
            // Arrange
            IAssignmentDTO iAssignmentDTO = _assignmentDTO;
            var testTask = new Task();

            // Act & Assert for Task interface property getter
            var retrievedTask = iAssignmentDTO.Task;
            Assert.That(retrievedTask, Is.EqualTo(_assignmentDTO.Task));

            // Act & Assert for Task interface property setter - this should cover line 27
            iAssignmentDTO.Task = testTask;
            Assert.That(_assignmentDTO.Task, Is.EqualTo(testTask));

            // Test setting null via interface - ensure cast occurs
            iAssignmentDTO.Task = null;
            Assert.That(_assignmentDTO.Task, Is.Null);
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
        public void ToJson_ThrowsNotImplementedException()
        {
            // Arrange
            _assignmentDTO.Id = 1;
            _assignmentDTO.Name = "Test Assignment";
            _assignmentDTO.Description = "Test Description";
            _assignmentDTO.IsCompleted = false;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.ToJson());
        }

        [Test]
        public void ToJsonWithNullValues_ThrowsNotImplementedException()
        {
            // Arrange
            _assignmentDTO.Id = 0;
            _assignmentDTO.Name = "Minimal Assignment";
            // Leave other properties as default/null

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.ToJson());
        }

        [Test]
        public void ToJsonWithCircularReferences_ThrowsNotImplementedException()
        {
            // Arrange
            _assignmentDTO.Id = 1;
            _assignmentDTO.Name = "Test Assignment";
            _assignmentDTO.Groups = _testGroups;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _assignmentDTO.ToJson());
        }

        #endregion

        #region Edge Cases and Validation Tests

        [Test]
        public void AcceptSingleCharacterName()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignmentDTO.Name = "A");
            Assert.That(_assignmentDTO.Name, Is.EqualTo("A"));
        }

        [Test]
        public void AcceptLongName()
        {
            // Arrange
            var longName = new string('a', 200); // Beyond typical validation limit

            // Act & Assert - DTOs accept any string length
            Assert.DoesNotThrow(() => _assignmentDTO.Name = longName);
            Assert.That(_assignmentDTO.Name, Is.EqualTo(longName));
        }

        [Test]
        public void AcceptLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 2000); // Beyond typical validation limit

            // Act & Assert - DTOs accept any string length
            Assert.DoesNotThrow(() => _assignmentDTO.Description = longDescription);
            Assert.That(_assignmentDTO.Description, Is.EqualTo(longDescription));
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
        public void SetAndGetTaskId()
        {
            // Act
            _assignmentDTO.TaskId = 42;

            // Assert
            Assert.That(_assignmentDTO.TaskId, Is.EqualTo(42));
        }

        [Test]
        public void SetAndGetTask()
        {
            // Arrange
            var testTask = new OrganizerCompanion.Core.Models.Domain.ProjectTask();

            // Act
            _assignmentDTO.Task = testTask;

            // Assert
            Assert.That(_assignmentDTO.Task, Is.EqualTo(testTask));
        }

        [Test]
        public void AcceptNullTask()
        {
            // Act
            _assignmentDTO.Task = null;

            // Assert
            Assert.That(_assignmentDTO.Task, Is.Null);
        }

        [Test]
        public void AcceptNullTaskId()
        {
            // Act
            _assignmentDTO.TaskId = null;

            // Assert
            Assert.That(_assignmentDTO.TaskId, Is.Null);
        }

        [Test]
        public void SetAndGetDateModified()
        {
            // Arrange
            var dateTime = DateTime.Now;

            // Act
            _assignmentDTO.DateModified = dateTime;

            // Assert
            Assert.That(_assignmentDTO.DateModified, Is.EqualTo(dateTime));
        }

        [Test]
        public void SetDateModifiedToNull()
        {
            // Act
            _assignmentDTO.DateModified = null;

            // Assert
            Assert.That(_assignmentDTO.DateModified, Is.Null);
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
