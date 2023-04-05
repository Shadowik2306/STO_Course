using STOContracts.BindingModels;
using STOContracts.ViewModels;
using STODataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STODatabaseImplement.Models
{
    public class WorkDuration : IWorkDurationModel
    {
        public int Id {get; set;}

        [Required]
        public int Duration { get; set; }

        public static WorkDuration Create(WorkDurationBindingModel model)
        {
            return new WorkDuration()
            {
                Id = model.Id,
                Duration = model.Duration,
            };
        }
        public void Update(WorkDurationBindingModel model)
        {
            Duration = model.Duration;
        }
        public WorkDurationViewModel GetViewModel => new()
        {
            Id = Id,
            Duration = Duration,
        };
    }
}
