using System;

public class Osoba
{
    private string imie;
    private string nazwisko;

    public DateTime? DataUrodzenia { get; set; } = null;
    public DateTime? DataŚmierci { get; set; } = null;

    public Osoba(string imięNazwisko)
    {
        ImięNazwisko = imięNazwisko;
    }

    public string Imię
    {
        get => imie;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Imię nie może być puste.");
            }
            imie = value;
        }
    }

    public string Nazwisko
    {
        get => nazwisko;
        set => nazwisko = value;
    }

    public string ImięNazwisko
    {
        get => $"{Imię} {Nazwisko}".TrimEnd();
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Imię i nazwisko nie mogą być puste.");
            }

            string[] parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Imię = parts[0];
            Nazwisko = parts.Length > 1 ? parts[^1] : string.Empty;
        }
    }

    public TimeSpan? Wiek
    {
        get
        {
            if (DataUrodzenia == null)
            {
                return null;
            }

            DateTime endDate = DataŚmierci ?? DateTime.Now;
            return endDate - DataUrodzenia;
        }
    }
}

public class Program
{
    public static void Main()
    {
        Osoba osoba = new Osoba("Jan Kowalski");
        osoba.DataUrodzenia = new DateTime(1990, 1, 1);

        Console.WriteLine($"Imię: {osoba.Imię}");
        Console.WriteLine($"Nazwisko: {osoba.Nazwisko}");
        Console.WriteLine($"Imię i nazwisko: {osoba.ImięNazwisko}");
        Console.WriteLine($"Wiek: {osoba.Wiek?.Days / 365} lat");

        osoba.ImięNazwisko = "Anna Nowak";
        Console.WriteLine($"Zaktualizowane imię i nazwisko: {osoba.ImięNazwisko}");
    }
}
