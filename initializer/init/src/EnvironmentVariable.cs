namespace FP.ContainerTraining.Initializer.Init;
using System.Collections;
public static class EnvironmentVariable
{
    public static string? GetValueOrDefault(string key, string? defaultValue)
    {
        foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
        {
            if (de.Key.ToString() == key)
            {
                return de.Value?.ToString();
            }
        }
        return defaultValue;
    }
}