using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class ProjectTaskDTO : IProjectTaskDTO
    {
        #region Fields
        private readonly DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit ITaskDTO Implementation
        [JsonIgnore]
        List<IAssignmentDTO>? IProjectTaskDTO.Assignments
        {
            get => [.. Assignments!.Cast<IAssignmentDTO>()];
            set => Assignments = [.. value!.Cast<AssignmentDTO>()];
        }
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("name"), MinLength(1, ErrorMessage = "Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required, JsonPropertyName("description"), MinLength(1, ErrorMessage = "Description must be at least 1 character long"), MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string? Description { get; set; } = null;

        [Required, JsonPropertyName("assignments")]
        public List<AssignmentDTO>? Assignments { get; set; } = null;

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; } = false;

        [Required, JsonPropertyName("dateDue")]
        public DateTime? DateDue { get; set; } = null;

        [Required, JsonPropertyName("dateCompleted")]
        public DateTime? DateCompleted => _dateCompleted;

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public ProjectTaskDTO() { }

        [JsonConstructor]
        public ProjectTaskDTO(int id, string name, string? description, List<AssignmentDTO>? assignments, bool isCompleted, DateTime? dateDue, DateTime? dateCompleted, DateTime dateCreated, DateTime? dateModified)
        {
            Id = id;
            Name = name;
            Description = description;
            Assignments = assignments;
            IsCompleted = isCompleted;
            DateDue = dateDue;
            _dateCompleted = dateCompleted;
            _dateCreated = dateCreated;
            DateModified = dateModified;
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
