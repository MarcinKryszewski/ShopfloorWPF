using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using System.Collections.Generic;
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
        private const int _maxDescriptionLength = 120;
        private readonly Errand _errand;

        public ErrandDisplay(Errand errand)
        {
            _errand = errand;
        }
        private ErrandStatus? LastStatus => _errand.Statuses.OrderByDescending(es => es.Id).FirstOrDefault();

        public string MachineText => _errand.Machine?.Path ?? _machineNotAssigned;
        public string OwnerText => _errand.Responsible?.FullName ?? _ownerNotAssigned;
        public string LastStatusName => LastStatus?.StatusName ?? _statusNotSet;
        public string LastStatusDate => LastStatus?.SetDate.ToString("dd/MM/yyyy") ?? "";
        public string PartsStatus => GetPartsStatus();
        public string ExpectedDateShortString => _errand.ExpectedDate?.Date.ToString("dd.MM.yyyy") ?? _expectedDateNotAssigned;
        public string DescriptionShort => _errand.Description.Length > _maxDescriptionLength ? _errand.Description[.._maxDescriptionLength] + "..." : _errand.Description;
        public string CreatedDateShortString => _errand.CreatedDate.Date.ToString("dd.MM.yyyy");
        public string ErrandTypeName => _errand.Type?.Name ?? _typeNotAssigned;
        private string GetPartsStatus()
        {
            int partsAmount = _errand.Parts.Count;
            if (partsAmount == 0) return "SPECYFIKOWANIE";

            Dictionary<string, int> partsCount = [];
            foreach (KeyValuePair<int, string> status in ErrandPartStatus.Status)
            {
                partsCount.Add(status.Value, 0);
            }

            foreach (ErrandPart errandPart in _errand.Parts)
            {
                partsCount[errandPart.LastStatusText]++;
            }

            if (partsAmount == partsCount[ErrandPartStatus.Status[5]]) return ErrandPartStatus.Status[5];
            if (partsCount[ErrandPartStatus.Status[0]] > 0) return ErrandPartStatus.Status[0];
            if (partsCount[ErrandPartStatus.Status[1]] > 0) return ErrandPartStatus.Status[1];
            if (partsCount[ErrandPartStatus.Status[2]] > 0) return ErrandPartStatus.Status[2];
            if (partsCount[ErrandPartStatus.Status[3]] > 0) return ErrandPartStatus.Status[3];
            if (partsCount[ErrandPartStatus.Status[4]] > 0) return ErrandPartStatus.Status[4];
            return "ERROR";
        }
    }
}