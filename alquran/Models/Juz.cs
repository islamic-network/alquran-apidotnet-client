namespace alquran.Models;

public class Juz : IBaseGeneral
{
    public int number { get; set; }
    public IEnumerable<Ayah> ayahs { get; set; }
    public IDictionary<string, Surah> surahs { get; set; }
    public Edition edition { get; set; }
}