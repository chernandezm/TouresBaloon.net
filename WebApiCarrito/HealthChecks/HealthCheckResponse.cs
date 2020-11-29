using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCarrito.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }

        public IEnumerable<CheckInfo> Checks { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
