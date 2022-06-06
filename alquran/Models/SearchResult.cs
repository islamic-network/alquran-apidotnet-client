namespace alquran.Models;

public class SearchResult
{
    public int Count { get; set; }
    public IEnumerable<Ayah> matches { get; set; }
}