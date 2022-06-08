using System.Net;
using System.Text;
using alquran.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace alquran;

public static partial class AlQuranV1
{
    public static IEnumerable<string>? Types => GetResource(_endPoints["types"]);
    public static IEnumerable<string>? Formats => GetResource(_endPoints["formats"]);
    public static IEnumerable<string>? Languages => GetResource(_endPoints["languages"]);

    private static T? GetData<T>(this RestClient t, RestRequest request)
    {
        var rspons = t.ExecuteAsync(request).Result;
        if (rspons.StatusCode != HttpStatusCode.OK)
            throw new Exception(
                "Couldn't process the request: " + request.Resource + " HttpsStatusCode: " + rspons.StatusCode);
        return JObject.Parse(rspons.Content)["data"].ToObject<T>();
    }

    private static string ConcatNonEmpty(string startpoint, (string, string)[] strings)
    {
        var result = new StringBuilder();
        result.Append(startpoint).Append('?');
        foreach (var s1 in strings)
            if (!string.IsNullOrWhiteSpace(s1.Item2))
            {
                result.Append(s1.Item1).Append('=');
                result.Append(s1.Item2).Append('&');
            }

        return result.ToString();
    }


    private static IEnumerable<TP>? SurahOrAyah<TS, TP>(string name, TS position, string edition = "", int offset = -99,
        int limit = -99)
    {
        StringBuilder rs;
        rs = position?.ToString() != "-99" ? new StringBuilder(name + "/" + position) : new StringBuilder(name);

        if (!string.IsNullOrEmpty(edition))
            rs.Append('/').Append("editions").Append('/').Append(edition);

        if (offset != -99 || limit != -99)
        {
            rs.Append('?');
            if (offset != -99) rs.Append("offset=" + offset + '&');
            if (limit != -99) rs.Append("limit=" + limit + '&');
        }

        try
        {
            return Client.GetData<IEnumerable<TP>>(new RestRequest(rs.ToString()));
        }
        catch (Exception exception)
        {
            // note that this API, currently, will not return 404 if the edition was not found, instead it returns 
            // a default edition.
            var editions = edition.Replace(" ", string.Empty).Split(',');

            foreach (var e in editions)
            {
                ValidateEdition(e);
            }

            // if (editions.Length == 1 && surah != -99)
            //     throw new MethodAccessException(
            //         "The Surah(int, string) Method must returns an IEnumerable of Surah model, to get single model use SingleSurah(int, string) instead");
            throw new Exception("See inner exception", exception);
        }
    }


    private static object? SingleSurahOrAyah<TP, TA>(string name, TP position, string edition = "", int offset = -99,
        int limit = -99)
    {
        var rs = new StringBuilder(name + "/" + position);
        if (!string.IsNullOrEmpty(edition))
            rs.Append('/').Append(edition);
        if (offset != -99 || limit != -99)
        {
            rs.Append('?');
            if (offset != -99) rs.Append("offset=" + offset + '&');
            if (limit != -99) rs.Append("limit=" + limit + '&');
        }

        try
        {
            return Client.GetData<TA>(new RestRequest(rs.ToString()));
        }
        catch (Exception exception)
        {
            // note that this API, currently, will not return 404 if the edition was not found, instead it returns 
            // a default edition.
            // var editions = edition.Replace(" ", string.Empty).Split(',');
            // if (editions.Length > 1)
            // {
            //     throw new MethodAccessException(
            //         "The SingleSurah(int, string) Method must returns only 1 Surah model, to get multiple models" +
            //         " use Surah(int, string) instead");
            // }
            ValidateEdition(edition);
            throw new Exception("See inner exception", exception);
        }
    }


    private static T? GenaricGet<T>(string naming, int rf, string edition, int offset = -99, int limit = -99)
        where T : IBaseGeneral
    {
        var rs = new StringBuilder(naming + "/" + rf + "/" + edition);
        if (offset != -99 || limit != -99)
        {
            rs.Append('?');
            if (offset != -99) rs.Append("offset=" + offset + '&');
            if (limit != -99) rs.Append("limit=" + limit + '&');
        }

        try
        {
            return Client.GetData<T>(new RestRequest(rs.ToString()));
        }
        catch (Exception e)
        {
            // ValidateManzil(manzil);
            ValidateEdition(edition);
            throw new Exception("See inner exception", e);
        }
    }


    private static IEnumerable<string>? GetResource(string r) =>
        Client.GetData<IEnumerable<string>>(new RestRequest(r));
}