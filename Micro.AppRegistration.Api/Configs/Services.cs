namespace Micro.AppRegistration.Api.Configs
{
    public class Services
    {
        public KeyStoreConfig KeyStore { set; get; }
    }

    public class KeyStoreConfig
    {
        public string Url { set; get; }
    }
}
