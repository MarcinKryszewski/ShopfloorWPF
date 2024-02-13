namespace Shopfloor.Database.Configuration
{
    internal sealed class DatabaseConfiguration
    {
        public string? Type { get; set; }
        public string? ConnectionString { get; set; }
        public string? Path { get; set; }
    }
}