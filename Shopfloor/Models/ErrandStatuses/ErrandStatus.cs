namespace Shopfloor.Models.ErrandStatusModel
{
    internal sealed class ErrandStatus
    {
        private readonly int _id;
        private string _description;

        public ErrandStatus(int id, string description)
        {
            _id = id;
            _description = description;
        }

        public int Id => _id;
        public string Description { get => _description; set => _description = value; }
    }
}