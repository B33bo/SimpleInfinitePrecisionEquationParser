using System.Numerics;

namespace SIPEP;

public static class Conversions
{
    public static bool Added = false;

    private static Dictionary<string, BigComplex>? all;

    public static Dictionary<string, BigComplex> All
    {
        get
        {
            if (all is null)
            {
                all = new();
                foreach (var unit in Length)
                    all.Add(unit.Key, unit.Value);
                foreach (var unit in DataStorage)
                    all.Add(unit.Key, unit.Value);
                foreach (var unit in Energy)
                    all.Add(unit.Key, unit.Value);
                foreach (var unit in Mass)
                    all.Add(unit.Key, unit.Value);
                foreach (var unit in Angle)
                    all.Add(unit.Key, unit.Value);
                foreach (var unit in Time)
                    all.Add(unit.Key, unit.Value);
                foreach (var unit in Temperature)
                    all.Add(unit.Key, unit.Value);
            }
            
            return all;
        }
    }
    public static Dictionary<string, BigComplex> Length = new()
    {
        { "meter", 1 },
        { "metre", 1 },
        { "centimeter", BigRational.Parse(".01") },
        { "centimetre", BigRational.Parse(".01") },
        { "kilometer", 1000 },
        { "kilometre", 1000 },
        { "millimeter", BigRational.Parse(".001") },
        { "millimetre", BigRational.Parse(".001") },
        { "micrometer", BigRational.Parse("1e-6") },
        { "micrometre", BigRational.Parse("1e-6") },
        { "nanometre", BigRational.Parse("1e-9") },
        { "nanometer", BigRational.Parse("1e-9") },
        { "mile", BigRational.Parse("1609.34") },
        { "yard", BigRational.Parse("0.9144") },
        { "foot", BigRational.Parse("0.3048") },
        { "inch", BigRational.Parse("0.0254") },
        { "nauticalmile", 1852 },
        { "lightyear", BigRational.Parse("9.461e+15") },
        { "au", BigRational.Parse("1.496e+11") },
        { "plancklength", BigRational.Parse("1.6e-35") },
        { "universediameter", BigRational.Parse("8.79999305638e+26") },
    };

    public static Dictionary<string, BigComplex> DataStorage = new()
    {
        { "bit", 1 },
        { "nibble", 4 },
        { "byte", 8 },
        { "kilobit", 1000 },
        { "kilobyte", 8000 },
        { "megabit", 1000000 },
        { "megabyte", 8000000 },
        { "gigabit", BigRational.Parse("1e+9") },
        { "gigabyte", BigRational.Parse("8e+9") },
        { "terabit", BigRational.Parse("1e+12") },
        { "terabyte", BigRational.Parse("8e+12") },
        { "petabit", BigRational.Parse("1e+15") },
        { "petabyte", BigRational.Parse("8e+15") },
    };

    public static Dictionary<string, BigComplex> Energy = new()
    {
        { "joule", 1 },
        { "kilojoule", 1000 },
        { "calorie", BigRational.Parse("4.184") },
        { "kilocalorie", 4184 },
        { "watthour", 3600 },
        { "kilowatthour", BigRational.Parse("3.6e+6") },
        { "electronvolt", BigRational.Parse("1.6022e-19") },
        { "britishthermalunit", BigRational.Parse("1055.06") },
        { "usthermalunit", BigRational.Parse("1.055e+8") },
        { "footpound", BigRational.Parse("1.35582") },
    };

    public static Dictionary<string, BigComplex> Mass = new()
    {
        { "gram", 1 },
        { "kilogram", 1000 },
        { "tonne", 1000000 },
        { "milligram", BigRational.Parse(".001") }, //what 1 mg does to an mf
        { "microgram", BigRational.Parse("1e-6") },
        { "imperialton", BigRational.Parse("1.016e+6") },
        { "uston", BigRational.Parse("907185") },
        { "stone", BigRational.Parse("6350.29") },
        { "pound", BigRational.Parse("453.592") },
        { "ounce", BigRational.Parse("28.3495") },
    };

    public static Dictionary<string, BigComplex> Angle = new()
    {
        { "degree", 1 },
        { "radian", Constants.Pi / 180 },
        { "milliradian", 1000 * Constants.Pi / 180 },
        { "arcminute", 60 },
        { "arcsecond", 3600 },
        { "gradian", 200 / (BigRational)180 },
    };

    public static Dictionary<string, BigComplex> Time = new()
    {
        { "second", 1 },
        { "millisecond", BigRational.Parse(".001") },
        { "microsecond", BigRational.Parse("1e-6") },
        { "nanosecond", BigRational.Parse("1e-9") },
        { "minute", 60 },
        { "hour", 3600 },
        { "day", 86400 },
        { "week", 604800 },
        { "month", 604800 },
        { "year", 31557600 },
        { "decade", 315576000 },
        { "century", BigRational.Parse("3155760000") },
        { "millennium", BigRational.Parse("31557600000") },
        { "plancktime", BigRational.Parse("5.3912e-44") },
        { "tick", new BigComplex(TimeSpan.TicksPerSecond, 0) },

        { "january", 86400 * 31 },
        { "february", 86400 * 28 },
        { "februaryly", 86400 * 29 },
        { "march", 86400 * 31 },
        { "april", 86400 * 30 },
        { "may", 86400 * 31 },
        { "june", 86400 * 30 },
        { "july", 86400 * 31 },
        { "august", 86400 * 31 },
        { "september", 86400 * 30 },
        { "october", 86400 * 31 },
        { "november", 86400 * 30 },
        { "december", 86400 * 31 },
    };

    public static Dictionary<string, BigComplex> Temperature = new()
    {
        { "celsius", Celsius },
        { "centigrade", Celsius },
        { "kelvin", Kelvin },
        { "fahrenheit", Fahrenheit },
        { "rankine", Rankine },
    };

    public const int Celsius = 0, Kelvin = 1, Fahrenheit = 2, Rankine = 3;
}
