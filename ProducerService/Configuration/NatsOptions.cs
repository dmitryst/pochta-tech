namespace ProducerService.Configuration
{
    public class NatsOptions
    {
        public string Url { get; set; }

        public string ClusterId { get; set; }

        public string Subject { get; set; }
    }
}
