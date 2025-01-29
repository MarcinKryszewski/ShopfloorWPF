using Microsoft.Extensions.DependencyInjection;
using Shopfloor.Models.Activities;
using Shopfloor.Models.ActivitiesInstructions;
using Shopfloor.Models.ActivityTypes;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Instructions;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Machines;
using Shopfloor.Models.MachinesResponsibles;
using Shopfloor.Models.Persons;
using Shopfloor.Models.Trainings;
using Shopfloor.Models.Workshops;

namespace Shopfloor.Hosts.Features.Services
{
    internal static class ModelServices
    {
        public static void Activities(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Activity, ActivityCreation>, ActivityRepository>();
            services.AddSingleton<IStore<Activity>, ActivityStore>();
            services.AddSingleton<IProvider<Activity, ActivityCreation>, ActivityProvider>();
        }
        public static void ActivitiesInstructions(IServiceCollection services)
        {
            services.AddSingleton<IRepository<ActivityInstruction, ActivityInstructionCreation>, ActivityInstructionRepository>();
            services.AddSingleton<IStore<ActivityInstruction>, ActivityInstructionStore>();
            services.AddSingleton<IProvider<ActivityInstruction, ActivityInstructionCreation>, ActivityInstructionProvider>();
        }
        public static void ActivityTypes(IServiceCollection services)
        {
            services.AddSingleton<IRepository<ActivityType, ActivityTypeCreation>, ActivityTypeRepository>();
            services.AddSingleton<IStore<ActivityType>, ActivityTypeStore>();
            services.AddSingleton<IProvider<ActivityType, ActivityTypeCreation>, ActivityTypeProvider>();
        }
        public static void Get(IServiceCollection services)
        {
            Activities(services);
            ActivitiesInstructions(services);
            ActivityTypes(services);
            Instructions(services);
            Lines(services);
            Machines(services);
            Persons(services);
            Responsibles(services);
            Trainings(services);
            Workshops(services);
        }
        public static void Instructions(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Instruction, InstructionCreation>, InstructionRepository>();
            services.AddSingleton<IStore<Instruction>, InstructionStore>();
            services.AddSingleton<IProvider<Instruction, InstructionCreation>, InstructionProvider>();
        }
        public static void Lines(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Line, LineCreation>, LineRepository>();
            services.AddSingleton<IStore<Line>, LineStore>();
            services.AddSingleton<IProvider<Line, LineCreation>, LineProvider>();
        }
        public static void Machines(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Machine, MachineCreation>, MachineRepository>();
            services.AddSingleton<IStore<Machine>, MachineStore>();
            services.AddSingleton<IProvider<Machine, MachineCreation>, MachineProvider>();
        }
        public static void Persons(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Person, PersonCreation>, PersonRepository>();
            services.AddSingleton<IStore<Person>, PersonStore>();
            services.AddSingleton<IProvider<Person, PersonCreation>, PersonProvider>();
        }
        public static void Trainings(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Training, TrainingCreation>, TrainingRepository>();
            services.AddSingleton<IStore<Training>, TrainingStore>();
            services.AddSingleton<IProvider<Training, TrainingCreation>, TrainingProvider>();
        }
        public static void Workshops(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Workshop, WorkshopCreation>, WorkshopRepository>();
            services.AddSingleton<IStore<Workshop>, WorkshopStore>();
            services.AddSingleton<IProvider<Workshop, WorkshopCreation>, WorkshopProvider>();
        }
        public static void Responsibles(IServiceCollection services)
        {
            services.AddSingleton<IRepository<MachineResponsible, MachineResponsibleCreation>, MachineResponsibleRepository>();
            services.AddSingleton<IStore<MachineResponsible>, MachineResponsibleStore>();
            services.AddSingleton<IProvider<MachineResponsible, MachineResponsibleCreation>, MachineResponsibleProvider>();
        }
    }
}