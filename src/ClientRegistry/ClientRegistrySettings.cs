using System;

namespace Health.Services
{
    public class ClientRegistrySettings
    {
        public const string SectionName = "ClientRegistry";

        public ServiceUrls ServiceUrls { get; set; } = new ServiceUrls();
        public string ClientCertificateSubjectName { get; set; }
        public string SendingOrgId { get; set; }
        public string SendingSystemId { get; set; }
    }
}

public class ServiceUrls
{
    public Uri QueryServices { get; set; }
}
