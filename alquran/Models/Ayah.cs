using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace alquran.Models;

public class Ayah
{
    public int number { get; set; }
    public string audio { get; set; }
    public IEnumerable<string> audioSecondary { get; set; }
    public string text { get; set; }
    public Surah surah { get; set; }
    public int numberInSurah { get; set; }
    public int juz { get; set; }
    public int manzil { get; set; }
    public int page { get; set; }
    public int ruku { get; set; }
    public int hizbQuarter { get; set; }

    public object sajda
    {
        set
        {
            if (value is bool b) _sjod = b;
            else
            {
                _sajda = JsonConvert.DeserializeObject<Sajda>(value.ToString());
            }
        }
        get
        {
            if (_sjod != null)
            {
                return _sjod;
            }

            return _sajda;
        }
    }

    private Sajda _sajda;
    private bool? _sjod;
}