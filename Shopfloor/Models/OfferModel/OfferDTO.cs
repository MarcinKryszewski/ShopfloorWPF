using System;
using Shopfloor.Models.UserModel;

namespace Shopfloor.Models.OfferModel
{
    internal sealed class OfferDto
    {
        public User? CreateBy { get; set; }
        public int CreateById { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Id { get; set; }
    }
}