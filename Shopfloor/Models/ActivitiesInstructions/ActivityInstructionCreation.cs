using Shopfloor.Models.Activities;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Instructions;

namespace Shopfloor.Models.ActivitiesInstructions
{
    internal class ActivityInstructionCreation : ModelValidationBase, IModelCreationModel<ActivityInstruction>
    {
        public Activity? Activity { get; set; }
        required public int ActivityId { get; set; }
        required public int Id { get; set; }
        public Instruction? Instruction { get; set; }
        required public int InstructionId { get; set; }
        public ActivityInstruction CreateModel(int id)
        {
            return new ActivityInstruction()
            {
                Id = id,
                ActivityId = ActivityId,
                Activity = Activity,
                Instruction = Instruction,
                InstructionId = InstructionId,
            };
        }
    }
}