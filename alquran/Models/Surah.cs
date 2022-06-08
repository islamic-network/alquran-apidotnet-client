namespace alquran.Models;

public class Surah
{
    public int number { get; set; }
    public string name { get; set; }
    public string englishName { get; set; }
    public string englishNameTranslation { get; set; }
    public string revelationType { get; set; }
    public int numberOfAyahs { get; set; }
    public IEnumerable<Ayah> ayahs { get; set; }
    public Edition edition { get; set; }
}