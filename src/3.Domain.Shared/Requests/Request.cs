using Domain.Shared.Responses;
using MediatR;
using System.Net;

namespace Domain.Shared.Requests
{
    public class Request<TResponse> : IRequest<TResponse> where TResponse : Response
    {
        private Guid Id { get; set; }

        private Guid UserId { get; set; }
        private Guid PersonId { get; set; }
        private Guid? TenantId { get; set; }
        private Guid? CompanyId { get; set; }
        private Guid? UnitId { get; set; }

        private IPAddress IPAddress { get; set; }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public Guid GetId()
        {
            return Id;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public Guid GetUserId()
        {
            return UserId;
        }

        public void SetPersonId(Guid personId)
        {
            PersonId = personId;
        }

        public Guid GetPersonId()
        {
            return PersonId;
        }

        public void SetTenantId(Guid? tenantId)
        {
            TenantId = tenantId;
        }

        public Guid? GetTenantId()
        {
            return TenantId;
        }

        public void SetCompanyId(Guid? companyId)
        {
            CompanyId = companyId;
        }

        public Guid? GetCompanyId()
        {
            return CompanyId;
        }

        public void SetUnitId(Guid? unitId)
        {
            UnitId = unitId;
        }

        public Guid? GetUnitId()
        {
            return UnitId;
        }

        public void SetIpAddr(IPAddress ipAddress)
        {
            IPAddress = ipAddress;
        }

        public IPAddress GetIpAddr()
        {
            return IPAddress;
        }
    }
}
