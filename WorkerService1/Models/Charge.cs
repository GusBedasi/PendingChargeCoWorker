namespace WorkerService1.Models
{
    public class Charge
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public bool? PendingCancellation { get; set; }
    }
}
