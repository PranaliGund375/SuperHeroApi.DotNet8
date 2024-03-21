namespace SuperHeroApi.DotNet8.Entities
{
    public class SuperHero
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
    }
}
