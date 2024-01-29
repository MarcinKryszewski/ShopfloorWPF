using System;

namespace Shopfloor.Models.ErrandModel
{
    public class ErrandDTO
    {
        public int Created_By_Id { get; set; }
        public DateTime Created_Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Errand_Type_Id { get; set; }
        public DateTime Expected_Date { get; set; }
        public int Id { get; set; }
        public int Machine_Id { get; set; }
        public int Owner_Id { get; set; }
        public string Priority { get; set; } = Errand.DefaultPriority;
        public string Sap_Number { get; set; } = string.Empty;
    }
}