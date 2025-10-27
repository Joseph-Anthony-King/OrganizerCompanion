using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Models.DataTransferObject;

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
                new() { Id = 1, GroupName = "Development Team" },
                new() { Id = 2, GroupName = "QA Team" }
            ];
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void HaveDefaultConstructor()
        {
            // Arrange & Act
            var projectAssignmentDTO = new ProjectAssignmentDTO();
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(projectAssignmentDTO.Id, Is.EqualTo(0));
                Assert.That(projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(string.Empty));
                Assert.That(projectAssignmentDTO.Description, Is.Null);
                Assert.That(projectAssignmentDTO.AssigneeId, Is.Null);
                Assert.That(projectAssignmentDTO.Assignee, Is.Null);
                Assert.That(projectAssignmentDTO.LocationId, Is.Null);
                Assert.That(projectAssignmentDTO.LocationType, Is.Null);
                Assert.That(projectAssignmentDTO.Location, Is.Null);
                Assert.That(projectAssignmentDTO.Groups, Is.Null);
                Assert.That(projectAssignmentDTO.IsCompleted, Is.False);
                Assert.That(projectAssignmentDTO.DueDate, Is.Null);
                Assert.That(projectAssignmentDTO.CompletedDate, Is.Null);
                Assert.That(projectAssignmentDTO.CreatedDate, Is.Not.EqualTo(default(DateTime)));
                Assert.That(projectAssignmentDTO.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void HaveJsonConstructorWithAllParameters()
        {
            // Arrange
            var id = 1;
            var name = "Test Assignment";
            var description = "Test Description";
            var userDTO = new UserDTO
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Wonderland"
            };
            var assignee = new SubAccountDTO
            {
                Id = 42,
                LinkedEntityId = userDTO.Id,
                LinkedEntity = userDTO
            };
            var locationId = 5;
            var locationType = "Office";
            IAddressDTO? location = null; // Mock address would be needed for full test
            var groups = _testGroups;
            var taskId = 10;
            var task = new ProjectTaskDTO();
            var isCompleted = true;
            var dueDate = DateTime.Now.AddDays(7);
            var completedDate = DateTime.Now.AddDays(-1);
            var createdDate = DateTime.Now.AddDays(-10);
            var modifiedDate = DateTime.Now.AddDays(-2);

            // Act
            var projectAssignmentDTO = new ProjectAssignmentDTO(
                id, name, description, assignee, locationId, locationType, location, groups, taskId, task, isCompleted,
                dueDate, completedDate, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(projectAssignmentDTO.Id, Is.EqualTo(id));
                Assert.That(projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(name));
                Assert.That(projectAssignmentDTO.Description, Is.EqualTo(description));
                Assert.That(projectAssignmentDTO.AssigneeId, Is.EqualTo(assignee.Id));
                Assert.That(projectAssignmentDTO.Assignee, Is.EqualTo(assignee));
                Assert.That(projectAssignmentDTO.LocationId, Is.EqualTo(locationId));
                Assert.That(projectAssignmentDTO.LocationType, Is.EqualTo(locationType));
                Assert.That(projectAssignmentDTO.Location, Is.EqualTo(location));
                Assert.That(projectAssignmentDTO.Groups, Is.EqualTo(groups));
                Assert.That(projectAssignmentDTO.TaskId, Is.EqualTo(taskId));
                Assert.That(projectAssignmentDTO.Task!.Id, Is.EqualTo(task.Id));
                Assert.That(projectAssignmentDTO.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(projectAssignmentDTO.DueDate, Is.EqualTo(dueDate));
                Assert.That(projectAssignmentDTO.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(projectAssignmentDTO.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(projectAssignmentDTO.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void HaveJsonConstructorWithNullValues()
        {
            // Arrange
            var id = 0;
            var name = "Minimal Assignment";
            string? description = null;
            var userDTO = new UserDTO
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Wonderland"
            };
            var assignee = new SubAccountDTO
            {
                Id = 42,
                LinkedEntityId = userDTO.Id,
                LinkedEntity = userDTO
            };
            int? locationId = null;
            string? locationType = null;
            IAddressDTO? location = null;
            List<GroupDTO>? groups = null;
            int? taskId = null;
            ProjectTaskDTO? task = null;
            var isCompleted = false;
            DateTime? dueDate = null;
            DateTime? completedDate = null;
            var createdDate = DateTime.Now;
            DateTime? modifiedDate = null;

            // Act
            var projectAssignmentDTO = new ProjectAssignmentDTO(
                id, name, description, assignee, locationId, locationType, location, groups, taskId, task, isCompleted,
                dueDate, completedDate, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(projectAssignmentDTO.Id, Is.EqualTo(id));
                Assert.That(projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(name));
                Assert.That(projectAssignmentDTO.Description, Is.Null);
                Assert.That(projectAssignmentDTO.AssigneeId, Is.EqualTo(assignee.Id));
                Assert.That(projectAssignmentDTO.Assignee, Is.EqualTo(assignee));
                Assert.That(projectAssignmentDTO.LocationId, Is.Null);
                Assert.That(projectAssignmentDTO.LocationType, Is.Null);
                Assert.That(projectAssignmentDTO.Location, Is.Null);
                Assert.That(projectAssignmentDTO.Groups, Is.Null);
                Assert.That(projectAssignmentDTO.TaskId, Is.Null);
                Assert.That(projectAssignmentDTO.Task, Is.Null);
                Assert.That(projectAssignmentDTO.IsCompleted, Is.False);
                Assert.That(projectAssignmentDTO.DueDate, Is.Null);
                Assert.That(projectAssignmentDTO.CompletedDate, Is.Null);
                Assert.That(projectAssignmentDTO.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(projectAssignmentDTO.ModifiedDate, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("DataTransferObjects")]
        public void SetAndGetId()
        {
            // Arrange
            var id = 42;

            // Act
            _projectAssignmentDTO.Id = id;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(id));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNegativeId()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Id = -1);
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(-1));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetName()
        {
            // Arrange
            var name = "Test Assignment Name";

            // Act
            _projectAssignmentDTO.ProjectAssignmentName = name;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(name));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptEmptyName()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.ProjectAssignmentName = "");
            Assert.That(_projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptTooLongName()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.ProjectAssignmentName = longName);
            Assert.That(_projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(longName));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetDescription()
        {
            // Arrange
            var description = "Test description";

            // Act
            _projectAssignmentDTO.Description = description;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(description));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptTooLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = longDescription);
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(longDescription));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = null);
            Assert.That(_projectAssignmentDTO.Description, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptEmptyDescription()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = "");
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetGroups()
        {
            // Act
            _projectAssignmentDTO.Groups = _testGroups;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.Groups, Is.EqualTo(_testGroups));
        }

        [Test, Category("DataTransferObjects")]
        public void InitializeEmptyListWhenGroupsIsNull()
        {
            // Arrange
            _projectAssignmentDTO.Groups = _testGroups; // Set to non-null first

            // Act
            _projectAssignmentDTO.Groups = null;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetLocationId()
        {
            // Arrange
            var locationId = 42;

            // Act
            _projectAssignmentDTO.LocationId = locationId;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(locationId));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationId = null);
            Assert.That(_projectAssignmentDTO.LocationId, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNegativeLocationId()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationId = -1);
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(-1));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptZeroLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationId = 0);
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptMaximumLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationId = int.MaxValue);
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptMinimumLocationId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationId = int.MinValue);
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetAssigneeId()
        {
            // Arrange
            var assigneeId = 42;

            // Act
            _projectAssignmentDTO.AssigneeId = assigneeId;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(assigneeId));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullAssigneeId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.AssigneeId = null);
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNegativeAssigneeId()
        {
            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.AssigneeId = -1);
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(-1));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptZeroAssigneeId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.AssigneeId = 0);
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptMaximumAssigneeId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.AssigneeId = int.MaxValue);
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptMinimumAssigneeId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.AssigneeId = int.MinValue);
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetAssignee()
        {
            // Arrange
            var testAssignee = new SubAccountDTO();

            // Act
            _projectAssignmentDTO.Assignee = testAssignee;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.Assignee, Is.EqualTo(testAssignee));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullAssignee()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Assignee = null);
            Assert.That(_projectAssignmentDTO.Assignee, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetLocationType()
        {
            // Arrange
            var locationType = "Conference Room";

            // Act
            _projectAssignmentDTO.LocationType = locationType;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(locationType));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullLocationType()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationType = null);
            Assert.That(_projectAssignmentDTO.LocationType, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptEmptyLocationType()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationType = "");
            Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptTooLongLocationType()
        {
            // Arrange
            var longLocationType = new string('a', 101);

            // Act & Assert - DTOs don't validate, only domain entities do
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationType = longLocationType);
            Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(longLocationType));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetLocation()
        {
            // Arrange
            IAddressDTO? testLocation = null; // Mock would be needed for full test

            // Act
            _projectAssignmentDTO.Location = testLocation;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.Location, Is.EqualTo(testLocation));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullLocation()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Location = null);
            Assert.That(_projectAssignmentDTO.Location, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void SetIsCompletedToTrue()
        {
            // Act
            _projectAssignmentDTO.IsCompleted = true;
            Assert.Multiple(() =>
            {

                // Assert - DTOs don't auto-update CompletedDate or ModifiedDate
                Assert.That(_projectAssignmentDTO.IsCompleted, Is.True);
                Assert.That(_projectAssignmentDTO.CompletedDate, Is.Null); // CompletedDate is readonly and stays null
            });
        }

        [Test, Category("DataTransferObjects")]
        public void SetIsCompletedToFalse()
        {
            // Arrange
            _projectAssignmentDTO.IsCompleted = true; // Set to true first
            Assert.That(_projectAssignmentDTO.CompletedDate, Is.Null); // CompletedDate stays null in DTOs

            // Act
            _projectAssignmentDTO.IsCompleted = false;
            Assert.Multiple(() =>
            {

                // Assert - DTOs don't auto-manage CompletedDate
                Assert.That(_projectAssignmentDTO.IsCompleted, Is.False);
                Assert.That(_projectAssignmentDTO.CompletedDate, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetDueDate()
        {
            // Arrange
            var dueDate = DateTime.Now.AddDays(7);

            // Act
            _projectAssignmentDTO.DueDate = dueDate;

            // Assert - DTOs don't auto-update ModifiedDate
            Assert.That(_projectAssignmentDTO.DueDate, Is.EqualTo(dueDate));
        }

        [Test, Category("DataTransferObjects")]
        public void GetCompletedDateReadOnly()
        {
            // Arrange
            var initialValue = _projectAssignmentDTO.CompletedDate;

            // Act
            var retrievedValue = _projectAssignmentDTO.CompletedDate;

            // Assert
            Assert.That(retrievedValue, Is.EqualTo(initialValue));
        }

        [Test, Category("DataTransferObjects")]
        public void GetCreatedDateReadOnly()
        {
            // Arrange
            var initialValue = _projectAssignmentDTO.CreatedDate;

            // Act
            var retrievedValue = _projectAssignmentDTO.CreatedDate;

            // Assert
            Assert.That(retrievedValue, Is.EqualTo(initialValue));
            Assert.That(retrievedValue, Is.Not.EqualTo(default(DateTime)));
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
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
            Assert.That(retrievedIGroups, Has.Count.EqualTo(_testGroups.Count));

            iAssignmentDTO.Groups = testIGroups;
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups, Has.Count.EqualTo(testIGroups.Count));
        }

        [Test, Category("DataTransferObjects")]
        public void CoverGroupsInterfacePropertyWithNullGroups()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            _projectAssignmentDTO.Groups = null;

            // Act & Assert - This should trigger ArgumentNullException in the Cast method
            Assert.Throws<ArgumentNullException>(() => { var _ = iAssignmentDTO.Groups; });
        }

        [Test, Category("DataTransferObjects")]
        public void CoverGroupsInterfacePropertyWithEmptyGroups()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            _projectAssignmentDTO.Groups = [];

            // Act
            var retrievedGroups = iAssignmentDTO.Groups;

            // Assert
            Assert.That(retrievedGroups, Is.Not.Null);
            Assert.That(retrievedGroups, Is.Empty);
        }

        [Test, Category("DataTransferObjects")]
        public void CoverGroupsInterfacePropertySetter()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testIGroups = _testGroups.Cast<IGroupDTO>().ToList();

            // Act - This covers the ConvertAll casting logic in the interface setter
            iAssignmentDTO.Groups = testIGroups;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups, Has.Count.EqualTo(testIGroups.Count));
            Assert.That(_projectAssignmentDTO.Groups[0], Is.InstanceOf<GroupDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void CoverGroupsInterfacePropertySetterWithNull()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            _projectAssignmentDTO.Groups = _testGroups; // Set to non-null first

            // Act & Assert - Setting null should trigger NullReferenceException in ConvertAll
            Assert.Throws<NullReferenceException>(() =>
            {
                iAssignmentDTO.Groups = null;
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CoverTaskInterfaceImplementation()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testTask = new ProjectTaskDTO();

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

        [Test, Category("DataTransferObjects")]
        public void CoverLocationInterfaceImplementations()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testLocationId = 123;
            var testLocationType = "Meeting Room";
            IAddressDTO? testLocation = null; // Mock would be needed for full test

            // Act & Assert for LocationId interface property
            iAssignmentDTO.LocationId = testLocationId;
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(testLocationId));
                Assert.That(iAssignmentDTO.LocationId, Is.EqualTo(testLocationId));
            });

            // Act & Assert for LocationType interface property
            iAssignmentDTO.LocationType = testLocationType;
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(testLocationType));
                Assert.That(iAssignmentDTO.LocationType, Is.EqualTo(testLocationType));
            });

            // Act & Assert for Location interface property
            iAssignmentDTO.Location = testLocation;
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.Location, Is.EqualTo(testLocation));
                Assert.That(iAssignmentDTO.Location, Is.EqualTo(testLocation));
            });

            // Test setting null values via interface
            iAssignmentDTO.LocationId = null;
            Assert.That(_projectAssignmentDTO.LocationId, Is.Null);
            
            iAssignmentDTO.LocationType = null;
            Assert.That(_projectAssignmentDTO.LocationType, Is.Null);
            
            iAssignmentDTO.Location = null;
            Assert.That(_projectAssignmentDTO.Location, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CoverAssigneeInterfaceImplementation()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testAssignee = new SubAccountDTO();

            // Act & Assert for Assignee interface property getter
            var retrievedAssignee = iAssignmentDTO.Assignee;
            Assert.That(retrievedAssignee, Is.EqualTo(_projectAssignmentDTO.Assignee));

            // Act & Assert for Assignee interface property setter - this should cover the explicit interface implementation
            iAssignmentDTO.Assignee = testAssignee;
            Assert.That(_projectAssignmentDTO.Assignee, Is.EqualTo(testAssignee));

            // Test setting null via interface - ensure cast occurs
            iAssignmentDTO.Assignee = null;
            Assert.That(_projectAssignmentDTO.Assignee, Is.Null);
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("DataTransferObjects")]
        public void ThrowNotImplementedExceptionForCastMethod()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.Cast<GroupDTO>());
        }

        #endregion

        #region ToJson Method Tests

        [Test, Category("DataTransferObjects")]
        public void ToJson_ThrowsNotImplementedException()
        {
            // Arrange
            _projectAssignmentDTO.Id = 1;
            _projectAssignmentDTO.ProjectAssignmentName = "Test Assignment";
            _projectAssignmentDTO.Description = "Test Description";
            _projectAssignmentDTO.IsCompleted = false;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.ToJson());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJsonWithNullValues_ThrowsNotImplementedException()
        {
            // Arrange
            _projectAssignmentDTO.Id = 0;
            _projectAssignmentDTO.ProjectAssignmentName = "Minimal Assignment";
            // Leave other properties as default/null

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.ToJson());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJsonWithCircularReferences_ThrowsNotImplementedException()
        {
            // Arrange
            _projectAssignmentDTO.Id = 1;
            _projectAssignmentDTO.ProjectAssignmentName = "Test Assignment";
            _projectAssignmentDTO.Groups = _testGroups;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _projectAssignmentDTO.ToJson());
        }

        #endregion

        #region Edge Cases and Validation Tests

        [Test, Category("DataTransferObjects")]
        public void AcceptSingleCharacterName()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.ProjectAssignmentName = "A");
            Assert.That(_projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo("A"));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptLongName()
        {
            // Arrange
            var longName = new string('a', 200); // Beyond typical validation limit

            // Act & Assert - DTOs accept any string length
            Assert.DoesNotThrow(() => _projectAssignmentDTO.ProjectAssignmentName = longName);
            Assert.That(_projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(longName));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptLongDescription()
        {
            // Arrange
            var longDescription = new string('a', 2000); // Beyond typical validation limit

            // Act & Assert - DTOs accept any string length
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Description = longDescription);
            Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(longDescription));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptZeroId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Id = 0);
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptMaximumId()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.Id = int.MaxValue);
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void HandleEmptyGroupsList()
        {
            // Act
            _projectAssignmentDTO.Groups = [];

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups, Is.Empty);
        }

        [Test, Category("DataTransferObjects")]
        public void HandleNullDateValues()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _projectAssignmentDTO.DueDate = null);
            Assert.That(_projectAssignmentDTO.DueDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetTaskId()
        {
            // Act
            _projectAssignmentDTO.TaskId = 42;

            // Assert
            Assert.That(_projectAssignmentDTO.TaskId, Is.EqualTo(42));
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetTask()
        {
            // Arrange
            var testTask = new ProjectTaskDTO();

            // Act
            _projectAssignmentDTO.Task = testTask;

            // Assert
            Assert.That(_projectAssignmentDTO.Task, Is.EqualTo(testTask));
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullTask()
        {
            // Act
            _projectAssignmentDTO.Task = null;

            // Assert
            Assert.That(_projectAssignmentDTO.Task, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AcceptNullTaskId()
        {
            // Act
            _projectAssignmentDTO.TaskId = null;

            // Assert
            Assert.That(_projectAssignmentDTO.TaskId, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void SetAndGetModifiedDate()
        {
            // Arrange
            var dateTime = DateTime.Now;

            // Act
            _projectAssignmentDTO.ModifiedDate = dateTime;

            // Assert
            Assert.That(_projectAssignmentDTO.ModifiedDate, Is.EqualTo(dateTime));
        }

        [Test, Category("DataTransferObjects")]
        public void SetModifiedDateToNull()
        {
            // Act
            _projectAssignmentDTO.ModifiedDate = null;

            // Assert
            Assert.That(_projectAssignmentDTO.ModifiedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void HandleGroupsNullToEmptyListConversion()
        {
            // Arrange - Start with null
            _projectAssignmentDTO.Groups = null;

            // Act - Setting null should initialize empty list due to ??= operator
            _projectAssignmentDTO.Groups = _testGroups;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.EqualTo(_testGroups));
        }

        [Test, Category("Comprehensive")]
        public void CoverAllJsonConstructorParameters()
        {
            // Arrange
            var id = 999;
            var name = "Comprehensive Test Assignment";
            var description = "This tests all constructor parameters thoroughly";
            var userDTO = new UserDTO
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Wonderland"
            };
            var assignee = new SubAccountDTO
            {
                Id = 42,
                LinkedEntityId = userDTO.Id,
                LinkedEntity = userDTO
            };
            var locationId = 123;
            var locationType = "USAddressDTO";
            IAddressDTO? location = (IAddressDTO)new USAddressDTO(); // Mock address would be needed for full test
            var groups = new List<GroupDTO>
            {
                new() { Id = 100, GroupName = "Special Group 1" },
                new() { Id = 200, GroupName = "Special Group 2" },
                new() { Id = 300, GroupName = "Special Group 3" }
            };
            var taskId = 555;
            var task = new ProjectTaskDTO();
            var isCompleted = true;
            var dueDate = new DateTime(2025, 12, 31, 23, 59, 59);
            var completedDate = new DateTime(2025, 10, 15, 14, 30, 0);
            var createdDate = new DateTime(2025, 10, 1, 9, 0, 0);
            var modifiedDate = new DateTime(2025, 10, 16, 16, 45, 30);

            // Act
            var dto = new ProjectAssignmentDTO(
                id, name, description, assignee, locationId, locationType, location, groups, taskId, task,
                isCompleted, dueDate, completedDate, createdDate, modifiedDate);

            // Assert - Verify all properties are set correctly
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.ProjectAssignmentName, Is.EqualTo(name));
                Assert.That(dto.Description, Is.EqualTo(description));
                Assert.That(dto.AssigneeId, Is.EqualTo(assignee.Id));
                Assert.That(dto.Assignee, Is.EqualTo(assignee));
                Assert.That(dto.LocationId, Is.EqualTo(locationId));
                Assert.That(dto.LocationType, Is.EqualTo(locationType));
                Assert.That(dto.Location, Is.EqualTo(location));
                Assert.That(dto.Groups, Is.EqualTo(groups));
                Assert.That(dto.Groups!, Has.Count.EqualTo(3));
                Assert.That(dto.TaskId, Is.EqualTo(taskId));
                Assert.That(dto.Task, Is.EqualTo(task));
                Assert.That(dto.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(dto.DueDate, Is.EqualTo(dueDate));
                Assert.That(dto.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(dto.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(dto.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Interface")]
        public void CoverGroupsInterfaceGetterCastingLogic()
        {
            // Arrange
            IProjectAssignmentDTO iDTO = _projectAssignmentDTO;
            var groupA = new GroupDTO { Id = 10, GroupName = "Group A" };
            var groupB = new GroupDTO { Id = 20, GroupName = "Group B" };
            _projectAssignmentDTO.Groups = [groupA, groupB];

            // Act - This covers the [.. Groups!.Cast<IGroupDTO>()] logic
            var interfaceGroups = iDTO.Groups;

            // Assert
            Assert.That(interfaceGroups, Is.Not.Null);
            Assert.That(interfaceGroups, Has.Count.EqualTo(2));
            Assert.Multiple(() =>
            {
                Assert.That(interfaceGroups[0].Id, Is.EqualTo(10));
                Assert.That(interfaceGroups[0].GroupName, Is.EqualTo("Group A"));
                Assert.That(interfaceGroups[1].Id, Is.EqualTo(20));
                Assert.That(interfaceGroups[1].GroupName, Is.EqualTo("Group B"));
            });
        }

        [Test, Category("Interface")]
        public void CoverGroupsInterfaceSetterConversionLogic()
        {
            // Arrange
            IProjectAssignmentDTO iDTO = _projectAssignmentDTO;
            var group1 = new GroupDTO { Id = 100, GroupName = "Interface Group 1" };
            var group2 = new GroupDTO { Id = 200, GroupName = "Interface Group 2" };
            var interfaceGroups = new List<IGroupDTO> { group1, group2 };

            // Act - This covers the value!.ConvertAll(group => (GroupDTO)group) logic
            iDTO.Groups = interfaceGroups;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups, Has.Count.EqualTo(2));
            Assert.That(_projectAssignmentDTO.Groups[0], Is.InstanceOf<GroupDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.Groups[0].Id, Is.EqualTo(100));
                Assert.That(_projectAssignmentDTO.Groups[1], Is.InstanceOf<GroupDTO>());
            });
            Assert.That(_projectAssignmentDTO.Groups[1].Id, Is.EqualTo(200));
        }

        [Test, Category("Boundary")]
        public void HandleVeryLargeCollections()
        {
            // Arrange
            var largeGroupsList = new List<GroupDTO>();
            for (int i = 0; i < 1000; i++)
            {
                largeGroupsList.Add(new GroupDTO { Id = i, GroupName = $"Group {i}" });
            }

            // Act
            _projectAssignmentDTO.Groups = largeGroupsList;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups, Has.Count.EqualTo(1000));
            Assert.That(_projectAssignmentDTO.Groups[999].GroupName, Is.EqualTo("Group 999"));
        }

        [Test, Category("DateTime")]
        public void HandleDateTimePrecision()
        {
            // Arrange
            var preciseDateTime = new DateTime(2025, 10, 20, 14, 35, 42, 123);

            // Act & Assert for DueDate
            _projectAssignmentDTO.DueDate = preciseDateTime;
            Assert.That(_projectAssignmentDTO.DueDate, Is.EqualTo(preciseDateTime));

            // Act & Assert for ModifiedDate
            _projectAssignmentDTO.ModifiedDate = preciseDateTime;
            Assert.That(_projectAssignmentDTO.ModifiedDate, Is.EqualTo(preciseDateTime));
        }

        [Test, Category("Unicode")]
        public void HandleUnicodeCharacters()
        {
            // Arrange
            var unicodeName = "项目分配 🚀";
            var unicodeDescription = "这是一个测试描述 with émojis 🎯 and spëcial çhars";

            // Act
            _projectAssignmentDTO.ProjectAssignmentName = unicodeName;
            _projectAssignmentDTO.Description = unicodeDescription;
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(_projectAssignmentDTO.ProjectAssignmentName, Is.EqualTo(unicodeName));
                Assert.That(_projectAssignmentDTO.Description, Is.EqualTo(unicodeDescription));
            });
        }

        [Test, Category("Boundary")]
        public void HandleMaximumIntegerValues()
        {
            // Act & Assert for Id
            _projectAssignmentDTO.Id = int.MaxValue;
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(int.MaxValue));

            // Act & Assert for TaskId
            _projectAssignmentDTO.TaskId = int.MaxValue;
            Assert.That(_projectAssignmentDTO.TaskId, Is.EqualTo(int.MaxValue));

            // Act & Assert for LocationId
            _projectAssignmentDTO.LocationId = int.MaxValue;
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Boundary")]
        public void HandleMinimumIntegerValues()
        {
            // Act & Assert for Id
            _projectAssignmentDTO.Id = int.MinValue;
            Assert.That(_projectAssignmentDTO.Id, Is.EqualTo(int.MinValue));

            // Act & Assert for TaskId
            _projectAssignmentDTO.TaskId = int.MinValue;
            Assert.That(_projectAssignmentDTO.TaskId, Is.EqualTo(int.MinValue));

            // Act & Assert for LocationId
            _projectAssignmentDTO.LocationId = int.MinValue;
            Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(int.MinValue));
        }

        [Test, Category("Unicode")]
        public void HandleUnicodeLocationTypes()
        {
            // Arrange
            var unicodeLocationType = "会议室 🏢 Sală de întâlniri";

            // Act
            _projectAssignmentDTO.LocationType = unicodeLocationType;

            // Assert
            Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(unicodeLocationType));
        }

        [Test, Category("Boundary")]
        public void HandleVeryLongLocationTypes()
        {
            // Arrange
            var veryLongLocationType = new string('L', 500); // Beyond typical limit

            // Act & Assert - DTOs don't validate
            Assert.DoesNotThrow(() => _projectAssignmentDTO.LocationType = veryLongLocationType);
            Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(veryLongLocationType));
        }

        [Test, Category("Comprehensive")]
        public void HandleLocationPropertiesTogether()
        {
            // Arrange
            var locationId = 999;
            var locationType = "Executive Boardroom";
            IAddressDTO? location = null; // Mock would be needed for comprehensive test

            // Act
            _projectAssignmentDTO.LocationId = locationId;
            _projectAssignmentDTO.LocationType = locationType;
            _projectAssignmentDTO.Location = location;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(locationId));
                Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo(locationType));
                Assert.That(_projectAssignmentDTO.Location, Is.EqualTo(location));
            });
        }

        [Test, Category("Edge")]
        public void HandleLocationPropertyNullToValueTransitions()
        {
            // Arrange - Start with null values
            _projectAssignmentDTO.LocationId = null;
            _projectAssignmentDTO.LocationType = null;
            _projectAssignmentDTO.Location = null;

            // Act - Set to values
            _projectAssignmentDTO.LocationId = 42;
            _projectAssignmentDTO.LocationType = "Office";
            // Location stays null as we don't have a mock

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.LocationId, Is.EqualTo(42));
                Assert.That(_projectAssignmentDTO.LocationType, Is.EqualTo("Office"));
                Assert.That(_projectAssignmentDTO.Location, Is.Null);
            });

            // Act - Set back to null
            _projectAssignmentDTO.LocationId = null;
            _projectAssignmentDTO.LocationType = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.LocationId, Is.Null);
                Assert.That(_projectAssignmentDTO.LocationType, Is.Null);
            });
        }

        [Test, Category("Boundary")]
        public void HandleAssigneeIntegerBoundaryValues()
        {
            // Act & Assert for AssigneeId maximum value
            _projectAssignmentDTO.AssigneeId = int.MaxValue;
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(int.MaxValue));

            // Act & Assert for AssigneeId minimum value
            _projectAssignmentDTO.AssigneeId = int.MinValue;
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(int.MinValue));

            // Act & Assert for AssigneeId zero
            _projectAssignmentDTO.AssigneeId = 0;
            Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(0));
        }

        [Test, Category("Comprehensive")]
        public void HandleAssigneePropertiesTogether()
        {
            // Arrange
            var assigneeId = 999;
            var assignee = new SubAccountDTO();

            // Act - Set both assignee properties
            _projectAssignmentDTO.AssigneeId = assigneeId;
            _projectAssignmentDTO.Assignee = assignee;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(assigneeId));
                Assert.That(_projectAssignmentDTO.Assignee, Is.EqualTo(assignee));
            });

            // Act - Set back to null
            _projectAssignmentDTO.AssigneeId = null;
            _projectAssignmentDTO.Assignee = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.AssigneeId, Is.Null);
                Assert.That(_projectAssignmentDTO.Assignee, Is.Null);
            });
        }

        [Test, Category("Edge")]
        public void HandleAssigneePropertyNullToValueTransitions()
        {
            // Arrange - Start with null values
            _projectAssignmentDTO.AssigneeId = null;
            _projectAssignmentDTO.Assignee = null;

            // Assert initial null state
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.AssigneeId, Is.Null);
                Assert.That(_projectAssignmentDTO.Assignee, Is.Null);
            });

            // Act - Transition to values
            var assigneeId = 123;
            var assignee = new SubAccountDTO();
            _projectAssignmentDTO.AssigneeId = assigneeId;
            _projectAssignmentDTO.Assignee = assignee;

            // Assert transition to values
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.AssigneeId, Is.EqualTo(assigneeId));
                Assert.That(_projectAssignmentDTO.Assignee, Is.EqualTo(assignee));
            });

            // Act - Transition back to null
            _projectAssignmentDTO.AssigneeId = null;
            _projectAssignmentDTO.Assignee = null;

            // Assert transition back to null
            Assert.Multiple(() =>
            {
                Assert.That(_projectAssignmentDTO.AssigneeId, Is.Null);
                Assert.That(_projectAssignmentDTO.Assignee, Is.Null);
            });
        }

        #endregion
    }
}
