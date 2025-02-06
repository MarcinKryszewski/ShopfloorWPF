using System;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Activities
{
    internal class ActivityMapper : IModelMapper<Activity, ActivityCreation>
    {
        public static ActivityCreation ToCreationModel(Activity model)
        {
            return new ActivityCreation()
            {
                Id = model.Id,
                MachineId = model.MachineId,
                Machine = model.Machine,
                WorkshopId = model.WorkshopId,
                Workshop = model.Workshop,
                Status = model.Status,
                Description = model.Description,
                TypeId = model.TypeId,
                Type = model.Type,
                IsLoto = model.IsLoto,
                IsJog = model.IsJog,
                IsDurningProduction = model.IsDurningProduction,
                OccuranceValue = model.OccuranceValue,
                Occurance = model.Occurance,
            };
        }

        public static Activity ToModel(int id, ActivityCreation creationModel)
        {
            return new Activity()
            {
                Id = id,
                MachineId = creationModel.MachineId,
                Machine = creationModel.Machine,
                WorkshopId = creationModel.WorkshopId,
                Workshop = creationModel.Workshop,
                Status = creationModel.Status,
                Description = creationModel.Description,
                TypeId = creationModel.TypeId,
                Type = creationModel.Type,
                IsLoto = creationModel.IsLoto,
                IsJog = creationModel.IsJog,
                IsDurningProduction = creationModel.IsDurningProduction,
                OccuranceValue = creationModel.OccuranceValue,
                Occurance = creationModel.Occurance,
            };
        }
    }
}