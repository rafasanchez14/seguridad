using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using misocket1.Code;
using System.Security.Cryptography;

namespace misocket1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sim = genkeysim();
            Console.WriteLine(sim);
            const int keySize = 1024;
            string publicKey = Recibir();              
            string encrypted = EncryptText(sim, keySize, publicKey);
            Enviar(encrypted);
            Console.WriteLine("Dato a encriptar\n");
            string data = Console.ReadLine();
            Console.WriteLine("Encriptado\n");
            Console.WriteLine(Encriptar(data, sim));
            string y = Encriptar(data, sim);
            Console.WriteLine("Desencriptado\n");
            Console.WriteLine(Desencriptar(y, sim));
            Console.ReadKey();
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
        public static string EncryptText(string text, int keySize, string publicKeyXml)
        {
            var encrypted = Encrypt(Encoding.UTF8.GetBytes(text), keySize, publicKeyXml);
            return Convert.ToBase64String(encrypted);
        }

        public static byte[] Encrypt(byte[] data, int keySize, string publicKeyXml)
        {
            if (data == null || data.Length == 0) throw new ArgumentException("Data are empty", "data");
            int maxLength = GetMaxDataLength(keySize);
            if (data.Length > maxLength) throw new ArgumentException(String.Format("Maximum data length is {0}", maxLength), "data");
            if (!IsKeySizeValid(keySize)) throw new ArgumentException("Key size is not valid", "keySize");
            if (String.IsNullOrEmpty(publicKeyXml)) throw new ArgumentException("Key is null or empty", "publicKeyXml");

            using (var provider = new RSACryptoServiceProvider(keySize))
            {
                provider.FromXmlString(publicKeyXml);
                return provider.Encrypt(data, _optimalAsymmetricEncryptionPadding);
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
        public static string genkeysim()
        {
             Random _random = new Random();
            string key="";
            for (int i = 1; i <= 20; i++)
            {


                int num = _random.Next(0, 26);
                char let = (char)('a' + num);
                key = key + let;
            }

            byte[] bytes = Encoding.Unicode.GetBytes(key);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }


        public static string Encriptar(string texto, string key)
        {
            try
            {

               

                byte[] keyArray;

                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

                //Se utilizan las clases de encriptación MD5

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

            }
            catch (Exception)
            {

            }
            return texto;
        }
        public static string Desencriptar(string textoEncriptado, string key)
        {
            try
            {
               
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception)
            {

            }
            return textoEncriptado;
        }


    }
}
