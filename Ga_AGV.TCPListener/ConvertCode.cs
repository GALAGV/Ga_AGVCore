using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ga_AGV.TCPListener
{
    class ConvertCode
    {
        public static string AsciiToStr(string AsciiHexStr)
        {
            int byteLen = AsciiHexStr.Length / 2;
            byte[] byteArray = new byte[byteLen];
            for (int i = 0; i < byteLen; i++)
            {
                int asciiInt = Convert.ToInt32(AsciiHexStr.Substring(i * 2, 2), 16);
                byteArray[i] = (byte)asciiInt;
            }
            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetString(byteArray);
        }

        public static string AsciiToStr(int[] AsciiDecInt)
        {
            int byteLen = AsciiDecInt.Length;

            byte[] byteArray = new byte[byteLen];

            for (int i = 0; i < byteLen; i++)
            {
                byteArray[i] = (byte)AsciiDecInt[i];
            }
            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetString(byteArray);
        }

        public static string AsciiToStr(byte[] AsciiByte)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            return ascii.GetString(AsciiByte);
        }

        public static byte[] StrToAscii(string Str)
        {
            return Encoding.Default.GetBytes(Str);
        }

        public static string HexToBin(int HexInt)
        {
            return Convert.ToString(HexInt, 2);
        }

        public static string HexToBin(string HexStr)
        {
            int byteLen = HexStr.Length / 2;
            byte[] byteArray = new byte[byteLen];
            for (int i = 0; i < byteLen; i++)
            {
                int hexInt = Convert.ToInt32(HexStr.Substring(i * 2, 2), 16);
                byteArray[i] = (byte)hexInt;
            }
            return HexToBin(byteArray);
        }

        public static string HexToBin(byte[] HexByte)
        {
            int byteLen = HexByte.Length;
            StringBuilder byteSb = new StringBuilder();
            for (int i = 0; i < byteLen; i++)
            {
                byteSb.Append(Convert.ToString(HexByte[i], 2).PadLeft(8, '0'));
                if (i < byteLen - 1)
                {
                    byteSb.Append(" ");
                }
            }
            return byteSb.ToString();
        }


        public static int HexToDec(string HexStr)
        {
            return Convert.ToInt32(HexStr, 16);
        }

        public static int HexToDec(byte[] HexByte)
        {
            int DecInt = 0;
            for (int i = 0; i < HexByte.Length; i++)
            {
                DecInt += HexByte[i] * (16) ^ (2 * i);
            }
            return DecInt;
        }

        public static string DecToHex(int DecInt)
        {
            return Convert.ToString(DecInt, 16);
        }

        public static string DecToBin(int DecInt)
        {
            return Convert.ToString(DecInt, 2);
        }

        public static string BinToHex(string BinStr)
        {
            return string.Format("{0:x}", Convert.ToInt32(BinStr, 2));
        }

        public static int BinToDec(string BinStr)
        {
            return Convert.ToInt32(BinStr, 2);
        }
    }
}
