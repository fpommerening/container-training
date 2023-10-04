namespace FP.ContainerTraining.CoffeeMaker.Business;

public class OrderRepository : IOrderRepository
{
    private readonly Queue<Order> _orders = new Queue<Order>();

    private Order? _lastOrder;

    public bool CanHandleOrder()
    {
        return _lastOrder == null || _lastOrder.FinishedAt < DateTime.UtcNow;
    }

    public void AddOrder(string product)
    {
        if (!CanHandleOrder()) return;
        
        var order = new Order
        {
            Product = product
        };
        _lastOrder = order;
        _orders.Enqueue(order);
    }

    public IEnumerable<Order> GetOrders()
    {
        return _orders.ToArray();
    }
}