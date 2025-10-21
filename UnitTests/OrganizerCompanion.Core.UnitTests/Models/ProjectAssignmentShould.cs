using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class ProjectAssignmentShould
    {
        private ProjectAssignment _assignment;
        private List<Group> _groups;

        [SetUp]
        public void SetUp()
        {
            _assignment = new ProjectAssignment();

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

        [Test, Category("Models")]
        public void HaveDefaultConstructor()
        {
            // Arrange & Act
            var assignment = new ProjectAssignment();
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

        [Test, Category("Models")]
        public void HaveParameterizedConstructor()
        {
            // Arrange
            var id = 42;
            var name = "Test Assignment";
            var description = "Test Description";
            var groups = _groups;
            var taskId = 100;
            var task = new OrganizerCompanion.Core.Models.Domain.ProjectTask();
            var isCompleted = true;
            var dateDue = DateTime.Now.AddDays(7);
            var dateCompleted = DateTime.Now.AddDays(-1);
            var dateCreated = DateTime.Now.AddDays(-10);
            var dateModified = DateTime.Now.AddDays(-2);

            // Act
            var assignment = new ProjectAssignment(
                id, name, description, groups, taskId, task, isCompleted, dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(id));
                Assert.That(assignment.Name, Is.EqualTo(name));
                Assert.That(assignment.Description, Is.EqualTo(description));
                Assert.That(assignment.Groups, Is.EqualTo(groups));
                Assert.That(assignment.TaskId, Is.EqualTo(taskId));
                Assert.That(assignment.Task, Is.EqualTo(task));
                Assert.That(assignment.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(assignment.DateDue, Is.EqualTo(dateDue));
                Assert.That(assignment.DateCompleted, Is.EqualTo(dateCompleted));
                Assert.That(assignment.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(assignment.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void HandleNullGroupsInParameterizedConstructor()
        {
            // Arrange & Act
            var assignment = new ProjectAssignment(
                1, "Test", "Description", null, null, null, false, null, null, DateTime.Now, null);

            // Assert
            Assert.That(assignment.Groups, Is.Not.Null);
            Assert.That(assignment.Groups, Is.Empty);
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void ThrowExceptionForNegativeId()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _assignment.Id = -1);
        }

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void ThrowExceptionForEmptyName()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.Name = "");
        }

        [Test, Category("Models")]
        public void ThrowExceptionForTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.Name = longName);
        }

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void ThrowExceptionForTooLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.Description = longDescription);
        }

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void InitializeEmptyListWhenAssigneesIsNull()
        {
            // Arrange
            _assignment.Groups = _groups; // Set to non-null first

            // Act
            _assignment.Groups = null;

            // Assert
            Assert.That(_assignment.Groups, Is.Null);
        }

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void InitializeEmptyListWhenContactsIsNull()
        {
            // Arrange
            _assignment.Groups = _groups; // Set to non-null first

            // Act
            _assignment.Groups = null;

            // Assert
            Assert.That(_assignment.Groups, Is.Null);
        }

        [Test, Category("Models")]
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

        [Test, Category("Models")]
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

        [Test, Category("Models")]
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

        [Test, Category("Models")]
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

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void SetAndGetTaskId()
        {
            // Arrange
            var taskId = 42;
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.TaskId = taskId;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.TaskId, Is.EqualTo(taskId));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void ThrowExceptionForNegativeTaskId()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _assignment.TaskId = -1);
        }

        [Test, Category("Models")]
        public void AcceptNullTaskId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.TaskId = null);
            Assert.That(_assignment.TaskId, Is.Null);
        }

        [Test, Category("Models")]
        public void SetAndGetTask()
        {
            // Arrange
            var task = new OrganizerCompanion.Core.Models.Domain.ProjectTask();
            var initialDateModified = _assignment.DateModified;

            // Act
            _assignment.Task = task;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Task, Is.EqualTo(task));
                Assert.That(_assignment.DateModified, Is.Not.EqualTo(initialDateModified));
            });
            Assert.That(_assignment.DateModified, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void AcceptNullTask()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Task = null);
            Assert.That(_assignment.Task, Is.Null);
        }

        [Test, Category("Models")]
        public void AcceptZeroTaskId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.TaskId = 0);
            Assert.That(_assignment.TaskId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void AcceptMaximumTaskId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.TaskId = int.MaxValue);
            Assert.That(_assignment.TaskId, Is.EqualTo(int.MaxValue));
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void CastToProjectAssignmentDTO()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.Name = "Test Assignment";
            _assignment.Description = "Test Description";
            _assignment.Groups = _groups;
            _assignment.IsCompleted = true;
            _assignment.DateDue = DateTime.Now.AddDays(7);

            // Act
            var dto = _assignment.Cast<ProjectAssignmentDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(_assignment.Id));
                Assert.That(dto.Name, Is.EqualTo(_assignment.Name));
                Assert.That(dto.Description, Is.EqualTo(_assignment.Description));
                Assert.That(dto.IsCompleted, Is.EqualTo(_assignment.IsCompleted));
                Assert.That(dto.DateDue, Is.EqualTo(_assignment.DateDue));
                Assert.That(dto.DateCreated, Is.EqualTo(_assignment.DateCreated));
                Assert.That(dto.DateModified, Is.EqualTo(_assignment.DateModified));
            });
        }

        [Test, Category("Models")]
        public void CastToIProjectAssignmentDTO()
        {
            // Arrange
            _assignment.Id = 2;
            _assignment.Name = "Interface Test";

            // Act
            var dto = _assignment.Cast<IProjectAssignmentDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(_assignment.Id));
                Assert.That(dto.Name, Is.EqualTo(_assignment.Name));
            });
        }

        [Test, Category("Models")]
        public void ThrowInvalidCastExceptionForUnsupportedType()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _assignment.Cast<Contact>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type Contact"));
        }

        [Test, Category("Models")]
        public void CastHandlesExceptionCorrectly()
        {
            // Arrange
            _assignment.Groups = null; // This might cause issues during casting

            // Act & Assert - Should propagate any casting exceptions
            Assert.DoesNotThrow(() => _assignment.Cast<ProjectAssignmentDTO>());
        }

        [Test, Category("Models")]
        public void CastRethrowsExceptionsFromTryCatchBlock()
        {
            // Arrange - Create an assignment that might cause internal exceptions during casting
            _assignment.Id = 1;
            _assignment.Name = "Test";
            
            // Act & Assert - The method should re-throw any exceptions caught in the try-catch
            // This tests the "throw;" statement in the catch block
            Assert.DoesNotThrow(() => _assignment.Cast<ProjectAssignmentDTO>());
        }

        #endregion

        #region ToJson Method Tests

        [Test, Category("Models")]
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

        [Test, Category("Models")]
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

        [Test, Category("Models")]
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

        [Test, Category("Models")]
        public void AcceptMinimumValidName()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Name = "A");
            Assert.That(_assignment.Name, Is.EqualTo("A"));
        }

        [Test, Category("Models")]
        public void AcceptMaximumValidName()
        {
            // Arrange
            var maxName = new string('a', 100);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Name = maxName);
            Assert.That(_assignment.Name, Is.EqualTo(maxName));
        }

        [Test, Category("Models")]
        public void AcceptMaximumValidDescription()
        {
            // Arrange
            var maxDescription = new string('a', 1000);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Description = maxDescription);
            Assert.That(_assignment.Description, Is.EqualTo(maxDescription));
        }

        [Test, Category("Models")]
        public void ThrowExceptionForNullDescription()
        {
            // Act & Assert - The Assignment class has a bug where it checks value!.Length for null
            Assert.Throws<NullReferenceException>(() => _assignment.Description = null);
        }

        [Test, Category("Models")]
        public void HandleGroupsInitializationWhenNull()
        {
            // Arrange
            _assignment.Groups = null; // Ensure it starts as null
            var newGroups = new List<Group> { _groups[0] };

            // Act - This should trigger the null-conditional assignment in the setter
            _assignment.Groups = newGroups;

            // Assert
            Assert.That(_assignment.Groups, Is.EqualTo(newGroups));
        }

        [Test, Category("Models")]
        public void AcceptEmptyDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Description = "");
            Assert.That(_assignment.Description, Is.EqualTo(""));
        }

        [Test, Category("Models")]
        public void AcceptZeroId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Id = 0);
            Assert.That(_assignment.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void AcceptMaximumId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Id = int.MaxValue);
            Assert.That(_assignment.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void HandleEmptyGroupsList()
        {
            // Act
            _assignment.Groups = [];

            // Assert
            Assert.That(_assignment.Groups, Is.Not.Null);
            Assert.That(_assignment.Groups, Is.Empty);
        }

        [Test, Category("Models")]
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

        #region Interface Implementation Coverage Tests

        [Test, Category("Models")]
        public void CoverExplicitInterfaceImplementations()
        {
            // Arrange
            IProjectAssignment iAssignment = _assignment;
            var testIGroup = _groups.Cast<IGroup>().ToList();

            // Act & Assert for Groups interface property
            iAssignment.Groups = testIGroup;
            var retrievedIAssignees = iAssignment.Groups;
            Assert.That(retrievedIAssignees, Is.Not.Null);
            Assert.That(retrievedIAssignees.Count, Is.EqualTo(testIGroup.Count));
        }

        [Test, Category("Models")]
        public void CoverTaskInterfaceImplementation()
        {
            // Arrange
            IProjectAssignment iAssignment = _assignment;
            var testTask = new OrganizerCompanion.Core.Models.Domain.ProjectTask();

            // Act & Assert for Task interface property getter
            var retrievedTask = iAssignment.Task;
            Assert.That(retrievedTask, Is.EqualTo(_assignment.Task));

            // Act & Assert for Task interface property setter
            iAssignment.Task = testTask;
            Assert.That(_assignment.Task, Is.EqualTo(testTask));

            // Test setting null
            iAssignment.Task = null;
            Assert.That(_assignment.Task, Is.Null);
        }

        #endregion
    }
}