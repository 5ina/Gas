using System.Data.Common;
using Abp.EntityFramework;
using GasSolution.Domain.Customers;
using GasSolution.Domain.Settings;
using System.Data.Entity;
using GasSolution.Domain.Gas;
using GasSolution.Domain.Common;
using GasSolution.Domain.Vehicles;

namespace GasSolution.EntityFramework
{
    public class GasSolutionDbContext : AbpDbContext
    {
        //TODO: Define an IDbSet for each Entity...

        //Example:
        //public virtual IDbSet<User> Users { get; set; }

        //用户
        public virtual IDbSet<Customer> Customer { get; set; }
        public virtual IDbSet<CustomerAttribute> CustomerAttribute { get; set; }

        //配置
        public virtual IDbSet<Setting> Setting { get; set; }

        /// <summary>
        /// 加油站信息
        /// </summary>
        public virtual IDbSet<GasStation> GasStation { get; set; }
        public virtual IDbSet<Promotion> Promotion { get; set; }
        public virtual IDbSet<GasStationCustom> GasStationCustom { get; set; }

        /// <summary>
        /// 行政规划表
        /// </summary>
        public virtual IDbSet<Area> Area { get; set; }
        public virtual IDbSet<KeyFont> KeyFont { get; set; }

        //Vehicle
        public virtual IDbSet<Vehicle> Vehicle { get; set; }
        public virtual IDbSet<CarBrand> CarBrand { get; set; }

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public GasSolutionDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in GasSolutionDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of GasSolutionDbContext since ABP automatically handles it.
         */
        public GasSolutionDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public GasSolutionDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public GasSolutionDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
