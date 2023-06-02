using Microsoft.EntityFrameworkCore;
using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.StoragesContracts;
using STOContracts.ViewModels;
using STODatabaseImplement;
using STODatabaseImplement.Models;

namespace STODatabaseImplement.Implements
{
    public class MaintenanceStorage : IMaintenanceStorage
    {
        public List<MaintenanceViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.Maintenances.Include(x => x.Cars).ThenInclude(x => x.Car).ToList()
                    .Select(x => x.GetViewModel).ToList();
        }

        public List<MaintenanceViewModel> GetFilteredList(MaintenanceSearchModel model)
        {
            using var context = new STODatabase();
            return context.Maintenances.Include(x => x.Cars).ThenInclude(x => x.Car).ToList().Select(x => x.GetViewModel).ToList();
        }

        public MaintenanceViewModel? GetElement(MaintenanceSearchModel model)
        {
            if ( !model.Id.HasValue)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.Maintenances.Include(x => x.Cars).ThenInclude(x => x.Car)
                .FirstOrDefault(x =>                
                (model.Id.HasValue && x.Id == model.Id))
                    ?.GetViewModel;
        }

        public MaintenanceViewModel? Insert(MaintenanceBindingModel model)
        {
            using var context = new STODatabase();
            var newMaintenance = Maintenance.Create(context, model);
            if (newMaintenance == null)
            {
                return null;
            }
            context.Maintenances.Add(newMaintenance);
            context.SaveChanges();
            return newMaintenance.GetViewModel;
        }

        public MaintenanceViewModel? Update(MaintenanceBindingModel model)
        {
            using var context = new STODatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var Maintenance = context.Maintenances.FirstOrDefault(rec => rec.Id == model.Id);
                if (Maintenance == null)
                {
                    return null;
                }
                Maintenance.Update(model);
                context.SaveChanges();
                Maintenance.UpdateComponents(context, model);
                transaction.Commit();
                return Maintenance.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public MaintenanceViewModel? Delete(MaintenanceBindingModel model)
        {
            using var context = new STODatabase();
            var element = context.Maintenances.Include(x => x.Cars).FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Maintenances.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public List<CarViewModel>? GetCars(MaintenanceSearchModel model)
        {
            using var context = new STODatabase();
            return context.MaintenanceCars.Where(x => x.MaintenanceId == model.Id)
                .Select(x => context.Cars.Where(y => y.Id == x.CarId)
                .Select(y => y.GetViewModel).First()).ToList();
        }
    }
}
