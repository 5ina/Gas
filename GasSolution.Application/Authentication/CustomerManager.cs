using Abp.Application.Services;
using GasSolution.Authentication.Dto;
using Microsoft.AspNet.Identity;

namespace GasSolution.Authentication
{

    public class CustomerManager : UserManager<CustomerDto, int>, IApplicationService
    {
        public CustomerManager(CustomerStore store) : base(store)
        {
        }
    }
}
