using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICanastaPublisher
    {
        void PublicarCanasta(Canasta canasta);

        void DistribuirCanasta(Canasta canasta);
    }
}
