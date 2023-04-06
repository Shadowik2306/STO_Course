
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
    public class WorkDurationStorage : IWorkDurationStorage
    {
        public List<WorkDurationViewModel> GetFullList()
        {
            using var context = new STODatabase();
            return context.WorkDurations.Select(x => x.GetViewModel).ToList();
        }

        public List<WorkDurationViewModel> GetFilteredList(WorkDurationSearchModel model)
        {
            using var context = new STODatabase();
            return context.WorkDurations.Select(x => x.GetViewModel).ToList();
        }

        public WorkDurationViewModel? GetElement(WorkDurationSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            using var context = new STODatabase();
            return context.WorkDurations.FirstOrDefault(x => (model.Id.HasValue && x.Id == model.Id))?.GetViewModel;
        }

        public WorkDurationViewModel? Insert(WorkDurationBindingModel model)
        {
            using var context = new STODatabase();
            var newWorkDuration = WorkDuration.Create( model);
            if(newWorkDuration == null)
            {
                return null;
            }
            context.WorkDurations.Add(newWorkDuration);
            context.SaveChanges();
            return newWorkDuration.GetViewModel;
        }

        public WorkDurationViewModel? Update(WorkDurationBindingModel model)
        {
            using var context = new STODatabase();
            var workDuration = context.WorkDurations.FirstOrDefault(x=>x.Id == model.Id);
            if(workDuration == null)
            {
                return null;
            }
            workDuration.Update(model);
            context.SaveChanges();
            return workDuration.GetViewModel;
        }

        public WorkDurationViewModel? Delete(WorkDurationBindingModel model)
        {
            using var context = new STODatabase();
            var workDuration = context.WorkDurations.FirstOrDefault(x=>x.Id==model.Id);
            if(workDuration != null)
            {
                context.WorkDurations.Remove(workDuration);
                context.SaveChanges();
                return workDuration.GetViewModel;
            }
            return null;
        }
    }
}
