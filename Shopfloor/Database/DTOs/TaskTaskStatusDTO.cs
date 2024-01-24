using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopfloor.Database.DTOs
{
    public class TaskTaskStatusDTO
    {
        public int Task_Id { get; set; }
        public int Task_Status_Id { get; set; }
        public DateTime Set_Date { get; set; }
        public int Set_By_Id { get; set; }
    }
}