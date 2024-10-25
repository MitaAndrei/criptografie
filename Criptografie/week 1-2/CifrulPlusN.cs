namespace Criptografie;

public class CifrulPlusN : CifrulSubstitutieMonoalfabetica
{
    private int n { get; set; }

    public CifrulPlusN(int n)
    {
        this.n = n;
    }
    protected override void Shuffle(ref List<char> list)
    {
        while (n > 26)
            n %= 26;

        list = [..list.TakeLast(list.Count - n), ..list.Take(n)];
    }
}