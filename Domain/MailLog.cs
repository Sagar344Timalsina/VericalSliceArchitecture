namespace verticalSliceArchitecture.Domain
{
    public class MailLog:EntityBase
    {
        public string ToEmail {  get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent{ get; set; }
        public string ErrorMessage{ get; set; }
    }
}
