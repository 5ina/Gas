using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GasSolution.Web.Areas.Admin.Models.Setting
{
    public class AllSettingsListModel 
    {
        [DisplayName("配置名称")]
        public string SearchSettingName { get; set; }

        [DisplayName("配置值")]
        public string SearchSettingValue { get; set; }
    }
}