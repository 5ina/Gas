using Abp.Domain.Repositories;
using System;
using Abp.Application.Services.Dto;
using System.Linq;
using System.Threading.Tasks;
using GasSolution.Domain.Customers;
using Abp.Runtime.Caching;
using GasSolution.Security;
using GasSolution.Authentication.Dto;

namespace GasSolution.Customers
{

    public class CustomerService : GasSolutionAppServiceBase, ICustomerService
    {
        #region Ctor && Field

        /// <summary>
        /// 用户缓存key，{0}为用户ID
        /// </summary>
        private const string CUSTOMER_BY_ID = "gas.cache.customer.by-id.{0}";
        private const string CUSTOMER_BY_OPENID = "gas.cache.customer.by-openid.{0}";


        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IEncryptionService _encryptionService;
        
        private readonly IRepository<CustomerAttribute> _attributeRepository;
        public CustomerService(
            IRepository<Customer> customerRepository,
            ICacheManager cacheManager,
            IEncryptionService encryptionService,
            IRepository<CustomerAttribute> attributeRepository)
        {
            this._customerRepository = customerRepository;
            this._cacheManager = cacheManager;
            this._encryptionService = encryptionService;
            this._attributeRepository = attributeRepository;
        }

        #endregion

        #region Method        
        public int CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            return _customerRepository.InsertAndGetId(customer);

        }

        public void DeleteCustomer(int customerId)
        {
            _customerRepository.Delete(customerId);
        }

        public IPagedResult<Customer> GetAllCustomers(DateTime? createdFrom = null,
            DateTime? createdTo = null,
            bool? isSub = null,
            CustomerRole? roleId = null,
            string keywords = null,
            bool showHidden = false,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerRepository.GetAll();

            if (createdFrom.HasValue)
                query = query.Where(c => createdFrom.Value <= c.CreationTime);
            if (createdTo.HasValue)
                query = query.Where(c => createdTo.Value >= c.CreationTime);
            
            if (isSub.HasValue)
                query = query.Where(c => c.IsSubscribe == isSub.Value);

            if (!String.IsNullOrWhiteSpace(keywords))
                query = query.Where(c => c.NickName.Contains(keywords) || c.Mobile.Contains(keywords));

            if (roleId.HasValue)
                query = query.Where(c => c.CustomerRoleId == (int)roleId.Value);
            
            query = query.OrderByDescending(c => c.CreationTime);

            return new PagedResult<Customer>(query, pageIndex, pageSize);
        }

        public Customer GetCustomerByMobile(string mobile)
        {
            return _customerRepository.GetAllList(x => x.Mobile == mobile).FirstOrDefault();
        }

        public async Task<Customer> GetCustomerByMobileAsync(string mobile)
        {
            return await _customerRepository.FirstOrDefaultAsync(c => c.Mobile == mobile);
        }
        public Customer GetCustomerId(int customerId)
        {
            if (customerId > 0)
            {
                var key = string.Format(CUSTOMER_BY_ID, customerId);
                return _cacheManager.GetCache(key).Get(customerId.ToString(), () =>
                {
                    return _customerRepository.Get(customerId);
                });
            }
            else
                return null;
        }

        public async Task<Customer> GetCustomerIdAsync(int customerId)
        {
            var key = string.Format(CUSTOMER_BY_ID, customerId);
            return await _cacheManager.GetCache(key).GetAsync(customerId.ToString(), () =>
            {
                return _customerRepository.GetAsync(customerId);
            });
        }


        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            _customerRepository.Update(customer);
            if (customer.Id > 0)
                _cacheManager.GetCache(string.Format(CUSTOMER_BY_ID, customer.Id)).Remove(customer.Id.ToString());
            if (!String.IsNullOrWhiteSpace(customer.OpenId))
                _cacheManager.GetCache(string.Format(CUSTOMER_BY_OPENID, customer.OpenId)).Remove(customer.OpenId);
        }

        public async Task UpdateAsyncCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            await _customerRepository.UpdateAsync(customer);
            await _cacheManager.GetCache(string.Format(CUSTOMER_BY_ID, customer.Id)).RemoveAsync(customer.Id.ToString());
            await _cacheManager.GetCache(string.Format(CUSTOMER_BY_OPENID, customer.OpenId)).RemoveAsync(customer.OpenId);
        }

        public CustomerLoginResults ValidateCustomer(string loginName, string password, CustomerRole? role = null)
        {
            var result = new CustomerLoginResults();
            result.Result = LoginResults.WrongPassword;
            var customer = GetCustomerByMobile(loginName);
            if (customer == null)
                return new CustomerLoginResults(LoginResults.NotRegistered);
            var psd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt);
            bool isValid = psd == customer.Password;
            if (isValid)
                result.Result = LoginResults.Successful;
            if (role.HasValue)
            {
                if (role.Value != (CustomerRole)customer.CustomerRoleId)
                    result.Result = LoginResults.Unauthorized;
            }

            customer.LastModificationTime = DateTime.Now;
            //_customerService.UpdateCustomer(customer);
            result.Customer = customer;
            return result;
        }

        public async Task<CustomerLoginResults> ValidateAsyncCustomer(string loginName, string password, CustomerRole? role = null)
        {
            var result = new CustomerLoginResults();
            var customer = await GetCustomerByMobileAsync(loginName);
            if (customer == null)
                return new CustomerLoginResults(LoginResults.NotRegistered);
            var psd = _encryptionService.CreatePasswordHash(password, customer.PasswordSalt);
            bool isValid = psd == customer.Password;
            if (!isValid)
                result.Result = LoginResults.WrongPassword;
            if (role.HasValue)
            {
                if (role.Value != (CustomerRole)customer.CustomerRoleId)
                    result.Result = LoginResults.Unauthorized;
            }

            customer.LastModificationTime = DateTime.Now;
            result.Result = LoginResults.Successful;
            result.Customer = customer;
            return result;
        }

        public Customer GetCustomerByOpenId(string openId)
        {
            return _customerRepository.FirstOrDefault(c => c.OpenId == openId);
           
        }

        public bool HasCustomer(string openId, out Customer customer)
        {
            customer = _customerRepository.FirstOrDefault(e => e.OpenId == openId);
            if (customer == null)
                return false;
            if (customer.Id == 0)
                return false;
            return true;
        }

        public int GetRegisteredCustomersReport(int days, bool isSubscrbe = true)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var query = from c in _customerRepository.GetAll()
                        where
                        c.Id == (int)CustomerRole.Member &&
                        c.CreationTime >= date &&
                        (isSubscrbe && c.IsSubscribe)
                        select c;
            int count = query.Count();
            return count;
        }

        #endregion
    }
}
