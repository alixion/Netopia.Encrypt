namespace Netopia.Encrypt.Models;

public class CardPaymentRequest: PaymentRequestBase
{
    [XmlElement("invoice")] public PaymentInvoice Invoice { get; set; }

    [XmlArrayItem("destination")]
    [XmlArray("split")]
    public List<PaymentSplitDestination> Split { get; set; }

    [XmlElement("recurrence")] public Recurrence Recurrence { get; set; }
}