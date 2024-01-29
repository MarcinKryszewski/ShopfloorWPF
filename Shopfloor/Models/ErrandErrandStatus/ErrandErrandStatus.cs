using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandStatusModel;
using System;

namespace Shopfloor.Models.ErrandErrandStatusesModel
{
    internal sealed class ErrandErrandStatus
    {
        private int? _errandStatusId;
        private int? _errandId;
        private readonly DateTime _createDate;
        private Errand? _errand;
        private ErrandStatus? _errandStatus;
        public ErrandErrandStatus(int errandId, int errandStatusId, DateTime createDate)
        {
            _errandId = errandId;
            _errandStatusId = errandStatusId;
            _createDate = createDate;
        }
        public int? ErrandId => _errandId;
        public Errand? Errand
        {
            get => _errand;
            set
            {
                _errand = value;
                _errandId = value?.Id;
            }
        }
        public ErrandStatus? ErrandStatus
        {
            get => _errandStatus;
            set
            {
                _errandStatus = value;
                _errandStatusId = value?.Id;
            }
        }
        public int? ErrandStatusId => _errandStatusId;
        public DateTime CreateDate => _createDate;
    }
}