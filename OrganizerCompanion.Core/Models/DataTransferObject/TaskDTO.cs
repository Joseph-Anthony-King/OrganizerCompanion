using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class TaskDTO : ITaskDTO
    {
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IAssignment>? Assignments { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsCompleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? DateDue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime? DateCompleted => throw new NotImplementedException();

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime DateCreated => throw new NotImplementedException();

        public DateTime? DateModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }
}
