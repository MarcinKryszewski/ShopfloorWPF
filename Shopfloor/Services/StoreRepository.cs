using Shopfloor.Features.Mechanic.Errands.Stores;
using Shopfloor.Interfaces;
using Shopfloor.Models.ErrandModel;
using Shopfloor.Models.ErrandPartModel;
using Shopfloor.Models.ErrandPartOfferModel;
using Shopfloor.Models.ErrandPartOrderModel;
using Shopfloor.Models.ErrandPartStatusModel;
using Shopfloor.Models.ErrandStatusModel;
using Shopfloor.Models.ErrandTypeModel;
using Shopfloor.Models.MachineModel;
using Shopfloor.Models.MachinePartModel;
using Shopfloor.Models.MessageModel;
using Shopfloor.Models.OfferModel;
using Shopfloor.Models.OrderModel;
using Shopfloor.Models.PartModel;
using Shopfloor.Models.PartTypeModel;
using Shopfloor.Models.ReservationModel;
using Shopfloor.Models.RoleModel;
using Shopfloor.Models.RoleUserModel;
using Shopfloor.Models.SupplierModel;
using Shopfloor.Models.UserModel;
using Shopfloor.Stores;

namespace Shopfloor.Services
{
    internal class StoreRepository
    {
        public StoreRepository(
            IDataStore<User> userStore,
            IDataStore<Supplier> supplierStore,
            IDataStore<RoleUser> roleUserStore,
            IDataStore<Role> roleStore,
            IDataStore<Reservation> reservationStore,
            IDataStore<PartType> partTypeStore,
            IDataStore<Part> partStore,
            IDataStore<Order> orderStore,
            IDataStore<Offer> offerStore,
            IDataStore<Message> messageStore,
            IDataStore<MachinePart> machinePartStore,
            IDataStore<Machine> machineStore,
            IDataStore<ErrandType> errandTypeStore,
            IDataStore<ErrandStatus> errandStatusStore,
            IDataStore<ErrandPartStatus> errandPartStatusStore,
            IDataStore<ErrandPartOrder> errandPartOrderStore,
            IDataStore<ErrandPartOffer> errandPartOfferStore,
            IDataStore<ErrandPart> errandPartStore,
            IDataStore<Errand> errandStore,
            SelectedErrandStore selectedErrand,
            ICurrentUserStore currentUser)
        {
            ErrandPartOffer = errandPartOfferStore;
            ErrandPartOrder = errandPartOrderStore;
            ErrandPartStatus = errandPartStatusStore;
            ErrandPart = errandPartStore;
            ErrandStatus = errandStatusStore;
            Errand = errandStore;
            ErrandType = errandTypeStore;
            MachinePart = machinePartStore;
            Machine = machineStore;
            Message = messageStore;
            Offer = offerStore;
            Order = orderStore;
            Part = partStore;
            PartType = partTypeStore;
            Reservation = reservationStore;
            Role = roleStore;
            RoleUser = roleUserStore;
            Supplier = supplierStore;
            User = userStore;
            SelectedErrand = selectedErrand;
            CurrentUser = currentUser;
        }
        public ICurrentUserStore CurrentUser { get; init; }
        public IDataStore<Errand> Errand { get; init; }
        public IDataStore<ErrandPart> ErrandPart { get; init; }
        public IDataStore<ErrandPartOffer> ErrandPartOffer { get; init; }
        public IDataStore<ErrandPartOrder> ErrandPartOrder { get; init; }
        public IDataStore<ErrandPartStatus> ErrandPartStatus { get; init; }
        public IDataStore<ErrandStatus> ErrandStatus { get; init; }
        public IDataStore<ErrandType> ErrandType { get; init; }
        public IDataStore<Machine> Machine { get; init; }
        public IDataStore<MachinePart> MachinePart { get; init; }
        public IDataStore<Message> Message { get; init; }
        public IDataStore<Offer> Offer { get; init; }
        public IDataStore<Order> Order { get; init; }
        public IDataStore<Part> Part { get; init; }
        public IDataStore<PartType> PartType { get; init; }
        public IDataStore<Reservation> Reservation { get; init; }
        public IDataStore<Role> Role { get; init; }
        public IDataStore<RoleUser> RoleUser { get; init; }
        public SelectedErrandStore SelectedErrand { get; init; }
        public IDataStore<Supplier> Supplier { get; init; }
        public IDataStore<User> User { get; init; }
    }
}