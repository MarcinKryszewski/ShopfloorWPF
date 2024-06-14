using System;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferDTO
    {
        public int? Id { get; set; }
        public User? CreateBy { get; set; }
        public int CreateById { get; set; }
        public DateTime CreateDate { get; set; }
    }
}