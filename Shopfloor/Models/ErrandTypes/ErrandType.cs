namespace Shopfloor.Models.ErrandTypeModel
{
    public class ErrandType
    {
        private readonly int? _id;
        private readonly string _name;
        private readonly string? _description;
        public ErrandType(string name, string? description)
        {
            _name = name;
            _description = description;
        }
        public ErrandType(int id, string name, string? description)
        {
            _id = id;
            _name = name;
            _description = description;
        }
        public int? Id => _id;
        public string Name => _name;
        public string? Description => _description;
    }
}