using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Produkt
{
    private decimal cenaNetto;
    private static readonly Dictionary<string, decimal> VATRates = new Dictionary<string, decimal>
    {
        { "A", 0.23m },
        { "B", 0.08m },
        { "C", 0.05m },
        { "D", 0.00m }
    };

    public string Nazwa { get; set; }

    public decimal CenaNetto
    {
        get => cenaNetto;
        set
        {
            if (value < 0)
                throw new ArgumentException("Cena netto nie może być ujemna.");
            cenaNetto = value;
        }
    }

    private string kategoriaVAT;
    public virtual string KategoriaVAT
    {
        get => kategoriaVAT;
        set
        {
            if (!VATRates.ContainsKey(value))
                throw new ArgumentException("Nieprawidłowa kategoria VAT.");
            kategoriaVAT = value;
        }
    }

    public virtual decimal CenaBrutto
    {
        get
        {
            if (!VATRates.ContainsKey(KategoriaVAT))
                throw new NotImplementedException();
            return CenaNetto + (CenaNetto * VATRates[KategoriaVAT]);
        }
    }

    private static readonly HashSet<string> KrajPochodzeniaSet = new HashSet<string> { "Polska", "Niemcy", "Francja", "USA" };

    private string krajPochodzenia;
    public virtual string KrajPochodzenia
    {
        get => krajPochodzenia;
        set
        {
            if (!KrajPochodzeniaSet.Contains(value))
                throw new ArgumentException("Nieprawidłowy kraj pochodzenia.");
            krajPochodzenia = value;
        }
    }
}

public abstract class ProduktSpożywczy : Produkt
{
    private decimal kalorie;
    private static readonly HashSet<string> SpożywczyKategoriaVATSet = new HashSet<string> { "B", "C" };

    public override string KategoriaVAT
    {
        get => base.KategoriaVAT;
        set
        {
            if (!SpożywczyKategoriaVATSet.Contains(value))
                throw new ArgumentException("Nieprawidłowa kategoria VAT dla produktu spożywczego.");
            base.KategoriaVAT = value;
        }
    }

    public decimal Kalorie
    {
        get => kalorie;
        set
        {
            if (value < 0)
                throw new ArgumentException("Kalorie nie mogą być ujemne.");
            kalorie = value;
        }
    }

    private static readonly HashSet<string> AlergenySet = new HashSet<string> { "Orzechy", "Mleko", "Gluten", "Jaja" };

    private HashSet<string> alergeny;
    public virtual HashSet<string> Alergeny
    {
        get => alergeny;
        set
        {
            if (value == null || !value.All(a => AlergenySet.Contains(a)))
                throw new ArgumentException("Nieprawidłowy zestaw alergenów.");
            alergeny = value;
        }
    }
}

public class ProduktSpożywczyNaWagę : ProduktSpożywczy
{
}

public class ProduktSpożywczyPaczka : ProduktSpożywczy
{
    private decimal waga;
    public decimal Waga
    {
        get => waga;
        set
        {
            if (value < 0)
                throw new ArgumentException("Waga nie może być ujemna.");
            waga = value;
        }
    }
}

public class ProduktSpożywczyNapój : ProduktSpożywczyPaczka
{
    private uint objętość;
    public uint Objętość
    {
        get => objętość;
        set
        {
            if (value < 0)
                throw new ArgumentException("Objętość nie może być ujemna.");
            objętość = value;
        }
    }
}

public class Wielopak
{
    public Produkt Produkt { get; set; }
    public ushort Ilość { get; set; }

    private decimal cenaNetto;
    public decimal CenaNetto
    {
        get => cenaNetto;
        set
        {
            if (value < 0)
                throw new ArgumentException("Cena netto nie może być ujemna.");
            cenaNetto = value;
        }
    }

    public decimal CenaBrutto => Produkt.CenaBrutto * Ilość;

    public string KategoriaVAT => Produkt.KategoriaVAT;
    public string KrajPochodzenia => Produkt.KrajPochodzenia;
}

public class Program
{
    public static void Main()
    {
        try
        {
            ProduktSpożywczyPaczka czekolada = new ProduktSpożywczyPaczka
            {
                Nazwa = "Czekolada",
                CenaNetto = 10,
                KategoriaVAT = "B",
                KrajPochodzenia = "Polska",
                Kalorie = 500,
                Waga = 100,
                Alergeny = new HashSet<string> { "Mleko", "Orzechy" }
            };

            Wielopak wielopakCzekolady = new Wielopak
            {
                Produkt = czekolada,
                Ilość = 5,
                CenaNetto = 45
            };

            Console.WriteLine($"Produkt: {czekolada.Nazwa}, Cena Brutto: {czekolada.CenaBrutto}");
            Console.WriteLine($"Wielopak: {wielopakCzekolady.Ilość}x {wielopakCzekolady.Produkt.Nazwa}, Cena Brutto: {wielopakCzekolady.CenaBrutto}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}
