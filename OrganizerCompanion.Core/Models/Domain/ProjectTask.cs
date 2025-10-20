using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class ProjectTask : IProjectTask
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string _name = string.Empty;
        private string? _description = null;
        private List<ProjectAssignment>? _assignments = null;
        private bool _isCompleted = false;
        private DateTime? _dateDue = null;
        private readonly DateTime? _dateCompleted = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit ITaskDTO Implementation
        List<IProjectAssignment>? IProjectTask.Assignments
        {
            get => _assignments?.Select(a => a.Cast<IProjectAssignment>()).ToList();
            set => _assignments = value?.Select(a => (ProjectAssignment)a).ToList();
        }
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        [Required, JsonPropertyName("assignments")]
        public List<ProjectAssignment>? Assignments
        {
            get => _assignments;
            set
            {
                _assignments ??= [];
                _assignments = value;
                DateModified = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isCompleted")]
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                DateModified = DateTime.UtcNow;
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
        public ProjectTask() { }

        [JsonConstructor]
        public ProjectTask(
            int id,
            string name,
            string? description,
            List<ProjectAssignment>? assignments,
            bool isCompleted,
            DateTime? dateDue,
            DateTime? dateCompleted,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _name = name;
            _description = description;
            _assignments = assignments;
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
                if (typeof(T) == typeof(ProjectTaskDTO) || typeof(T) == typeof(IProjectTaskDTO))
                {
                    // ProjectTaskDTO is currently a stub implementation that throws NotImplementedException
                    // for all properties and methods, so we just return a new instance
                    var dto = new ProjectTaskDTO();
                    return (T)(object)dto;
                }
                else throw new InvalidCastException($"Cannot cast Feature to type {typeof(T).Name}.");
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
