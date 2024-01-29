using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.UserModel;
using System;

namespace Shopfloor.Models.ErrandsErrandStatuses
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
        public int SetById { get; set; }
        public User user { get; set; }
    }
}