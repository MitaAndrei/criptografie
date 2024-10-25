using System.Threading.Channels;

namespace Criptografie;

public class CifrulPlayfair
{
    public char[,] table = new char[5, 5];

    public string plaintext = "";
    public string keyword;
    private string ciphertext = "";
    private List<string> digrams;
    private List<string> encryptedDigrams = [];
    private List<string> decryptedDigrams = [];

    
    public CifrulPlayfair(string plaintext, string keyword)
    {
        this.plaintext = plaintext;
        this.keyword = keyword;

        CreateTable();
        digrams = BreakIntoDigrams(this.plaintext);
        Console.Write("digrams  : ");
        digrams.ForEach(d => Console.Write($"{d} "));
        Console.WriteLine();
    }

    public void Encrypt()
    {
        foreach (var digram in digrams)
        {
            var idx1 = CoordinatesOf(table, digram[0]);
            var idx2 = CoordinatesOf(table, digram[1]);
            string encrypted = "";

            if (idx1.i == idx2.i)
            {
                encrypted += idx1.j < 4 ? table[idx1.i, idx1.j + 1] : table[idx1.i, 0];
                encrypted += idx2.j < 4 ? table[idx2.i, idx2.j + 1] : table[idx2.i, 0];
            }
            else if (idx1.j == idx2.j)
            {
                encrypted += idx1.i < 4 ? table[idx1.i + 1, idx1.j] : table[0, idx1.j];
                encrypted += idx2.i < 4 ? table[idx2.i + 1, idx2.j] : table[0, idx2.j];
            }
            else
            {
                encrypted += table[idx1.i, idx2.j];
                encrypted += table[idx2.i, idx1.j];
            }
            encryptedDigrams.Add(encrypted);
        }
        Console.Write("encrypted: ");
        encryptedDigrams.ForEach(d => Console.Write($"{d} "));
        Console.WriteLine();
        ciphertext = string.Join("", encryptedDigrams);
    }

    public void Decrypt()
    {
        foreach (var digram in encryptedDigrams)
        {
            var idx1 = CoordinatesOf(table, digram[0]);
            var idx2 = CoordinatesOf(table, digram[1]);
            string decrypted = "";

            if (idx1.i == idx2.i)
            {
                decrypted += idx1.j > 0 ? table[idx1.i, idx1.j - 1] : table[idx1.i, 4];
                decrypted += idx2.j > 0 ? table[idx2.i, idx2.j - 1] : table[idx2.i, 4];
            }
            
            else if (idx1.j == idx2.j)
            {
                decrypted += idx1.i > 0 ? table[idx1.i - 1, idx1.j] : table[4, idx1.j];
                decrypted += idx2.i > 0 ? table[idx2.i - 1, idx2.j] : table[4, idx2.j];
            }
            else
            {
                decrypted += table[idx1.i, idx2.j];
                decrypted += table[idx2.i, idx1.j];
            }
            decryptedDigrams.Add(decrypted);
        }
        Console.Write("decrypted: ");
        decryptedDigrams.ForEach(d => Console.Write($"{d} "));
        Console.WriteLine();
        ciphertext = string.Join("", decryptedDigrams);
    }
    
    private List<string> BreakIntoDigrams(string text)
    {
        text = text.Replace(" ", "");
        List<string> result = [];
        
        for (int i = 0; i < text.Length; i++)
        {
            if (i == text.Length - 1)
                result.Add($"{text[i]}x");
            
            else if(text[i] == text[i+1])
                result.Add($"{text[i]}x");
            
            else
            {
                result.Add($"{text[i]}{text[i + 1]}");
                i++;
            }
        }
            
        return result;
    }
    
    private void CreateTable()
    {
        string alphabet = "abcdefghiklmnopqrstuvwxyz"; // fara j
        var uniqueChars = keyword.Replace("j", "i").Intersect(alphabet).ToArray();
        string usedChars = string.Join("", uniqueChars);
        
        for (int i = 0, idx = -1; i < 5; i++)
            for (int j = 0; j < 5; j++)
            {
                if(idx++ < uniqueChars.Length - 1)
                    table[i, j] = uniqueChars[idx];
                else
                    foreach (var letter in alphabet)
                        if (!usedChars.Contains(letter))
                        {
                            table[i, j] = letter;
                            usedChars += letter;
                            break;
                        }
            }
    }
    
    public static (int i, int j) CoordinatesOf(char [,] matrix, char value)
    {
        for (int x = 0; x < 5; x++)
            for (int y = 0; y < 5; y++)
                if (matrix[x, y] == value)
                    return (x, y);

        return (-1, -1);
    }
}
