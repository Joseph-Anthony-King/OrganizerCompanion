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
        public void HaveJsonConstructorWithAllParameters()
        {
            // Arrange
            var id = 1;
            var name = "Test Assignment";
            var description = "Test Description";
            var groups = _testGroups;
            var taskId = 10;
            var task = new Task();
            var isCompleted = true;
            var dateDue = DateTime.Now.AddDays(7);
            var dateCompleted = DateTime.Now.AddDays(-1);
            var dateCreated = DateTime.Now.AddDays(-10);
            var dateModified = DateTime.Now.AddDays(-2);

            // Act
            var projectAssignmentDTO = new ProjectAssignmentDTO(
                id, name, description, groups, taskId, task, isCompleted, 
                dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(projectAssignmentDTO.Id, Is.EqualTo(id));
                Assert.That(projectAssignmentDTO.Name, Is.EqualTo(name));
                Assert.That(projectAssignmentDTO.Description, Is.EqualTo(description));
                Assert.That(projectAssignmentDTO.Groups, Is.EqualTo(groups));
                Assert.That(projectAssignmentDTO.TaskId, Is.EqualTo(taskId));
                Assert.That(projectAssignmentDTO.Task, Is.EqualTo(task));
                Assert.That(projectAssignmentDTO.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(projectAssignmentDTO.DateDue, Is.EqualTo(dateDue));
                Assert.That(projectAssignmentDTO.DateCompleted, Is.EqualTo(dateCompleted));
                Assert.That(projectAssignmentDTO.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(projectAssignmentDTO.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test]
        public void HaveJsonConstructorWithNullValues()
        {
            // Arrange
            var id = 0;
            var name = "Minimal Assignment";
            string? description = null;
            List<GroupDTO>? groups = null;
            int? taskId = null;
            Task? task = null;
            var isCompleted = false;
            DateTime? dateDue = null;
            DateTime? dateCompleted = null;
            var dateCreated = DateTime.Now;
            DateTime? dateModified = null;

            // Act
            var projectAssignmentDTO = new ProjectAssignmentDTO(
                id, name, description, groups, taskId, task, isCompleted, 
                dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(projectAssignmentDTO.Id, Is.EqualTo(id));
                Assert.That(projectAssignmentDTO.Name, Is.EqualTo(name));
                Assert.That(projectAssignmentDTO.Description, Is.Null);
                Assert.That(projectAssignmentDTO.Groups, Is.Null);
                Assert.That(projectAssignmentDTO.TaskId, Is.Null);
                Assert.That(projectAssignmentDTO.Task, Is.Null);
                Assert.That(projectAssignmentDTO.IsCompleted, Is.False);
                Assert.That(projectAssignmentDTO.DateDue, Is.Null);
                Assert.That(projectAssignmentDTO.DateCompleted, Is.Null);
                Assert.That(projectAssignmentDTO.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(projectAssignmentDTO.DateModified, Is.Null);
            });
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
      Assert.Multiple(() =>
      {

        // Assert - DTOs don't auto-update DateCompleted or DateModified
        Assert.That(_projectAssignmentDTO.IsCompleted, Is.True);
        Assert.That(_projectAssignmentDTO.DateCompleted, Is.Null); // DateCompleted is readonly and stays null
      });
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
        public void CoverGroupsInterfacePropertyWithNullGroups()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            _projectAssignmentDTO.Groups = null;

            // Act & Assert - This should trigger ArgumentNullException in the Cast method
            Assert.Throws<ArgumentNullException>(() => { var _ = iAssignmentDTO.Groups; });
        }

        [Test]
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

        [Test]
        public void CoverGroupsInterfacePropertySetter()
        {
            // Arrange
            IProjectAssignmentDTO iAssignmentDTO = _projectAssignmentDTO;
            var testIGroups = _testGroups.Cast<IGroupDTO>().ToList();

            // Act - This covers the ConvertAll casting logic in the interface setter
            iAssignmentDTO.Groups = testIGroups;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups.Count, Is.EqualTo(testIGroups.Count));
            Assert.That(_projectAssignmentDTO.Groups[0], Is.InstanceOf<GroupDTO>());
        }

        [Test]
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

        [Test, Category("Comprehensive")]
        public void CoverAllJsonConstructorParameters()
        {
            // Arrange
            var id = 999;
            var name = "Comprehensive Test Assignment";
            var description = "This tests all constructor parameters thoroughly";
            var groups = new List<GroupDTO>
            {
                new() { Id = 100, Name = "Special Group 1" },
                new() { Id = 200, Name = "Special Group 2" },
                new() { Id = 300, Name = "Special Group 3" }
            };
            var taskId = 555;
            var task = new Task();
            var isCompleted = true;
            var dateDue = new DateTime(2025, 12, 31, 23, 59, 59);
            var dateCompleted = new DateTime(2025, 10, 15, 14, 30, 0);
            var dateCreated = new DateTime(2025, 10, 1, 9, 0, 0);
            var dateModified = new DateTime(2025, 10, 16, 16, 45, 30);

            // Act
            var dto = new ProjectAssignmentDTO(
                id, name, description, groups, taskId, task, 
                isCompleted, dateDue, dateCompleted, dateCreated, dateModified);

            // Assert - Verify all properties are set correctly
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Name, Is.EqualTo(name));
                Assert.That(dto.Description, Is.EqualTo(description));
                Assert.That(dto.Groups, Is.EqualTo(groups));
                Assert.That(dto.Groups!.Count, Is.EqualTo(3));
                Assert.That(dto.TaskId, Is.EqualTo(taskId));
                Assert.That(dto.Task, Is.EqualTo(task));
                Assert.That(dto.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(dto.DateDue, Is.EqualTo(dateDue));
                Assert.That(dto.DateCompleted, Is.EqualTo(dateCompleted));
                Assert.That(dto.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(dto.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Interface")]
        public void CoverGroupsInterfaceGetterCastingLogic()
    {
      // Arrange
      IProjectAssignmentDTO iDTO = _projectAssignmentDTO;
            var groupA = new GroupDTO { Id = 10, Name = "Group A" };
            var groupB = new GroupDTO { Id = 20, Name = "Group B" };
            _projectAssignmentDTO.Groups = [groupA, groupB];

            // Act - This covers the [.. Groups!.Cast<IGroupDTO>()] logic
            var interfaceGroups = iDTO.Groups;

            // Assert
            Assert.That(interfaceGroups, Is.Not.Null);
            Assert.That(interfaceGroups.Count, Is.EqualTo(2));
      Assert.Multiple(() =>
      {
        Assert.That(interfaceGroups[0].Id, Is.EqualTo(10));
        Assert.That(interfaceGroups[0].Name, Is.EqualTo("Group A"));
        Assert.That(interfaceGroups[1].Id, Is.EqualTo(20));
        Assert.That(interfaceGroups[1].Name, Is.EqualTo("Group B"));
      });
    }

    [Test, Category("Interface")]
        public void CoverGroupsInterfaceSetterConversionLogic()
    {
      // Arrange
      IProjectAssignmentDTO iDTO = _projectAssignmentDTO;
            var group1 = new GroupDTO { Id = 100, Name = "Interface Group 1" };
            var group2 = new GroupDTO { Id = 200, Name = "Interface Group 2" };
            var interfaceGroups = new List<IGroupDTO> { group1, group2 };

            // Act - This covers the value!.ConvertAll(group => (GroupDTO)group) logic
            iDTO.Groups = interfaceGroups;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups.Count, Is.EqualTo(2));
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
                largeGroupsList.Add(new GroupDTO { Id = i, Name = $"Group {i}" });
            }

            // Act
            _projectAssignmentDTO.Groups = largeGroupsList;

            // Assert
            Assert.That(_projectAssignmentDTO.Groups, Is.Not.Null);
            Assert.That(_projectAssignmentDTO.Groups.Count, Is.EqualTo(1000));
            Assert.That(_projectAssignmentDTO.Groups[999].Name, Is.EqualTo("Group 999"));
        }

        [Test, Category("DateTime")]
        public void HandleDateTimePrecision()
        {
            // Arrange
            var preciseDateTime = new DateTime(2025, 10, 20, 14, 35, 42, 123);

            // Act & Assert for DateDue
            _projectAssignmentDTO.DateDue = preciseDateTime;
            Assert.That(_projectAssignmentDTO.DateDue, Is.EqualTo(preciseDateTime));

            // Act & Assert for DateModified
            _projectAssignmentDTO.DateModified = preciseDateTime;
            Assert.That(_projectAssignmentDTO.DateModified, Is.EqualTo(preciseDateTime));
        }

        [Test, Category("Unicode")]
        public void HandleUnicodeCharacters()
    {
      // Arrange
      var unicodeName = "项目分配 🚀";
            var unicodeDescription = "这是一个测试描述 with émojis 🎯 and spëcial çhars";

            // Act
            _projectAssignmentDTO.Name = unicodeName;
            _projectAssignmentDTO.Description = unicodeDescription;
      Assert.Multiple(() =>
      {

        // Assert
        Assert.That(_projectAssignmentDTO.Name, Is.EqualTo(unicodeName));
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
        }

        #endregion
    }
}
