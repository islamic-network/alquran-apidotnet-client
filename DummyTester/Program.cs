using alquran;
using alquran.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

#pragma warning disable CS8602
#pragma warning disable CS8604

namespace DummyTester;

public static class F
{
    private static void TestRuku()
    {
        var e1 = AlQuranV1.Ruku(7, "en.asad");
        var e2 = AlQuranV1.Ruku(7, "quran-uthmani");
        var e3 = AlQuranV1.Ruku(7, "quran-uthmani", 3, 3);
    }

    static void TestQuran()
    {
        var e1 = AlQuranV1.Quran();
        var e2 = AlQuranV1.Quran("en.asad ");
        var e3 = AlQuranV1.Quran("ar.alafasy ");
    }

    static void TestJuz()
    {
        var e1 = AlQuranV1.Juz(30, "en.asad");
        var e2 = AlQuranV1.Juz(30, "quran-uthmani");
        var e3 = AlQuranV1.Juz(1, "quran-uthmani", 3, 10);
    }

    static void TestSurah()
    {
        var e1 = AlQuranV1.Surah() as IEnumerable<Surah>;
        var e2 = AlQuranV1.Surah(114, "ar.alafasy") as Surah;
        var e3 = AlQuranV1.Surah(3, offset: 4, limit: 7) as Surah;
        var e4 = AlQuranV1.Surah(114, "quran-uthmani,en.asad,en.pickthall") as IEnumerable<Surah>;
    }

    static void TestAyah()
    {
        var e1 = AlQuranV1.Ayah(262) as Ayah;
        var e2 = AlQuranV1.Ayah(2, 255) as Ayah;
        var e3 = AlQuranV1.Ayah(262, "ar.alafasy") as Ayah;
        var e4 = AlQuranV1.Ayah(262, "quran-uthmani,en.asad,en.pickthall") as IEnumerable<Ayah>;
    }

    static void TestSearch()
    {
        var e1 = AlQuranV1.Search("Abraham", "en");
        var e2 = AlQuranV1.Search("Abraham", "en.pickthall");
    }

    static void TestManzil()
    {
        var e1 = AlQuranV1.Manzil(7, "en.asad ");
        var e2 = AlQuranV1.Manzil(7, "quran-uthmani");
        var e3 = AlQuranV1.Manzil(7, "quran-uthmani", 3, 10);
    }

    static void TestPage()
    {
        var e1 = AlQuranV1.Page(7, "en.asad ");
        var e2 = AlQuranV1.Page(7, "quran-uthmani");
        var e3 = AlQuranV1.Page(7, "quran-uthmani", 3, 10);
    }

    static void TestHizb()
    {
        var e1 = AlQuranV1.Hizb(7, "en.asad ");
        var e2 = AlQuranV1.Hizb(7, "quran-uthmani");
        var e3 = AlQuranV1.Hizb(7, "quran-uthmani", 3, 10);
    }

    static void TestSajda()
    {
        var e1 = AlQuranV1.Sajda("en.asad ");
    }

    public static void Main()
    {
        // TestQuran();
        // TestSurah();
        // TestAyah();
        // TestSearch();
        // TestManzil();
        // TestJuz();
        // TestRuku();
        // TestPage();
        // TestHizb();
        // TestSajda();

        // var e = AlQuranV1.Editions("audio", "fr", "versebyverse");


        var r = AlQuranV1.Surah(13) as Surah;
        Console.WriteLine();
    }
    // TODO unit testing 
}
