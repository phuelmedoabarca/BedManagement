﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface ISalaRepository
    {
        Task<List<Sala>> GetListByUnidad(int idUnidad);
    }
}
