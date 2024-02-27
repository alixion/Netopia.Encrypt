namespace Netopia.Encrypt.Models;

[Serializable]
public class PaymentInvoice
{
    [XmlAttribute("currency")] public string Currency { get; set; }
    [XmlAttribute("amount")] public decimal Amount { get; set; }
    [XmlElement("details")] public string Details { get; set; }
    [XmlElement("contact_info")] public ContactInfo ContactInfo { get; set; }

    [XmlArrayItem("item")]
    [XmlArray("items")]
    public List<InvoiceItem> Items { get; set; }

    [XmlArray("exchange_rates")]
    [XmlArrayItem("rate")]
    public List<ExchangeRate> ExchangeRates { get; set; }

    [XmlAttribute("installments")] public string Installments { get; set; }

    [XmlAttribute("selected_installments")]
    public string SelectedInstallments { get; set; }
}