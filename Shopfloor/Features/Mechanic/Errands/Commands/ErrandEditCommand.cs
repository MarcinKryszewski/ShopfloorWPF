using Shopfloor.Interfaces.Models;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Shared.Commands;
using System;

namespace Shopfloor.Features.Mechanic.Errands.Commands
{
    internal sealed class ErrandEditCommand : CommandBase
    {
        private readonly IModelEditorService<Errand> _errandEditor;
        public event Action<bool>? ErrandEdited;
        public ErrandEditCommand(IModelEditorService<Errand> errandEditor)
        {
            _errandEditor = errandEditor;
        }
        public override void Execute(object? parameter)
        {
            if (parameter is null) return;
            ErrandCreatorData creatorData = (ErrandCreatorData)parameter;
            int userId = creatorData.UserId;

            EditErrand(creatorData.Errand);

            ErrandEdited?.Invoke(true);
        }
        private void EditErrand(Errand errand)
        {
            errand.Validate();
            if (errand.HasErrors) return;

            _errandEditor.Edit(errand);

        }
    }
}