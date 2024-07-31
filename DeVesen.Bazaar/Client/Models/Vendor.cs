namespace DeVesen.Bazaar.Client.Models;

public record Vendor
{
    public string Id { get; init; } = string.Empty;
    public string Salutation { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string EMail { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;

    public static Vendor New => new();

    public bool Contains(string text)
    {
        text = text.Trim().ToLower();

        return Id.ToLower().Contains(text) ||
               FirstName.ToLower().Contains(text) ||
               LastName.ToLower().Contains(text) ||
               Note.ToLower().Contains(text);
    }

    private string GetGender()
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

        return $"{gender}{FirstName} {LastName}";
    }
}