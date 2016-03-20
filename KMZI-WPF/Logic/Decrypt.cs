namespace KMZI_WPF.Logic
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Decrypt
    {
        // Create matrix and fill the cells with respective letters
        private static char[,] FillCharMatrix(string message, int blockLength)
        {
            int rows = message.Length / blockLength;
            char[,] messageMatrix = new char[rows, blockLength];
            int count = 0;

            for (int col = 0; col < blockLength; col++)
            {
                for (int row = 0; row < rows; row++)
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
                    arrangeMatrix[row, keyValues.IndexOf(orderedKeyValues[col])] = originalMatrix[row, col];
                }
            }

            return arrangeMatrix;
        }

        private static StringBuilder TransformMatrixInMessage(char[,] arrMartix)
        {
            StringBuilder message = new StringBuilder();

            int rows = arrMartix.GetLength(0);
            int cols = arrMartix.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (arrMartix[row, col] == '_')
                    {
                        message.Append(' ');
                    }
                    else
                    {
                        message.Append(arrMartix[row, col]);
                    }
                }
            }


            return message;
        }

        public static string DeCryptMessage(string cryptogram, string key)
        {
            StringBuilder deCryptedMessage = new StringBuilder();
            if (!Validator.CheckKeySymbols(key))
            {
                return "Error";
            }

            cryptogram = Validator.CheckDivisible(cryptogram, key);
            char[,] blockMessage = FillCharMatrix(cryptogram, key.Length);
            char[,] arrMartix = ArrangeMatrix(blockMessage, key);

            deCryptedMessage = TransformMatrixInMessage(arrMartix);

            return deCryptedMessage.ToString();
        }
    }
}
