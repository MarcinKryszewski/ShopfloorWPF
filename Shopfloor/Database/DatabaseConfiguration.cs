namespace Shopfloor.Database
{
    internal sealed class DatabaseConfiguration
    {
        public string? ConnectionString { get; set; }
        public string? Path { get; set; }
        public string? Type { get; set; }
    }
}