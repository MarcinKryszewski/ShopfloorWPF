using System;
using System.Collections.Generic;
using Shopfloor.Models.Interfaces;
using Shopfloor.Models.Lines;

namespace Shopfloor.Models.WorkOrders
{
    public class WorkOrderDto : IModelDto
    {
        private LineModel? _line;
        public string Description { get; set; } = string.Empty;
        public List<PartModel> Parts { get; set; } = [];
        public List<int> PartsId { get; set; } = [];
        public LineModel? Line
        {
            get => _line;
            set
            {
                if (value is not null && LineId != value.Id)
                {
                    LineId = value.Id;
                }
                _line = value;
            }
        }
        public int LineId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}