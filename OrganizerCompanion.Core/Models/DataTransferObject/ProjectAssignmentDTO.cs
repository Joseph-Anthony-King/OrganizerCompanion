using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class ProjectAssignmentDTO : IProjectAssignmentDTO
    {
        #region Fields
        private readonly DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        ISubAccountDTO? IProjectAssignmentDTO.Assignee
        {
            get => Assignee;
            set => Assignee = (SubAccountDTO?)value;
        }

        [JsonIgnore]
        List<IGroupDTO>? IProjectAssignmentDTO.Groups
        {
            get => [.. Groups!.Cast<IGroupDTO>()];
            set => Groups = value!.ConvertAll(group => (GroupDTO)group);
        }

        [JsonIgnore]
        IProjectTaskDTO? IProjectAssignmentDTO.Task
        {
            get => Task;
            set => Task = (ProjectTaskDTO?)value;
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("name"), MinLength(1, ErrorMessage = "Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { set; get; } = string.Empty;

        [JsonPropertyName("description"), MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; } = null;

        [JsonPropertyName("assigneeId"), Range(0, int.MaxValue, ErrorMessage = "Assignee Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AssigneeId { get; set; } = null;

        [JsonPropertyName("assignee"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SubAccountDTO? Assignee { get; set; } = null;

        [JsonPropertyName("locationId"), Range(0, int.MaxValue, ErrorMessage = "Location Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? LocationId { get; set; } = null;

        [JsonPropertyName("locationType"), MaxLength(100, ErrorMessage = "Location Type cannot exceed 100 characters."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LocationType { get; set; } = null;

        [JsonPropertyName("location"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IAddressDTO? Location { get; set; } = null;

        [Required, JsonPropertyName("groups")]
        public List<GroupDTO>? Groups { get; set; } = null;

        [Required, JsonPropertyName("taskId"), Range(0, int.MaxValue, ErrorMessage = "Task Id must be a non-negative number."), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? TaskId { get; set; } = null;

        [Required, JsonPropertyName("task"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectTaskDTO? Task { get; set; } = null;

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
        public ProjectAssignmentDTO() { }

        [JsonConstructor]
        public ProjectAssignmentDTO(
            int id,
            string name,
            string? description,
            SubAccountDTO? assignee,
            int? locationId,
            string? locationType,
            IAddressDTO? location,
            List<GroupDTO>? groups,
            int? taskId,
            ProjectTaskDTO? task,
            bool isCompleted,
            DateTime? dateDue,
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            Name = name;
            Description = description;
            Assignee = assignee;
            AssigneeId = assignee!.Id;
            LocationId = locationId;
            LocationType = locationType;
            Location = location;
            Groups = groups;
            TaskId = taskId;
            Task = task;
            IsCompleted = isCompleted;
            DateDue = dateDue;
            _dateCompleted = dateCompleted;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => throw new NotImplementedException();
        #endregion
    }
}
