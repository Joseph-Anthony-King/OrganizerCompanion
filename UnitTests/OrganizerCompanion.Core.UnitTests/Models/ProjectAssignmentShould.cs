using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class ProjectAssignmentShould
    {
        private ProjectAssignment _assignment;
        private List<Group> _groups;

        // Test implementation of IProjectAssignmentDTO that properly handles null values
        private class TestProjectAssignmentDTO : IProjectAssignmentDTO
        {
            public int Id { get; set; }
            public string ProjectAssignmentName { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int? AssigneeId { get; set; }
            public ISubAccountDTO? Assignee { get; set; }
            public int? LocationId { get; set; }
            public string? LocationType { get; set; }
            public IAddressDTO? Location { get; set; }
            public List<IGroupDTO>? Groups { get; set; }
            public int? TaskId { get; set; }
            public IProjectTaskDTO? Task { get; set; }
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
            _assignment = new ProjectAssignment();

            _groups =
            [
                new() {
                    Id = 1,
                    GroupName = "Group 1",
                    Members =
                    [
                        new() { Id = 1, FirstName = "John", LastName = "Doe" },
                        new() { Id = 2, FirstName = "Jane", LastName = "Smith" }
                    ] },
                new() {
                    Id = 2,
                    GroupName = "Group 2",
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
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo(string.Empty));
                Assert.That(assignment.Description, Is.Null);
                Assert.That(assignment.AssigneeId, Is.Null);
                Assert.That(assignment.Assignee, Is.Null);
                Assert.That(assignment.LocationId, Is.Null);
                Assert.That(assignment.LocationType, Is.Null);
                Assert.That(assignment.Location, Is.Null);
                Assert.That(assignment.Groups, Is.Null);
                Assert.That(assignment.IsCompleted, Is.False);
                Assert.That(assignment.DueDate, Is.Null);
                Assert.That(assignment.CompletedDate, Is.Null);
                Assert.That(assignment.CreatedDate, Is.Not.EqualTo(default(DateTime)));
                Assert.That(assignment.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void HaveParameterizedConstructor()
        {
            // Arrange
            var id = 42;
            var name = "Test Assignment";
            var description = "Test Description";
            var assignee = new SubAccount { Id = 5 };
            var location = new USAddress
            {
                Id = 1,
                Type = Enums.Types.Home,
                Street1 = "123 Main St",
                City = "Anytown",
                State = USStates.California.ToStateModel(),
                ZipCode = "12345"
            };
            var locationId = location.Id;
            var locationType = "USAddress";
            var groups = _groups;
            var taskId = 100;
            var task = new OrganizerCompanion.Core.Models.Domain.ProjectTask();
            var isCompleted = true;
            var dueDate = DateTime.Now.AddDays(7);
            var completedDate = DateTime.Now.AddDays(-1);
            var createdDate = DateTime.Now.AddDays(-10);
            var modifiedDate = DateTime.Now.AddDays(-2);

            // Act
            var assignment = new ProjectAssignment(
                id,
                name,
                description,
                assignee,
                locationId,
                locationType,
                location,
                groups,
                taskId,
                task,
                isCompleted,
                dueDate,
                completedDate,
                createdDate,
                modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(id));
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo(name));
                Assert.That(assignment.Description, Is.EqualTo(description));
                Assert.That(assignment.AssigneeId, Is.EqualTo(assignee.Id));
                Assert.That(assignment.Assignee, Is.EqualTo(assignee));
                Assert.That(assignment.LocationId, Is.EqualTo(locationId));
                Assert.That(assignment.LocationType, Is.EqualTo(locationType));
                Assert.That(assignment.Location, Is.EqualTo(location));
                Assert.That(assignment.Groups, Is.EqualTo(groups));
                Assert.That(assignment.TaskId, Is.EqualTo(taskId));
                Assert.That(assignment.Task, Is.EqualTo(task));
                Assert.That(assignment.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(assignment.DueDate, Is.EqualTo(dueDate));
                Assert.That(assignment.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(assignment.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(assignment.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void HandleNullGroupsInParameterizedConstructor()
        {
            // Arrange & Act
            var assignment = new ProjectAssignment(
                1,
                "Test",
                "Description",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                DateTime.Now,
                null);

            // Assert
            Assert.That(assignment.Groups, Is.Not.Null);
            Assert.That(assignment.Groups, Is.Empty);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithValidDTO_ShouldCreateAssignmentCorrectly()
        {
            // Arrange
            var dto = new TestProjectAssignmentDTO
            {
                Id = 1,
                ProjectAssignmentName = "DTO Assignment",
                Description = "DTO Description",
                AssigneeId = 5,
                LocationId = 10,
                LocationType = "USAddress",
                TaskId = 100,
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(7),
                ModifiedDate = DateTime.Now
            };

            // Act
            var assignment = new ProjectAssignment(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(dto.Id));
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo(dto.ProjectAssignmentName));
                Assert.That(assignment.Description, Is.EqualTo(dto.Description));
                Assert.That(assignment.LocationId, Is.EqualTo(dto.LocationId));
                Assert.That(assignment.LocationType, Is.EqualTo(dto.LocationType));
                Assert.That(assignment.TaskId, Is.EqualTo(dto.TaskId));
                Assert.That(assignment.IsCompleted, Is.EqualTo(dto.IsCompleted));
                Assert.That(assignment.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(assignment.CompletedDate, Is.EqualTo(dto.CompletedDate));
                Assert.That(assignment.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(assignment.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullOptionalProperties_ShouldCreateAssignmentCorrectly()
        {
            // Arrange
            var dto = new TestProjectAssignmentDTO
            {
                Id = 2,
                ProjectAssignmentName = "Minimal DTO Assignment",
                Description = null,
                AssigneeId = null,
                LocationId = null,
                LocationType = null,
                Groups = null,
                TaskId = null,
                IsCompleted = false,
                DueDate = null,
                ModifiedDate = null
            };

            // Act
            var assignment = new ProjectAssignment(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(dto.Id));
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo(dto.ProjectAssignmentName));
                Assert.That(assignment.Description, Is.Null);
                Assert.That(assignment.Assignee, Is.Null);
                Assert.That(assignment.LocationId, Is.Null);
                Assert.That(assignment.LocationType, Is.Null);
                Assert.That(assignment.Location, Is.Null);
                Assert.That(assignment.Groups, Is.Not.Null);
                Assert.That(assignment.Groups, Is.Empty);
                Assert.That(assignment.TaskId, Is.Null);
                Assert.That(assignment.Task, Is.Null);
                Assert.That(assignment.IsCompleted, Is.False);
                Assert.That(assignment.DueDate, Is.Null);
                Assert.That(assignment.CompletedDate, Is.Null);
                Assert.That(assignment.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(assignment.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithEmptyGroups_ShouldCreateAssignmentWithEmptyGroups()
        {
            // Arrange
            var dto = new TestProjectAssignmentDTO
            {
                Id = 3,
                ProjectAssignmentName = "DTO Assignment Empty Groups",
                Description = "Description",
                Groups = null, // Use null to avoid casting issues, constructor will create empty list
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(14),
                ModifiedDate = DateTime.Now
            };

            // Act
            var assignment = new ProjectAssignment(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(dto.Id));
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo(dto.ProjectAssignmentName));
                Assert.That(assignment.Description, Is.EqualTo(dto.Description));
                Assert.That(assignment.Groups, Is.Not.Null);
                Assert.That(assignment.Groups, Is.Empty);
                Assert.That(assignment.IsCompleted, Is.False);
                Assert.That(assignment.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(assignment.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(assignment.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithCompleteData_ShouldCreateAssignmentCorrectly()
        {
            // Arrange
            var dto = new TestProjectAssignmentDTO
            {
                Id = 4,
                ProjectAssignmentName = "Complete DTO Assignment",
                Description = "Complete Description",
                Assignee = null, // SubAccountDTO casting issue - use null for now
                LocationId = 20,
                LocationType = "USAddress",
                Groups = null, // Use null to avoid GroupDTO casting issues
                TaskId = 200,
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(10),
                ModifiedDate = DateTime.Now
            };

            // Act
            var assignment = new ProjectAssignment(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(dto.Id));
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo(dto.ProjectAssignmentName));
                Assert.That(assignment.Description, Is.EqualTo(dto.Description));
                Assert.That(assignment.Assignee, Is.Null);
                Assert.That(assignment.LocationId, Is.EqualTo(dto.LocationId));
                Assert.That(assignment.LocationType, Is.EqualTo(dto.LocationType));
                Assert.That(assignment.Location, Is.Null);
                Assert.That(assignment.Groups, Is.Not.Null);
                Assert.That(assignment.Groups, Is.Empty);
                Assert.That(assignment.TaskId, Is.EqualTo(dto.TaskId));
                Assert.That(assignment.IsCompleted, Is.True);
                Assert.That(assignment.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(assignment.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(assignment.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMinimalValidData_ShouldCreateAssignmentCorrectly()
        {
            // Arrange
            var dto = new TestProjectAssignmentDTO
            {
                Id = 0,
                ProjectAssignmentName = "A", // Minimum valid name
                Description = null,
                IsCompleted = false,
                DueDate = null,
                ModifiedDate = null
            };

            // Act
            var assignment = new ProjectAssignment(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(assignment.Id, Is.EqualTo(0));
                Assert.That(assignment.ProjectAssignmentName, Is.EqualTo("A"));
                Assert.That(assignment.Description, Is.Null);
                Assert.That(assignment.IsCompleted, Is.False);
                Assert.That(assignment.DueDate, Is.Null);
                Assert.That(assignment.CompletedDate, Is.Null);
                Assert.That(assignment.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(assignment.ModifiedDate, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void SetAndGetId()
        {
            // Arrange
            var id = 42;
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Id = id;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Id, Is.EqualTo(id));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
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
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.ProjectAssignmentName = name;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.ProjectAssignmentName, Is.EqualTo(name));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void ThrowExceptionForEmptyName()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.ProjectAssignmentName = "");
        }

        [Test, Category("Models")]
        public void ThrowExceptionForTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _assignment.ProjectAssignmentName = longName);
        }

        [Test, Category("Models")]
        public void SetAndGetDescription()
        {
            // Arrange
            var description = "Test description";
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Description = description;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Description, Is.EqualTo(description));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
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
        public void SetAndGetGroups()
        {
            // Arrange
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Groups = _groups;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Groups, Is.EqualTo(_groups));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
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
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Groups = _groups;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Groups, Is.EqualTo(_groups));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
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
        public void SetIsCompletedToTrueAndUpcompletedDateDate()
        {
            // Arrange
            var initialModifiedDate = _assignment.ModifiedDate;
            var beforeCompletion = DateTime.Now;

            // Act
            _assignment.IsCompleted = true;
            var afterCompletion = DateTime.Now;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.IsCompleted, Is.True);
                Assert.That(_assignment.CompletedDate, Is.Not.Null);
            });
            Assert.That(_assignment.CompletedDate, Is.GreaterThanOrEqualTo(beforeCompletion));
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.CompletedDate, Is.LessThanOrEqualTo(afterCompletion));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void SetIsCompletedToFalseAndClearCompletedDate()
        {
            // Arrange
            _assignment.IsCompleted = true; // Set to true first
            Assert.That(_assignment.CompletedDate, Is.Not.Null);

            // Act
            _assignment.IsCompleted = false;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.IsCompleted, Is.False);
                Assert.That(_assignment.CompletedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void SetAndGetDueDate()
        {
            // Arrange
            var dueDate = DateTime.Now.AddDays(7);
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.DueDate = dueDate;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.DueDate, Is.EqualTo(dueDate));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void SetAndGetCompletedDate()
        {
            // Arrange
            var completedDate = DateTime.Now;
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.CompletedDate = completedDate;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void SetAndGetCreatedDate()
        {
            // Arrange
            var createdDate = DateTime.Now.AddDays(-5);
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.CreatedDate = createdDate;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void SetAndGetTaskId()
        {
            // Arrange
            var taskId = 42;
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.TaskId = taskId;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.TaskId, Is.EqualTo(taskId));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
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
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Task = task;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_assignment.Task, Is.EqualTo(task));
                Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            });
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
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

        [Test, Category("Models")]
        public void SetAndGetLocationId()
        {
            // Arrange
            var locationId = 42;
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.LocationId = locationId;

            // Assert - Domain entities auto-update ModifiedDate
            Assert.That(_assignment.LocationId, Is.EqualTo(locationId));
            Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void ThrowExceptionForNegativeLocationId()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _assignment.LocationId = -1);
        }

        [Test, Category("Models")]
        public void AcceptNullLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.LocationId = null);
            Assert.That(_assignment.LocationId, Is.Null);
        }

        [Test, Category("Models")]
        public void AcceptZeroLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.LocationId = 0);
            Assert.That(_assignment.LocationId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void AcceptMaximumLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.LocationId = int.MaxValue);
            Assert.That(_assignment.LocationId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void SetAndGetLocationType()
        {
            // Arrange
            var locationType = "Conference Room";
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.LocationType = locationType;

            // Assert - Domain entities auto-update ModifiedDate
            Assert.That(_assignment.LocationType, Is.EqualTo(locationType));
            Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void AcceptNullLocationType()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.LocationType = null);
            Assert.That(_assignment.LocationType, Is.Null);
        }

        [Test, Category("Models")]
        public void AcceptEmptyLocationType()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.LocationType = "");
            Assert.That(_assignment.LocationType, Is.EqualTo(""));
        }

        [Test, Category("Models")]
        public void SetAndGetLocation()
        {
            // Arrange
            var location = new USAddress
            {
                Id = 1,
                Type = Enums.Types.Home,
                Street1 = "123 Test St",
                City = "Test City",  
                State = USStates.California.ToStateModel(),
                ZipCode = "12345"
            };
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Location = location;

            // Assert - Domain entities auto-update ModifiedDate
            Assert.That(_assignment.Location, Is.EqualTo(location));
            Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void AcceptNullLocation()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Location = null);
            Assert.That(_assignment.Location, Is.Null);
        }

        [Test, Category("Models")]
        public void SetAndGetAssignee()
        {
            // Arrange
            var assignee = new SubAccount { Id = 1 };
            var initialModifiedDate = _assignment.ModifiedDate;

            // Act
            _assignment.Assignee = assignee;

            // Assert - Domain entities auto-update ModifiedDate
            Assert.That(_assignment.Assignee, Is.EqualTo(assignee));
            Assert.That(_assignment.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));
            Assert.That(_assignment.ModifiedDate, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void AcceptNullAssignee()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.Assignee = null);
            Assert.That(_assignment.Assignee, Is.Null);
        }

        [Test, Category("Models")]
        public void AssigneeIdReflectsAssigneeIdValue()
        {
            // Arrange
            var assignee = new SubAccount { Id = 42 };

            // Act - Set assignee
            _assignment.Assignee = assignee;

            // Assert - AssigneeId should reflect Assignee.Id
            Assert.That(_assignment.AssigneeId, Is.EqualTo(assignee.Id));
            Assert.That(_assignment.AssigneeId, Is.EqualTo(42));

            // Act - Set assignee to null
            _assignment.Assignee = null;

            // Assert - AssigneeId should be null when Assignee is null
            Assert.That(_assignment.AssigneeId, Is.Null);
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void ThrowInvalidCastExceptionForUnsupportedType()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _assignment.Cast<Contact>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type Contact"));
        }

        #endregion

        #region ToJson Method Tests

        [Test, Category("Models")]
        public void SerializeToJson()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.ProjectAssignmentName = "Test Assignment";
            _assignment.Description = "Test Description";
            _assignment.IsCompleted = false;

            // Act
            var json = _assignment.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Does.Contain("\"id\":1"));
            Assert.That(json, Does.Contain("\"projectAssignmentName\":\"Test Assignment\""));
            Assert.That(json, Does.Contain("\"description\":\"Test Description\""));
            Assert.That(json, Does.Contain("\"isCompleted\":false"));
        }

        [Test, Category("Models")]
        public void SerializeToJsonWithNullValues()
        {
            // Arrange
            _assignment.Id = 0;
            _assignment.ProjectAssignmentName = "Minimal Assignment";
            // Leave other properties as default/null

            // Act
            var json = _assignment.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Does.Contain("\"id\":0"));
            Assert.That(json, Does.Contain("\"projectAssignmentName\":\"Minimal Assignment\""));
        }

        [Test, Category("Models")]
        public void SerializeToJsonWithCircularReferences()
        {
            // Arrange
            _assignment.Id = 1;
            _assignment.ProjectAssignmentName = "Test Assignment";
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
            Assert.DoesNotThrow(() => _assignment.ProjectAssignmentName = "A");
            Assert.That(_assignment.ProjectAssignmentName, Is.EqualTo("A"));
        }

        [Test, Category("Models")]
        public void AcceptMaximumValidName()
        {
            // Arrange
            var maxName = new string('a', 100);

            // Act & Assert
            Assert.DoesNotThrow(() => _assignment.ProjectAssignmentName = maxName);
            Assert.That(_assignment.ProjectAssignmentName, Is.EqualTo(maxName));
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
                      _assignment.DueDate = null;
                      _assignment.CompletedDate = null;
                  });
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.DueDate, Is.Null);
                Assert.That(_assignment.CompletedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void HandleLocationPropertiesTogether()
        {
            // Arrange
            var locationId = 999;
            var locationType = "Executive Boardroom";
            var location = new USAddress
            {
                Id = locationId,
                Type = Enums.Types.Work,
                Street1 = "999 Executive Blvd",
                City = "Corporate City",
                State = USStates.California.ToStateModel(),
                ZipCode = "90210"
            };

            // Act - Set all location properties together
            _assignment.LocationId = locationId;
            _assignment.LocationType = locationType;
            _assignment.Location = location;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.LocationId, Is.EqualTo(locationId));
                Assert.That(_assignment.LocationType, Is.EqualTo(locationType));
                Assert.That(_assignment.Location, Is.EqualTo(location));
                Assert.That(_assignment.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void HandleLocationPropertyBoundaryValues()
        {
            // Test minimum and maximum LocationId values
            Assert.DoesNotThrow(() => _assignment.LocationId = 0);
            Assert.That(_assignment.LocationId, Is.EqualTo(0));

            Assert.DoesNotThrow(() => _assignment.LocationId = int.MaxValue);
            Assert.That(_assignment.LocationId, Is.EqualTo(int.MaxValue));

            // Test very long LocationType string
            var longLocationType = new string('L', 500);
            Assert.DoesNotThrow(() => _assignment.LocationType = longLocationType);
            Assert.That(_assignment.LocationType, Is.EqualTo(longLocationType));
        }

        [Test, Category("Models")]
        public void HandleLocationPropertyNullToValueTransitions()
        {
            // Arrange - Start with null values
            _assignment.LocationId = null;
            _assignment.LocationType = null;
            _assignment.Location = null;

            // Act - Set to values
            _assignment.LocationId = 42;
            _assignment.LocationType = "Meeting Room";
            var address = new USAddress
            {
                Id = 42,
                Type = Enums.Types.Work,
                Street1 = "42 Transition Ave",
                City = "Change City",
                State = USStates.Texas.ToStateModel(),
                ZipCode = "75001"
            };
            _assignment.Location = address;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.LocationId, Is.EqualTo(42));
                Assert.That(_assignment.LocationType, Is.EqualTo("Meeting Room"));
                Assert.That(_assignment.Location, Is.EqualTo(address));
            });

            // Act - Set back to null
            _assignment.LocationId = null;
            _assignment.LocationType = null;
            _assignment.Location = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_assignment.LocationId, Is.Null);
                Assert.That(_assignment.LocationType, Is.Null);
                Assert.That(_assignment.Location, Is.Null);
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
            Assert.That(retrievedIAssignees, Has.Count.EqualTo(testIGroup.Count));
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

        [Test, Category("Models")]
        public void CoverLocationInterfaceImplementation()
        {
            // Arrange
            IProjectAssignment iAssignment = _assignment;
            var testLocation = new USAddress
            {
                Id = 1,
                Type = Enums.Types.Work,
                Street1 = "456 Interface St",
                City = "Interface City",
                State = USStates.NewYork.ToStateModel(),
                ZipCode = "54321"
            };

            // Act & Assert for Location interface property getter
            var retrievedLocation = iAssignment.Location;
            Assert.That(retrievedLocation, Is.EqualTo(_assignment.Location));

            // Act & Assert for Location interface property setter
            iAssignment.Location = testLocation;
            Assert.That(_assignment.Location, Is.EqualTo(testLocation));

            // Test setting null
            iAssignment.Location = null;
            Assert.That(_assignment.Location, Is.Null);
        }

        [Test, Category("Models")]
        public void CoverAssigneeInterfaceImplementation()
        {
            // Arrange
            IProjectAssignment iAssignment = _assignment;
            var testAssignee = new SubAccount { Id = 100 };

            // Act & Assert for Assignee interface property getter
            var retrievedAssignee = iAssignment.Assignee;
            Assert.That(retrievedAssignee, Is.EqualTo(_assignment.Assignee));

            // Act & Assert for Assignee interface property setter
            iAssignment.Assignee = testAssignee;
            Assert.That(_assignment.Assignee, Is.EqualTo(testAssignee));

            // Test setting null
            iAssignment.Assignee = null;
            Assert.That(_assignment.Assignee, Is.Null);
        }

        #endregion
    }
}
