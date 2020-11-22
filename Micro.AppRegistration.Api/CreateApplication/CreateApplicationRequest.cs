namespace Micro.AppRegistration.Api.CreateApplication
{
    public class CreateApplicationRequest
    {
        public string Name { set; get; }
        public string ShortCode { set; get; }
        public bool UseDefault { set; get; }
    }
}
