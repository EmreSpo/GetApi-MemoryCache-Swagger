using System.Text.Json.Serialization;

public class Geo
{
    /// <summary>
    /// Primary key for the Geo entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Latitude coordinate.
    /// </summary>
    [JsonPropertyName("lat")]
    public string? Lat { get; set; }

    /// <summary>
    /// Longitude coordinate.
    /// </summary>
    [JsonPropertyName("lng")]
    public string? Lng { get; set; }
}

/// <summary>
/// Represents a user address.
/// </summary>
public class Address
{
    /// <summary>
    /// Primary key for the address.
    /// </summary>
    public int Id { get; set; }  // Birincil anahtar olarak kullanılacak

    /// <summary>
    /// Street address.
    /// </summary>
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    /// <summary>
    /// Suite or apartment number.
    /// </summary>
    [JsonPropertyName("suite")]
    public string? Suite { get; set; }

    /// <summary>
    /// City name.
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    /// Zip code.
    /// </summary>
    [JsonPropertyName("zipcode")]
    public string? Zipcode { get; set; }

    /// <summary>
    /// Geographical location information.
    /// </summary>
    [JsonPropertyName("geo")]
    public Geo? Geo { get; set; }
}

/// <summary>
/// Represents a company.
/// </summary>
public class Company
{
    /// <summary>
    /// Primary key for the company.
    /// </summary>
    public int Id { get; set; }  // Birincil anahtar olarak kullanılacak

    /// <summary>
    /// Name of the company.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Catchphrase of the company.
    /// </summary>
    [JsonPropertyName("catchPhrase")]
    public string? CatchPhrase { get; set; }

    /// <summary>
    /// Business sector of the company.
    /// </summary>
    [JsonPropertyName("bs")]
    public string? Bs { get; set; }
}

/// <summary>
/// Represents a user.
/// </summary>
public class User
{
    /// <summary>
    /// Unique identifier of the user.
    /// </summary>
    public int Id { get; set; } // Primary key olarak int kullanılabilir.

    /// <summary>
    /// Full name of the user.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Username of the user.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Email address of the user.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Address information of the user.
    /// </summary>
    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    /// <summary>
    /// Phone number of the user.
    /// </summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// Website URL of the user.
    /// </summary>
    [JsonPropertyName("website")]
    public string? Website { get; set; }

    /// <summary>
    /// Company information related to the user.
    /// </summary>
    [JsonPropertyName("company")]
    public Company? Company { get; set; }
}
