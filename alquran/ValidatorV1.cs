namespace alquran;

public static partial class AlQuranV1
{
    private static void ValidateList(IEnumerable<string>? objects, string obj, string naming)
    {
        if (obj == "" || (objects != null && objects.Any(x => x == obj))) return;
        throw new KeyNotFoundException($"The given {naming}: {obj} was not found");
    }

    private static void ValidateEdition(string obj)
    {
        var naming = "edition";
        var objects = Editions();
        if (obj == "" || (objects != null && objects.All(x => x.name != obj))) return;
        throw new KeyNotFoundException($"The given {naming}: {obj} was not found");
    }


    private static void ValidateAyah(int ayah)
    {
        ValidateConstrains("Ayah", ayah, 1, 6236);
    }

    private static void ValidateSurah(int surah)
    {
        ValidateConstrains("Surah", surah, 1, 144);
    }

    private static void ValidateManzil(int manizl)
    {
        ValidateConstrains("Manzil", manizl, 1, 7);
    }

    private static void ValidateRuku(int ruku)
    {
        ValidateConstrains("Ruku", ruku, 1, 556);
    }

    private static void ValidateConstrains(string naming, int val, int min, int max)
    {
        if ((val > max || val < min) && val != -99)
            throw new IndexOutOfRangeException(
                $"{naming} number should be between {min} and {max}, given {naming}: {val}");
    }

    private static void ValidatePage(int page)
    {
        ValidateConstrains("Page", page, 1, 604);
    }

    private static void ValidateHizb(int hizb)
    {
        ValidateConstrains("Hizb", hizb, 1, 204);
    }
}