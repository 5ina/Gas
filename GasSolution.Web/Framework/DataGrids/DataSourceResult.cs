using System.Collections;

namespace GasSolution.Web.Framework.DataGrids
{
    public class DataSourceResult
    {
        public object ExtraData { get; set; }

        public IEnumerable Data { get; set; }

        public object Errors { get; set; }

        public int Total { get; set; }

        public int NextPage { get; set; }
    }
}