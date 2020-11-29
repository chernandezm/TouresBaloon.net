using ApplicationCore.DTO;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Interfaces
{
    public interface IOrdenPublisher
    {
        void PublicarOrden(Orden orden);

        void DistribuirOrden(OrdenDTO orden);
    }
}
