using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandStatusModel;
using System;

namespace Shopfloor.Models.ErrandErrandStatusesModel
{
    internal sealed class ErrandErrandStatus
    {
        public ErrandErrandStatus(int errandId, int errandStatusId, DateTime createDate)
        {
            ErrandId = errandId;
            ErrandStatusId = errandStatusId;
            CreateDate = createDate;
        }

        public int ErrandId { get; set; }
        public Errand Errand { get; set; }
        public int ErrandStatusId { get; set; }
        public ErrandStatus ErrandStatus { get; set; }
        public DateTime CreateDate { get; set; }
    }
}