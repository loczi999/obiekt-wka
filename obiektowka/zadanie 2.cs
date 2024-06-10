using System;
using System.Collections.Generic;

public class Prostokat
{
    
    private double bokA;
    private double bokB;

    
    public double BokA
    {
        get { return bokA; }
        set
        {
            if (double.IsFinite(value) && value >= 0)
            {
                bokA = value;
            }
            else
            {
                throw new ArgumentException("Wartość musi być skończoną, nieujemną liczbą.");
            }
        }
    }

    public double BokB
    {
        get { return bokB; }
        set
        {
            if (double.IsFinite(value) && value >= 0)
            {
                bokB = value;
            }
            else
            {
                throw new ArgumentException("Wartość musi być skończoną, nieujemną liczbą.");
            }
        }
    }

    
    public Prostokat(double bokA, double bokB)
    {
        BokA = bokA;
        BokB = bokB;
    }

    
    private static readonly Dictionary<char, decimal> wysokośćArkusza0 = new Dictionary<char, decimal>
    {
        ['A'] = 1189m,
        ['B'] = 1414m,
        ['C'] = 1297m
    };

    
    private static readonly double pierwiastekZDwoch = Math.Sqrt(2);

    
    public static Prostokat ArkuszPapieru(string format)
    {
        if (string.IsNullOrEmpty(format) || format.Length < 2)
        {
            throw new ArgumentException("Niepoprawny format.");
        }

        char X = format[0];
        if (!wysokośćArkusza0.ContainsKey(X))
        {
            throw new ArgumentException("Niepoprawny format.");
        }

        if (!byte.TryParse(format.Substring(1), out byte i))
        {
            throw new ArgumentException("Niepoprawny format.");
        }

        decimal bazowaWysokosc = wysokośćArkusza0[X];
        double bokA = (double)bazowaWysokosc / Math.Pow(pierwiastekZDwoch, i);
        double bokB = bokA / pierwiastekZDwoch;

        return new Prostokat(bokA, bokB);
    }
}


public class Program
{
    public static void Main()
    {
        try
        {
            Prostokat arkuszA0 = Prostokat.ArkuszPapieru("A0");
            Console.WriteLine($"Arkusz A0: BokA = {arkuszA0.BokA}, BokB = {arkuszA0.BokB}");

            Prostokat arkuszA4 = Prostokat.ArkuszPapieru("A4");
            Console.WriteLine($"Arkusz A4: BokA = {arkuszA4.BokA}, BokB = {arkuszA4.BokB}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}

