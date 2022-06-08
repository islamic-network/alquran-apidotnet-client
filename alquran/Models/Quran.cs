namespace alquran.Models;

public class Quran
{
    public IEnumerable<Surah> surahs { get; set; }
    public Edition edition { get; set; }
}