using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Netopia.Encrypt.Models;

namespace Netopia.Encrypt;

public class EncryptionService<T> where T: PaymentRequestBase
{
    private string _x509CertificateFilePath;
    private string _privateKeyFilePath;

    public EncryptionService(string publicKeyPath, string privateKeyPath, T paymentRequest)
    {
        _x509CertificateFilePath = publicKeyPath;
        _privateKeyFilePath = privateKeyPath;
        PaymentRequest = paymentRequest;
    }
    
    public EncryptionService(string publicKeyPath, string privateKeyPath, string encryptedData, string envelopeKey)
    {
        _x509CertificateFilePath = publicKeyPath;
        _privateKeyFilePath = privateKeyPath;
        EncryptedData = encryptedData;
        EnvelopeKey = envelopeKey;
    }
    

    public string Data { get; private set; }
    public string EncryptedData { get; private set; }
    public string EnvelopeKey { get; private set; }
    
    public T PaymentRequest { get; private set; }
    
    public void Encrypt()
    {
        Data = XmlFromRequest(PaymentRequest);
        
        var bytes = Encoding.ASCII.GetBytes(Data);
        var random = new Random();
        var numArray = new byte[8];
        for (var index = 0; index < numArray.Length; ++index)
            numArray[index] = (byte) random.Next(0, (int) byte.MaxValue);
        RC4(ref bytes, numArray);

        var collection = new X509Certificate2Collection();
        collection.Import(_x509CertificateFilePath);
        var cert = collection[0];


        using var csp = cert.GetRSAPublicKey();
        if (csp == null)
            throw new Exception($"Could not get public key from path: {_x509CertificateFilePath}");
        
        
        var inArray = csp.Encrypt(numArray, RSAEncryptionPadding.Pkcs1);
        EncryptedData = Convert.ToBase64String(bytes);
        EnvelopeKey = Convert.ToBase64String(inArray);
    }
    
    public void Decrypt()
    {
        var privateKeyText = File.ReadAllText(_privateKeyFilePath);
        var privateKeyBlocks = privateKeyText.Split("-", StringSplitOptions.RemoveEmptyEntries);
        var privateKeyBytes = Convert.FromBase64String(privateKeyBlocks[1]);
            
        var csp = RSA.Create();
        csp.ImportPkcs8PrivateKey(privateKeyBytes, out _);
        
        if (csp == null)
            throw new Exception($"Could not load private key from path {_privateKeyFilePath}");
        
        var rgb = Convert.FromBase64String(EnvelopeKey);
        var bytes = Convert.FromBase64String(EncryptedData);
        var key = csp.Decrypt(rgb, RSAEncryptionPadding.Pkcs1);
        RC4(ref bytes, key);
        Data = Encoding.ASCII.GetString(bytes);

        PaymentRequest = RequestFromXml(Data);
    }


    private static string XmlFromRequest(T paymentRequest)
    {
        if (string.IsNullOrEmpty(paymentRequest.Type))
            throw new Exception($"{nameof(paymentRequest.Type)} is missing ");
            
        if (string.IsNullOrEmpty(paymentRequest.OrderId))
            throw new Exception($"{nameof(paymentRequest.OrderId)} is missing ");
        if (string.IsNullOrEmpty(paymentRequest.Signature))
            throw new Exception($"{nameof(paymentRequest.Signature)} is missing ");
        if (paymentRequest.Invoice == null || paymentRequest.Invoice.Amount<0.01m)
            throw new Exception($"{nameof(paymentRequest.Invoice.Amount)} is missing or invalid");
        if (string.IsNullOrEmpty(paymentRequest.Invoice.Currency))
            throw new Exception($"{nameof(paymentRequest.Invoice.Currency)} is missing");


        var xmlSerializer = new XmlSerializer(typeof(T));
        var stream = (Stream) new MemoryStream();
        xmlSerializer.Serialize(stream, paymentRequest, new XmlSerializerNamespaces());
        stream.Position = 0L;
        return new StreamReader(stream).ReadToEnd();
    }

    private static T RequestFromXml(string xml)
    {
        var xmlBytes = Encoding.ASCII.GetBytes(xml);
        var xmlSerializer = new XmlSerializer(typeof (T));
        var stream = (Stream) new MemoryStream(xmlBytes);
        return (T)xmlSerializer.Deserialize(stream);
    }
    
    private void RC4(ref byte[] bytes, byte[] key)
    {
        var numArray1 = new byte[256];
        var numArray2 = new byte[256];
        for (var index = 0; index < 256; ++index)
        {
            numArray1[index] = (byte) index;
            numArray2[index] = key[index % key.GetLength(0)];
        }
        var index1 = 0;
        for (var index2 = 0; index2 < 256; ++index2)
        {
            index1 = (index1 + (int) numArray1[index2] + (int) numArray2[index2]) % 256;
            (numArray1[index2], numArray1[index1]) = (numArray1[index1], numArray1[index2]);
        }
        int index3;
        var index4 = index3 = 0;
        for (var index5 = 0; index5 < bytes.GetLength(0); ++index5)
        {
            index4 = (index4 + 1) % 256;
            index3 = (index3 + (int) numArray1[index4]) % 256;
            (numArray1[index4], numArray1[index3]) = (numArray1[index3], numArray1[index4]);
            var index6 = ((int) numArray1[index4] + (int) numArray1[index3]) % 256;
            bytes[index5] ^= numArray1[index6];
        }
    }
}