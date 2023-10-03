namespace FP.ContainerTraining.CityTrip.App;

public static class ResourceLink
{
    public static string ReadEmbeddedTextFile(string resourcePath)
    {
        var name = $"{typeof(ResourceLink).Namespace}.{resourcePath}";
        using var resourceStream = typeof(Program).Assembly.GetManifestResourceStream(name);
        if (resourceStream == null) return string.Empty;
        using var reader = new StreamReader(resourceStream);
        return reader.ReadToEnd();
    }
}