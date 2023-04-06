
using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;
using STODatabaseImplement;
using STODatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Implements
{
    public class SpareStorage : ISpareStorage
    {
        public List<SpareViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.Spares.Select(x => x.GetViewModel).ToList();
        }

        public List<SpareViewModel> GetFilteredList(SpareSearchModel model)
        {
            using var context = new STODatabase();
            return context.Spares.Select(x => x.GetViewModel).ToList();
        }

        public SpareViewModel? GetElement(SpareSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.Spares.FirstOrDefault(x => (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
        }

        public SpareViewModel? Insert(SpareBindingModel model)
        {
            using var context = new STODatabase();
            var newSpares = Spare.Create( model);
            if(newSpares == null)
            {
                return null;
            }
            context.Spares.Add(newSpares);
            context.SaveChanges();
            return newSpares.GetViewModel;
        }

        public SpareViewModel? Update(SpareBindingModel model)
        {
            using var context = new STODatabase();
            var spares = context.Spares.FirstOrDefault(x=>x.Id == model.Id);
            if(spares == null)
            {
                return null;
            }
            spares.Update(model);
            context.SaveChanges();
            return spares.GetViewModel;
        }

        public SpareViewModel? Delete(SpareBindingModel model)
        {
            using var context = new STODatabase();
            var spare = context.Spares.FirstOrDefault(x=>x.Id==model.Id);
            if(spare != null)
            {
                context.Spares.Remove(spare);
                context.SaveChanges();
                return spare.GetViewModel;
            }
            return null;
        }
    }
}
