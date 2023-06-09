﻿using STOContracts.BindingModels;
using STOContracts.SearchModels;
using STOContracts.ViewModels;

namespace STOContracts.BusinessLogicsContracts
{
    public interface IMaintenanceLogic
    {
        List<MaintenanceViewModel>? ReadList(MaintenanceSearchModel? model);
        MaintenanceViewModel? ReadElement(MaintenanceSearchModel? model);
        bool Create(MaintenanceBindingModel model);
        bool Update(MaintenanceBindingModel model);
        bool Delete(MaintenanceBindingModel model);
    }
}
