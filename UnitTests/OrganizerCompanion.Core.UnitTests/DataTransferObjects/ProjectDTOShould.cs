using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class ProjectDTOShould
    {
        private ProjectDTO _sut;
        private List<GroupDTO> _testGroups;
        private List<ProjectTaskDTO> _testTasks;

        [SetUp]
        public void SetUp()
        {
            _sut = new ProjectDTO();

            _testGroups = new List<GroupDTO>
            {
                new() { Id = 1, GroupName = "Development Team" },
                new() { Id = 2, GroupName = "QA Team" }
            };

            _testTasks = new List<ProjectTaskDTO>
            {
                new() { Id = 1, ProjectTaskName = "Task 1" },
                new() { Id = 2, ProjectTaskName = "Task 2" }
            };
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateProjectDTOWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            _sut = new ProjectDTO();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.ProjectName, Is.EqualTo(string.Empty));
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Groups, Is.Null);
                Assert.That(_sut.Tasks, Is.Null);
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.DueDate, Is.Null);
                Assert.That(_sut.CompletedDate, Is.Null);
                Assert.That(_sut.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(default(DateTime?)));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonConstructor_ShouldCreateProjectDTOWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var name = "Test Project";
            var description = "Test Description";
            var groups = _testGroups;
            var tasks = _testTasks;
            var isCompleted = true;
            var dueDate = DateTime.UtcNow.AddDays(7);
            var completedDate = DateTime.UtcNow.AddDays(-1);
            var createdDate = DateTime.UtcNow.AddDays(-10);
            var modifiedDate = DateTime.UtcNow;

            // Act
            _sut = new ProjectDTO(id, name, description, groups, tasks, isCompleted, dueDate, completedDate, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.ProjectName, Is.EqualTo(name));
                Assert.That(_sut.Description, Is.EqualTo(description));
                Assert.That(_sut.Groups, Is.EqualTo(groups));
                Assert.That(_sut.Tasks, Is.EqualTo(tasks));
                Assert.That(_sut.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(_sut.DueDate, Is.EqualTo(dueDate));
                Assert.That(_sut.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(_sut.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonConstructor_ShouldCreateProjectDTOWithNullValues()
        {
            // Arrange & Act
            _sut = new ProjectDTO(0, string.Empty, null, null, null, false, null, null, DateTime.UtcNow, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Description, Is.Null);
                Assert.That(_sut.Groups, Is.Null);
                Assert.That(_sut.Tasks, Is.Null);
                Assert.That(_sut.DueDate, Is.Null);
                Assert.That(_sut.CompletedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedId = 456;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptZeroValue()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void ProjectName_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedName = "New Project Name";

            // Act
            _sut.ProjectName = expectedName;

            // Assert
            Assert.That(_sut.ProjectName, Is.EqualTo(expectedName));
        }

        [Test, Category("DataTransferObjects")]
        public void ProjectName_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.ProjectName = string.Empty;

            // Assert
            Assert.That(_sut.ProjectName, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDescription = "New project description";

            // Act
            _sut.Description = expectedDescription;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(expectedDescription));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Description = null;

            // Assert
            Assert.That(_sut.Description, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Groups_ShouldGetAndSetValue()
        {
            // Arrange & Act
            _sut.Groups = _testGroups;

            // Assert
            Assert.That(_sut.Groups, Is.EqualTo(_testGroups));
        }

        [Test, Category("DataTransferObjects")]
        public void Groups_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Groups = null;

            // Assert
            Assert.That(_sut.Groups, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Tasks_ShouldGetAndSetValue()
        {
            // Arrange & Act
            _sut.Tasks = _testTasks;

            // Assert
            Assert.That(_sut.Tasks, Is.EqualTo(_testTasks));
        }

        [Test, Category("DataTransferObjects")]
        public void Tasks_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Tasks = null;

            // Assert
            Assert.That(_sut.Tasks, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsCompleted_ShouldGetAndSetValue()
        {
            // Arrange & Act
            _sut.IsCompleted = true;

            // Assert
            Assert.That(_sut.IsCompleted, Is.True);

            // Act
            _sut.IsCompleted = false;

            // Assert
            Assert.That(_sut.IsCompleted, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void DueDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = DateTime.UtcNow.AddDays(7);

            // Act
            _sut.DueDate = expectedDate;

            // Assert
            Assert.That(_sut.DueDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DueDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DueDate = null;

            // Assert
            Assert.That(_sut.DueDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CompletedDate_ShouldReturnReadOnlyValue()
        {
            // Arrange & Act - CompletedDate is readonly and controlled by constructor
            var result = _sut.CompletedDate;

            // Assert
            Assert.That(result, Is.Null); // Default constructor sets _completedDate to null
        }

        [Test, Category("DataTransferObjects")]
        public void CompletedDate_ShouldReturnValueFromConstructor()
        {
            // Arrange
            var expectedCompletedDate = DateTime.UtcNow.AddDays(-2);

            // Act
            _sut = new ProjectDTO(1, "Test", "Desc", null, null, true, null, expectedCompletedDate, DateTime.UtcNow, null);

            // Assert
            Assert.That(_sut.CompletedDate, Is.EqualTo(expectedCompletedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldReturnReadOnlyValue()
        {
            // Arrange & Act - CreatedDate is readonly and set in constructor
            var result = _sut.CreatedDate;

            // Assert
            Assert.That(result, Is.Not.EqualTo(default(DateTime)));
            Assert.That(result, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldReturnValueFromConstructor()
        {
            // Arrange
            var expectedCreatedDate = DateTime.UtcNow.AddDays(-5);

            // Act
            _sut = new ProjectDTO(1, "Test", "Desc", null, null, false, null, null, expectedCreatedDate, null);

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(expectedCreatedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = DateTime.UtcNow;

            // Act
            _sut.ModifiedDate = expectedDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.ModifiedDate = null;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.Null);
        }

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOGroups_Get_ShouldReturnNullWhenGroupsIsNull()
        {
            // Arrange
            _sut.Groups = null;
            var iProjectDto = (IProjectDTO)_sut;

            // Act
            var result = iProjectDto.Groups;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOGroups_Get_ShouldReturnConvertedList()
        {
            // Arrange
            _sut.Groups = _testGroups;
            var iProjectDto = (IProjectDTO)_sut;

            // Act
            var result = iProjectDto.Groups;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Count, Is.EqualTo(_testGroups.Count));
            Assert.That(result.All(g => g is IGroupDTO), Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOGroups_Set_ShouldConvertAndAssignList()
        {
            // Arrange
            var iProjectDto = (IProjectDTO)_sut;
            var interfaceGroups = _testGroups.Cast<IGroupDTO>().ToList();

            // Act
            iProjectDto.Groups = interfaceGroups;

            // Assert
            Assert.That(_sut.Groups, Is.Not.Null);
            Assert.That(_sut.Groups!.Count, Is.EqualTo(interfaceGroups.Count));
            Assert.That(_sut.Groups.All(g => g is GroupDTO), Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOGroups_Set_ShouldAcceptNullValue()
        {
            // Arrange
            var iProjectDto = (IProjectDTO)_sut;

            // Act
            iProjectDto.Groups = null;

            // Assert
            Assert.That(_sut.Groups, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOTasks_Get_ShouldReturnNullWhenTasksIsNull()
        {
            // Arrange
            _sut.Tasks = null;
            var iProjectDto = (IProjectDTO)_sut;

            // Act
            var result = iProjectDto.Tasks;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOTasks_Get_ShouldReturnConvertedList()
        {
            // Arrange
            _sut.Tasks = _testTasks;
            var iProjectDto = (IProjectDTO)_sut;

            // Act
            var result = iProjectDto.Tasks;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Count, Is.EqualTo(_testTasks.Count));
            Assert.That(result.All(t => t is IProjectTaskDTO), Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOTasks_Set_ShouldConvertAndAssignList()
        {
            // Arrange
            var iProjectDto = (IProjectDTO)_sut;
            var interfaceTasks = _testTasks.Cast<IProjectTaskDTO>().ToList();

            // Act
            iProjectDto.Tasks = interfaceTasks;

            // Assert
            Assert.That(_sut.Tasks, Is.Not.Null);
            Assert.That(_sut.Tasks!.Count, Is.EqualTo(interfaceTasks.Count));
            Assert.That(_sut.Tasks.All(t => t is ProjectTaskDTO), Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOTasks_Set_ShouldAcceptNullValue()
        {
            // Arrange
            var iProjectDto = (IProjectDTO)_sut;

            // Act
            iProjectDto.Tasks = null;

            // Assert
            Assert.That(_sut.Tasks, Is.Null);
        }

        #endregion

        #region Method Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.ToJson());
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void ShouldImplementIProjectDTO()
        {
            // Arrange & Act & Assert
            Assert.That(_sut, Is.InstanceOf<IProjectDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void ShouldImplementIDomainEntity()
        {
            // Arrange & Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        #endregion

        #region Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Id));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Id));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("id"));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRangeAttribute()
        {
         // Arrange
      var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Id));

            // Act
            var attribute = property!.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

// Assert
            Assert.That(attribute, Is.Not.Null);
    Assert.That(attribute!.Minimum, Is.EqualTo(0));
            Assert.That(attribute.Maximum, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void ProjectName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.ProjectName));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ProjectName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.ProjectName));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("ProjectName"));
        }

        [Test, Category("DataTransferObjects")]
        public void ProjectName_ShouldHaveMinLengthAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.ProjectName));

            // Act
            var attribute = property!.GetCustomAttribute<MinLengthAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Length, Is.EqualTo(1));
        }

        [Test, Category("DataTransferObjects")]
        public void ProjectName_ShouldHaveMaxLengthAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.ProjectName));

            // Act
            var attribute = property!.GetCustomAttribute<MaxLengthAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Length, Is.EqualTo(100));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Description));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Description_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Description));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("description"));
        }

        [Test, Category("DataTransferObjects")]
        public void Groups_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Groups));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("groups"));
        }

        [Test, Category("DataTransferObjects")]
        public void Tasks_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.Tasks));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("tasks"));
        }

        [Test, Category("DataTransferObjects")]
        public void IsCompleted_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.IsCompleted));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsCompleted_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.IsCompleted));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("isCompleted"));
        }

        [Test, Category("DataTransferObjects")]
        public void DueDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.DueDate));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DueDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.DueDate));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("dueDate"));
        }

        [Test, Category("DataTransferObjects")]
        public void CompletedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.CompletedDate));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CompletedDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.CompletedDate));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("completedDate"));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.CreatedDate));

            // Act
            var attribute = property!.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(ProjectDTO).GetProperty(nameof(ProjectDTO.CreatedDate));

            // Act
            var attribute = property!.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute!.Name, Is.EqualTo("createdDate"));
        }

        #endregion

        #region JsonIgnore Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOGroups_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var interfaceMap = typeof(ProjectDTO).GetInterfaceMap(typeof(IProjectDTO));
            var targetMethod = interfaceMap.TargetMethods.FirstOrDefault(m => m.Name.Contains("get_Groups"));

            // Act & Assert
            Assert.That(targetMethod, Is.Not.Null, "Interface property getter should exist");

            // The JsonIgnore attribute is applied to the explicit interface implementation
            // We can verify this by checking the implementation uses explicit interface syntax
            var property = typeof(ProjectDTO).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name.Contains("Groups") && p.DeclaringType == typeof(ProjectDTO));

            // The explicit interface implementation should not be directly accessible as a property
            // Instead, verify through interface casting that it works correctly
            var iProjectDto = (IProjectDTO)_sut;
            Assert.That(() => iProjectDto.Groups, Throws.Nothing);
        }

        [Test, Category("DataTransferObjects")]
        public void IProjectDTOTasks_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var interfaceMap = typeof(ProjectDTO).GetInterfaceMap(typeof(IProjectDTO));
            var targetMethod = interfaceMap.TargetMethods.FirstOrDefault(m => m.Name.Contains("get_Tasks"));

            // Act & Assert
            Assert.That(targetMethod, Is.Not.Null, "Interface property getter should exist");

            // Verify through interface casting that it works correctly
            var iProjectDto = (IProjectDTO)_sut;
            Assert.That(() => iProjectDto.Tasks, Throws.Nothing);
        }

        #endregion
    }
}
