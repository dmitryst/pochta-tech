namespace ConsumerService.Configuration
{
    public class NatsOptions
    {
        public string Url { get; set; }

        public string ClusterId { get; set; }

        public string Subject { get; set; }

        public string DurableName { get; set; }
    }
}
