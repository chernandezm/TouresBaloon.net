using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTO
{
    public class CanastaDTO
    {
        public int Id { get; set; }
        public int Id_producto { get; set; }
        public int Id_estado { get; set; }
        public int Id_usuario { get; set; }
        public int Cantidad_canasta { get; set; }
        public float precio_canasta { get; set; }
    }
}
