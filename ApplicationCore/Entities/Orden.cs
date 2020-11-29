using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Orden
    {
        [Key]
        public int id_orden { get; set; }
        public int id_usuario { get; set; }
        public int id_estado { get; set; }
        public DateTime fecha_orden { get; set; }
        public int id_canasta { get; set; }
    }
}
