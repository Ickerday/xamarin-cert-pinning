using CertPin.Droid.Handlers;
using Java.Security;
using Java.Security.Cert;
using Javax.Net.Ssl;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Dependency(typeof(TlsClientHandler))]
namespace CertPin.Droid.Handlers
{
    public class TlsClientHandler : AndroidClientHandler
    {
        private TrustManagerFactory _trustManagerFactory;
        private KeyManagerFactory _keyManagerFactory;
        private KeyStore _keyStore;

        protected override TrustManagerFactory ConfigureTrustManagerFactory(KeyStore keyStore)
        {
            if (_trustManagerFactory != null)
                return _trustManagerFactory;

            _trustManagerFactory = TrustManagerFactory.GetInstance(TrustManagerFactory.DefaultAlgorithm);
            _trustManagerFactory.Init(keyStore);

            return _trustManagerFactory;
        }

        protected override KeyManagerFactory ConfigureKeyManagerFactory(KeyStore keyStore)
        {
            if (_keyManagerFactory != null)
                return _keyManagerFactory;

            _keyManagerFactory = KeyManagerFactory.GetInstance(KeyManagerFactory.DefaultAlgorithm);
            _keyManagerFactory.Init(keyStore, null);

            return _keyManagerFactory;
        }

        protected override KeyStore ConfigureKeyStore(KeyStore keyStore)
        {
            if (_keyStore != null)
                return _keyStore;

            _keyStore = KeyStore.GetInstance(KeyStore.DefaultType);
            _keyStore.Load(null, null);

            var cff = CertificateFactory.GetInstance("X.509");

            Certificate cert;
            // Add your Certificate to the Assets folder and address it here by its name
            using (var certStream = Application.Context.Assets.Open("google-com.cert"))
                cert = cff.GenerateCertificate(certStream);

            _keyStore.SetCertificateEntry("TrustedCert", cert);

            return _keyStore;
        }
    }
}