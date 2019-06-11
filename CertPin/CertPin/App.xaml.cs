namespace CertPin
{
    public partial class App
    {
        public const string BASE_URL = "https://google.com";

        public App()
        {
            InitializeComponent();

            CertificateValidator.SetUp();

            MainPage = new MainPage();
        }
    }
}
