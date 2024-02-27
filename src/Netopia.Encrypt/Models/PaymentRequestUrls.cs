namespace Netopia.Encrypt.Models;

public class PaymentRequestUrls
{
    [XmlElement("confirm")] public string ConfirmUrl { get; set; }
    [XmlElement("return")] public string ReturnUrl { get; set; }
    [XmlElement("cancel")] public string CancelUrl { get; set; }
}