namespace COMP2139_Labs.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => RequestId != null || !string.IsNullOrEmpty(RequestId);
    }
}
