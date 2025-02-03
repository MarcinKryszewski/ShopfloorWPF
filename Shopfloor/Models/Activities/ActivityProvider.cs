using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shopfloor.Models.Commons.Interfaces;

namespace Shopfloor.Models.Activities
{
    internal class ActivityProvider : IProvider<Activity, ActivityCreation>
    {
        public Task<int> Create(ActivityCreation item)
        {
            Random rnd = new();
            return Task.FromResult(rnd.Next(99, 9999));
        }
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
        public Task<IEnumerable<Activity>> GetAll()
        {
            IEnumerable<Activity> data = [
                new Activity { Id = 1, MachineId = 1, WorkshopId = 1, Description = "Daily inspection", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.D, Status = ActivityStatus.Confirmed },
                new Activity { Id = 2, MachineId = 2, WorkshopId = 1, Description = "Weekly maintenance", TypeId = 2, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 3, MachineId = 3, WorkshopId = 2, Description = "Monthly inspection", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 4, MachineId = 4, WorkshopId = 2, Description = "Annual calibration", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y, Status = ActivityStatus.Confirmed },
                new Activity { Id = 5, MachineId = 5, WorkshopId = 1, Description = "Jog test", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.D, Status = ActivityStatus.Confirmed },
                new Activity { Id = 6, MachineId = 6, WorkshopId = 1, Description = "Production monitoring", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 2, OccuranceUnit = OccuranceUnit.D, Status = ActivityStatus.Confirmed },
                new Activity { Id = 7, MachineId = 7, WorkshopId = 2, Description = "Quarterly quality check", TypeId = 2, IsLoto = true, IsJog = true, IsDurningProduction = true, OccuranceValue = 3, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 8, MachineId = 8, WorkshopId = 1, Description = "Machine clean-up", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 9, MachineId = 9, WorkshopId = 1, Description = "Safety check", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 10, MachineId = 10, WorkshopId = 2, Description = "Routine inspection", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 2, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 11, MachineId = 11, WorkshopId = 1, Description = "Production readiness", TypeId = 2, IsLoto = false, IsJog = true, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.D, Status = ActivityStatus.Confirmed },
                new Activity { Id = 12, MachineId = 12, WorkshopId = 1, Description = "Lockout Tagout verification", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 13, MachineId = 13, WorkshopId = 2, Description = "Monthly quality assurance", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 14, MachineId = 14, WorkshopId = 1, Description = "Inspection for wear and tear", TypeId = 2, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y, Status = ActivityStatus.Confirmed },
                new Activity { Id = 15, MachineId = 15, WorkshopId = 2, Description = "Emergency stop test", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 16, MachineId = 16, WorkshopId = 1, Description = "Weekly operational check", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 17, MachineId = 17, WorkshopId = 2, Description = "Calibration check", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 6, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 18, MachineId = 18, WorkshopId = 2, Description = "Jog function inspection", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 19, MachineId = 19, WorkshopId = 1, Description = "Annual safety audit", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y, Status = ActivityStatus.Confirmed },
                new Activity { Id = 20, MachineId = 20, WorkshopId = 2, Description = "Cycle test", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 21, MachineId = 3, WorkshopId = 1, Description = "Routine calibration", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 12, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 22, MachineId = 7, WorkshopId = 2, Description = "Production line audit", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
                new Activity { Id = 23, MachineId = 5, WorkshopId = 2, Description = "Operational safety", TypeId = 2, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y, Status = ActivityStatus.Confirmed },
                new Activity { Id = 24, MachineId = 4, WorkshopId = 1, Description = "Routine maintenance", TypeId = 2, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 25, MachineId = 10, WorkshopId = 2, Description = "Line calibration", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 3, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 26, MachineId = 15, WorkshopId = 1, Description = "Monthly audit", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M, Status = ActivityStatus.Confirmed },
                new Activity { Id = 27, MachineId = 6, WorkshopId = 2, Description = "Annual hazard check", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y, Status = ActivityStatus.Confirmed },
                new Activity { Id = 28, MachineId = 9, WorkshopId = 1, Description = "Weekly jog test", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W, Status = ActivityStatus.Confirmed },
            ];

            return Task.FromResult(data);
        }
        public Task<Activity?> GetById(int id)
        {
            List<Activity> data = [
                new Activity { Id = 1, MachineId = 1, WorkshopId = 1, Description = "Daily inspection", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.D },
                new Activity { Id = 2, MachineId = 2, WorkshopId = 1, Description = "Weekly maintenance", TypeId = 2, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 3, MachineId = 3, WorkshopId = 2, Description = "Monthly inspection", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 4, MachineId = 4, WorkshopId = 2, Description = "Annual calibration", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y },
                new Activity { Id = 5, MachineId = 5, WorkshopId = 1, Description = "Jog test", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.D },
                new Activity { Id = 6, MachineId = 6, WorkshopId = 1, Description = "Production monitoring", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 2, OccuranceUnit = OccuranceUnit.D },
                new Activity { Id = 7, MachineId = 7, WorkshopId = 2, Description = "Quarterly quality check", TypeId = 2, IsLoto = true, IsJog = true, IsDurningProduction = true, OccuranceValue = 3, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 8, MachineId = 8, WorkshopId = 1, Description = "Machine clean-up", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 9, MachineId = 9, WorkshopId = 1, Description = "Safety check", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 10, MachineId = 10, WorkshopId = 2, Description = "Routine inspection", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 2, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 11, MachineId = 11, WorkshopId = 1, Description = "Production readiness", TypeId = 2, IsLoto = false, IsJog = true, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.D },
                new Activity { Id = 12, MachineId = 12, WorkshopId = 1, Description = "Lockout Tagout verification", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 13, MachineId = 13, WorkshopId = 2, Description = "Monthly quality assurance", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 14, MachineId = 14, WorkshopId = 1, Description = "Inspection for wear and tear", TypeId = 2, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y },
                new Activity { Id = 15, MachineId = 15, WorkshopId = 2, Description = "Emergency stop test", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 16, MachineId = 16, WorkshopId = 1, Description = "Weekly operational check", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 17, MachineId = 17, WorkshopId = 2, Description = "Calibration check", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 6, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 18, MachineId = 18, WorkshopId = 2, Description = "Jog function inspection", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 19, MachineId = 19, WorkshopId = 1, Description = "Annual safety audit", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y },
                new Activity { Id = 20, MachineId = 20, WorkshopId = 2, Description = "Cycle test", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 21, MachineId = 3, WorkshopId = 1, Description = "Routine calibration", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 12, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 22, MachineId = 7, WorkshopId = 2, Description = "Production line audit", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
                new Activity { Id = 23, MachineId = 5, WorkshopId = 2, Description = "Operational safety", TypeId = 2, IsLoto = true, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y },
                new Activity { Id = 24, MachineId = 4, WorkshopId = 1, Description = "Routine maintenance", TypeId = 2, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 25, MachineId = 10, WorkshopId = 2, Description = "Line calibration", TypeId = 1, IsLoto = false, IsJog = false, IsDurningProduction = true, OccuranceValue = 3, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 26, MachineId = 15, WorkshopId = 1, Description = "Monthly audit", TypeId = 1, IsLoto = true, IsJog = false, IsDurningProduction = true, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.M },
                new Activity { Id = 27, MachineId = 6, WorkshopId = 2, Description = "Annual hazard check", TypeId = 2, IsLoto = false, IsJog = false, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.Y },
                new Activity { Id = 28, MachineId = 9, WorkshopId = 1, Description = "Weekly jog test", TypeId = 1, IsLoto = false, IsJog = true, IsDurningProduction = false, OccuranceValue = 1, OccuranceUnit = OccuranceUnit.W },
            ];

            Activity? result = data.Find(x => x.Id == id);

            return Task.FromResult(result);
        }
        public Task Update(Activity item)
        {
            return Task.CompletedTask;
        }
    }
}