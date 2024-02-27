namespace Netopia.Encrypt.Models;

[Serializable]
public class Recurrence
{
    [XmlAttribute("interval_day")] public int IntervalDay { get; set; }
    [XmlAttribute("payments_no")] public int PaymentsNo { get; set; }
}