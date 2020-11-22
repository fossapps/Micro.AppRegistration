using System;

namespace Micro.AppRegistration.Api.Models
{
    public class Application
    {
        public string Id { set; get; }
        public string User { set; get; }
        public string Secret { set; get; }
        public string ShortCode { set; get; }
        public bool UseDefaultCode { set; get; }
        public bool Approved { set; get; }
        public DateTime CreatedAt { set; get; }
        public string CreatedBy { set; get; }
    }
}
