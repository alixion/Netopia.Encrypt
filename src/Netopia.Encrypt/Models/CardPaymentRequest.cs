namespace Netopia.Encrypt.Models;

public class CardPaymentRequest: PaymentRequestBase
{
    [XmlArrayItem("destination")]
    [XmlArray("split")]
    public List<PaymentSplitDestination> Split { get; set; }

    [XmlElement("recurrence")] public Recurrence Recurrence { get; set; }
}