using System.Text.RegularExpressions;

namespace DeVesen.Bazaar.Server.Basics;

public static class ResourceText
{
    public static class Global
    {
        public static string IdMayNotBeEmpty => "'Id' darf nicht leer sein!";

        public static string NameMayNotBeEmpty => "'Bezeichnung' darf nicht leer sein!";
        public static string NameMayNotBeLongerThan => "'Bezeichnung' darf nicht lönger {count} Zeichen sein!";
        public static string NameAlreadyTaken => "'Bezeichnung' ist bereits vergeben!";

        public static string NumberMayNotBeEmpty => "'Nummer' muss größer 0 sein!";
        public static string NumberAlreadyTaken => "'Nummer' ist bereits vergeben!";

        public static string SalutationMayNotBeEmpty => "'Anrede' darf nicht leer sein!";
        public static string SalutationMayNotBeLongerThan => "'Anrede' darf nicht lönger {count} Zeichen sein!";

        public static string FirstNameMayNotBeEmpty => "'Vorname' darf nicht leer sein!";
        public static string FirstNameMayNotBeLongerThan => "'Vorname' darf nicht lönger {count} Zeichen sein!";

        public static string LastNameMayNotBeEmpty => "'Nachname' darf nicht leer sein!";
        public static string LastNameMayNotBeLongerThan => "'Nachname' darf nicht lönger {count} Zeichen sein!";

        public static string AddressMayNotBeLongerThan => "'Anschrift' darf nicht lönger {count} Zeichen sein!";

        public static string EMailMayNotBeLongerThan => "'E-Mail' darf nicht lönger {count} Zeichen sein!";

        public static string PhoneMayNotBeEmpty => "'Telefon' darf nicht leer sein!";
        public static string PhoneMayNotBeLongerThan => "'Telefon' darf nicht lönger {count} Zeichen sein!";


        public static string VendorIdMayNotBeEmpty => "'VendorId' darf nicht leer sein!";
        public static string VendorIdMayNotExist => "'VendorId' ist nicht bekannt!";
        public static string TitleMayNotBeEmpty => "'Title' darf nicht leer sein!";
        public static string TitleMayNotBeLongerThan => "'Title' darf nicht lönger {count} Zeichen sein!";
        public static string ArticleCategoryMayNotBeEmpty => "'Kategorie' darf nicht leer sein!";
        public static string ArticleCategoryMayNotBeLongerThan => "'Kategorie' darf nicht lönger {count} Zeichen sein!";
        public static string ManufacturerMayNotBeEmpty => "'Hersteller' darf nicht leer sein!";
        public static string ManufacturerMayNotBeLongerThan => "'Hersteller' darf nicht lönger {count} Zeichen sein!";
        public static string Price01MayNotNotBeEmpty => "'Preis' muss größer gleich 0 sein!";
        public static string Price02MayNotInvalid => "'Preis 2' muss leer order größer gleich 0 sein!";
    }

    public static class ArticleCategory
    {
        public static string NotFoundById => "Article-Category '{id}' nicht gefunden!";
    }

    public static class Manufacturer
    {
        public static string NotFoundById => "Manufacturer '{id}' nicht gefunden!";
    }

    public static class Vendor
    {
        public static string NotFoundById => "Anbieter '{id}' nicht gefunden!";
    }

    public static class Article
    {
        public static string NotFoundById => "Artikel '{id}' nicht gefunden!";
    }

    public static string Transform(string text, params KeyValuePair<string, object>[] keyToText)
        => Transform(text, new Dictionary<string, object>(keyToText));

    public static string Transform(string text, IDictionary<string, object> keyToText)
    {
        return Transform(text, ValuePicker);

        string ValuePicker(string key)
        {
            return keyToText.TryGetValue(key, out var value)
                ? value.ToString()!
                : string.Empty;
        }
    }

    public static string Transform(string text, Func<string, object> keyToText)
    {
        const string pattern = @"\{([^}]+)\}";

        var matches = Regex.Matches(text, pattern);

        var extractedValues = matches.Select(m => m.Groups[0].Value).Distinct();

        return extractedValues.Aggregate(text, (current, value) => current.Replace(value, keyToText(value).ToString()));
    }
}