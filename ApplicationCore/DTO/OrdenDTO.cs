using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO
{
    public class OrdenDTO
    {
        public int id_orden { get; set; }
        public int id_usuario { get; set; }
        public int id_estado { get; set; }
        public DateTime fecha_orden { get; set; }
        public int id_canasta { get; set; }
    }
}
