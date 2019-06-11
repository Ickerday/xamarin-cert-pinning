using CertPin.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace CertPin
{
    public partial class MainPage
    {
        private readonly RefitDataService _refitService;
        private readonly DotNetDataService _dotNetService;

        private ICommand _confirmCertWithDotNetCommand;
        public ICommand ConfirmCertWithDotNetCommand => _confirmCertWithDotNetCommand
            ?? (_confirmCertWithDotNetCommand = new Command(async () => await _dotNetService.GetPinnedDataAsync()));

        private ICommand _confirmCertWithRefitCommand;
        public ICommand ConfirmCertWithRefitCommand => _confirmCertWithRefitCommand
            ?? (_confirmCertWithRefitCommand = new Command(async () => await _refitService.GetDataAsync()));

        public MainPage()
        {
            InitializeComponent();

            _dotNetService = new DotNetDataService();
            _refitService = new RefitDataService();

            BindingContext = this;
        }
    }
}
