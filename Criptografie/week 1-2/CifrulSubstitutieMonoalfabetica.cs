using System.Security.Cryptography;

namespace Criptografie;

public class CifrulSubstitutieMonoalfabetica : SubstitutiePolialfabetica
{
    public override string plainText { get; set; }
    public override string cipherText { get; set; }
    public override List<Dictionary<char, char>> EncryptionKey { get; set; }
    public override List<Dictionary<char, char>> DecryptionKey { get; set; }
    
    public override void Encrypt()
    {
        Console.WriteLine($"plaintext : {plainText}");
        var keysMapped = EncryptionKey[0];
        var x = plainText.Select(c => keysMapped.ContainsKey(c) ? keysMapped[c] : c);
        cipherText = string.Concat(x);

        Console.WriteLine($"ciphertext: {cipherText}");
    }

    public override void Decrypt()
    {
        var keysMapped = DecryptionKey[0];
        var x = cipherText.Select(c => keysMapped.ContainsKey(c) ? keysMapped[c] : c);
        string decryptedText = string.Concat(x);

        Console.WriteLine($"decrypted : {decryptedText}");
    }

    public override void GenerateKeys()
    {
        List<char> alphabet = new("abcdefghijklmnopqrstuvwxyz");
        List<char> alphabetPermutation = [..alphabet];
        Shuffle(ref alphabetPermutation);
        Console.WriteLine("a b c d e f g h i j k l m n o p q r s t u v w x y z");
        
        alphabetPermutation.ForEach(c => Console.Write($"{c} "));
        
        Console.WriteLine();

        var mapped = alphabet.Zip(alphabetPermutation, (key, value) => new { key, value })
            .ToDictionary(x => x.key, x => x.value);
        
        EncryptionKey = [mapped];
        DecryptionKey = [mapped.ToDictionary(x => x.Value, x => x.Key)];
    }

    protected virtual void Shuffle(ref List<char> list)
    {
        for (int i = list.Count - 1; i > 1; i--)
        {
            int j = RandomNumberGenerator.GetInt32(0, i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
    }
}