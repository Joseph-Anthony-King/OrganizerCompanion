using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class ProjectTaskShould
    {
        private ProjectTask _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ProjectTask();
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateTaskWithDefaultValues()
        {
            // Arrange & Act
            _sut = new ProjectTask();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.Name, Is.EqualTo(string.Empty));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.DateDue, Is.Null);
                Assert.That(_sut.DateCompleted, Is.Null);
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithAllParameters_ShouldCreateTaskCorrectly()
        {
            // Arrange
            var id = 1;
            var name = "Test Task";
            var description = "Test Description";
            var assignments = new List<ProjectAssignment>();
            var isCompleted = true;
            var dateDue = DateTime.Now.AddDays(7);
            var dateCompleted = DateTime.Now;
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now;

            // Act
            _sut = new ProjectTask(id, name, description, assignments, isCompleted, dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.Name, Is.EqualTo(name));
                Assert.That(_sut.Description, Is.EqualTo(description));
                Assert.That(_sut.Assignments, Is.EqualTo(assignments));
                Assert.That(_sut.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(_sut.DateDue, Is.EqualTo(dateDue));
                Assert.That(_sut.DateCompleted, Is.EqualTo(dateCompleted));
                Assert.That(_sut.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullValues_ShouldCreateTaskCorrectly()
        {
            // Arrange
            var id = 2;
            var name = "Another Task";
            var dateCreated = DateTime.Now;

            // Act
            _sut = new ProjectTask(id, name, null, null, false, null, null, dateCreated, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.Name, Is.EqualTo(name));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.DateDue, Is.Null);
                Assert.That(_sut.DateCompleted, Is.Null);
                Assert.That(_sut.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        #endregion

        #region Id Property Tests

        [Test, Category("Models")]
        public void Id_WhenSetToValidValue_ShouldReturnCorrectValue()
        {
            // Arrange
            var id = 123;

            // Act
            _sut.Id = id;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(id));
        }

        [Test, Category("Models")]
        public void Id_WhenSetToZero_ShouldReturnZero()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Id_WhenSetToMaxValue_ShouldReturnMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Id_WhenSetToNegativeValue_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.Id = 1;
            
            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region Name Property Tests

        [Test, Category("Models")]
        public void Name_WhenSetToValidValue_ShouldReturnCorrectValue()
        {
            // Arrange
            var name = "Valid Task Name";

            // Act
            _sut.Name = name;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(name));
        }

        [Test, Category("Models")]
        public void Name_WhenSetToMaxLength_ShouldReturnCorrectValue()
        {
            // Arrange
            var name = new string('A', 100);

            // Act
            _sut.Name = name;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(name));
        }

        [Test, Category("Models")]
        public void Name_WhenSetToSingleCharacter_ShouldReturnCorrectValue()
        {
            // Arrange
            var name = "A";

            // Act
            _sut.Name = name;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(name));
        }

        [Test, Category("Models")]
        public void Name_WhenSetToNull_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Name = null!);
        }

        [Test, Category("Models")]
        public void Name_WhenSetToEmptyString_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Name = string.Empty);
        }

        [Test, Category("Models")]
        public void Name_WhenSetToWhitespace_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Name = "   ");
        }

        [Test, Category("Models")]
        public void Name_WhenSetToTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var name = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Name = name);
        }

        [Test, Category("Models")]
        public void Name_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.Name = "Test Name";
            
            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region Description Property Tests

        [Test, Category("Models")]
        public void Description_WhenSetToValidValue_ShouldReturnCorrectValue()
        {
            // Arrange
            var description = "Valid task description";

            // Act
            _sut.Description = description;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(description));
        }

        [Test, Category("Models")]
        public void Description_WhenSetToMaxLength_ShouldReturnCorrectValue()
        {
            // Arrange
            var description = new string('A', 1000);

            // Act
            _sut.Description = description;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(description));
        }

        [Test, Category("Models")]
        public void Description_WhenSetToSingleCharacter_ShouldReturnCorrectValue()
        {
            // Arrange
            var description = "A";

            // Act
            _sut.Description = description;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(description));
        }

        [Test, Category("Models")]
        public void Description_WhenSetToNull_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = null!);
        }

        [Test, Category("Models")]
        public void Description_WhenSetToEmptyString_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = string.Empty);
        }

        [Test, Category("Models")]
        public void Description_WhenSetToWhitespace_ShouldThrowArgumentException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = "   ");
        }

        [Test, Category("Models")]
        public void Description_WhenSetToTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var description = new string('A', 1001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = description);
        }

        [Test, Category("Models")]
        public void Description_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.Description = "Test Description";
            
            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region Assignments Property Tests

        [Test, Category("Models")]
        public void Assignments_WhenSetToValidList_ShouldReturnCorrectValue()
        {
            // Arrange
            var assignments = new List<ProjectAssignment>
            {
                new ProjectAssignment { Id = 1, Name = "Assignment 1" },
                new ProjectAssignment { Id = 2, Name = "Assignment 2" }
            };

            // Act
            _sut.Assignments = assignments;

            // Assert
            Assert.That(_sut.Assignments, Is.EqualTo(assignments));
        }

        [Test, Category("Models")]
        public void Assignments_WhenSetToEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            var assignments = new List<ProjectAssignment>();

            // Act
            _sut.Assignments = assignments;

            // Assert
            Assert.That(_sut.Assignments, Is.EqualTo(assignments));
        }

        [Test, Category("Models")]
        public void Assignments_WhenSetToNull_ShouldCreateEmptyListAndSetToNull()
        {
            // Arrange & Act
            _sut.Assignments = null;

            // Assert
            Assert.That(_sut.Assignments, Is.Null);
        }

        [Test, Category("Models")]
        public void Assignments_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            var assignments = new List<ProjectAssignment>();
            
            // Act
            _sut.Assignments = assignments;
            
            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region IsCompleted Property Tests

        [Test, Category("Models")]
        public void IsCompleted_WhenSetToTrue_ShouldReturnTrue()
        {
            // Arrange & Act
            _sut.IsCompleted = true;

            // Assert
            Assert.That(_sut.IsCompleted, Is.True);
        }

        [Test, Category("Models")]
        public void IsCompleted_WhenSetToFalse_ShouldReturnFalse()
        {
            // Arrange & Act
            _sut.IsCompleted = false;

            // Assert
            Assert.That(_sut.IsCompleted, Is.False);
        }

        [Test, Category("Models")]
        public void IsCompleted_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.IsCompleted = true;
            
            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region DateDue Property Tests

        [Test, Category("Models")]
        public void DateDue_WhenSetToValidDate_ShouldReturnCorrectValue()
        {
            // Arrange
            var dateDue = DateTime.Now.AddDays(7);

            // Act
            _sut.DateDue = dateDue;

            // Assert
            Assert.That(_sut.DateDue, Is.EqualTo(dateDue));
        }

        [Test, Category("Models")]
        public void DateDue_WhenSetToNull_ShouldReturnNull()
        {
            // Arrange & Act
            _sut.DateDue = null;

            // Assert
            Assert.That(_sut.DateDue, Is.Null);
        }

        [Test, Category("Models")]
        public void DateDue_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            var dateDue = DateTime.Now.AddDays(7);
            
            // Act
            _sut.DateDue = dateDue;
            
            // Assert
            Assert.That(_sut.DateModified, Is.Not.Null);
            Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region DateCompleted Property Tests

        [Test, Category("Models")]
        public void DateCompleted_ShouldReturnReadOnlyValue()
        {
            // Arrange
            var dateCompleted = DateTime.Now;
            _sut = new ProjectTask(1, "Test", "Desc", null, false, null, dateCompleted, DateTime.Now, null);

            // Act & Assert
            Assert.That(_sut.DateCompleted, Is.EqualTo(dateCompleted));
        }

        [Test, Category("Models")]
        public void DateCompleted_WhenNull_ShouldReturnNull()
        {
            // Arrange
            _sut = new ProjectTask(1, "Test", "Desc", null, false, null, null, DateTime.Now, null);

            // Act & Assert
            Assert.That(_sut.DateCompleted, Is.Null);
        }

        #endregion

        #region DateCreated Property Tests

        [Test, Category("Models")]
        public void DateCreated_ShouldReturnReadOnlyValue()
        {
            // Arrange
            var dateCreated = DateTime.Now.AddDays(-1);
            _sut = new ProjectTask(1, "Test", "Desc", null, false, null, null, dateCreated, null);

            // Act & Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(dateCreated));
        }

        [Test, Category("Models")]
        public void DateCreated_WithDefaultConstructor_ShouldBeCloseToNow()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new ProjectTask();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.That(_sut.DateCreated, Is.InRange(beforeCreation, afterCreation));
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("Models")]
        public void ITask_Assignments_WhenSet_ShouldConvertAndSetCorrectly()
        {
            // Arrange 
            var task = (IProjectTask)_sut;
            var assignment1 = new ProjectAssignment { Id = 1, Name = "Assignment 1" };
            var assignment2 = new ProjectAssignment { Id = 2, Name = "Assignment 2" };
            var assignments = new List<IProjectAssignment> { assignment1, assignment2 };

            // Act
            task.Assignments = assignments;

            // Assert - Check that the assignments were set on the concrete implementation
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments.Count, Is.EqualTo(2));
            Assert.That(_sut.Assignments[0].Id, Is.EqualTo(1));
            Assert.That(_sut.Assignments[1].Id, Is.EqualTo(2));
        }

        [Test, Category("Models")]
        public void ITask_Assignments_WhenGetWithAssignments_ShouldThrowInvalidCastException()
        {
            // Arrange
            var task = (IProjectTask)_sut;
            var assignments = new List<ProjectAssignment>
            {
                new ProjectAssignment { Id = 1, Name = "Assignment 1" },
                new ProjectAssignment { Id = 2, Name = "Assignment 2" }
            };
            _sut.Assignments = assignments;

            // Act & Assert
            // The Cast<T>() method in Assignment throws InvalidCastException
            Assert.Throws<InvalidCastException>(() => { var result = task.Assignments; });
        }

        [Test, Category("Models")]
        public void ITask_Assignments_WhenGetWithNullAssignments_ShouldReturnNull()
        {
            // Arrange
            var task = (IProjectTask)_sut;
            _sut.Assignments = null;

            // Act
            var result = task.Assignments;

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region JSON Serialization Tests

        [Test, Category("Models")]
        public void JsonDeserialization_WithJsonConstructor_ShouldCreateCorrectObject()
        {
            // Arrange
            var json = @"{
                ""id"": 1,
                ""name"": ""JSON Task"",
                ""description"": ""JSON Description"",
                ""assignments"": [],
                ""isCompleted"": true,
                ""dateDue"": ""2025-12-31T00:00:00"",
                ""dateCompleted"": ""2025-10-20T12:00:00"",
                ""dateCreated"": ""2025-10-19T12:00:00"",
                ""dateModified"": ""2025-10-20T12:00:00""
            }";

            // Act
            var task = JsonSerializer.Deserialize<ProjectTask>(json);

            // Assert
            Assert.That(task, Is.Not.Null);
            Assert.That(task.Id, Is.EqualTo(1));
            Assert.That(task.Name, Is.EqualTo("JSON Task"));
            Assert.That(task.Description, Is.EqualTo("JSON Description"));
            Assert.That(task.IsCompleted, Is.True);
            Assert.That(task.DateDue, Is.EqualTo(new DateTime(2025, 12, 31)));
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("Models")]
        public void MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var initialTime = DateTime.UtcNow;
            System.Threading.Thread.Sleep(1); // Ensure different timestamps

            // Act & Assert - First change
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.Not.Null);
            Assert.That(firstModified, Is.GreaterThan(initialTime));

            System.Threading.Thread.Sleep(1); // Ensure different timestamps

            // Act & Assert - Second change  
            _sut.Name = "Test Name";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1); // Ensure different timestamps

            // Act & Assert - Third change
            _sut.IsCompleted = true;
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));
        }

        [Test, Category("Models")]
        public void Task_WithComplexAssignmentStructure_ShouldHandleCorrectly()
        {
            // Arrange
            var assignment1 = new ProjectAssignment 
            { 
                Id = 1, 
                Name = "Complex Assignment 1",
                Description = "Description 1"
            };
            var assignment2 = new ProjectAssignment 
            { 
                Id = 2, 
                Name = "Complex Assignment 2",
                Description = "Description 2"
            };
            var assignments = new List<ProjectAssignment> { assignment1, assignment2 };

            // Act
            _sut.Id = 1;
            _sut.Name = "Complex Task";
            _sut.Description = "Complex Description";
            _sut.Assignments = assignments;
            _sut.IsCompleted = false;
            _sut.DateDue = DateTime.Now.AddDays(30);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Assignments, Is.Not.Null);
                Assert.That(_sut.Assignments.Count, Is.EqualTo(2));
                Assert.That(_sut.Assignments[0].Name, Is.EqualTo("Complex Assignment 1"));
                Assert.That(_sut.Assignments[1].Name, Is.EqualTo("Complex Assignment 2"));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Task_PropertyValidation_WithBoundaryValues_ShouldBehaveProperly()
        {
            // Test Id boundary
            _sut.Id = 0;
            Assert.That(_sut.Id, Is.EqualTo(0));

            _sut.Id = int.MaxValue;
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

            // Test Name boundary
            _sut.Name = "A"; // Minimum length
            Assert.That(_sut.Name, Is.EqualTo("A"));

            _sut.Name = new string('B', 100); // Maximum length
            Assert.That(_sut.Name, Is.EqualTo(new string('B', 100)));

            // Test Description boundary
            _sut.Description = "C"; // Minimum length
            Assert.That(_sut.Description, Is.EqualTo("C"));

            _sut.Description = new string('D', 1000); // Maximum length
            Assert.That(_sut.Description, Is.EqualTo(new string('D', 1000)));
        }

        [Test, Category("Models")]
        public void Assignments_WhenSetToNullWithInitialization_ShouldTriggerNullCoalescingAssignment()
        {
            // Arrange - Make sure _assignments starts as null
            _sut.Assignments = null;
            Assert.That(_sut.Assignments, Is.Null);

            // Act - Set again to trigger the ??= logic
            var newAssignments = new List<ProjectAssignment> { new ProjectAssignment { Id = 1, Name = "Test" } };
            _sut.Assignments = newAssignments;

            // Assert
            Assert.That(_sut.Assignments, Is.EqualTo(newAssignments));
        }

        [Test, Category("Models")]
        public void ITask_Assignments_WhenSetToNull_ShouldSetCorrectly()
        {
            // Arrange
            var task = (IProjectTask)_sut;

            // Act
            task.Assignments = null;

            // Assert
            Assert.That(_sut.Assignments, Is.Null);
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToProjectTaskDTO_ShouldReturnCorrectType()
        {
            // Arrange & Act
            var dto = _sut.Cast<ProjectTaskDTO>();

            // Assert
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto, Is.TypeOf<ProjectTaskDTO>());
        }

        [Test, Category("Models")]
        public void Cast_ToIProjectTaskDTO_ShouldReturnCorrectType()
        {
            // Arrange & Act
            var dto = _sut.Cast<Interfaces.DataTransferObject.IProjectTaskDTO>();

            // Assert
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto, Is.TypeOf<ProjectTaskDTO>());
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange & Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<ProjectAssignment>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type ProjectAssignment"));
        }

        [Test, Category("Models")]
        public void Cast_HandlesExceptionCorrectly()
        {
            // Arrange - Test the try-catch block in Cast method
            _sut.Id = 1;
            _sut.Name = "Test Task";

            // Act & Assert - Should not throw for valid types
            Assert.DoesNotThrow(() => _sut.Cast<OrganizerCompanion.Core.Models.DataTransferObject.ProjectTaskDTO>());
        }

        #endregion

        #region ToJson Method Tests

        [Test, Category("Models")]
        public void ToJson_ShouldSerializeToValidJson()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Name = "Test Task";
            _sut.Description = "Test Description";
            _sut.IsCompleted = false;
            _sut.DateDue = new DateTime(2025, 12, 31);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Does.Contain("\"id\":1"));
            Assert.That(json, Does.Contain("\"name\":\"Test Task\""));
            Assert.That(json, Does.Contain("\"description\":\"Test Description\""));
            Assert.That(json, Does.Contain("\"isCompleted\":false"));
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldSerializeCorrectly()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Name = "Minimal Task";
            _sut.Description = "Simple Description";
            // Leave other properties as default/null

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Does.Contain("\"id\":0"));
            Assert.That(json, Does.Contain("\"name\":\"Minimal Task\""));
        }

        [Test, Category("Models")]
        public void ToJson_WithAssignments_ShouldHandleCircularReferences()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Name = "Task with Assignments";
            _sut.Description = "Test Description";
            _sut.Assignments =
            [
                new ProjectAssignment { Id = 1, Name = "Assignment 1" },
                new ProjectAssignment { Id = 2, Name = "Assignment 2" }
            ];

            // Act & Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() => _sut.ToJson());
        }

        #endregion
    }
}
