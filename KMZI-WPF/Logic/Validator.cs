namespace KMZI_WPF.Logic
{
    using System.Text;

    public static class Validator
    {
        // Check if the key has diffrent symbols
        public static bool CheckKeySymbols(string key)
        {
            for (int i = 1; i < key.Length; i++)
            {
                if (key.IndexOf(key[i - 1], i) != -1)
                {
                    return false;
                }
            }
            return true;
        }

        // Check if message is divisible by the key,
        // if not add symbols while it is divisible
        public static string CheckDivisible(string message, string key)
        {
            StringBuilder finalMessage = new StringBuilder();
            finalMessage.Append(message);

            while (finalMessage.Length % key.Length != 0)
            {
                finalMessage.Append('_');
            }

            finalMessage.Replace(' ', '_');

            return message = finalMessage.ToString().ToUpper();
        }
    }
}
