using System.Collections.Generic;
using Shopfloor.Models.Commons.BaseClasses;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderCreationModel : ModelValidationBase, IModelCreationModel
    {
        private LineModel? _line;
        public string Description { get; set; } = string.Empty;
        public int? Id { get; set; }
        public LineModel? Line
        {
            get => _line;
            set
            {
                _line = value;
                if (value is not null)
                {
                    LineId = value.Id;
                }
            }
        }
        public int LineId { get; set; }
        public List<PartModel> Parts { get; } = [];
        public List<int> PartsId { get; } = [];
    }
}