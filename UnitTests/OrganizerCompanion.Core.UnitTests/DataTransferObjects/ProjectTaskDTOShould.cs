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
            Assert.That(_sut.Id, Is.EqualTo(id));
            Assert.That(_sut.Name, Is.EqualTo(name));
            Assert.That(_sut.Description, Is.EqualTo(description));
            Assert.That(_sut.Assignments, Is.EqualTo(assignments));
            Assert.That(_sut.IsCompleted, Is.EqualTo(isCompleted));
            Assert.That(_sut.DateDue, Is.EqualTo(dateDue));
            Assert.That(_sut.DateCompleted, Is.EqualTo(dateCompleted));
            Assert.That(_sut.DateCreated, Is.EqualTo(dateCreated));
            Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
        }

        #endregion

        #region IDomainEntity Properties Tests

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var value = _sut.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var value = _sut.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var value = _sut.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "TestType");
        }

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
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<IProjectTaskDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_WithDifferentType_ShouldThrowNotImplementedException()
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
        public void AsIDomainEntity_IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var value = entity.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => entity.IsCast = true);
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var value = entity.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => entity.CastId = 1);
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var value = entity.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void AsIDomainEntity_CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange
            var entity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => entity.CastType = "TestType");
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

            // Assert
            Assert.That(ReferenceEquals(_sut, asProjectTaskDTO), Is.True);
            Assert.That(ReferenceEquals(_sut, asDomainEntity), Is.True);
            Assert.That(ReferenceEquals(_sut, backToProjectTaskDTO), Is.True);
            Assert.That(ReferenceEquals(asProjectTaskDTO, backToProjectTaskDTO), Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void NotImplementedMembers_ShouldThrowNotImplementedException()
        {
            // This test documents that specific casting-related members throw NotImplementedException
            // This is important for understanding the current implementation state

            // Properties with getters and setters that throw NotImplementedException
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => { var value = _sut.IsCast; });
                Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
                Assert.Throws<NotImplementedException>(() => { var value = _sut.CastId; });
                Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
                Assert.Throws<NotImplementedException>(() => { var value = _sut.CastType; });
                Assert.Throws<NotImplementedException>(() => _sut.CastType = "test");
            });

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
                Assert.DoesNotThrow(() => _sut.Assignments = new List<ProjectAssignmentDTO>());
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
            // Arrange & Act & Assert
            Assert.That(_sut.GetType(), Is.EqualTo(typeof(ProjectTaskDTO)));
            Assert.That(_sut.GetType().Name, Is.EqualTo("ProjectTaskDTO"));
            Assert.That(_sut.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.DataTransferObject"));
        }

        [Test, Category("DataTransferObjects")]
        public void InterfaceImplementations_ShouldBeVerifiable()
        {
            // Arrange & Act & Assert
            Assert.That(_sut is IProjectTaskDTO, Is.True);
            Assert.That(_sut is IDomainEntity, Is.True);
            Assert.That(_sut is ProjectTaskDTO, Is.True);
            
            // Verify inheritance hierarchy
            Assert.That(typeof(IProjectTaskDTO).IsAssignableFrom(typeof(ProjectTaskDTO)), Is.True);
            Assert.That(typeof(IDomainEntity).IsAssignableFrom(typeof(ProjectTaskDTO)), Is.True);
            Assert.That(typeof(IDomainEntity).IsAssignableFrom(typeof(IProjectTaskDTO)), Is.True);
        }

        #endregion
    }
}
