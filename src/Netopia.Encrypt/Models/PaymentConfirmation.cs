namespace Netopia.Encrypt.Models;

[Serializable]
public class PaymentConfirmation
{
    [XmlAttribute("timestamp")] public string TimeStamp { get; set; }
    [XmlAttribute("crc")] public string Crc { get; set; }
    [XmlElement("action")] public string Action { get; set; }
    [XmlElement("original_amount")] public decimal OriginalAmount { get; set; }
    [XmlElement("processed_amount")] public decimal ProcessedAmount { get; set; }
    [XmlElement("purchase")] public int Purchase { get; set; }
    [XmlElement("paid_by_phone")] public string PayerMsisdn { get; set; }
    [XmlElement("opr_code")] public string PayerOprCode { get; set; }
    [XmlElement("error")] public PaymentError Error { get; set; }
    [XmlElement("issue_invoice")] public IssueInvoice IssueInvoice { get; set; }
}