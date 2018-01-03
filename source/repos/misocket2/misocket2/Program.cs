using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using misocket2.Code;
using System.Security.Cryptography;

namespace misocket2
{
    class Program
    {
        
        static void Main()
        {
            const int keySize = 1024;
            string publicAndPrivateKey;
            string publicKey;
            GenerateKeys(keySize, out publicKey, out publicAndPrivateKey);
            Enviar(publicKey);
            string mensaje_enc = Recibir();
            string decrypted = DecryptText(mensaje_enc, keySize, publicAndPrivateKey);
            Console.WriteLine(decrypted);
            Console.ReadKey();
        }
        public static void Enviar(String mensaje)
        {

            Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint miDireccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            miPrimerSocket.Connect(miDireccion); // Conectamos              

            Console.WriteLine("Conectado con exito\n");

            byte[] infoEnviar = Encoding.Default.GetBytes(mensaje);
            Console.WriteLine("Enviada clave publica\n");

            miPrimerSocket.Send(infoEnviar, 0, infoEnviar.Length, 0);

            miPrimerSocket.Close();


          

        }

        public static string Recibir()
        {
            Socket miPrimerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint direccion = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            miPrimerSocket.Bind(direccion);

            miPrimerSocket.Listen(5);

            Console.WriteLine("Escuchando...");

            Socket Escuchar = miPrimerSocket.Accept();

            Console.WriteLine("Conectado con exito");


            byte[] ByRec = new byte[255];

            int a = Escuchar.Receive(ByRec, 0, ByRec.Length, 0);

            Array.Resize(ref ByRec, a);

            Console.WriteLine("Cliente dice: " + Encoding.Default.GetString(ByRec)); //mostramos lo recibido
            String mensaje = Encoding.Default.GetString(ByRec);
            miPrimerSocket.Close();
           return mensaje;


        }
        private static bool _optimalAsymmetricEncryptionPadding = false;

        public static void GenerateKeys(int keySize, out string publicKey, out string publicAndPrivateKey)
        {
            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                publicKey = provider.ToXmlString(false);
                publicAndPrivateKey = provider.ToXmlString(true);
            }

            
        }
        public static string DecryptText(string text, int keySize, string publicAndPrivateKeyXml)
        {
            var decrypted = Decrypt(Convert.FromBase64String(text), keySize, publicAndPrivateKeyXml);
            return Encoding.UTF8.GetString(decrypted);
        }

        public static byte[] Decrypt(byte[] data, int keySize, string publicAndPrivateKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicAndPrivateKeyXml)) throw new ArgumentException("Key is null or empty", "publicAndPrivateKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicAndPrivateKeyXml);
                return provider.Decrypt(data, _optimalAsymmetricEncryptionPadding);
            }
        }

        public static int GetMaxDataLength(int keySize)
        {
            if (_optimalAsymmetricEncryptionPadding)
            {
                return ((keySize - 384) / 8) + 7;
            }
            return ((keySize - 384) / 8) + 37;
        }

        public static bool IsKeySizeValid(int keySize)
        {
            return keySize >= 384 &&
                    keySize <= 16384 &&
                    keySize % 8 == 0;
        }
    }
}
