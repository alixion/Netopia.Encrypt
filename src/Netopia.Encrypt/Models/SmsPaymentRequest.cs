namespace Netopia.Encrypt.Models;

[XmlRoot("order")]
[Serializable]
public class SmsPaymentRequest:PaymentRequestBase
{
    public string Msisdn { get; set; }
    
}