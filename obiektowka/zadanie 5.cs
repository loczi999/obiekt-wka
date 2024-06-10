using System;
using System.Collections.Generic;

public class Macierz<T> : IEquatable<Macierz<T>>
{
    private readonly T[,] macierz;

    public Macierz(int wiersze, int kolumny)
    {
        if (wiersze <= 0 || kolumny <= 0)
            throw new ArgumentException("Wymiary macierzy muszą być dodatnie.");
        macierz = new T[wiersze, kolumny];
    }

    public T this[int wiersz, int kolumna]
    {
        get
        {
            return macierz[wiersz, kolumna];
        }
        set
        {
            macierz[wiersz, kolumna] = value;
        }
    }

    public int LiczbaWierszy => macierz.GetLength(0);
    public int LiczbaKolumn => macierz.GetLength(1);

    public override bool Equals(object obj)
    {
        if (obj is Macierz<T> other)
        {
            return Equals(other);
        }
        return false;
    }

    public bool Equals(Macierz<T> other)
    {
        if (other == null)
            return false;
        if (LiczbaWierszy != other.LiczbaWierszy || LiczbaKolumn != other.LiczbaKolumn)
            return false;

        for (int i = 0; i < LiczbaWierszy; i++)
        {
            for (int j = 0; j < LiczbaKolumn; j++)
            {
                if (!EqualityComparer<T>.Default.Equals(this[i, j], other[i, j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        for (int i = 0; i < LiczbaWierszy; i++)
        {
            for (int j = 0; j < LiczbaKolumn; j++)
            {
                hash = hash * 23 + (this[i, j]?.GetHashCode() ?? 0);
            }
        }
        return hash;
    }

    public static bool operator ==(Macierz<T> lhs, Macierz<T> rhs)
    {
        if (ReferenceEquals(lhs, rhs))
            return true;
        if (ReferenceEquals(lhs, null))
            return false;
        if (ReferenceEquals(rhs, null))
            return false;

        return lhs.Equals(rhs);
    }

    public static bool operator !=(Macierz<T> lhs, Macierz<T> rhs)
    {
        return !(lhs == rhs);
    }
}

public class Program
{
    public static void Main()
    {
        Macierz<int> macierz1 = new Macierz<int>(2, 2);
        macierz1[0, 0] = 1;
        macierz1[0, 1] = 2;
        macierz1[1, 0] = 3;
        macierz1[1, 1] = 4;

        Macierz<int> macierz2 = new Macierz<int>(2, 2);
        macierz2[0, 0] = 1;
        macierz2[0, 1] = 2;
        macierz2[1, 0] = 3;
        macierz2[1, 1] = 4;

        Console.WriteLine(macierz1 == macierz2);
        Console.WriteLine(macierz1.Equals(macierz2)); 

        macierz2[1, 1] = 5;
        Console.WriteLine(macierz1 != macierz2);
        Console.WriteLine(macierz1.Equals(macierz2)); 
    }
}
