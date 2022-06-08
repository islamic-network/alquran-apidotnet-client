using System.Net;
using System.Text;
using alquran.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace alquran;

public static partial class AlQuranV1
{
    private static IEnumerable<string>? _types;
    private static IEnumerable<string>? _langs;
    private static IEnumerable<string>? _formats;
    private static Dictionary<string, string> _endPoints;
    private const string Base = "https://api.alquran.cloud/v1/";
    private static readonly RestClient Client;

    static AlQuranV1()
    {
        _endPoints = new Dictionary<string, string>
        {
            {"types", "edition/type"},
            {"languages", "edition/language"},
            {"formats", "edition/format"},
        };
        Client = new RestClient(Base);
        _types = Types;
        _langs = Languages;
        _formats = Formats;
    }

    public static IEnumerable<Edition>? Editions(string format = "", string lang = "", string type = "")
    {
        var strings = new[]
        {
            ("format", format),
            ("language", lang),
            ("type", type)
        };
        try
        {
            return Client.GetData<IEnumerable<Edition>>(
                new RestRequest(ConcatNonEmpty("edition", strings))
            );
        }
        catch (Exception exception)
        {
            // I had an idea of having the values of formats, languages, and types lists within
            // the tuple so I don't have to send them again in validation (I could have written
            // a loop or something, but it would be unnecessary to load the 3 lists from the 
            // APIs just to save 3 lines of code.
            ValidateList(_formats, strings[0].Item2, strings[0].Item1); // validate format
            ValidateList(_langs, strings[1].Item2, strings[1].Item1); // validate lang
            ValidateList(_types, strings[2].Item2, strings[2].Item1); // validate type
            throw new Exception("See inner exception", exception);
        }
    }

    public static Quran? Quran(string edition = "")
    {
        var rs = new StringBuilder("quran");
        if (!string.IsNullOrEmpty(edition))
            rs.Append('/' + edition);

        try
        {
            return Client.GetData<Quran>(new RestRequest(rs.ToString()));
        }
        catch (Exception exception)
        {
            // note that this API, currently, will not return 404 if the edition was not found, instead it returns 
            // a default edition.
            ValidateEdition(edition);
            throw new Exception("See inner exception", exception);
        }
    }

    public static Juz? Juz(int juz, string edition = "", int offset = -99, int limit = -99)
    {
        // if (juz is > 30 or < 1)
        //     throw new IndexOutOfRangeException($"Juz number should be between 1 and 30, given Juz: {juz}");
        // var rs = new StringBuilder("juz" + "/" + juz);
        // if (!string.IsNullOrEmpty(edition))
        //     rs.Append('/' + edition);
        // if (offset != -99 || limit != -99)
        // {
        //     rs.Append('?');
        //     if (offset != -99) rs.Append("offset=" + offset + '&');
        //     if (limit != -99) rs.Append("limit=" + limit + '&');
        // }
        //
        // try
        // {
        //     return Client.GetData<Juz>(new RestRequest(rs.ToString()));
        // }
        // catch (Exception exception)
        // {
        //     // note that this API, currently, will not return 404 if the edition was not found, instead it returns 
        //     // a default edition.
        //     ValidateEdition(edition);
        //     throw new Exception("See inner exception", exception);
        // }
        return GenaricGet<Juz>("juz", juz, edition, offset, limit);
    }

    public static object? Surah(int surah = -99, string edition = "", int offset = -99, int limit = -99)
    {
        ValidateSurah(surah);
        var r = edition.Split(',');
        if (surah == -99 || r.Length > 1) return SurahOrAyah<int, Surah>("surah", surah, edition, offset, limit);
        return SingleSurahOrAyah<int, Surah>("surah", surah, edition, offset, limit);
    }

    public static object? Ayah(int ayah, string edition = "")
    {
        ValidateAyah(ayah);
        var r = edition.Split(',');
        if (r.Length > 1) return SurahOrAyah<int, Ayah>("ayah", ayah, edition);
        return SingleSurahOrAyah<int, Ayah>("ayah", ayah, edition);
    }

    public static object? Ayah(int surah, int ayah, string edition = "")
    {
        ValidateAyah(ayah);
        ValidateSurah(surah);
        string ayahWithSurah = surah + ":" + ayah;
        var r = edition.Split(',');
        if (r.Length > 1) return SurahOrAyah<string, Ayah>("ayah", ayahWithSurah, edition);
        return SingleSurahOrAyah<string, Ayah>("ayah", ayahWithSurah, edition);
    }

    public static SearchResult? Search(string keyword, string editionOrLanguage, int surah = -99)
    {
        var rs = new StringBuilder("search/" + keyword);
        if (surah == -99) rs.Append("/all");
        else
        {
            ValidateSurah(surah);
            rs.Append("/" + surah);
        }

        rs.Append("/" + editionOrLanguage);
        try
        {
            return Client.GetData<SearchResult>(new RestRequest(rs.ToString()));
        }
        catch (Exception e)
        {
            if (editionOrLanguage.Length == 2)
                ValidateList(Languages, editionOrLanguage, "language");
            else ValidateEdition(editionOrLanguage);
            throw new Exception("See inner exception", e);
        }
    }

    public static Manzil? Manzil(int manzil, string edition, int offset = -99, int limit = -99)
    {
        ValidateManzil(manzil);
        return GenaricGet<Manzil>("manzil", manzil, edition, offset, limit);
    }

    public static Ruku? Ruku(int ruku, string edition, int offset = -99, int limit = -99)
    {
        ValidateRuku(ruku);
        return GenaricGet<Ruku>("ruku", ruku, edition, offset, limit);
    }


    public static Page? Page(int page, string edition, int offset = -99, int limit = -99)
    {
        ValidatePage(page);
        return GenaricGet<Page>("page", page, edition, offset, limit);
    }

    public static Hizb? Hizb(int hizb, string edition, int offset = -99, int limit = -99)
    {
        ValidateHizb(hizb);
        return GenaricGet<Hizb>("hizbQuarter", hizb, edition, offset, limit);
    }

    public static (IEnumerable<Ayah>, Edition) Sajda(string edition = "")
    {
        RestRequest request;
        if (edition != "")
        {
            ValidateEdition(edition);
            request = new RestRequest("sajda/" + edition);
        }
        else
            request = new RestRequest("sajda");


        var rspons = Client.ExecuteAsync(request).Result;
        if (rspons.StatusCode != HttpStatusCode.OK)
            throw new Exception(
                "Couldn't process the request: " + request.Resource + " HttpsStatusCode: " + rspons.StatusCode);
        (IEnumerable<Ayah>, Edition) r;
        r.Item1 = JObject.Parse(rspons.Content)["data"]["ayahs"].ToObject<IEnumerable<Ayah>>();
        r.Item2 = JObject.Parse(rspons.Content)["data"]["edition"].ToObject<Edition>();
        return r;
    }
}