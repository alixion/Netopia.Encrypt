namespace Netopia.Encrypt.Models;

[Serializable]
public class PaymentError
{
    [XmlAttribute("code")] public string Code { get; set; }
    [XmlText] public string Message { get; set; }
}