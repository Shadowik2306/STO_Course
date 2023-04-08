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

        public List<CarViewModel> GetMaintenaceCars(MaintenanceSearchModel model)
        {
            if (model == null)
            {
                return new();
            }
            using var context = new STODatabase();
            var cars = context.MaintenanceCars
                .Where(x => x.MaintenanceId == model.Id)
                .Select(x => x.Car.GetViewModel)
                .ToList();
            return cars;
        }

        public List<SpareViewModel> GetCarsSpares(MaintenanceSearchModel model1, CarSearchModel model2)
        {
            if (model1 == null || model2 == null)
            {
                return new();
            }
            using var context = new STODatabase();

            var cars = context.MaintenanceCars.Where(x => x.MaintenanceId == model1.Id).Select(x=>x.Car).ToList();
            var spare = context.CarSpares.Where(x => cars.Contains(x.Car)).Select(x => x.Spare.GetViewModel).ToList();

            return spare;
        }
    }
}
