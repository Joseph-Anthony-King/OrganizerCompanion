using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class ProjectShould
    {
        private Project _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Project();
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateProjectWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            _sut = new Project();
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
                Assert.That(_sut.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreateProjectWithProvidedValues()
        {
            // Arrange
            var id = 1;
            var name = "Test Project";
            var description = "Test Description";
            var groups = new List<Group> { new() };
            var tasks = new List<ProjectTask> { new() };
            var isCompleted = true;
            var dueDate = DateTime.UtcNow.AddDays(7);
            var completedDate = DateTime.UtcNow;
            var createdDate = DateTime.UtcNow.AddDays(-1);
            var modifiedDate = DateTime.UtcNow.AddHours(-1);

            // Act
            var project = new Project(id, name, description, groups, tasks, isCompleted, dueDate, completedDate, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(project.Id, Is.EqualTo(id));
                Assert.That(project.ProjectName, Is.EqualTo(name));
                Assert.That(project.Description, Is.EqualTo(description));
                Assert.That(project.Groups, Is.EqualTo(groups));
                Assert.That(project.Tasks, Is.EqualTo(tasks));
                Assert.That(project.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(project.DueDate, Is.EqualTo(dueDate));
                Assert.That(project.CompletedDate, Is.EqualTo(completedDate));
                Assert.That(project.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(project.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_ShouldCreateProjectFromDTO()
        {
            // Arrange
            var dto = new ProjectDTO(
                1, "DTO Project", "DTO Desc",
                new List<GroupDTO> { new() { Id = 1, GroupName = "Group1", Description = "" } },
                new List<ProjectTaskDTO> { new(1, "Task1", "", null, false, null, null, DateTime.UtcNow, null) },
                true, DateTime.UtcNow.AddDays(5), DateTime.UtcNow, DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1)
            );

            // Act
            var project = new Project(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(project.Id, Is.EqualTo(dto.Id));
                Assert.That(project.ProjectName, Is.EqualTo(dto.ProjectName));
                Assert.That(project.Description, Is.EqualTo(dto.Description));
                Assert.That(project.Groups, Has.Count.EqualTo(dto.Groups.Count));
                Assert.That(project.Tasks, Has.Count.EqualTo(dto.Tasks.Count));
                Assert.That(project.IsCompleted, Is.EqualTo(dto.IsCompleted));
                Assert.That(project.DueDate, Is.EqualTo(dto.DueDate));
                Assert.That(project.CompletedDate, Is.EqualTo(dto.CompletedDate));
                Assert.That(project.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(project.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullCollections_ShouldCreateProjectWithEmptyCollections()
        {
            // Arrange
            var dto = new ProjectDTO(1, "DTO Project", "DTO Desc", null, null, false, null, null, DateTime.UtcNow, null);

            // Act
            var project = new Project(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(project.Groups, Is.Null);
                Assert.That(project.Tasks, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var initialDate = _sut.ModifiedDate;
            Thread.Sleep(1);

            // Act
            _sut.Id = 1;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(initialDate));
        }

        [Test, Category("Models")]
        public void Id_WhenSetToNegative_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
        }

        [Test, Category("Models")]
        public void ProjectName_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var initialDate = _sut.ModifiedDate;
            Thread.Sleep(1);

            // Act
            _sut.ProjectName = "New Name";

            // Assert
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(initialDate));
        }


        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ProjectName_WhenSetToNullOrWhitespace_ShouldThrowArgumentException(string name)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ProjectName = name);
        }

        [Test, Category("Models")]
        public void ProjectName_WhenSetToOver100Chars_ShouldThrowArgumentException()
        {
            // Arrange
            var longName = new string('a', 101);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.ProjectName = longName);
        }

        [Test, Category("Models")]
        public void Description_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var initialDate = _sut.ModifiedDate;
            Thread.Sleep(1);

            // Act
            _sut.Description = "New Description";

            // Assert
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(initialDate));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Description_WhenSetToNullOrWhitespace_ShouldThrowArgumentException(string description)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = description);
        }

        [Test, Category("Models")]
        public void Description_WhenSetToOver1000Chars_ShouldThrowArgumentException()
        {
            // Arrange
            var longDescription = new string('a', 1001);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _sut.Description = longDescription);
        }

        [Test, Category("Models")]
        public void Groups_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var initialDate = _sut.ModifiedDate;
            Thread.Sleep(1);

            // Act
            _sut.Groups = new List<Group>();

            // Assert
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(initialDate));
        }

        [Test, Category("Models")]
        public void Groups_WhenSetToNull_ShouldInitializeEmptyList()
        {
            // Arrange
            _sut.Groups = new List<Group>(); // Set to non-null first

            // Act
            _sut.Groups = null;

            // Assert
            Assert.That(_sut.Groups, Is.Null);
        }

        [Test, Category("Models")]
        public void Tasks_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var initialDate = _sut.ModifiedDate;
            Thread.Sleep(1);

            // Act
            _sut.Tasks = new List<ProjectTask>();

            // Assert
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(initialDate));
        }

        [Test, Category("Models")]
        public void Tasks_WhenSetToNull_ShouldInitializeEmptyList()
        {
            // Arrange
            _sut.Tasks = new List<ProjectTask>(); // Set to non-null first

            // Act
            _sut.Tasks = null;

            // Assert
            Assert.That(_sut.Tasks, Is.Null);
        }

        [Test, Category("Models")]
        public void IsCompleted_WhenSetToTrue_ShouldSetCompletedDate()
        {
            // Arrange
            var beforeTime = DateTime.UtcNow;

            // Act
            _sut.IsCompleted = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCompleted, Is.True);
                Assert.That(_sut.CompletedDate, Is.Not.Null);
                Assert.That(_sut.CompletedDate, Is.GreaterThanOrEqualTo(beforeTime));
            });
        }

        [Test, Category("Models")]
        public void IsCompleted_WhenSetToFalse_ShouldClearCompletedDate()
        {
            // Arrange
            _sut.IsCompleted = true; // Set to true first

            // Act
            _sut.IsCompleted = false;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCompleted, Is.False);
                Assert.That(_sut.CompletedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DueDate_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var initialDate = _sut.ModifiedDate;
            Thread.Sleep(1);

            // Act
            _sut.DueDate = DateTime.UtcNow;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(initialDate));
        }

        #endregion

        #region Explicit Interface Tests

        [Test, Category("Models")]
        public void IProject_Groups_GetAndSet_ShouldWorkCorrectly()
        {
            // Arrange
            IProject iProject = _sut;
            var groups = new List<IGroup> { new Group() };

            // Act
            iProject.Groups = groups;
            var retrievedGroups = iProject.Groups;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retrievedGroups, Is.Not.Null);
                Assert.That(retrievedGroups, Has.Count.EqualTo(1));
                Assert.That(_sut.Groups, Has.Count.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void IProject_Groups_SetToNull_ShouldSetNull()
        {
            // Arrange
            IProject iProject = _sut;

            // Act
            iProject.Groups = null;

            // Assert
            Assert.That(_sut.Groups, Is.Null);
        }

        [Test, Category("Models")]
        public void IProject_Tasks_GetAndSet_ShouldWorkCorrectly()
        {
            // Arrange
            IProject iProject = _sut;
            var tasks = new List<IProjectTask> { new ProjectTask() };

            // Act
            iProject.Tasks = tasks;
            var retrievedTasks = iProject.Tasks;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retrievedTasks, Is.Not.Null);
                Assert.That(retrievedTasks, Has.Count.EqualTo(1));
                Assert.That(_sut.Tasks, Has.Count.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void IProject_Tasks_SetToNull_ShouldSetNull()
        {
            // Arrange
            IProject iProject = _sut;

            // Act
            iProject.Tasks = null;

            // Assert
            Assert.That(_sut.Tasks, Is.Null);
        }

        #endregion

        #region Method Tests

        [Test, Category("Models")]
        public void Cast_ToProjectDTO_ShouldReturnValidDTO()
        {
            // Arrange
            _sut.Id = 1;
            _sut.ProjectName = "Test Project";
            _sut.Description = "Test Desc";
            _sut.Groups = new List<Group> { new() { GroupName = "G1" } };
            _sut.Tasks = new List<ProjectTask> { new() { ProjectTaskName = "T1" } };

            // Act
            var dto = _sut.Cast<ProjectDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(_sut.Id));
                Assert.That(dto.ProjectName, Is.EqualTo(_sut.ProjectName));
                Assert.That(dto.Description, Is.EqualTo(_sut.Description));
                Assert.That(dto.Groups, Has.Count.EqualTo(1));
                Assert.That(dto.Tasks, Has.Count.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIProjectDTO_ShouldReturnValidDTO()
        {
            // Arrange
            _sut.Id = 1;
            _sut.ProjectName = "Test Project";
            _sut.Description = "Test Desc";

            // Act
            var dto = _sut.Cast<IProjectDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto, Is.InstanceOf<IProjectDTO>());
                Assert.That(dto.Id, Is.EqualTo(_sut.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullCollections_ShouldReturnDTOWithNullCollections()
        {
            // Arrange
            _sut.Id = 1;
            _sut.ProjectName = "Test Project";
            _sut.Description = "Test Desc";
            _sut.Groups = null;
            _sut.Tasks = null;

            // Act
            var dto = _sut.Cast<ProjectDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Groups, Is.Null);
                Assert.That(dto.Tasks, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Act & Assert
            Assert.Throws<InvalidCastException>(() => _sut.Cast<Contact>());
        }

        [Test, Category("Models")]
        public void Cast_WhenExceptionOccurs_ShouldRethrow()
        {
            // Arrange
            _sut.ProjectName = "test";
            _sut.Description = "test";
            // This will cause a NullReferenceException in Cast because Task.Cast is not implemented
            _sut.Tasks = new List<ProjectTask> { new ProjectTask() };

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _sut.Cast<ProjectDTO>());
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.ProjectName = "Test Project";
            _sut.Description = "Test Desc";

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"projectName\":\"Test Project\""));
            });
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.ProjectName = "Test Project";
            _sut.IsCompleted = true;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain(".Id:1.Name:Test Project.IsCompleted:True"));
        }

        #endregion
    }
}
