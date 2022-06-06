
# Table of Contents

1.  [Alquran](#org806e01c)
2.  [Demo](#orgced1d89)
3.  [Usage](#orge055cb5)
    1.  [`Edition`](#orge6a121a)
    2.  [`Quran`](#orgb6ea504)
    3.  [`Juz`](#orgba48ee0)
    4.  [`Surah`](#org4e62617)
    5.  [`Ayah`](#orgb1e33e7)
    6.  [`Search`](#org9481938)
    7.  [`Manzil`](#orge8eaaaa)
    8.  [`Ruku`](#org56e7e00)
    9.  [`Page`](#orgb7926e0)
    10. [`Hizb`](#org4d0ef37)
    11. [`Sajda`](#org636e543)
4.  [Improve Documentation](#org8d3446f)
5.  [Add Unit Testing Class](#orgec103cb)
6.  [Note About `Meta`](#org9391237)



<a id="org806e01c"></a>

# Alquran

.NET lightweight Quran library using the alquran.cloud v1 APIs.

-   Dependencies:
    -   [RestSharp](https://www.nuget.org/packages/RestSharp/) (>= 107.3.0)
    -   [RestSharp.Serializers.NewtonsoftJson](https://www.nuget.org/packages/RestSharp.Serializers.NewtonsoftJson/) (>= 107.3.0)

This release was built using .NET 6


<a id="orgced1d89"></a>

# Demo

    using alquran;
    var e1 = AlQuranV1.Quran(); //getting all of Quran
    var e2 = AlQuranV1.Quran("en.asad "); //getting Muhammad Asad's edition of the Holy Quran
    var e3 = AlQuranV1.Quran("ar.alafasy "); // getting Mishary Alafasy's recitation of the Quran


<a id="orge055cb5"></a>

# Usage


<a id="orge6a121a"></a>

## `Edition`

All calls of this methods give you an IEnumerable object of editions describing the filte
editions. From this object, you need to use the identifier to get data from other endpoints
in this API. For any of the endpoints that require an edition identifier, if you do not
specify one, &rsquo;quran-uthmani&rsquo; is used and returns the Arabic text of the Holy Quran.

    IEnumerable<Edition>? Editions(string format, string lang, string type)


<a id="org1dcf1dd"></a>

### Parameters

-   `format`: specify a format. &rsquo;text&rsquo; or &rsquo;audio.
-   `lang`:  a 2 digit language code. Example: &rsquo;en&rsquo;, &rsquo;fr&rsquo;, etc.
-   `type`: a valid type. Example - &rsquo;versebyverse&rsquo;, &rsquo;translation&rsquo; etc.


<a id="orge3fc819"></a>

### Cases

Lists all editions for a given language:

    IEnumerable<Edition>? Editions(string lang)

Lists all editions for a given format:

    IEnumerable<Edition>? Editions(string format)

Lists all editions for a given type:

    IEnumerable<Edition>? Editions(string type)


<a id="org4deb4dd"></a>

### Required Parameters

None


<a id="orgc727dd9"></a>

### Default Values

Returns a list of all available editions


<a id="org1f0ef2b"></a>

### Example

    var e = AlQuranV1.Editions("audio", "fr", "versebyverse") // Lists all audio editions in french of the versebyverse type


<a id="orgc95d711"></a>

### Return Type

This methods always returns an `IEnumerable<edition>`


<a id="orgb6ea504"></a>

## `Quran`

Returns a complete Quran edition in the audio or text format

    Quran? Quran(string edition)


<a id="orgfec799b"></a>

### Parameters

-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation


<a id="org995c4a4"></a>

### Cases

    Quran? Quran()

Returns the text of the Holy Quran in the quran-uthmani edition.

    Quran? Quran(string edition)

Returns a complete Quran edition.


<a id="orgf1c4d40"></a>

### Default Values

Returns the text of the Holy Quran in the quran-uthmani edition.


<a id="org5e7585c"></a>

### Required Parameters

None


<a id="orgc2aac94"></a>

### Example

    var e1 = AlQuranV1.Quran(); // Returns Muhammad Asad's translation of the Holy Quran
    var e2 = AlQuranV1.Quran("en.asad "); // Returns the text of the Holy Quran
    var e3 = AlQuranV1.Quran("ar.alafasy "); // Returns Mishary Alafasy's recitation of the Quran


<a id="orgf3becbc"></a>

### Return Type

An object of type `Quran`


<a id="orgba48ee0"></a>

## `Juz`

The Quran has 30 Juz. You can get the text for each Juz using the method below.

    Juz? Juz(int juz, string edition, int offset, int limit)


<a id="orga7d0dc8"></a>

### Parameters

-   `juz`: number of juz.
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="org85cb9d1"></a>

### Cases

    Juz? Juz(int juz, string edition)

Returns an object of `juz` from edition `edition` of the Holy Quran

    Juz? Juz(int juz, string edition, int offset, int limit)


<a id="org1efbbe3"></a>

### Required Parameters

-   `juz`: number of juz


<a id="org610ea69"></a>

### Example

    var e1 = AlQuranV1.Juz(30, "en.asad");
    var e2 = AlQuranV1.Juz(30, "quran-uthmani");
    var e3 = AlQuranV1.Juz(1, "quran-uthmani", 3, 10);


<a id="orgfb17ecb"></a>

### Return Values

An object of type `Juz`


<a id="org4e62617"></a>

## `Surah`

Get a single Surah in one object or in a list with its other ports in other editions of the
Holy Quran.

    object? Surah(int surah, string edition, int offset, int limit)


<a id="org52454cc"></a>

### Parameters

-   `surah`: number of surah
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="org39807cf"></a>

### Cases

    object? Surah(int surah , string edition , int offset , int limit )

Returns an object (castable to `Surah` object) of the Surah with number `surah`, edition
`edition` and offset `offset` to the limit `limit`.

    object? Surah(int surah)

Returns an object (castable to `Surah` object) of Surah with number `surah` and edition
`quran-simple`. It will return an object castable to `IEnumerable<Surah>` if `edition` is
more than 1 edition.

    object? Surah()

Returns an object (castable to `IEnumerable<Surah>` object) of all Surahs of the holy Quran


<a id="org513c63f"></a>

### Default Value

Returns an object (castable to `IEnumerable<Surah>` object) of all Surahs of the holy Quran


<a id="org23860e3"></a>

### Required Parameters

None


<a id="org06b7d42"></a>

### Example

    var e1 = AlQuranV1.Surah() as IEnumerable<Surah>; // all Surahs of Quran
    var e2 = AlQuranV1.Surah(114, "ar.alafasy") as Surah; // Returns Mishary Alafasy's recitation of Surat An-Naas
    var e3 = AlQuranV1.Surah(3, offset: 4, limit: 7) as Surah; // - Returns verses 2-4 of Surah Al-Fatiha
    var e4 = AlQuranV1.Surah(114, "quran-uthmani,en.asad,en.pickthall") as IEnumerable<Surah>; //  Returns Surat An-Naas from 3 editions: Simple Quran, Muhammad Asad and Marmaduke Pickthall


<a id="org3fcef70"></a>

### Return Value

`object?`


<a id="orgcbfff20"></a>

### Note

Since the `Surah` API may return a single Surah, as in example `e2` as well it might
returns a list of Surahs, as in examples e1, e4, it return an object type which can be
safely casted to whichever type.


<a id="orgb1e33e7"></a>

## `Ayah`

Get a single Ayah in one object or in a list with its other ports in other editions of the
Holy Quran.

    object? Ayah(int ayah, string edition = "")
    object? Ayah(int surah, int ayah, string edition = "")


<a id="org2721bf8"></a>

### Parameters

-   `ayah`: number of ayah
-   `surah`: number of surah
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="org8086a37"></a>

### Cases

    Ayah(int ayah, string edition = "")

Returns an object (castable to `Ayah` object) of the Ayah with number `ayah`, edition
`edition`.

    Ayah(int ayah)

Returns an object (castable to `ayah` object) of ayah with number `ayah` and edition `quran-simple`

    object? Ayah(int surah, int ayah, string edition = "")

Returns an object (castable to `IEnumerable<Surah>` object) of all Surahs of the holy Quran


<a id="org3e1f553"></a>

### Required Parameters

-   `ayah`: number of ayah
-   `surah`: number of surah


<a id="org390c978"></a>

### Example

    var e1 = AlQuranV1.Ayah(262) as Ayah; // Returns Muhammad Asad's translation Ayat Al Kursi
    var e2 = AlQuranV1.Ayah(2, 255) as Ayah; // Returns Muhammad Asad's translation Ayat Al Kursi
    var e3 = AlQuranV1.Ayah(262, "ar.alafasy") as Ayah; // Returns Mishary Alafasy's recitation of the Ayat Al Kursi
    var e4 = AlQuranV1.Ayah(262, "quran-uthmani,en.asad,en.pickthall") as IEnumerable<Ayah>; // Returns Ayat Al Kursi from 3 editions: Simple Quran, Muhammad Asad and Maramduke Pickthall


<a id="org5dbb424"></a>

### Return Value

`object?`


<a id="orgae1dd97"></a>

### Note

Since the `ayah` API may return a single Surah, as in example `e2` as well it might
returns a list of Ayahs, as in examples e1, e4, it return an object type which can be
safely casted to whichever type.


<a id="org9481938"></a>

## `Search`

Search the Holy Quran. Please note that only text editions of the Quran are searchable.

    SearchResult? Search(string keyword, string editionOrLanguage, int surah)


<a id="orgfbe0fb5"></a>

### Parameters

-   `keyword` the keyword to seach for
-   `surah` number of surah
-   `editionOrLanguage` an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation. or an language identifier. Example: en for english, ar for Arabic


<a id="org0176306"></a>

### Cases

    SearchResult? Search(string keyword, string editionOrLanguage, int surah)

Returns and object of `SearchResult` with results of searching in edition[or, editions of
the language] `editionOrLanguage`, only in Surah with number `surah`

    SearchResult? Search(string keyword, string editionOrLanguage)

Returns and object of `SearchResult` with results of searching in edition[or, editions of
the language] `editionOrLanguage`


<a id="org71971d5"></a>

### Required Parameters

-   `keyword`: the keyword to seach for
-   `editionOrLanguage`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation. or an language identifier. Example: en for english, ar for Arabic


<a id="org6afed91"></a>

### Example

    var e1 = AlQuranV1.Search("Abraham", "en"); //  Returns all ayahs that contain the word 'Abraham' in all the english editions
    var e2 = AlQuranV1.Search("Abraham", "en.pickthall"); // Returns all ayahs that contain the word 'Abraham' in Maramduke Pickthall's English translation
    var e3 = AlQuranV1.Search("Abraham", "en.pickthall", 37); // Returns all ayahs that contain the word 'Abraham' Surat As-Saafaat in Maramduke Pickthall's English translation


<a id="org8e51031"></a>

### Return Value

`SearchResult?`


<a id="orge8eaaaa"></a>

## `Manzil`

The Quran has 7 Manzils (for those who want to read / recite it over one week). You can get
the text for each Manzil using this method.

    Manzil? Manzil(int manzil, string edition, int offset = -99, int limit = -99)


<a id="org438f742"></a>

### Parameters

-   `manzil`: number of manzil.
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="org60a0354"></a>

### Cases

    Manzil? Manzil(int manzil, string edition)

Returns an object of `manzil` from edition `edition` of the Holy Quran

    Manzil? Manzil(int manzil, string edition, int offset, int limit)


<a id="org8bffca9"></a>

### Required Parameters

-   `manzil`: number of manzil


<a id="org4c1fb27"></a>

### Example

    var e1 = AlQuranV1.Manzil(7, "en.asad "); // Returns manzil 7 from Muhammad Asad's translation of the Holy Quran
    var e2 = AlQuranV1.Manzil(7, "quran-uthmani"); // Returns the text of Manzil 7 of the Holy Quran
    var e3 = AlQuranV1.Manzil(7, "quran-uthmani", 3, 10); // Returns the the ayahs 4-13 from Manzil 7


<a id="orga67ee38"></a>

### Return Values

An object of type `manzil`


<a id="org56e7e00"></a>

## `Ruku`

The Quran has 556 Rukus. You can get the text for each Ruku using the method below.

    Ruku? Ruku(int ruku, string edition, int offset = -99, int limit = -99)


<a id="org0ccdf96"></a>

### Parameters

-   `ruku`: number of ruku.
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="org8218490"></a>

### Cases

    Ruku? Ruku(int ruku, string edition)

Returns an object of `Ruku` from edition `edition` of the Holy Quran

    Ruku? Ruku(int manzil, string edition, int offset, int limit)


<a id="orgff4b646"></a>

### Required Parameters

-   `ruku`: number of manzil


<a id="org00f99a1"></a>

### Example

    var e1 = AlQuranV1.Ruku(7, "en.asad"); // Returns ruku 7 from Muhammad Asad's translation of the Holy Quran
    var e2 = AlQuranV1.Ruku(7, "quran-uthmani"); // Returns the text of ruku 7 of the Holy Quran
    var e3 = AlQuranV1.Ruku(7, "quran-uthmani", 3, 3); // Returns the the ayahs 4-6 from ruku 7


<a id="org48a69bf"></a>

### Return Values

An object of type `Ruku`


<a id="orgb7926e0"></a>

## `Page`

The Quran is traditionally printed / written on 604 pages. You can get the text for each
page using the method below.

    Page? Page(int page, string edition, int offset = -99, int limit = -99)


<a id="org27ba7a9"></a>

### Parameters

-   `page`: number of page.
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="orgb4ca2a4"></a>

### Cases

    Page? Page(int page, string edition)

Returns an object of `Page` from edition `edition` of the Holy Quran

    Page? Page(int page, string edition, int offset, int limit)


<a id="org573481a"></a>

### Required Parameters

-   `page`: number of page


<a id="org9e1b9fe"></a>

### Example

    var e1 = AlQuranV1.Page(1, "en.asad "); // Returns page 1 from Muhammad Asad's translation of the Holy Quran
    var e2 = AlQuranV1.Page(1, "quran-uthmani"); // Returns the text of page 1 of the Holy Quran
    var e3 = AlQuranV1.Page(1, "quran-uthmani", 2, 2); // Returns the the ayahs 3-4 from page 1


<a id="org85c004f"></a>

### Return Values

An object of type `Page`


<a id="org4d0ef37"></a>

## `Hizb`

The Quran comprises 240 Hizb Quarters. One Hizb is half a Juz.

    Hizb? Hizb(int hizb, string edition, int offset = -99, int limit = -99)


<a id="org8daecb2"></a>

### Parameters

-   `hizb`: number of hizb.
-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.
-   `offset`:  offset ayahs in a juz by the given number.
-   `limit`: number of ayahs that the response will be limited to.


<a id="orgd74c026"></a>

### Cases

    Hizb? Hizb(int hizb, string edition)

Returns an object of `Hizb` from edition `edition` of the Holy Quran

    Hizb? Ruku(int hizv, string edition, int offset, int limit)


<a id="org205d3f0"></a>

### Required Parameters

-   `hizb`: number of hizb


<a id="org207c1b5"></a>

### Example

    var e1 = AlQuranV1.Hizb(7, "en.asad "); // Returns hizb quarter 1 from Muhammad Asad's translation of the Holy Quran
    var e2 = AlQuranV1.Hizb(7, "quran-uthmani"); // Returns the text of hizb quarater 1 of the Holy Quran
    var e3 = AlQuranV1.Hizb(7, "quran-uthmani", 2, 2); // Returns the the ayahs 3-4 from hizb Quarter 1


<a id="org57869b9"></a>

### Return Values

An object of type `Hizb`


<a id="org636e543"></a>

## `Sajda`

Depending on the madhab, there can be 14, 15 or 16 sajdas. This API has 15.

    (IEnumerable<Ayah>, Edition) Sajda(string edition)


<a id="org5f44890"></a>

### Parameters

-   `edition`: an edition identifier. Example: en.asad for Muhammad Asad&rsquo;s english translation.


<a id="orgf996894"></a>

### Cases

    (IEnumerable<Ayah>, Edition) Sajda(string edition)

Returns a tuple of `IEnumerable<Ayah>` and `Edition` contains Sajdas of the edition and
metadata about the edition, respectively.

    (IEnumerable<Ayah>, Edition) Sajda()

Returns a tuple of `IEnumerable<Ayah>` and `Edition` contains Sajdas of the edition and
metadata about the quran-simple edition.


<a id="org21624a9"></a>

### Required Parameters

None.


<a id="org1f00144"></a>

### Example

    var e1 = AlQuranV1.Sajda("en.asad "); // Returns the text of sajda ayahs of the Holy Quran


<a id="org315ba72"></a>

### Return Values

An object of type `(IEnumerable<Ayah>, Edition)`


<a id="org8d3446f"></a>

# TODO Improve Documentation


<a id="orgec103cb"></a>

# TODO Add Unit Testing Class


<a id="org9391237"></a>

# Note About `Meta`

I did not implement an interface for the meta API (`http://api.alquran.cloud/v1/meta`), and
I&rsquo;m not sure of adding &rsquo;yet&rsquo;, because of: 1. It seems useless since all the metadata can be
concatenated throw the AlquranV1 methods, using an extention method for example. 2. It is
too complex type to bind.

