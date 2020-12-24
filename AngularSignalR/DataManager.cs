using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSignalR
{
    public static class DataManager
    {
        public static List<ChartModel> GetData()
        {
            var r = new Random();
            return new List<ChartModel>()
                        {
                           new ChartModel { Data = new List<int> { r.Next(1, 40),r.Next(1,40) }, Label = "Data1" },
                           new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data2" },
                           new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data3" },
                           new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data4" }
                        };
        }
    }

    public class ChartModel
    {
        public List<int> Data { get; set; }
        public string Label { get; set; }
        public ChartModel()
        {
            Data = new List<int>();
        }
    }

    public class TableData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
