using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCMedico.Models;

namespace MVCMedico.Context
{
    public class MedicoDatabaseContext : DbContext
    {
        public MedicoDatabaseContext(DbContextOptions<MedicoDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<PrestadorMedico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Turno> Turnos { get; set; }
    }
}

