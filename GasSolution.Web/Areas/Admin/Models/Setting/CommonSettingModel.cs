using System.ComponentModel;

namespace GasSolution.Web.Areas.Admin.Models.Setting
{

    /// <summary>
    /// 公共配置
    /// </summary>
    public class CommonSettingModel
    {
        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("关键字")]
        public string Keywords { get; set; }
        [DisplayName("说明")]
        public string Description { get; set; }

        [DisplayName("92#市场价")]
        public decimal No_Ninety_Two { get; set; }
        [DisplayName("95#市场价")]
        public decimal No_Ninety_Fine { get; set; }
        [DisplayName("98#市场价")]
        public decimal No_Ninety_Eight { get; set; }


        [DisplayName("联系们回复")]
        public string CONTACT_US { get; set; }

        [DisplayName("首次关注回复")]
        public string First_Subscribe { get; set; }

        [DisplayName("关键字未匹配回复")]
        public string Keywords_NoMatch { get; set; }
    }
}