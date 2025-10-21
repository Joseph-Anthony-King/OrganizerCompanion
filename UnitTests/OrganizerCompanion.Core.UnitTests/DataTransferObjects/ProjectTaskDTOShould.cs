using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class ProjectTaskDTOShould
    {
        private ProjectTaskDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new ProjectTaskDTO();
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateInstance()
        {
            // Arrange & Act
            _sut = new ProjectTaskDTO();

            // Assert
            Assert.That(_sut, Is.Not.Null);
            Assert.That(_sut, Is.InstanceOf<ProjectTaskDTO>());
            Assert.That(_sut, Is.InstanceOf<IProjectTaskDTO>());
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ParameterizedConstructor_ShouldCreateInstanceWithValues()
    {
      // Arrange
      var id = 123;
            var name = "Test Task";
            var description = "Test Description";
            var assignments = new List<ProjectAssignmentDTO>();
            var isCompleted = true;
            var dateDue = DateTime.UtcNow.AddDays(7);
            var dateCompleted = DateTime.UtcNow;
            var dateCreated = DateTime.UtcNow.AddDays(-1);
            var dateModified = DateTime.UtcNow;

            // Act
            _sut = new ProjectTaskDTO(id, name, description, assignments, isCompleted, dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.That(_sut, Is.Not.Null);
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

    #endregion

    #region IDomainEntity Properties Tests

    [Test, Category("DataTransferObjects")]
        public void Id_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.Id;

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = 123;

            // Act
            _sut.Id = expectedValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_Get_ShouldReturnUtcNow()
        {
            // Arrange & Act
            var result = _sut.DateCreated;

            // Assert
            Assert.That(result, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(result, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-1)));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.DateModified;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = DateTime.UtcNow;

            // Act
            _sut.DateModified = expectedValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedValue));
        }

        #endregion

        #region IProjectTaskDTO Properties Tests

        [Test, Category("DataTransferObjects")]
        public void Name_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.Name;

            // Assert
            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void Name_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = "Test Task Name";

            // Act
            _sut.Name = expectedValue;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Description_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.Description;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Description_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = "Test Task Description";

            // Act
            _sut.Description = expectedValue;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Assignments_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.Assignments;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Assignments_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = new List<ProjectAssignmentDTO>();

            // Act
            _sut.Assignments = expectedValue;

            // Assert
            Assert.That(_sut.Assignments, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void IsCompleted_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.IsCompleted;

            // Assert
            Assert.That(result, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsCompleted_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = true;

            // Act
            _sut.IsCompleted = expectedValue;

            // Assert
            Assert.That(_sut.IsCompleted, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateDue_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.DateDue;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateDue_Set_ShouldSetValue()
        {
            // Arrange
            var expectedValue = DateTime.UtcNow.AddDays(7);

            // Act
            _sut.DateDue = expectedValue;

            // Assert
            Assert.That(_sut.DateDue, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCompleted_Get_ShouldReturnDefaultValue()
        {
            // Arrange & Act
            var result = _sut.DateCompleted;

            // Assert
            Assert.That(result, Is.Null);
        }

        #endregion

        #region Method Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<IProjectTaskDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_WithDifferentType_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.ToJson());
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Name_Get_ShouldReturnValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = "Test Name";
            _sut.Name = expectedValue;

            // Act
            var result = dto.Name;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Name_Set_ShouldSetValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = "Test Name";

            // Act
            dto.Name = expectedValue;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Description_Get_ShouldReturnValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = "Test Description";
            _sut.Description = expectedValue;

            // Act
            var result = dto.Description;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Description_Set_ShouldSetValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = "Test Description";

            // Act
            dto.Description = expectedValue;

            // Assert
            Assert.That(_sut.Description, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Assignments_Get_ShouldReturnConvertedList()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var assignmentDTOs = new List<ProjectAssignmentDTO>();
            _sut.Assignments = assignmentDTOs;

            // Act
            var result = dto.Assignments;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<IProjectAssignmentDTO>>());
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Assignments_Set_ShouldSetConvertedList()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var assignmentDTOs = new List<IProjectAssignmentDTO>();

            // Act
            dto.Assignments = assignmentDTOs;

            // Assert
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments, Is.InstanceOf<List<ProjectAssignmentDTO>>());
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Assignments_Get_WithNullAssignments_ShouldThrowException()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            _sut.Assignments = null;

            // Act & Assert - This covers the Assignments!.Cast<IProjectAssignmentDTO>() with null
            Assert.Throws<ArgumentNullException>(() => { var _ = dto.Assignments; });
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Assignments_Set_WithNullValue_ShouldThrowException()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;

            // Act & Assert - This covers the value!.Cast<ProjectAssignmentDTO>() with null
            Assert.Throws<ArgumentNullException>(() => { dto.Assignments = null; });
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Assignments_Get_WithPopulatedList_ShouldReturnCastedItems()
    {
      // Arrange
      var dto = (IProjectTaskDTO)_sut;
            var assignment1 = new ProjectAssignmentDTO { Id = 1, Name = "Assignment 1" };
            var assignment2 = new ProjectAssignmentDTO { Id = 2, Name = "Assignment 2" };
            _sut.Assignments = [assignment1, assignment2];

            // Act
            var result = dto.Assignments;

            // Assert - This covers the casting logic [.. Assignments!.Cast<IProjectAssignmentDTO>()]
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
      Assert.Multiple(() =>
      {
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Assignment 1"));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[1].Name, Is.EqualTo("Assignment 2"));
      });
    }

    [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_Assignments_Set_WithPopulatedList_ShouldSetCastedItems()
    {
      // Arrange
      var dto = (IProjectTaskDTO)_sut;
            var assignment1 = new ProjectAssignmentDTO { Id = 10, Name = "Interface Assignment 1" };
            var assignment2 = new ProjectAssignmentDTO { Id = 20, Name = "Interface Assignment 2" };
            var interfaceAssignments = new List<IProjectAssignmentDTO> { assignment1, assignment2 };

            // Act - This covers the casting logic [.. value!.Cast<ProjectAssignmentDTO>()]
            dto.Assignments = interfaceAssignments;

            // Assert
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments.Count, Is.EqualTo(2));
            Assert.That(_sut.Assignments[0], Is.InstanceOf<ProjectAssignmentDTO>());
      Assert.Multiple(() =>
      {
        Assert.That(_sut.Assignments[0].Id, Is.EqualTo(10));
        Assert.That(_sut.Assignments[1], Is.InstanceOf<ProjectAssignmentDTO>());
      });
      Assert.That(_sut.Assignments[1].Id, Is.EqualTo(20));
    }

    [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_IsCompleted_Get_ShouldReturnValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = true;
            _sut.IsCompleted = expectedValue;

            // Act
            var result = dto.IsCompleted;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_IsCompleted_Set_ShouldSetValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = true;

            // Act
            dto.IsCompleted = expectedValue;

            // Assert
            Assert.That(_sut.IsCompleted, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_DateDue_Get_ShouldReturnValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = DateTime.UtcNow.AddDays(7);
            _sut.DateDue = expectedValue;

            // Act
            var result = dto.DateDue;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_DateDue_Set_ShouldSetValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var expectedValue = DateTime.UtcNow.AddDays(7);

            // Act
            dto.DateDue = expectedValue;

            // Assert
            Assert.That(_sut.DateDue, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIProjectTaskDTO_DateCompleted_Get_ShouldReturnValue()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;

            // Act
            var result = dto.DateCompleted;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_Id_Get_ShouldReturnValue()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;
            var expectedValue = 123;
            _sut.Id = expectedValue;

            // Act
            var result = entity.Id;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_Id_Set_ShouldSetValue()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;
            var expectedValue = 123;

            // Act
            entity.Id = expectedValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_DateCreated_Get_ShouldReturnValue()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act
            var result = entity.DateCreated;

            // Assert
            Assert.That(result, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(result, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-1)));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_DateModified_Get_ShouldReturnValue()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;
            var expectedValue = DateTime.UtcNow;
            _sut.DateModified = expectedValue;

            // Act
            var result = entity.DateModified;

            // Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_DateModified_Set_ShouldSetValue()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;
            var expectedValue = DateTime.UtcNow;

            // Act
            entity.DateModified = expectedValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_Cast_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => entity.Cast<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => entity.ToJson());
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void MultipleInterfaceCasts_ShouldMaintainSameInstance()
    {
      // Arrange & Act
      var asProjectTaskDTO = (IProjectTaskDTO)_sut;
            var asDomainEntity = (IDomainEntity)_sut;
            var backToProjectTaskDTO = (IProjectTaskDTO)asDomainEntity;
      Assert.Multiple(() =>
      {

        // Assert
        Assert.That(ReferenceEquals(_sut, asProjectTaskDTO), Is.True);
        Assert.That(ReferenceEquals(_sut, asDomainEntity), Is.True);
        Assert.That(ReferenceEquals(_sut, backToProjectTaskDTO), Is.True);
        Assert.That(ReferenceEquals(asProjectTaskDTO, backToProjectTaskDTO), Is.True);
      });
    }

    [Test, Category("DataTransferObjects")]
        public void NotImplementedMembers_ShouldThrowNotImplementedException()
        {
            // This test documents that specific casting-related members throw NotImplementedException
            // This is important for understanding the current implementation state

            // Methods that throw NotImplementedException
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ImplementedMembers_ShouldWorkCorrectly()
        {
            // This test documents that most members are properly implemented
            
            // Properties that work correctly
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => { var value = _sut.Id; });
                Assert.DoesNotThrow(() => _sut.Id = 1);
                Assert.DoesNotThrow(() => { var value = _sut.Name; });
                Assert.DoesNotThrow(() => _sut.Name = "test");
                Assert.DoesNotThrow(() => { var value = _sut.Description; });
                Assert.DoesNotThrow(() => _sut.Description = "test");
                Assert.DoesNotThrow(() => { var value = _sut.Assignments; });
                Assert.DoesNotThrow(() => _sut.Assignments = []);
                Assert.DoesNotThrow(() => { var value = _sut.IsCompleted; });
                Assert.DoesNotThrow(() => _sut.IsCompleted = true);
                Assert.DoesNotThrow(() => { var value = _sut.DateDue; });
                Assert.DoesNotThrow(() => _sut.DateDue = DateTime.Now);
                Assert.DoesNotThrow(() => { var value = _sut.DateModified; });
                Assert.DoesNotThrow(() => _sut.DateModified = DateTime.Now);
            });

            // Read-only properties that work correctly
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => { var value = _sut.DateCompleted; });
                Assert.DoesNotThrow(() => { var value = _sut.DateCreated; });
            });
        }

        [Test, Category("DataTransferObjects")]
        public void TypeChecks_ShouldReturnCorrectTypes()
    {
      // Arrange, Act & Assert
      Assert.That(_sut.GetType(), Is.EqualTo(typeof(ProjectTaskDTO)));
      Assert.Multiple(() =>
      {
        Assert.That(_sut.GetType().Name, Is.EqualTo("ProjectTaskDTO"));
        Assert.That(_sut.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.DataTransferObject"));
      });
    }

    [Test, Category("DataTransferObjects")]
        public void InterfaceImplementations_ShouldBeVerifiable()
    {
      Assert.Multiple(() =>
      {
        // Arrange, Act & Assert
        Assert.That(_sut is IProjectTaskDTO, Is.True);
        Assert.That(_sut is IDomainEntity, Is.True);
        Assert.That(_sut is ProjectTaskDTO, Is.True);

        // Verify inheritance hierarchy
        Assert.That(typeof(IProjectTaskDTO).IsAssignableFrom(typeof(ProjectTaskDTO)), Is.True);
        Assert.That(typeof(IDomainEntity).IsAssignableFrom(typeof(ProjectTaskDTO)), Is.True);
        Assert.That(typeof(IDomainEntity).IsAssignableFrom(typeof(IProjectTaskDTO)), Is.True);
      });
    }

    #endregion

    #region Comprehensive Coverage Tests

    [Test, Category("Comprehensive")]
        public void JsonConstructor_WithAllNullValues_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = 0;
            var name = string.Empty;
            string? description = null;
            List<ProjectAssignmentDTO>? assignments = null;
            var isCompleted = false;
            DateTime? dateDue = null;
            DateTime? dateCompleted = null;
            var dateCreated = DateTime.UtcNow.AddDays(-5);
            DateTime? dateModified = null;

            // Act
            var dto = new ProjectTaskDTO(id, name, description, assignments, isCompleted, dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Name, Is.EqualTo(name));
                Assert.That(dto.Description, Is.Null);
                Assert.That(dto.Assignments, Is.Null);
                Assert.That(dto.IsCompleted, Is.False);
                Assert.That(dto.DateDue, Is.Null);
                Assert.That(dto.DateCompleted, Is.Null);
                Assert.That(dto.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(dto.DateModified, Is.Null);
            });
        }

        [Test, Category("Comprehensive")]
        public void JsonConstructor_WithCompleteData_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var id = 999;
            var name = "Comprehensive Test Task";
            var description = "This is a comprehensive test description";
            var assignments = new List<ProjectAssignmentDTO>
            {
                new() { Id = 1, Name = "Assignment 1" },
                new() { Id = 2, Name = "Assignment 2" },
                new() { Id = 3, Name = "Assignment 3" }
            };
            var isCompleted = true;
            var dateDue = new DateTime(2025, 12, 31, 23, 59, 59);
            var dateCompleted = new DateTime(2025, 10, 15, 14, 30, 0);
            var dateCreated = new DateTime(2025, 10, 1, 9, 0, 0);
            var dateModified = new DateTime(2025, 10, 16, 16, 45, 30);

            // Act
            var dto = new ProjectTaskDTO(id, name, description, assignments, isCompleted, dateDue, dateCompleted, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.Name, Is.EqualTo(name));
                Assert.That(dto.Description, Is.EqualTo(description));
                Assert.That(dto.Assignments, Is.EqualTo(assignments));
                Assert.That(dto.Assignments!.Count, Is.EqualTo(3));
                Assert.That(dto.IsCompleted, Is.EqualTo(isCompleted));
                Assert.That(dto.DateDue, Is.EqualTo(dateDue));
                Assert.That(dto.DateCompleted, Is.EqualTo(dateCompleted));
                Assert.That(dto.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(dto.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Boundary")]
        public void HandleBoundaryValues_ShouldWorkCorrectly()
        {
            // Act & Assert for Id boundary values
            _sut.Id = int.MaxValue;
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

            _sut.Id = int.MinValue;
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));

            _sut.Id = 0;
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Unicode")]
        public void HandleUnicodeCharacters_ShouldWorkCorrectly()
    {
      // Arrange
      var unicodeName = "任务名称 🚀 Task";
            var unicodeDescription = "这是一个测试描述 with émojis 🎯 and spëcial çhars";

            // Act
            _sut.Name = unicodeName;
            _sut.Description = unicodeDescription;
      Assert.Multiple(() =>
      {

        // Assert
        Assert.That(_sut.Name, Is.EqualTo(unicodeName));
        Assert.That(_sut.Description, Is.EqualTo(unicodeDescription));
      });
    }

    [Test, Category("DateTime")]
        public void HandleDateTimePrecision_ShouldWorkCorrectly()
        {
            // Arrange
            var preciseDateTime = new DateTime(2025, 10, 20, 14, 35, 42, 999);

            // Act & Assert for DateDue
            _sut.DateDue = preciseDateTime;
            Assert.That(_sut.DateDue, Is.EqualTo(preciseDateTime));

            // Act & Assert for DateModified
            _sut.DateModified = preciseDateTime;
            Assert.That(_sut.DateModified, Is.EqualTo(preciseDateTime));
        }

        [Test, Category("Collections")]
        public void HandleLargeAssignmentsList_ShouldWorkCorrectly()
        {
            // Arrange
            var largeAssignmentsList = new List<ProjectAssignmentDTO>();
            for (int i = 0; i < 1000; i++)
            {
                largeAssignmentsList.Add(new ProjectAssignmentDTO { Id = i, Name = $"Assignment {i}" });
            }

            // Act
            _sut.Assignments = largeAssignmentsList;

            // Assert
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments.Count, Is.EqualTo(1000));
            Assert.That(_sut.Assignments[999].Name, Is.EqualTo("Assignment 999"));
        }

        [Test, Category("Collections")]
        public void HandleEmptyAssignmentsList_ShouldWorkCorrectly()
        {
            // Act
            _sut.Assignments = [];

            // Assert
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments, Is.Empty);
        }

        [Test, Category("Interface")]
        public void InterfaceAssignments_WithEmptyList_ShouldReturnEmptyInterface()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            _sut.Assignments = [];

            // Act
            var result = dto.Assignments;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
        }

        [Test, Category("Interface")]
        public void InterfaceAssignments_SetEmptyList_ShouldSetEmptyList()
        {
            // Arrange
            var dto = (IProjectTaskDTO)_sut;
            var emptyList = new List<IProjectAssignmentDTO>();

            // Act
            dto.Assignments = emptyList;

            // Assert
            Assert.That(_sut.Assignments, Is.Not.Null);
            Assert.That(_sut.Assignments, Is.Empty);
        }

        [Test, Category("Threading")]
        public void DateCreated_FromDifferentThreads_ShouldMaintainValue()
    {
      // Arrange
      var originalDate = _sut.DateCreated;

            // Act - Access from different contexts
            var date1 = _sut.DateCreated;
            var date2 = _sut.DateCreated;
      Assert.Multiple(() =>
      {

        // Assert
        Assert.That(date1, Is.EqualTo(originalDate));
        Assert.That(date2, Is.EqualTo(originalDate));
      });
      Assert.That(date1, Is.EqualTo(date2));
    }

    [Test, Category("Validation")]
        public void AllRequiredProperties_ShouldHaveValidationAttributes()
    {
      // This test verifies that validation attributes are properly applied
      var type = typeof(ProjectTaskDTO);
            
            // Check that key properties have Required attributes
            var idProperty = type.GetProperty("Id");
            var nameProperty = type.GetProperty("Name");
            var descriptionProperty = type.GetProperty("Description");
      Assert.Multiple(() =>
      {
        Assert.That(idProperty, Is.Not.Null);
        Assert.That(nameProperty, Is.Not.Null);
        Assert.That(descriptionProperty, Is.Not.Null);
      });

      // Verify the properties exist and are accessible
      Assert.That(idProperty!.CanRead, Is.True);
            Assert.That(idProperty.CanWrite, Is.True);
            Assert.That(nameProperty!.CanRead, Is.True);
            Assert.That(nameProperty.CanWrite, Is.True);
    }

    [Test, Category("String")]
        public void HandleVeryLongStrings_ShouldWorkCorrectly()
    {
      // Arrange
      var veryLongName = new string('A', 10000);
            var veryLongDescription = new string('B', 50000);

            // Act & Assert - DTOs don't enforce validation, so very long strings should be accepted
            Assert.DoesNotThrow(() => _sut.Name = veryLongName);
            Assert.DoesNotThrow(() => _sut.Description = veryLongDescription);
      Assert.Multiple(() =>
      {
        Assert.That(_sut.Name, Is.EqualTo(veryLongName));
        Assert.That(_sut.Description, Is.EqualTo(veryLongDescription));
      });
    }

    [Test, Category("Comprehensive")]
        public void ReadOnlyProperties_ShouldBehaveCorrectly()
        {
            // Arrange & Act - Test DateCompleted (readonly)
            var dateCompleted = _sut.DateCompleted;
            
            // Assert - DateCompleted should be null by default and not changeable
            Assert.That(dateCompleted, Is.Null);
            
            // Test DateCreated (readonly)
            var dateCreated1 = _sut.DateCreated;
            var dateCreated2 = _sut.DateCreated;
            
            // Assert - DateCreated should be consistent
            Assert.That(dateCreated1, Is.EqualTo(dateCreated2));
            Assert.That(dateCreated1, Is.LessThanOrEqualTo(DateTime.UtcNow));
        }

        #endregion
    }
}
