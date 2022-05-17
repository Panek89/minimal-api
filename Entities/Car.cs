namespace minimal_api.Entities
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Manufacturer { get; set; }
        public string Model { get; set; }
    }
}