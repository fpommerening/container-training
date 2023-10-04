namespace FP.ContainerTraining.CoffeeMaker.Business;

public class Order
{
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public string Product { get; set; }

    public DateTime? FinishedAt
    {
        get
        {
            var finishedAt = CreatedAt.AddMinutes(2);
            if (finishedAt < DateTime.UtcNow)
            {
                return finishedAt;
            }

            return (DateTime?) null;
        }
    }
}