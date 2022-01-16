namespace ProductManagement.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class Familys
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/familys";
        public const string GetRecord = $"{Base}/familys/{Id}";
        public const string Create = $"{Base}/familys";
        public const string Delete = $"{Base}/familys/{Id}";
        public const string Put = $"{Base}/familys/{Id}";
        public const string Patch = $"{Base}/familys/{Id}";
    }

public static class Ingredients
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/ingredients";
        public const string GetRecord = $"{Base}/ingredients/{Id}";
        public const string Create = $"{Base}/ingredients";
        public const string Delete = $"{Base}/ingredients/{Id}";
        public const string Put = $"{Base}/ingredients/{Id}";
        public const string Patch = $"{Base}/ingredients/{Id}";
    }

public static class Products
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/products";
        public const string GetRecord = $"{Base}/products/{Id}";
        public const string Create = $"{Base}/products";
        public const string Delete = $"{Base}/products/{Id}";
        public const string Put = $"{Base}/products/{Id}";
        public const string Patch = $"{Base}/products/{Id}";
    }
}
