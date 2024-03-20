namespace Shopfloor.Models.RoleModel
{
    public sealed class Role : DataModel
    {
        private readonly RoleDTO _data;
        public Role(int id, string name, int value)
        {
            _data = new();
            Id = id;
            Name = name;
            Value = value;
        }
        public Role(string name, int value)
        {
            _data = new();
            Name = name;
            Value = value;
        }
        public int? Id { get; }
        public string Name { get; }
        public int Value { get; }

    }
}