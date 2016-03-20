namespace KMZI_WPF.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Encrypt
    {
        public static string CryptMessage(string message, string originalKey)
        {
            StringBuilder cryptedMessage = new StringBuilder();
            string key = originalKey.ToUpper();

            if (!Validator.CheckKeySymbols(key))
            {
                return "Error";
            }

            message = Validator.CheckDivisible(message, key);
            char[,] blockMessage = FillCharMatrix(message, key.Length);
            char[,] arrMartix = ArrangeMatrix(blockMessage, key);

            cryptedMessage = TransformMatrixInCryptogram(arrMartix);

            return cryptedMessage.ToString();
        }

        // Create matrix and fill the cells with respective letters
        private static char[,] FillCharMatrix(string message, int blockLength)
        {
            int rows = message.Length / blockLength;
            char[,] messageMatrix = new char[rows, blockLength];
            int count = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < blockLength; col++)
                {
                    messageMatrix[row, col] = message[count];
                    ++count;
                }
            }

            return messageMatrix;
        }

        private static char[,] ArrangeMatrix(char[,] originalMatrix, string key)
        {
            List<int> keyValues = KeyValues.TakeKeyValues(key).ToList();
            int[] orderedKeyValues = keyValues.OrderBy(k => k).ToArray();

            int rows = originalMatrix.GetLength(0);
            int cols = originalMatrix.GetLength(1);
            char[,] arrangeMatrix = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    arrangeMatrix[row, col] = originalMatrix[row, keyValues.IndexOf(orderedKeyValues[col])];
                }
            }


            return arrangeMatrix;
        }

        private static StringBuilder TransformMatrixInCryptogram(char[,] arrMartix)
        {
            StringBuilder cryptogram = new StringBuilder();

            int rows = arrMartix.GetLength(0);
            int cols = arrMartix.GetLength(1);

            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    cryptogram.Append(arrMartix[row, col]);
                }
            }

            return cryptogram;
        }
    }
}
