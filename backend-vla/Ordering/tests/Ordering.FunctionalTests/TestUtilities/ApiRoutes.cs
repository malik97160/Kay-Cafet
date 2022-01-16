namespace Ordering.FunctionalTests.TestUtilities;
public class ApiRoutes
{
    public const string Base = "api";
    public const string Health = Base + "/health";

    // new api route marker - do not delete

public static class OrderLines
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/orderLines";
        public const string GetRecord = $"{Base}/orderLines/{Id}";
        public const string Create = $"{Base}/orderLines";
        public const string Delete = $"{Base}/orderLines/{Id}";
        public const string Put = $"{Base}/orderLines/{Id}";
        public const string Patch = $"{Base}/orderLines/{Id}";
    }

public static class Orders
    {
        public const string Id = "{id}";
        public const string GetList = $"{Base}/orders";
        public const string GetRecord = $"{Base}/orders/{Id}";
        public const string Create = $"{Base}/orders";
        public const string Delete = $"{Base}/orders/{Id}";
        public const string Put = $"{Base}/orders/{Id}";
        public const string Patch = $"{Base}/orders/{Id}";
    }
}
