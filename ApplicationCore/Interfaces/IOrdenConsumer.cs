﻿using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrdenConsumer
    {
        Canasta ProcesarOrden(Orden orden);
    }
}
