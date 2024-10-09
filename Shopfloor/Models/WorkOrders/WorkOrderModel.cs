using System;
using System.Collections.Generic;
using Shopfloor.Models.Commons.Interfaces;
using Shopfloor.Models.Lines;
using Shopfloor.Models.Parts;
using Shopfloor.Models.WorkOrderParts;

namespace Shopfloor.Models.WorkOrders
{
    internal class WorkOrderModel : IModel
    {
        private LineModel? _line;
        required public int Id { get; init; }
        public string Description { get; set; } = string.Empty;
        public List<WorkOrderPartModel> Parts { get; init; } = [];
        public List<int> PartsId { get; init; } = [];
        public bool IsDeleted { get; set; } = false;
        public DateTime DeletionTime { get; set; }
        // public PersonModel? DeletedBy { get; }
        public LineModel? Line
        {
            get => _line;
            set
            {
                if (value is null || value.Id == LineId)
                {
                    _line = value;
                }
            }
        }
        required public int LineId { get; set; }
        required public DateTime CreateDate { get; init; }
        public DateOnly CreateDateOnlyDate => DateOnly.FromDateTime(CreateDate);
    }
}