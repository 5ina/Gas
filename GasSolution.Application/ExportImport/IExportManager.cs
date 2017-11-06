using Abp.Application.Services;
using GasSolution.Domain.Gas;
using System.Collections.Generic;

namespace GasSolution.ExportImport
{
    /// <summary>
    /// 导入导出管理器
    /// </summary>
    public interface IExportManager: IApplicationService
    {
        /// <summary>
        /// 导出促销信息到Excel
        /// </summary>
        /// <param name="products">Products</param>
        byte[] ExportPromotionsToXlsx(IEnumerable<Promotion> products);
        
    }
}
