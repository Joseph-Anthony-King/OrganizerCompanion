using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Password : IPassword
    {
        #region Fields
        private int _id = 0;
        private string? _passwordValue = null;
        private string? _passwordHint = null;
        private readonly List<string> _previousPasswords = [];
        private DateTime? _expirationDate = null;
        private int _accountId = 0;
        private IAccount? _account = null;
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        [Key]
        [Column("PasswordId")]
        [JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
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
                ModifiedDate = DateTime.Now; 
            } 
        }

        [JsonPropertyName("passwordValue"), MaxLength(256, ErrorMessage = "Password cannot exceed 256 characters.")]
        public string? PasswordValue 
        { 
            get
            {
                if (DateTime.Now <=_expirationDate)
                {
                    if (_previousPasswords.Count == 5)
                    {
                        _previousPasswords.RemoveAt(0);
                    }
                    _previousPasswords.Add(_passwordValue!);
                    _passwordValue = null;
                    ModifiedDate = DateTime.Now;
                    throw new InvalidOperationException("Cannot set a new password after the expiration date.");
                }

                return _passwordValue;
            }
            set
            {
                if (value != null && value.Length > 256)
                {
                    throw new ArgumentException("Password cannot exceed 256 characters.", nameof(PasswordValue));
                }
                if (_previousPasswords.Contains(value!))
                {
                    throw new ArgumentException("New password cannot be the same as any previous 5 passwords.");
                }
                if (_previousPasswords.Count == 5)
                {
                    _previousPasswords.RemoveAt(0);
                }
                if (_passwordValue != null)
                {
                    _previousPasswords.Add(_passwordValue!);
                }

                _passwordValue = value;
                _expirationDate = DateTime.Now.AddMonths(3);
                ModifiedDate = DateTime.Now; 
            } 
        }

        [JsonPropertyName("passwordHint"), MaxLength(256, ErrorMessage = "Password Hint cannot exceed 256 characters.")]
        public string? PasswordHint 
        { 
            get => _passwordHint; 
            set
            {
                if (value != null && value.Length > 256)
                {
                    throw new ArgumentException("Password Hint cannot exceed 256 characters.", nameof(PasswordHint));
                }
                _passwordHint = value; 
                ModifiedDate = DateTime.Now; 
            }
        }

        [NotMapped]
        [JsonPropertyName("previousPasswords")]
        public List<string> PreviousPasswords => _previousPasswords!;

        [NotMapped]
        [JsonPropertyName("expirationDate")]
        public DateTime? ExpirationDate => _expirationDate;

        [JsonPropertyName("accountId"), Range(0, int.MaxValue, ErrorMessage = "Account Id must be a non-negative number.")]
        public int AccountId 
        { 
            get => _accountId; 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(AccountId), "Account Id must be a non-negative number.");
                }

                _accountId = value; 
                ModifiedDate = DateTime.Now; 
            } 
        }

        [ForeignKey(nameof(AccountId))]
        [JsonPropertyName("account")]
        public IAccount? Account 
        { 
            get => _account; 
            set 
            { 
                _account = value; 
                ModifiedDate = DateTime.Now; 
            }
        }

        [JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public Password() { }

        [JsonConstructor]
        public Password(
            int id,
            string? passwordValue,
            string? passwordHint,
            List<string>? previousPasswords,
            int accountId,
            IAccount? account,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _passwordValue = passwordValue;
            _passwordHint = passwordHint;
            _previousPasswords = previousPasswords ?? [];
            _accountId = accountId;
            _account = account;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Password(
            string? passwordValue,
            string? passwordHint,
            IAccount account)
        {
            _passwordValue = passwordValue;
            _passwordHint = passwordHint;
            if (account != null)
            {
                _accountId = account.Id;
            }
            _account = account;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => throw new NotImplementedException();
        #endregion
    }
}
