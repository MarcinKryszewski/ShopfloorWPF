using System;
using Shopfloor.Models.ErrandPartModel;

namespace Shopfloor.Models.ReservationModel
{
    internal sealed class ReservationDTO
    {
        public int? Id { get; set; }
        public ErrandPart? ErrandPart { get; set; }
        public int ErrandPartId { get; set; }
        public double Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Completed { get; set; }
    }
}