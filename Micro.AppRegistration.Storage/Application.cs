using System;

namespace Micro.AppRegistration.Storage
{
    public class Application
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string User { set; get; }
        public string Secret { set; get; }
        public DateTime CreatedAt { set; get; }
    }
}
