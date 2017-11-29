using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using GasSolution.Domain.Gas;
using GasSolution.ExportImport.Help;
using GasSolution.Gas;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace GasSolution.ExportImport
{
    /// <summary>
    /// 导入导出管理器
    /// </summary>
    public class ExportManager : GasSolutionAppServiceBase, IExportManager
    {
        #region Ctor && Field

        private readonly IGasStationService _gasRepository;
        

        /// <summary>
        /// 加油站缓存{0} 为ID
        /// </summary>
        private readonly string CACHE_STATION_ID = "gas.cache.station.id-{0}";

        public ExportManager(IGasStationService gasRepository)
        {
            this._gasRepository = gasRepository;
        }

        #endregion
        
        protected virtual void SetCaptionStyle(ExcelStyle style)
        {
            style.Fill.PatternType = ExcelFillStyle.Solid;
            style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
            style.Font.Bold = true;
        }

        /// <summary>
        /// 导出数据到XLSX
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="properties">Class access to the object through its properties</param>
        /// <param name="itemsToExport">The objects to export</param>
        /// <returns></returns>
        protected virtual byte[] ExportToXlsx<T>(PropertyByName<T>[] properties, IEnumerable<T> itemsToExport)
        {
            using (var stream = new MemoryStream())
            {
                using (var xlPackage = new ExcelPackage(stream))
                {

                    // get handles to the worksheets
                    var worksheet = xlPackage.Workbook.Worksheets.Add(typeof(T).Name);
                    var fWorksheet = xlPackage.Workbook.Worksheets.Add("DataForFilters");
                    fWorksheet.Hidden = eWorkSheetHidden.VeryHidden;

                    //create Headers and format them 
                    var manager = new PropertyManager<T>(properties.Where(p => !p.Ignore));
                    manager.WriteCaption(worksheet, SetCaptionStyle);

                    var row = 2;
                    foreach (var items in itemsToExport)
                    {
                        manager.CurrentObject = items;
                        manager.WriteToXlsx(worksheet, row++, true, fWorksheet: fWorksheet);
                    }

                    xlPackage.Save();
                }
                return stream.ToArray();
            }
        }

        #region Method

        public byte[] ExportPromotionsToXlsx(IEnumerable<GasStation> Promotions)
        {        

            var properties = new[]
           {
                new PropertyByName<GasStation>("加油站名称", p =>p.GasName),
                new PropertyByName<GasStation>("位置", p => p.Address),
                new PropertyByName<GasStation>("#92", p => p.Gasoline_Ninety_Two),
                new PropertyByName<GasStation>("#95", p => p.Gasoline_Ninety_Fine),
                new PropertyByName<GasStation>("#98", p => p.Gasoline_Ninety_Eight),
                new PropertyByName<GasStation>("活动时间", p =>{
                    if(p.FixedPromotion)
                        return "长期活动";
                    else
                        return string.Format("{0}至{1}",p.StartTime.Value.ToString("MM/dd"),p.EndTime.Value.ToString("MM/dd"));
                }),
                new PropertyByName<GasStation>("备注", p => p.Notice),
            };

            var PromotionList = Promotions.ToList();            
            
            return ExportToXlsx(properties, PromotionList);
        }

        #endregion
    }
}
