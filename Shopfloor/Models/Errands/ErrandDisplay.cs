using Shopfloor.Models.ErrandStatusModel;
using System.Linq;

namespace Shopfloor.Models.ErrandModel
{
    internal sealed class ErrandDisplay
    {
        private const string _expectedDateNotAssigned = "Nie określono";
        private const string _machineNotAssigned = "Nieprzypisana";
        private const string _ownerNotAssigned = "Nieprzypisane";
        private const string _typeNotAssigned = "Nie ustalono";
        private const string _statusNotSet = "Nowy";
        private readonly Errand _errand;

        public ErrandDisplay(Errand errand)
        {
            _errand = errand;
        }
        private ErrandStatus? LastStatus => _errand.ErrandStatuses.OrderByDescending(es => es.CreateDate).FirstOrDefault()?.ErrandStatus;

        public string MachineText => _errand.Machine?.Path ?? _machineNotAssigned;
        public string OwnerText => _errand.Responsible?.FullName ?? _ownerNotAssigned;
        public string LastStatusName => LastStatus?.Description ?? _statusNotSet;
        public string ExpectedDateShortString => _errand.ExpectedDate?.Date.ToString("d") ?? _expectedDateNotAssigned;
        public string DescriptionShort => _errand.Description.Length > 120 ? _errand.Description[..120] + "..." : _errand.Description;
        public string CreatedDateShortString => _errand.CreatedDate.Date.ToString("d");
        public string ErrandTypeName => _errand.Type?.Name ?? _typeNotAssigned;
    }
}