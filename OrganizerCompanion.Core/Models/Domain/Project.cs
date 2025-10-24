using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        private string _projectName = string.Empty;
        private string? _description = null;
        private List<Group>? _groups = null;
        private List<ProjectTask>? _tasks = null;
        private bool _isCompleted = false;
        private DateTime? _dueDate = null;
        private DateTime? _completedDate = null;
        private DateTime _createdDate = DateTime.UtcNow;
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

        [Key]
        [Column("ProjectId")]
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
                }

                _id = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("projectName"), MinLength(1, ErrorMessage = "ProjectName must be at least 1 character long."), MaxLength(100, ErrorMessage = "Project Name cannot exceed 100 characters.")]
        public string ProjectName
        {
            get => _projectName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ProjectName must be at least 1 character long.", nameof(ProjectName));
                }

                if (value.Length > 100)
                {
                    throw new ArgumentException("ProjectName cannot exceed 100 characters.", nameof(ProjectName));
                }

                _projectName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("description"), MinLength(1, ErrorMessage = "Description must be at least 1 character long"), MaxLength(1000, ErrorMessage = "Name cannot exceed 1000 characters.")]
        public string? Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description must be at least 1 character long.", nameof(Description));
                }

                if (value.Length > 1000)
                {
                    throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(Description));
                }

                _description = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("groups")]
        public List<Group>? Groups
        {
            get => _groups;
            set
            {
                _groups ??= [];
                _groups = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("tasks")]
        public List<ProjectTask>? Tasks
        {
            get => _tasks;
            set
            {
                _tasks ??= [];
                _tasks = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                var now = DateTime.UtcNow;
                _completedDate = value == true ? now : null;
                _isCompleted = value;
                ModifiedDate = now;
            }
        }

        [Required, JsonPropertyName("dueDate")]
        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("completedDate")]
        public DateTime? CompletedDate => _completedDate;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
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
            DateTime? dueDate,
            DateTime? completedDate,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _projectName = name;
            _description = description;
            _groups = groups;
            _tasks = tasks;
            _isCompleted = isCompleted;
            _dueDate = dueDate;
            _completedDate = completedDate;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Project(IProjectDTO dto)
        {
            _id = dto.Id;
            _projectName = dto.ProjectName;
            _description = dto.Description;
            _groups = dto.Groups?.ConvertAll(g => (GroupDTO)g).Select(g => new Group(g)).ToList();
            _tasks = dto.Tasks?.ConvertAll(t => (ProjectTaskDTO)t).Select(t => new ProjectTask(t)).ToList();
            _isCompleted = dto.IsCompleted;
            _dueDate = dto.DueDate;
            _completedDate = dto.CompletedDate;
            _createdDate = dto.CreatedDate;
            ModifiedDate = dto.ModifiedDate;
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
                        name: _projectName,
                        description: _description,
                        groups: _groups?.Select(g => g.Cast<GroupDTO>()).ToList(),
                        tasks: _tasks?.Select(t => t.Cast<ProjectTaskDTO>()).ToList(),
                        isCompleted: _isCompleted,
                        dueDate: _dueDate,
                        completedDate: _completedDate,
                        createdDate: _createdDate,
                        modifiedDate: ModifiedDate
                    );
                    return (T)obj;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Project to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.Name:{1}.IsCompleted:{2}", _id, _projectName, _isCompleted);
        #endregion
    }
}
