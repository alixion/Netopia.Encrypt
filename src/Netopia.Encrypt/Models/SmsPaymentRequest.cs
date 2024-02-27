namespace Netopia.Encrypt.Models;

public class SmsPaymentRequest:PaymentRequestBase
{
    public string Msisdn { get; set; }
    
}