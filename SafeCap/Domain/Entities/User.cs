namespace SafeCap.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<SensorReading> SensorReadings { get; set; }
        public ICollection<Alert> Alerts { get; set; }
    }
}
