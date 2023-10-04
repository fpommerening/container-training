namespace FP.ContainerTraining.CoffeeMaker.Business;

public interface IOrderRepository
{
    bool CanHandleOrder();
    void AddOrder(string product);
    IEnumerable<Order> GetOrders();
}