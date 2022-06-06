// ReSharper disable InconsistentNaming
#pragma warning disable CS8618
namespace alquran.Models;

public class Edition
{
    public string identifier { get; set; }
    public string language { get; set; }
    public string name { get; set; }
    public string englishName { get; set; }
    public string format { get; set; }
    public string type { get; set; }
    public string? direction { get; set; }
}