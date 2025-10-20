using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using Task = OrganizerCompanion.Core.Models.Domain.Task;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class AssignmentDTO : IAssignmentDTO
    {
        #region Fields
        private readonly DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IGroupDTO>? IAssignmentDTO.Groups
        {
            get => [.. Groups!.Cast<IGroupDTO>()];
            set => Groups = value!.ConvertAll(group => (GroupDTO)group);
        }
        [JsonIgnore]
        ITask? IAssignmentDTO.Task
        {
            get => Task;
            set => Task = (Task?)value;
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
        public string Name { set; get; } = string.Empty;

        [JsonPropertyName("description"), MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; } = null;

        [Required, JsonPropertyName("groups")]
        public List<GroupDTO>? Groups { get; set; } = null;

        [Required, JsonPropertyName("taskId"), Range(0, int.MaxValue, ErrorMessage = "Task Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? TaskId { get; set; } = null;
        
        [Required, JsonPropertyName("task"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Task? Task { get; set; } = null;

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
        public AssignmentDTO() { }

        [JsonConstructor]
        public AssignmentDTO(
            int id,
            string name, 
            string? description, 
            List<GroupDTO>? groups,
            int? taskId,
            Task? task,
            bool isCompleted, 
            DateTime? dateDue,
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Groups = groups;
            TaskId = taskId;
            Task = task;
            IsCompleted = isCompleted;
            DateDue = dateDue;
            _dateCompleted = dateCompleted;
            _dateCreated = dateCreated;
            DateModified = dateModified;
            IsCast = isCast ?? false;
            CastId = castId ?? 0;
            CastType = castType;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => throw new NotImplementedException();
        #endregion
    }
}
