using System;

namespace Shopfloor.Models.TaskModel
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By_Id { get; set; }
        public int Owner_Id { get; set; }
        public string Priority { get; set; } = "C";
        public int Machine_Id { get; set; }
        public int Task_Type_Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Sap_Number { get; set; } = string.Empty;
        public DateTime Expected_Date { get; set; }
    }
}