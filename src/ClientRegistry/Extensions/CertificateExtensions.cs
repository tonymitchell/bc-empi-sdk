using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Health.Services.Extensions
{
    static class CertificateExtensions
    {
        public static void AddCertificateBySubjectName(this X509CertificateCollection certificates, string clientCertificateSubjectName)
        {
            using X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, clientCertificateSubjectName, validOnly: false);   // validOnly: false needed for Azure
            if (certs.Count < 1) throw new Exception("No client certificate found.");
            if (certs.Count > 1) throw new Exception("Could not resolve client certificate to a single certificate.");

            certificates.Add(certs[0]);

            store.Close();
        }
    }
}