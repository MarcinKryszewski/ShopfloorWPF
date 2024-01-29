using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandStatusModel;
using System;

namespace Shopfloor.Models.ErrandErrandStatusesModel
{
    internal sealed class ErrandErrandStatus
    {
        public ErrandErrandStatus(int errandId, Errand errand, int errandStatusId, ErrandStatus errandStatus, DateTime setDate)
        {
            ErrandId = errandId;
            Errand = errand;
            ErrandStatusId = errandStatusId;
            ErrandStatus = errandStatus;
            SetDate = setDate;
        }

        public int ErrandId { get; set; }
        public Errand Errand { get; set; }
        public int ErrandStatusId { get; set; }
        public ErrandStatus ErrandStatus { get; set; }
        public DateTime SetDate { get; set; }
    }
}