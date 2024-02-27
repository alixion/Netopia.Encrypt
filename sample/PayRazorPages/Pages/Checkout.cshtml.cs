using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netopia.Encrypt;
using Netopia.Encrypt.Models;

namespace PayRazorPages.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public CheckoutModel(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        private const string Signature = "todo";
        private const string ConfirmUrl = "todo";
        private const string ReturnUrl = "todo";
        public readonly string UrlMobilPay = "https://sandboxsecure.mobilpay.ro";
        private const string CertPath = "todo.cer";
        private const string PrivateKeyPath = "todo.key";

        public Dictionary<string, string>? FormFields;
        public bool IsError = false;
        public string Message = string.Empty;
        
        [BindProperty]
        public Address Address { get; set; } = new()
        {
            Type = "person",
            FirstName = "Alex",
            LastName = "Manea",
            County = "Sector 5",
            City = "Bucuresti",
            AddressLine = "Sos Cotroceni 1",
            Country = "Romania",
            MobilPhone = "0722222222",
            Email = "test@example.com"
        };

        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var orderId = DateTime.Now.Ticks.ToString().Substring(0,11); 

            var transaction = new CardPaymentRequest()
            {
                Type = "card",
                Signature = Signature,
                OrderId = orderId,
                Urls = new PaymentRequestUrls()
                {
                    ConfirmUrl = ConfirmUrl,
                    ReturnUrl = ReturnUrl,
                    CancelUrl = ""
                },
                Invoice = new PaymentInvoice()
                {
                    Amount = 1000,
                    Currency = "RON",
                    Details = $"Test plata pentru comanda {orderId}",
                    ContactInfo = new ContactInfo()
                    {
                        Billing = Address,
                        Shipping = Address
                    },
                    
                },
            };
            var encrypt = new EncryptionService<CardPaymentRequest>(CertPath, PrivateKeyPath, transaction);

            try
            
            {
                encrypt.Encrypt();
                FormFields = new Dictionary<string, string>
                {
                    { "data", encrypt.EncryptedData }, { "env_key", encrypt.EnvelopeKey }
                };
            }
            catch (Exception e)
            {
                IsError = true;
                Message = e.Message;
            }

        }
    }
}
