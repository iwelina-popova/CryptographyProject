namespace KMZI_WPF.Logic
{
    public static class KeyValues
    {
        public static int[] TakeKeyValues(string key)
        {
            int[] keyValues = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                keyValues[i] = (key[i] - 'А');
            }

            return keyValues;
        }
    }
}
