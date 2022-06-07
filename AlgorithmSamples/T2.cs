namespace AlgorithmSamples
{
    public static class T2
    {
        public static string[] Load()
        {
            var lines = File.ReadAllLines(@"c:\temp\wordlist.txt");
            foreach (var line in lines)
            {

                Console.WriteLine(line);
            }
            return lines;
        }

        public static string Translate(string word)
        {
            var resultA = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                resultA[i] = TranslateLetter(word[i]);
            }
            return new string(resultA);
        }
        public static char TranslateLetter(char letter) => letter switch
        {
            'a' => '0',
            'e' => '0',
            'i' => '0',
            'o' => '0',
            'u' => '0',
            _ => '1',
        };
        public static Dictionary<string, List<string>> WordsByT2 = new Dictionary<string, List<string>>();

        public static void InitDict()
        {
            var lines = Load();
            foreach (var word in lines)
            {
                var t2 = Translate(word);
                if (WordsByT2.TryGetValue(t2, out var list))
                {
                    list.Add(word);
                }
                else
                {
                    WordsByT2[t2] = new() { word
};
                }
                Console.WriteLine(word + " " + t2);

            }
        }
    }
}