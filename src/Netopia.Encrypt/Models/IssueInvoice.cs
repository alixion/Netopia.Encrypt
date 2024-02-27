namespace Netopia.Encrypt.Models;

[Serializable]
public class IssueInvoice
{
    [XmlElement("amount")] public decimal Amount { get; set; }
    [XmlElement("currency")] public string Currency { get; set; }
    [XmlElement("date")] public string Date { get; set; }
    [XmlElement("exchangeRate")] public decimal ExchangeRate { get; set; }
}