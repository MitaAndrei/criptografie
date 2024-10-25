namespace Criptografie;

public abstract class SubstitutiePolialfabetica
{
    public abstract string plainText { get; set; }
    public abstract string cipherText { get; set; }
    public abstract List<Dictionary<char, char>> EncryptionKey { get; set; }
    public abstract List<Dictionary<char, char>> DecryptionKey { get; set; }

    public abstract void Encrypt();
    public abstract void Decrypt();
    public abstract void GenerateKeys();

}