using AMA.Common.Contracts;
using Stripe;
using Stripe.Infrastructure;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AMA.MobileClient.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        private readonly ApiService _paymentService = new ApiService("users/user/pay");
        public string CreditCardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CVC { get; set; }
        private string StripeToken { get; set; }
        public long? Amount { get; set; }
        public string Description { get; set; }
        private UsersResponse User;

        public PaymentViewModel(UsersResponse _user)
        {
            User = _user;
        }

        public async Task<bool> Pay()
        {   
            if(string.IsNullOrEmpty(CreditCardNumber) || string.IsNullOrWhiteSpace(CreditCardNumber)
                || string.IsNullOrEmpty(CVC) || string.IsNullOrWhiteSpace(CVC)
                || ExpiryMonth == 0
                || ExpiryYear == 0
                || Amount == null)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", "Check all fields", "Ok");
                return false;
            }

            CreateToken();

            var request = new
            {
                Token = StripeToken,
                Amount = Amount,
                Description = Description,
                ToUserId = User.Id
            };

            _ = _paymentService.Post<object>(request, null);

            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Information", "Payment processed. Please navigate back.", "Ok");
            return true;
        }

        void CreateToken()
        {
            StripeConfiguration.ApiKey = "pk_test_51IIgsdA1kW8vdqJdzeq4eJYLZKyEwOCHDKKZRV9pwPBGiRloaOEy3aZLc5ktosPYCBrGrbi1yvLIAcERrR7RUXrT00BbjODlcq";

            var tokenOptions = new TokenCreateOptions()
            {
                Card = new CreditCardOptions()
                {
                    Number = CreditCardNumber,
                    ExpYear = ExpiryYear,
                    ExpMonth = ExpiryMonth,
                    Cvc = CVC,
                    Currency = "EUR"
                }
            };

            var tokenService = new TokenService();
            Token stripeToken = tokenService.Create(tokenOptions);

            StripeToken = stripeToken.Id;
        }
    }
}
