using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Instructions;

namespace Shopfloor.Models.ActivitiesInstructions
{
    internal class ActivityInstruction : IModel
    {
        public string Name { get; } = string.Empty;
        public Activity? Activity { get; set; }
        required public int ActivityId { get; init; }
        required public int Id { get; init; }
        public Instruction? Instruction { get; set; }
        required public int InstructionId { get; init; }
        public void SetValues<T>(IModelCreationModel<T> data)
            where T : IModel
        {
            if (data is not ActivityInstructionCreation)
            {
                return;
            }

            ActivityInstructionCreation creation = (ActivityInstructionCreation)data;

            Activity = creation.Activity;
            Instruction = creation.Instruction;
        }
    }
}