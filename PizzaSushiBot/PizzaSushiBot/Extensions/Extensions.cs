using System;
using CustomLogger;
using PizzaSushiBot.Exceptions;

namespace PizzaSushiBot
{
    public static class Extensions
    {
        public static string AlignCenter(this string text, int width)
        {
            char filler = ' ';
            int fillerQty = (width - text.Length) / 2;
            string gap = new string(filler, fillerQty);
            string resultString = gap + text + gap;

            return resultString;
        }

        public static void AlignCenterAndPrint(this string text, int width)
        {
            char filler = ' ';
            int fillerQty = (width - text.Length) / 2;
            string gap = new string(filler, fillerQty);
            string resultString = gap + text + gap;

            Console.WriteLine(resultString);
        }

        public static void AlignRightAndPrint(this string text, int width)
        {
            string resultString = new string(' ', (width - text.Length)) + text;

            Console.WriteLine(resultString); 
        }

        public static string AlignedLeftRight(this string[] stringArr, char filler, int width)
        {
            if (stringArr.Length == 2)
            {
                int fillerQty = width - stringArr[0].Length - stringArr[1].Length;
                string resultString = stringArr[0] + new string(filler, fillerQty) + stringArr[1];

                return resultString;
            }
            else
                throw new InvalidParameterLengthException();
        }

        public static string SpreadEvenly(this string[] stringArr, char filler, int width)
        {
            int fillerQty = width;
            string resultString = string.Empty;
            if (stringArr.Length >= 2)
            {
                for (int i = 0; i < stringArr.Length; i++)
                    fillerQty -= stringArr[i].Length;

                fillerQty /= stringArr.Length - 1;
                for (int i = 0; i < stringArr.Length; i++)
                {
                    if (i == stringArr.Length - 1)
                        resultString += stringArr[i];
                    else
                        resultString += stringArr[i] + new string(filler, fillerQty);
                }
                return resultString;
            }
            else
            {
                Logger.Error("Invalid Parameter Length Exception");
                throw new InvalidParameterLengthException("InvalidParameterLength Exception",
                    "Value of array Length property should at least be equal to 2.");
            }
        }
    }
}
