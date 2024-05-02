﻿using Domain.Entities;
using Domain.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura
{
    public class IngresoRepository : IIngresoRepository
    {
        private readonly DataBaseContext _dataBase;
        public IngresoRepository(DataBaseContext dataBase)
        {
            _dataBase = dataBase;
        }
        public async Task AddIngresoAsync(Ingreso ingreso)
        {
            await _dataBase.Set<Ingreso>().AddAsync(ingreso);
            await _dataBase.SaveChangesAsync();
        }

        public async Task<Ingreso?> GetByIdIngreso(Guid id)
        {
            var ingreso = await _dataBase.Set<Ingreso>()
                            .SingleOrDefaultAsync(i => i.IdIngreso == id);
            return ingreso;
        }

        public async Task<List<Ingreso>> GetListIngreso()
        {
            return await _dataBase.Ingreso.ToListAsync();
        }

        public async Task SetIngresoAsync(Ingreso ingreso)
        {
            _dataBase.Set<Ingreso>().Update(ingreso);
            await _dataBase.SaveChangesAsync();
        }
    }
}