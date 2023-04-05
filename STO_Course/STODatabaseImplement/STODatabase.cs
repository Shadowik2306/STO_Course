using Microsoft.EntityFrameworkCore;
using STODatabaseImplement.Models;
using System.Diagnostics.Metrics;

namespace STODatabaseImplement
{
	public class STODatabase : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				optionsBuilder.UseSqlServer(); //todo привязывай данные
			}
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Car> Cars { set; get; }
        public virtual DbSet<Maintenance> Maintenances { set; get; }
        public virtual DbSet<MaintenanceCars> MaintenanceCars { set; get; }

    }
}
