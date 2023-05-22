using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    public class Work : IWorkModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = String.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public int StorekeeperId { get; set; }
        [Required]
        public int DurationId { get; set; }

        public virtual WorkDuration Duration { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        private Dictionary<int, (ISpareModel, int)>? _workSpares { get; set; } = null;
        [NotMapped]
        public Dictionary<int, (ISpareModel, int)> WorkSpares {
            get
            {
                if (_workSpares == null)
                {
                    _workSpares = Spares.ToDictionary(recWS => recWS.SpareId, recWS =>
                    (recWS.Spare as ISpareModel, recWS.Count));
                }
                return _workSpares;
            }
        }

        [NotMapped]
        public virtual List<WorkSpare> Spares { get; set; } = new();

        private Dictionary<int, (IMaintenanceModel, int)>? _workMaintenances = null;

        [NotMapped]
        public Dictionary<int, (IMaintenanceModel, int)> WorkMaintenances
        {
            get
            {
                if (_workMaintenances == null)
                {
                    _workMaintenances = Maintenances.ToDictionary(recWM => recWM.MaintenanceId, recWM =>
                    (recWM.Maintenance as IMaintenanceModel, recWM.Count));
                }
                return _workMaintenances;
            }
        }
        [ForeignKey("WorkId")]
        public virtual List<WorkMaintenance> Maintenances { get; set; } = new();

        public static Work Create(STODatabase context, WorkBindingModel model)
        {
            return new Work()
            {
                Id = model.Id,
                Title = model.Title,
                Price = model.Price,
                StorekeeperId= model.StorekeeperId,
                DurationId = model.DurationId,
                Duration = context.WorkDurations.First(x => x.Id == model.DurationId),
                Spares = model.WorkSpares.Select(x => new WorkSpare {
                    Spare = context.Spares.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList(),
                Maintenances = model.WorkMaintenances.Select(x => new WorkMaintenance
                {
                    Maintenance = context.Maintenances.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList(),
                Date = model.Date
            };
        }

        public void Update(WorkBindingModel model)
        {
            Title = model.Title;
            Price = model.Price;
        }

        public WorkViewModel GetViewModel => new()
        {
            Id = Id,
            Title = Title,
            Price = Price,
            StorekeeperId = StorekeeperId,
            Duration = Duration.Duration,
            DurationId = DurationId,
            WorkMaintenances = WorkMaintenances,
            WorkSpares = WorkSpares,
            Date = Date
        };

        

        public void UpdateSpares(STODatabase context, WorkBindingModel model)
        {
            var WorkSpares = context.WorkSpares.Where(rec => rec.WorkId == model.Id).ToList();
            if (WorkSpares != null && WorkSpares.Count > 0)
            {
                context.WorkSpares.RemoveRange(WorkSpares.Where(rec => !model.WorkSpares.ContainsKey(rec.SpareId)));
                context.SaveChanges();
                foreach (var updateComponent in WorkSpares)
                {
                    updateComponent.Count = model.WorkSpares[updateComponent.SpareId].Item2;
                    model.WorkSpares.Remove(updateComponent.SpareId);
                }
                context.SaveChanges();
            }
            var Work = context.Works.First(x => x.Id == Id);
            foreach (var pc in model.WorkSpares)
            {
                context.WorkSpares.Add(new WorkSpare
                {
                    Work = Work,
                    Spare = context.Spares.First(x => x.Id == pc.Key),
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            _workSpares = null;
        }

        public void UpdateMaintenances(STODatabase context, WorkBindingModel model)
        {
            var WorkMaintenance = context.WorkMaintenances.Where(rec => rec.WorkId == model.Id).ToList();
            if (WorkMaintenance != null && WorkMaintenance.Count > 0)
            {
                context.WorkMaintenances.RemoveRange(WorkMaintenance.Where(rec => !model.WorkMaintenances.ContainsKey(rec.MaintenanceId)));
                context.SaveChanges();
                foreach (var updateComponent in WorkMaintenance)
                {
                    updateComponent.Count = model.WorkMaintenances[updateComponent.MaintenanceId].Item2;
                    model.WorkMaintenances.Remove(updateComponent.MaintenanceId);
                }
                context.SaveChanges();
            }
            var Work = context.Works.First(x => x.Id == Id);
            foreach (var pc in model.WorkMaintenances)
            {
                context.WorkMaintenances.Add(new WorkMaintenance
                {
                    Work = Work,
                    Maintenance = context.Maintenances.First(x => x.Id == pc.Key),
                    Count = pc.Value.Item2
                });
                context.SaveChanges();
            }
            _workMaintenances = null;
        }
    }
}
