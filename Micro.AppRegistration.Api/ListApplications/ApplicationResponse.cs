using System;

namespace Micro.AppRegistration.Api.ListApplications
{
    public class ApplicationResponse
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string User { set; get; }
        public string ShortCode { set; get; }
        public bool UseDefaultCode { set; get; }
        public bool Approved { set; get; }
        public DateTime CreatedAt { set; get; }
        public string CreatedBy { set; get; }
    }
}
