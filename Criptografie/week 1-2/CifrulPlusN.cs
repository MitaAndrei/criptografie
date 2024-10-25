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
        n %= 26;
        list = [..list.TakeLast(list.Count - n), ..list.Take(n)];
    }
}