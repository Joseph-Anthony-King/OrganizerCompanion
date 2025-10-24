using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class ProjectTaskDTO : IProjectTaskDTO
    {
        #region Fields
        private readonly DateTime? _completedDate = null;
        private readonly DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit ITaskDTO Implementation
        [JsonIgnore]
        List<IProjectAssignmentDTO>? IProjectTaskDTO.Assignments
        {
            get => [.. Assignments!.Cast<IProjectAssignmentDTO>()];
            set => Assignments = [.. value!.Cast<ProjectAssignmentDTO>()];
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("name"), MinLength(1, ErrorMessage = "Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string ProjectTaskName { get; set; } = string.Empty;

        [Required, JsonPropertyName("description"), MinLength(1, ErrorMessage = "Description must be at least 1 character long"), MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string? Description { get; set; } = null;

        [Required, JsonPropertyName("assignments")]
        public List<ProjectAssignmentDTO>? Assignments { get; set; } = null;

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; } = false;

        [Required, JsonPropertyName("dueDate")]
        public DateTime? DueDate { get; set; } = null;

        [Required, JsonPropertyName("completedDate")]
        public DateTime? CompletedDate => _completedDate;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion

        #region Constructors
        public ProjectTaskDTO() { }

        [JsonConstructor]
        public ProjectTaskDTO(int id, string name, string? description, List<ProjectAssignmentDTO>? assignments, bool isCompleted, DateTime? dueDate, DateTime? completedDate, DateTime createdDate, DateTime? modifiedDate)
        {
            Id = id;
            ProjectTaskName = name;
            Description = description;
            Assignments = assignments;
            IsCompleted = isCompleted;
            DueDate = dueDate;
            _completedDate = completedDate;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
          throw new NotImplementedException();
        }
        #endregion
  }
}
