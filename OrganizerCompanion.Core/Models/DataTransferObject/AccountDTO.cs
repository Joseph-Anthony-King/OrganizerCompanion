using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class AccountDTO : IAccountDTO
    {
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IFeatureDTO> IAccountDTO.Features
        {
            get => Features.ConvertAll(feature => (IFeatureDTO)feature);
            set
            {
                Features = value.ConvertAll(feature => (FeatureDTO)feature);
            }
        }

        [JsonIgnore]
        List<ISubAccountDTO>? IAccountDTO.Accounts
        {
            get => Accounts?.ConvertAll(account => (ISubAccountDTO)account);
            set
            {
                if (value == null)
                {
                    Accounts = null;
                    return;
                }

                Accounts ??= [];
                Accounts.Clear();

                foreach (var account in value)
                {
                    Accounts.Add((SubAccountDTO)account);
                }
            }
        }

        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("accountName")]
        public string? AccountName { get; set; } = null;

        [Required, JsonPropertyName("accountNumber")]
        public string? AccountNumber { get; set; } = null;

        [Required, JsonPropertyName("license"), GuidValidator]
        public string? License { get; set; } = null;

        [Required, JsonPropertyName("features")]
        public List<FeatureDTO> Features { get; set; } = [];

        [JsonPropertyName("accounts"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SubAccountDTO>? Accounts { get; set; } = null;

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        #endregion
  }
}
