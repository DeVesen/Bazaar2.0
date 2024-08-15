namespace DeVesen.Bazaar.Server.Domain;

public record SalesOrder(IEnumerable<SalesOrder.Position> Articles)
{
    public record Position(long Number, double Price);
}