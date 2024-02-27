namespace Netopia.Encrypt.Models;

[Serializable]
public class PaymentRequestBase
{
    [XmlAttribute("id")] public string OrderId { get; set; }
    [XmlElement("invoice")] public PaymentInvoice Invoice { get; set; }
    [XmlAttribute("type")] public string Type { get; set; }
    [XmlElement("signature")] public string Signature { get; set; }
    [XmlElement("service")] public string Service { get; set; }
    
    [XmlAttribute("timestamp")] public string TimeStamp { get; set; }
    [XmlElement("url")] public PaymentRequestUrls Urls { get; set; }

    [XmlArrayItem("param")]
    [XmlArray("params")]
    public List<PaymentParam> Params { get; set; }

    [XmlElement("mobilpay")] public PaymentConfirmation Confirmation { get; set; }
}