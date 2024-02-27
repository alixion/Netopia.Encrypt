namespace Netopia.Encrypt.Models;

[Serializable]
public class PaymentSplitDestination
{
    [XmlAttribute("id")] public string Id { get; set; }
    [XmlAttribute("amount")] public decimal Amount { get; set; }
}