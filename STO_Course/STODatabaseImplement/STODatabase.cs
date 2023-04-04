using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
