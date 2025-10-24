using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class ProjectTaskShould
    {
        private ProjectTask _sut;

        // Test implementation of IProjectTaskDTO that properly handles null values
        private class TestProjectTaskDTO : IProjectTaskDTO
        {
            public int Id { get; set; }
            public string ProjectTaskName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public List<IProjectAssignmentDTO>? Assignments { get; set; }
            public bool IsCompleted { get; set; }
            public DateTime? DueDate { get; set; }
            public DateTime? CompletedDate { get; private set; }
            public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

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
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(string.Empty));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.DueDate, Is.Null);
                Assert.That(_sut.CompletedDate, Is.Null);
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(DateTime.Now));
                Assert.That(_sut.ModifiedDate, Is.Null);
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
            var dueDate = DateTime.Now.AddDays(7);
            var completedDate = DateTime.Now;
            var createdDate = DateTime.Now.AddDays(-1);
            var modifiedDate = DateTime.Now;

            // Act
            _sut = new ProjectTask(id, name, description, assignments, isCompleted, dueDate, completedDate, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(name));
                Assert.That(_sut.Description, Is.EqualTo(description));
                Assert.That(_sut.Assignments, Is.EqualTo(assignments));
                Assert.That(_sut.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(_sut.DueDate, Is.EqualTo(dueDate));
                Assert.That(_sut.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(_sut.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullValues_ShouldCreateTaskCorrectly()
        {
            // Arrange
            var id = 2;
            var name = "Another Task";
            var createdDate = DateTime.Now;

            // Act
            _sut = new ProjectTask(id, name, null, null, false, null, null, createdDate, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(name));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.DueDate, Is.Null);
                Assert.That(_sut.CompletedDate, Is.Null);
                Assert.That(_sut.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithValidDTO_ShouldCreateTaskCorrectly()
        {
            // Arrange
            var dto = new ProjectTaskDTO
            {
                Id = 1,
                ProjectTaskName = "DTO Task",
                Description = "DTO Description",
                Assignments = new List<ProjectAssignmentDTO>
                {
                    new ProjectAssignmentDTO { Id = 1, ProjectAssignmentName = "Assignment 1", Groups = new List<GroupDTO>() },
                    new ProjectAssignmentDTO { Id = 2, ProjectAssignmentName = "Assignment 2", Groups = new List<GroupDTO>() }
                },
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(7),
                ModifiedDate = DateTime.Now
            };

            // Act
            _sut = new ProjectTask(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(dto.ProjectTaskName));
                Assert.That(_sut.Description, Is.EqualTo(dto.Description));
                Assert.That(_sut.Assignments, Is.Not.Null);
                Assert.That(_sut.Assignments, Has.Count.EqualTo(2));
                Assert.That(_sut.IsCompleted, Is.EqualTo(dto.IsCompleted));
                Assert.That(_sut.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(_sut.CompletedDate, Is.EqualTo(dto.CompletedDate));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullAssignments_ShouldCreateTaskWithNullAssignments()
        {
            // Arrange
            // Create a custom DTO implementation that properly handles null assignments
            var dto = new TestProjectTaskDTO
            {
                Id = 2,
                ProjectTaskName = "DTO Task No Assignments",
                Description = "DTO Description",
                Assignments = null,
                IsCompleted = false,
                DueDate = null,
                ModifiedDate = null
            };

            // Act
            _sut = new ProjectTask(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(dto.ProjectTaskName));
                Assert.That(_sut.Description, Is.EqualTo(dto.Description));
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.EqualTo(dto.IsCompleted));
                Assert.That(_sut.DueDate, Is.Null);
                Assert.That(_sut.CompletedDate, Is.EqualTo(dto.CompletedDate));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithEmptyAssignments_ShouldCreateTaskWithEmptyAssignments()
        {
            // Arrange
            var dto = new ProjectTaskDTO
            {
                Id = 3,
                ProjectTaskName = "DTO Task Empty Assignments",
                Description = "DTO Description",
                Assignments = new List<ProjectAssignmentDTO>(),
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(14),
                ModifiedDate = DateTime.Now
            };

            // Act
            _sut = new ProjectTask(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(dto.ProjectTaskName));
                Assert.That(_sut.Description, Is.EqualTo(dto.Description));
                Assert.That(_sut.Assignments, Is.Not.Null);
                Assert.That(_sut.Assignments, Is.Empty);
                Assert.That(_sut.IsCompleted, Is.EqualTo(dto.IsCompleted));
                Assert.That(_sut.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(_sut.CompletedDate, Is.EqualTo(dto.CompletedDate));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullDescription_ShouldCreateTaskWithNullDescription()
        {
            // Arrange
            var dto = new TestProjectTaskDTO
            {
                Id = 4,
                ProjectTaskName = "DTO Task No Description",
                Description = null,
                Assignments = null,
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(3),
                ModifiedDate = DateTime.Now
            };

            // Act
            _sut = new ProjectTask(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo(dto.ProjectTaskName));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.EqualTo(dto.IsCompleted));
                Assert.That(_sut.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(_sut.CompletedDate, Is.EqualTo(dto.CompletedDate));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMinimalValidData_ShouldCreateTaskCorrectly()
        {
            // Arrange
            var dto = new TestProjectTaskDTO
            {
                Id = 0,
                ProjectTaskName = "A", // Minimum valid name
                Description = null,
                Assignments = null,
                IsCompleted = false,
                DueDate = null,
                ModifiedDate = null
            };

            // Act
            _sut = new ProjectTask(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.ProjectTaskName, Is.EqualTo("A"));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Assignments, Is.Null);
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.DueDate, Is.Null);
                Assert.That(_sut.CompletedDate, Is.Null);
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.Null);
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
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.Id = 1;
            
            // Assert
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
            Assert.That(_sut.ModifiedDate, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region Name Property Tests

        [Test, Category("Models")]
        public void Name_WhenSetToValidValue_ShouldReturnCorrectValue()
        {
            // Arrange
            var name = "Valid Task Name";

            // Act
            _sut.ProjectTaskName = name;

            // Assert
            Assert.That(_sut.ProjectTaskName, Is.EqualTo(name));
        }

        [Test, Category("Models")]
        public void Name_WhenSetToMaxLength_ShouldReturnCorrectValue()
        {
            // Arrange
            var name = new string('A', 100);

            // Act
            _sut.ProjectTaskName = name;

            // Assert
            Assert.That(_sut.ProjectTaskName, Is.EqualTo(name));
        }

        [Test, Category("Models")]
        public void Name_WhenSetToSingleCharacter_ShouldReturnCorrectValue()
        {
            // Arrange
            var name = "A";

            // Act
            _sut.ProjectTaskName = name;

            // Assert
            Assert.That(_sut.ProjectTaskName, Is.EqualTo(name));
        }

        [Test, Category("Models")]
        public void Name_WhenSetToNull_ShouldThrowArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ProjectTaskName = null!);
        }

        [Test, Category("Models")]
        public void Name_WhenSetToEmptyString_ShouldThrowArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ProjectTaskName = string.Empty);
        }

        [Test, Category("Models")]
        public void Name_WhenSetToWhitespace_ShouldThrowArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ProjectTaskName = "   ");
        }

        [Test, Category("Models")]
        public void Name_WhenSetToTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var name = new string('A', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ProjectTaskName = name);
        }

        [Test, Category("Models")]
        public void Name_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.ProjectTaskName = "Test Name";
            
            // Assert
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
            Assert.That(_sut.ModifiedDate, Is.GreaterThanOrEqualTo(beforeSet));
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
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = null!);
        }

        [Test, Category("Models")]
        public void Description_WhenSetToEmptyString_ShouldThrowArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = string.Empty);
        }

        [Test, Category("Models")]
        public void Description_WhenSetToWhitespace_ShouldThrowArgumentException()
        {
            // Arrange, Act & Assert
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
        public void Description_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.Description = "Test Description";
            
            // Assert
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
            Assert.That(_sut.ModifiedDate, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region Assignments Property Tests

        [Test, Category("Models")]
        public void Assignments_WhenSetToValidList_ShouldReturnCorrectValue()
        {
            // Arrange
            var assignments = new List<ProjectAssignment>
            {
                new ProjectAssignment { Id = 1, ProjectAssignmentName = "Assignment 1" },
                new ProjectAssignment { Id = 2, ProjectAssignmentName = "Assignment 2" }
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
        public void Assignments_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            var assignments = new List<ProjectAssignment>();
            
            // Act
            _sut.Assignments = assignments;
            
            // Assert
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
            Assert.That(_sut.ModifiedDate, Is.GreaterThanOrEqualTo(beforeSet));
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
        public void IsCompleted_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            
            // Act
            _sut.IsCompleted = true;
            
            // Assert
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
            Assert.That(_sut.ModifiedDate, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region DueDate Property Tests

        [Test, Category("Models")]
        public void DueDate_WhenSetToValidDate_ShouldReturnCorrectValue()
        {
            // Arrange
            var dueDate = DateTime.Now.AddDays(7);

            // Act
            _sut.DueDate = dueDate;

            // Assert
            Assert.That(_sut.DueDate, Is.EqualTo(dueDate));
        }

        [Test, Category("Models")]
        public void DueDate_WhenSetToNull_ShouldReturnNull()
        {
            // Arrange & Act
            _sut.DueDate = null;

            // Assert
            Assert.That(_sut.DueDate, Is.Null);
        }

        [Test, Category("Models")]
        public void DueDate_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow;
            var dueDate = DateTime.Now.AddDays(7);
            
            // Act
            _sut.DueDate = dueDate;
            
            // Assert
            Assert.That(_sut.ModifiedDate, Is.Not.Null);
            Assert.That(_sut.ModifiedDate, Is.GreaterThanOrEqualTo(beforeSet));
        }

        #endregion

        #region CompletedDate Property Tests

        [Test, Category("Models")]
        public void CompletedDate_ShouldReturnReadOnlyValue()
        {
            // Arrange
            var completedDate = DateTime.Now;
            _sut = new ProjectTask(1, "Test", "Desc", null, false, null, completedDate, DateTime.Now, null);

            // Act & Assert
            Assert.That(_sut.CompletedDate, Is.EqualTo(completedDate));
        }

        [Test, Category("Models")]
        public void CompletedDate_WhenNull_ShouldReturnNull()
        {
            // Arrange
            _sut = new ProjectTask(1, "Test", "Desc", null, false, null, null, DateTime.Now, null);

            // Act & Assert
            Assert.That(_sut.CompletedDate, Is.Null);
        }

        #endregion

        #region CreatedDate Property Tests

        [Test, Category("Models")]
        public void CreatedDate_ShouldReturnReadOnlyValue()
        {
            // Arrange
            var createdDate = DateTime.Now.AddDays(-1);
            _sut = new ProjectTask(1, "Test", "Desc", null, false, null, null, createdDate, null);

            // Act & Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(createdDate));
        }

        [Test, Category("Models")]
        public void CreatedDate_WithDefaultConstructor_ShouldBeCloseToNow()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new ProjectTask();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.That(_sut.CreatedDate, Is.InRange(beforeCreation, afterCreation));
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("Models")]
        public void ITask_Assignments_WhenSet_ShouldConvertAndSetCorrectly()
    {
      // Arrange 
      var task = (IProjectTask)_sut;
            var assignment1 = new ProjectAssignment { Id = 1, ProjectAssignmentName = "Assignment 1" };
            var assignment2 = new ProjectAssignment { Id = 2, ProjectAssignmentName = "Assignment 2" };
            var assignments = new List<IProjectAssignment> { assignment1, assignment2 };

            // Act
            task.Assignments = assignments;

            // Assert - Check that the assignments were set on the concrete implementation
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments, Has.Count.EqualTo(2));
      Assert.Multiple(() =>
      {
        Assert.That(_sut.Assignments[0].Id, Is.EqualTo(1));
        Assert.That(_sut.Assignments[1].Id, Is.EqualTo(2));
      });
    }

    [Test, Category("Models")]
        public void ITask_Assignments_WhenGetWithAssignments_ShouldThrowInvalidCastException()
        {
            // Arrange
            var task = (IProjectTask)_sut;
            var assignments = new List<ProjectAssignment>
            {
                new ProjectAssignment { Id = 1, ProjectAssignmentName = "Assignment 1" },
                new ProjectAssignment { Id = 2, ProjectAssignmentName = "Assignment 2" }
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
                ""dueDate"": ""2025-12-31T00:00:00"",
                ""completedDate"": ""2025-10-20T12:00:00"",
                ""createdDate"": ""2025-10-19T12:00:00"",
                ""modifiedDate"": ""2025-10-20T12:00:00""
            }";

            // Act
            var task = JsonSerializer.Deserialize<ProjectTask>(json);

            // Assert
            Assert.That(task, Is.Not.Null);
      Assert.Multiple(() =>
      {
        Assert.That(task.Id, Is.EqualTo(1));
        Assert.That(task.ProjectTaskName, Is.EqualTo("JSON Task"));
        Assert.That(task.Description, Is.EqualTo("JSON Description"));
        Assert.That(task.IsCompleted, Is.True);
        Assert.That(task.DueDate, Is.EqualTo(new DateTime(2025, 12, 31)));
      });
    }

    #endregion

    #region Edge Case Tests

    [Test, Category("Models")]
        public void MultiplePropertyChanges_ShouldUpdateModifiedDateEachTime()
        {
            // Arrange
            var initialTime = DateTime.UtcNow;
            System.Threading.Thread.Sleep(1); // Ensure different timestamps

            // Act & Assert - First change
            _sut.Id = 1;
            var firstModified = _sut.ModifiedDate;
            Assert.That(firstModified, Is.Not.Null);
            Assert.That(firstModified, Is.GreaterThan(initialTime));

            System.Threading.Thread.Sleep(1); // Ensure different timestamps

            // Act & Assert - Second change  
            _sut.ProjectTaskName = "Test Name";
            var secondModified = _sut.ModifiedDate;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1); // Ensure different timestamps

            // Act & Assert - Third change
            _sut.IsCompleted = true;
            var thirdModified = _sut.ModifiedDate;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));
        }

        [Test, Category("Models")]
        public void Task_WithComplexAssignmentStructure_ShouldHandleCorrectly()
        {
            // Arrange
            var assignment1 = new ProjectAssignment 
            { 
                Id = 1, 
                ProjectAssignmentName = "Complex Assignment 1",
                Description = "Description 1"
            };
            var assignment2 = new ProjectAssignment 
            { 
                Id = 2, 
                ProjectAssignmentName = "Complex Assignment 2",
                Description = "Description 2"
            };
            var assignments = new List<ProjectAssignment> { assignment1, assignment2 };

            // Act
            _sut.Id = 1;
            _sut.ProjectTaskName = "Complex Task";
            _sut.Description = "Complex Description";
            _sut.Assignments = assignments;
            _sut.IsCompleted = false;
            _sut.DueDate = DateTime.Now.AddDays(30);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Assignments, Is.Not.Null);
                Assert.That(_sut.Assignments, Has.Count.EqualTo(2));
                Assert.That(_sut.Assignments[0].ProjectAssignmentName, Is.EqualTo("Complex Assignment 1"));
                Assert.That(_sut.Assignments[1].ProjectAssignmentName, Is.EqualTo("Complex Assignment 2"));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
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
            _sut.ProjectTaskName = "A"; // Minimum length
            Assert.That(_sut.ProjectTaskName, Is.EqualTo("A"));

            _sut.ProjectTaskName = new string('B', 100); // Maximum length
            Assert.That(_sut.ProjectTaskName, Is.EqualTo(new string('B', 100)));

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
            var newAssignments = new List<ProjectAssignment> { new ProjectAssignment { Id = 1, ProjectAssignmentName = "Test" } };
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
            // Arrange, Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<ProjectAssignment>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type ProjectAssignment"));
        }

        [Test, Category("Models")]
        public void Cast_HandlesExceptionCorrectly()
        {
            // Arrange - Test the try-catch block in Cast method
            _sut.Id = 1;
            _sut.ProjectTaskName = "Test Task";

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
            _sut.ProjectTaskName = "Test Task";
            _sut.Description = "Test Description";
            _sut.IsCompleted = false;
            _sut.DueDate = new DateTime(2025, 12, 31);

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
            _sut.ProjectTaskName = "Minimal Task";
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
            _sut.ProjectTaskName = "Task with Assignments";
            _sut.Description = "Test Description";
            _sut.Assignments =
            [
                new ProjectAssignment { Id = 1, ProjectAssignmentName = "Assignment 1" },
                new ProjectAssignment { Id = 2, ProjectAssignmentName = "Assignment 2" }
            ];

            // Act & Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() => _sut.ToJson());
        }

        #endregion
    }
}
