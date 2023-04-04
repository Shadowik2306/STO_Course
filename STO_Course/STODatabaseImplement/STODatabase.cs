using FactoryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryDatabaseImplement
{
	public class FactoryDatabase : DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				optionsBuilder.UseSqlServer(); //todo привязывай данные
			}
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Master> Masters { set; get; }
        public virtual DbSet<Engenier> Engeniers { set; get; }
        public virtual DbSet<Reinforced> Reinforceds { set; get; }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<ReinforcedComponent> ReinforcedComponents { set; get; }
        public virtual DbSet<Lathe> Lathes { set; get; }
        public virtual DbSet<LatheBusy> LatheBusies { set; get; }
        public virtual DbSet<LatheReinforced> LatheReinforcedes { set; get; }
        public virtual DbSet<Plan> Plans { set; get; }
        public virtual DbSet<Stage> Stages { set; get; }
        public virtual DbSet<PlanLathe> PlanLathes { set; get; }
        public virtual DbSet<PlanComponents> PlanComponents { set; get; }

    }
}
