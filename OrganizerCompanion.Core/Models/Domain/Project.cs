using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Project : IProject
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string _name = string.Empty;
        private string? _description = null;
        private List<Group>? _groups = null;
        private List<ProjectTask>? _tasks = null;
        private bool _isCompleted = false;
        private DateTime? _dateDue = null;
        private DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IGroup>? IProject.Groups
        {
            get => _groups?.Cast<IGroup>().ToList();
            set => _groups = value?.Cast<Group>().ToList();
        }
        [JsonIgnore]
        List<IProjectTask>? IProject.Tasks
        {
            get => _tasks?.Cast<IProjectTask>().ToList();
            set => _tasks = value?.Cast<ProjectTask>().ToList();
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
                _id = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("name"), MinLength(1, ErrorMessage = "Name must be at least 1 character long."), MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name must be at least 1 character long.", nameof(Name));
                if (value.Length > 100)
                    throw new ArgumentException("Name cannot exceed 100 characters.", nameof(Name));
                _name = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("description"), MinLength(1, ErrorMessage = "Description must be at least 1 character long"), MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string? Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Description must be at least 1 character long.", nameof(Description));
                if (value.Length > 1000)
                    throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(Description));
                _description = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("groups")]
        public List<Group>? Groups
        {
            get => _groups;
            set
            {
                _groups ??= [];
                _groups = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("tasks")]
        public List<ProjectTask>? Tasks
        {
            get => _tasks;
            set
            {
                _tasks ??= [];
                _tasks = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                var now = DateTime.UtcNow;
                _dateCompleted = value == true ? now : null;
                _isCompleted = value;
                DateModified = now;
            }
        }

        [Required, JsonPropertyName("dateDue")]
        public DateTime? DateDue
        {
            get => _dateDue;
            set
            {
                _dateDue = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("dateCompleted")]
        public DateTime? DateCompleted => _dateCompleted;

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion

        #region Constructors
        public Project() { }

        [JsonConstructor]
        public Project(
            int id,
            string name,
            string? description,
            List<Group>? groups,
            List<ProjectTask>? tasks,
            bool isCompleted,
            DateTime? dateDue,
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _name = name;
            _description = description;
            _groups = groups;
            _tasks = tasks;
            _isCompleted = isCompleted;
            _dateDue = dateDue;
            _dateCompleted = dateCompleted;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(ProjectDTO) || typeof(T) == typeof(IProjectDTO))
                {
                    object obj = new ProjectDTO(
                        id: _id,
                        name: _name,
                        description: _description,
                        groups: _groups?.Select(g => g.Cast<GroupDTO>()).ToList(),
                        tasks: _tasks?.Select(t => t.Cast<ProjectTaskDTO>()).ToList(),
                        isCompleted: _isCompleted,
                        dateDue: _dateDue,
                        dateCompleted: _dateCompleted,
                        dateCreated: _dateCreated,
                        dateModified: DateModified
                    );
                    return (T)obj;
                }
                else throw new InvalidCastException($"Cannot cast Project to type {typeof(T).Name}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);
        #endregion
    }
}
