using System.ComponentModel;

namespace GasSolution.Web.Areas.Admin.Models.Setting
{

    public class AliyunSettingModel
    {
        [DisplayName("AccessKeyId")]
        public string AccessKeyId { get; set; }

        [DisplayName("AccessKeySecret")]
        public string AccessKeySecret { get; set; }
        [DisplayName("Endpoint")]
        public string Endpoint { get; set; }
    }
}