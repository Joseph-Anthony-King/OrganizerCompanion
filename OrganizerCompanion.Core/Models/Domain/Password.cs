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
        private int _accountId = 0;
        private IAccount? _account = null;
        private DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        public int Id 
        { 
            get => _id; 
            set 
            { 
                _id = value; 
                DateModified = DateTime.Now; 
            } 
        }

        public string? PasswordValue 
        { 
            get => _passwordValue; 
            set 
            { 
                _passwordValue = value; 
                DateModified = DateTime.Now; 
            } 
        }

        public string? PasswordHint 
        { 
            get => _passwordHint; 
            set 
            { 
                _passwordHint = value; 
                DateModified = DateTime.Now; 
            } 
        }

        public int AccountId 
        { 
            get => _accountId; 
            set 
            { 
                _accountId = value; 
                DateModified = DateTime.Now; 
            } 
        }

        public IAccount? Account 
        { 
            get => _account; 
            set 
            { 
                _account = value; 
                DateModified = DateTime.Now; 
            } 
        }

        public DateTime DateCreated { get => _dateCreated; }

        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public Password() { }

        [JsonConstructor]
        public Password(
            int id,
            string? passwordValue,
            string? passwordHint,
            int accountId,
            IAccount? account,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _passwordValue = passwordValue;
            _passwordHint = passwordHint;
            _accountId = accountId;
            _account = account;
            _dateCreated = dateCreated;
            DateModified = dateModified;
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
            _dateCreated = DateTime.Now;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();

        public string ToJson() => throw new NotImplementedException();
        #endregion
    }
}
