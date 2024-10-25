namespace Criptografie;

public class CifrulVigenere : SubstitutiePolialfabetica
{
    private List<char> alphabet = new("abcdefghijklmnopqrstuvwxyz");
    public override string plainText { get; set; } = string.Empty;
    public override string cipherText { get; set; } = string.Empty;
    
    public string keyword = string.Empty;
    public override List<Dictionary<char, char>> EncryptionKey { get; set; } = [];
    public override List<Dictionary<char, char>> DecryptionKey { get; set; } = [];
    
    public override void Encrypt()
    {
        Console.WriteLine($"plaintext : {plainText}");
        
        for (int i = 0, fromKeywordIdx = 0; i < plainText.Length; i++, fromKeywordIdx++)
        {
            var c = plainText[i];
            if (!alphabet.Contains(c))
            {
                cipherText += c;
                fromKeywordIdx--;
                continue;
            }
            
            char fromKeyword = keyword[fromKeywordIdx % keyword.Length];
            cipherText += EncryptionKey[alphabet.IndexOf(c)][fromKeyword];
        }
        
        Console.WriteLine($"ciphertext: {cipherText}");
    }

    public override void Decrypt()
    {
        string decrypted = string.Empty;
        
        for (int i = 0, fromKeywordIdx = 0; i < cipherText.Length; i++, fromKeywordIdx++)
        {
            var c = cipherText[i];
            if (!alphabet.Contains(c))
            {
                decrypted += c;
                fromKeywordIdx--;
                continue;
            }
            
            char fromKeyword = keyword[fromKeywordIdx % keyword.Length];
            decrypted += DecryptionKey[alphabet.IndexOf(fromKeyword)][c];
        }
        
        Console.WriteLine($"decrypted : {decrypted}");
    }

    public override void GenerateKeys()
    {
        for(int i = 0; i < 26; i++)
        {
            List<char> alphabetPermutation = [..alphabet];
            Shuffle(ref alphabetPermutation, i);
            
            var mapped = alphabet.Zip(alphabetPermutation, (key, value) => new { key, value })
                .ToDictionary(x => x.key, x => x.value);
            
            EncryptionKey.Add(mapped);
            DecryptionKey.Add(mapped.ToDictionary(x => x.Value, x => x.Key));
        }
    }
    
    private void Shuffle(ref List<char> list, int n)
    {
        list = [..list.TakeLast(list.Count - n), ..list.Take(n)];
    }
}