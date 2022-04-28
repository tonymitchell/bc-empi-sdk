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

            // If multiple valid certificates, take the one expiring the furthest in the future
            DateTime now = DateTime.Now;
            var cert = certs.Cast<X509Certificate2>()
                .Where(c => c.NotBefore <= now && now <= c.NotAfter)    // Remove certificates that aren't yet valid or are expired
                .OrderByDescending(c => c.NotAfter)                     // Sort so first certs is the one expiring furthest in the future
                .FirstOrDefault();                                      // Take first in the list
            if (cert == null) throw new Exception("Could not find a valid, non-expired client certificate.");

            certificates.Add(cert);

            store.Close();
        }
    }
}