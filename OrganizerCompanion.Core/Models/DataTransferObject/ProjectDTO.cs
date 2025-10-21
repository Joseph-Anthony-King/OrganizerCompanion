using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class ProjectDTO : IProjectDTO
    {
        #region Fields
        private readonly DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.UtcNow;
        #endregion

        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IGroupDTO>? IProjectDTO.Groups
        {
            get => Groups?.Cast<IGroupDTO>().ToList();
            set => Groups = value?.ConvertAll(group => (GroupDTO)group);
        }

        [JsonIgnore]
        List<IProjectTaskDTO>? IProjectDTO.Tasks
        {
            get => Tasks?.Cast<IProjectTaskDTO>().ToList();
            set => Tasks = value?.ConvertAll(task => (ProjectTaskDTO)task);
        }
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("name"), MinLength(1, ErrorMessage = "Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required, JsonPropertyName("description"), MinLength(1, ErrorMessage = "Description must be at least 1 character long"), MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string? Description { get; set; } = null;

        [JsonPropertyName("groups")]
        public List<GroupDTO>? Groups { get; set; } = null;

        [JsonPropertyName("tasks")]
        public List<ProjectTaskDTO>? Tasks { get; set; } = null;

        [Required, JsonPropertyName("isCompleted")] 
        public bool IsCompleted { get; set; } = false;

        [Required, JsonPropertyName("dateDue")]
        public DateTime? DateDue { get; set; } = null;

        [Required, JsonPropertyName("dateCompleted")]
        public DateTime? DateCompleted => _dateCompleted;

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public ProjectDTO() { }

        [JsonConstructor]
        public ProjectDTO(
            int id, 
            string name, 
            string? description, 
            List<GroupDTO>? groups, 
            List<ProjectTaskDTO>? tasks, 
            bool isCompleted, 
            DateTime? dateDue, 
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            Id = id;
            Name = name;
            Description = description;
            Groups = groups;
            Tasks = tasks;
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
