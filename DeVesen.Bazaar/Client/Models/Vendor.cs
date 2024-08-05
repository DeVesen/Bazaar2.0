namespace DeVesen.Bazaar.Client.Models;

public record Vendor
{
    public string Id { get; init; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? EMail { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }

    public static Vendor New => new();

    public bool Contains(string text)
    {
        text = text.Trim().ToLower();

        return text.Contains(Id.ToLower()) ||
               text.Contains(FirstName.ToLower()) ||
               text.Contains(LastName.ToLower()) ||
               (Note ?? string.Empty).ToLower().Contains(text);
    }

    public string GetGender()
    {
        return Salutation switch
        {
            "Male" => "Herr",
            "Female" => "Frau",
            _ => string.Empty
        };
    }

    public string GetTotalName()
    {
        var gender = GetGender();

        if (string.IsNullOrWhiteSpace(gender) is false)
        {
            gender += " ";
        }

        return $"{gender} {LastName}, {FirstName}";
    }
}