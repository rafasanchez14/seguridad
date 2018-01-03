using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace misocket2.Code
{
    class RSA
    {

        public RSACryptoServiceProvider RSAService { get; set; }
        
        public RSA()
        {
            this.RSAService = new RSACryptoServiceProvider(1024);
        }
        public byte[] CreatePublicKey()
        {
            string xmlPublicKey = this.RSAService.ToXmlString(false);
            return Encoding.ASCII.GetBytes(xmlPublicKey);
        }
        public byte[] CreatePrivatecKey()
        {
            string xmlPrivateKey = this.RSAService.ToXmlString(true);
            return Encoding.ASCII.GetBytes(xmlPrivateKey);
        }

        public byte[] DecrypText(string mensaje, string privada)
        {
            
            byte[] encryptedData;
            
           
                    this.RSAService.FromXmlString(privada);
                    var base64 = Convert.FromBase64String(mensaje);
                    encryptedData = this.RSAService.Decrypt(base64, false);
      
            return encryptedData;
  
        }






    }
}
