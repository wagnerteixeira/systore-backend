﻿
namespace Systore.Domain
{
    public class MetricsSettings
    {
        public bool UseMetrics { get; set; }
        public string InfluxDatabase { get; set; }
        public string InfluxServer { get; set; }
    }
}
