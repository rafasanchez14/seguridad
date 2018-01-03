using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace misocket1.Code
{
    class RSA
    {
        public RSACryptoServiceProvider RSAService { get; set; }

        public RSA()
        {
            this.RSAService = new RSACryptoServiceProvider();
        }
       

        public  byte[] EncrypText(string mensaje, string publica)
        {
            RSAParameters RSAKeyInfo = this.RSAService.ExportParameters(false);
            this.RSAService.FromXmlString(publica);
            byte[] encryptedData = this.RSAService.Encrypt(Encoding.ASCII.GetBytes(mensaje), false);
               return encryptedData;
        }

        



        






    }
}
