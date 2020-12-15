namespace Micro.AppRegistration.Api.VerifySecret
{
    public class VerifySecretResponse
    {
        public bool Success { set; get; }
        public string Owner { set; get; }
        public string ShortCode { set; get; }
        public bool Approved { set; get; }
        public bool UseDefaultShortCode { set; get; }
    }
}
