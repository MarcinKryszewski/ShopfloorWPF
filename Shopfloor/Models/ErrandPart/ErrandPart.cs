using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.PartModel;

namespace Shopfloor.Models.ErrandPartModel
{
    public class ErrandPart
    {
        private const string _defaultStatus = "NOWY";
        private readonly int _errandId;
        private readonly int _partId;
        private Errand? _errand;
        private Part? _part;
        private double? _amount;
        private string _status = _defaultStatus;
        public ErrandPart(int errandId, int partId)
        {
            _errandId = errandId;
            _partId = partId;
        }
        public ErrandPart(int errandId, int partId, double? amount, string? status = null)
        {
            _errandId = errandId;
            _partId = partId;
            _amount = amount;
            _status = status ?? _defaultStatus;
        }
        public int ErrandId => _errandId;
        public int PartId => _partId;
        public Part? Part
        {
            get => _part;
            set
            {
                if (value is null) return;
                if (value.Id == _partId) _part = value;
            }
        }
        public string Status { get => _status; set => _status = value; }
        public double? Amount { get => _amount; set => _amount = value; }
        internal Errand? Errand
        {
            get => _errand;
            set
            {
                if (value is null) return;
                if (value.Id == _errandId) _errand = value;
            }
        }
    }
}