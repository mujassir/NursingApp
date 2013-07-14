using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using EncrptDecrpt;

namespace EncrptDecrpt
{
    /// <summary>
    /// It is recommended that Des and TripleDes algorithm don't use to encrpt heavy text, because these algorithm is slower in nature. 
    /// </summary>
    public class Encrption
    {
        
        #region Enumerator

        //types of symmetric encyption
        public enum EncryptionTypes
        {
            DES,
            RC2,
            Rijndael,
            TripleDES
        }

        //direction fo the transform
        public enum TransformDirection
        {
            Encrypt,
            Decrypt
        }

        #endregion

        #region Constant

        private const string DEFAULT_PASSWORD = "SDN:!@#$";
        private const EncryptionTypes DEFAULT_ALGORITHM = EncryptionTypes.Rijndael;

        #endregion

        #region Variables

        private byte[] m_Key; // cryptographic secret key
        private byte[] m_IV; //initialization vector
        private byte[] m_SaltByteArray = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 }; //default salt for strengthen the key.

        private EncryptionTypes m_EncryptionType = DEFAULT_ALGORITHM;
        private string m_strPassword = DEFAULT_PASSWORD;
        private bool m_bCalculateNewKeyAndIV = true;

        #endregion

        #region Constructors

        public Encrption()
        {
        }

        public Encrption(EncryptionTypes type)
        {
            m_EncryptionType = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Read or Write/set Encrption Type. Use EncrptionTypes Enumerator.
        /// </summary>
        public EncryptionTypes EncryptionType
        {
            get { return m_EncryptionType; }
            set
            {
                if (m_EncryptionType != value)
                {
                    m_EncryptionType = value;
                    m_bCalculateNewKeyAndIV = true;
                }
            }
        }

        /// <summary>
        ///	Passsword Key Property.
        /// The password key used when encrypting / decrypting the plain text.
        /// </summary>
        public string Password
        {
            get { return m_strPassword; }
            set
            {
                if (m_strPassword != value)
                {
                    m_strPassword = value;
                    m_bCalculateNewKeyAndIV = true;
                }
            }
        }

        /// <summary>
        /// The Salt that is used. This can only be Write/set. Use to strenghen the key.
        /// </summary>
        public byte[] Salt
        {
            set
            {
                if (m_SaltByteArray != value)
                {
                    m_SaltByteArray = value;
                    m_bCalculateNewKeyAndIV = true;
                }
            }
        }

        #endregion

        #region Symmetric Engine

        /// <summary>
        ///	Performs the actual enc/dec.
        /// </summary>
        /// <param name="inputBytes">input byte array</param>
        /// <param name="Encrpyt">wheather or not to perform enc/dec</param>
        /// <returns>byte array output</returns>
        private byte[] Transform(byte[] inputBytes, TransformDirection direction)
        {
            //get the correct transform
            ICryptoTransform transform = GetEncryptionTransform(direction);

            //memory stream for output
            MemoryStream memStream = new MemoryStream();

            try
            {
                //setup the crypto stream - output written to memstream
                CryptoStream cryptStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);

                //write data to cryption engine
                cryptStream.Write(inputBytes, 0, inputBytes.Length);

                //flush the block.
                cryptStream.FlushFinalBlock();

                //get result
                byte[] output = memStream.ToArray();

                //Must close the crpto Stream.
                cryptStream.Close();

                return output;
            }
            catch (Exception e)
            {
                //throw an error
                throw new Exception("Error in symmetric engine. Error : " + e.Message, e);
            }
        }

        /// <summary>
        ///	returns the symmetric engine and creates the encyptor/decryptor
        /// </summary>
        /// <param name="encrypt">whether to return a encrpytor or decryptor</param>
        /// <returns>ICryptoTransform</returns>
        private ICryptoTransform GetEncryptionTransform(TransformDirection direction)
        {
            if (m_bCalculateNewKeyAndIV)
                CalculateNewKeyAndIV();
            if (direction == TransformDirection.Encrypt)
                return GetEncryptionAlgorithm().CreateEncryptor(m_Key, m_IV);
            else
                return GetEncryptionAlgorithm().CreateDecryptor(m_Key, m_IV);
        }

        /// <summary>
        ///	returns the specific symmetric algorithm acc. to the cryptotype
        /// </summary>
        /// <returns>SymmetricAlgorithm</returns>
        private SymmetricAlgorithm GetEncryptionAlgorithm()
        {
            switch (m_EncryptionType)
            {
                case EncryptionTypes.DES:
                    return DES.Create();
                    break;
                case EncryptionTypes.RC2:
                    return RC2.Create();
                    break;
                case EncryptionTypes.Rijndael:
                    return Rijndael.Create();
                    break;
                default:
                    return TripleDES.Create(); //default
            }
        }

        /// <summary>
        ///	Calculates the key and IV acc. to the symmetric method from the password
        ///	key and IV size dependant on symmetric method
        /// </summary>
        private void CalculateNewKeyAndIV()
        {
            //use salt so that key cannot be found with dictionary attack
            PasswordDeriveBytes password = new PasswordDeriveBytes(m_strPassword, m_SaltByteArray);
            SymmetricAlgorithm algorithm = GetEncryptionAlgorithm();
            m_Key = password.GetBytes(algorithm.KeySize / 8);
            m_IV = password.GetBytes(algorithm.BlockSize / 8);
        }

        #endregion

        #region Encryption

        /// <summary>
        /// Encrypts a byte array
        /// </summary>
        /// <param name="inputData">byte array to encrypt</param>
        /// <returns>an encrypted byte array</returns>
        public byte[] Encrypt(byte[] inputData)
        {
            return Transform(inputData, TransformDirection.Encrypt);
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="inputText">text to encrypt</param>
        /// <returns>an encrypted string</returns>
        public string Encrypt(string inputText)
        {
            //convert back to a string
            return Encrypt(inputText.ToByteArrayUTF8()).ToBase64String();
        }

        /// <summary>
        /// Static encrypt method
        /// </summary>
        public static string EncryptText(string inputText)
        {
            return EncryptText(inputText, DEFAULT_ALGORITHM);
        }

        /// <summary>
        /// Static encrypt method
        /// </summary>
        public static string EncryptText(string inputText, EncryptionTypes type)
        {
            return new Encrption(type).Encrypt(inputText);
        }

        #endregion

        #region Decryption

        /// <summary>
        ///	decrypts a string
        /// </summary>
        /// <param name="inputText">string to decrypt</param>
        /// <returns>a decrypted string</returns>
        public string Decrypt(string inputText)
        {
            //convert back to a string
            return Decrypt(inputText.ToByteArrayBase64()).ToUTF8String();
        }

        /// <summary>
        /// decrypts a byte array
        /// </summary>
        /// <param name="inputData">byte array to decrypt</param>
        /// <returns>a decrypted byte array</returns>
        public byte[] Decrypt(byte[] inputData)
        {
            return Transform(inputData, TransformDirection.Decrypt);
        }

        /// <summary>
        /// Static Decrypt method
        /// </summary>
        public static string DecryptText(string inputText)
        {
            return DecryptText(inputText, DEFAULT_ALGORITHM);
        }

        /// <summary>
        /// Static Decrypt method
        /// </summary>
        public static string DecryptText(string inputText, EncryptionTypes type)
        {
            return new Encrption(type).Decrypt(inputText);
        }

        #endregion

    }
}
