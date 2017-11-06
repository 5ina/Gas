namespace GasSolution.Domain.Customers
{
    /// <summary>
    /// 用户属性名称
    /// </summary>
    public static class CustomerAttributeNames
    {
        #region 收货地址
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public static string Consignee { get { return "consignee"; } }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public static string TelNumber { get { return "telNumber"; } }

        /// <summary>
        /// 收货省份
        /// </summary>
        public static string ProvinceName { get { return "provinceName"; } }
        /// <summary>
        /// 收货城市
        /// </summary>
        public static string CityName { get { return "cityName"; } }

        /// <summary>
        /// 收货区县
        /// </summary>
        public static string CountryName { get { return "countryName"; } }

        /// <summary>
        /// 收货详细地址
        /// </summary>
        public static string DetailInfo { get { return "detailInfo"; } }
        #endregion

        #region 基本信息
        /// <summary>
        /// 性别
        /// </summary>
        public static string Sex { get { return "sex"; } }

        /// <summary>
        /// 头像
        /// </summary>
        public static string Avatar { get { return "avatar"; } }
        /// <summary>
        /// 积分
        /// </summary>
        public static string Reward { get { return "reward"; } }
        
        #endregion
    }
}
