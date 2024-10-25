// See https://aka.ms/new-console-template for more information

using Criptografie;

// var x = new CifruSubstitutieMonoalfabetica();
//
// x.plainText = "helloa worl??d!";
// x.Encrypt();
// x.Decrypt();

// var p = new CifrulCezar(){ plainText = "gb trg gb gur bgure fvqr!" };
// p.GenerateKeys();
// p.Encrypt();
// p.Decrypt();

// var vigenere = new CifrulVigenere()
// {
//     plainText = "michigan technological university",
//     keyword = "houghton"
// };
// vigenere.GenerateKeys();
// vigenere.Encrypt();
// vigenere.Decrypt();

var playfair = new CifrulPlayfair("comsec means communications security", "galois");
playfair.Encrypt();
playfair.Decrypt();

Console.WriteLine();
for (int i = 0; i < 5; i++)
{
    for (int j = 0; j < 5; j++)
    {
        Console.Write(playfair.table[i,j]);
    }
    Console.WriteLine();
}

