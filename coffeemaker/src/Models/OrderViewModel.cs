namespace FP.ContainerTraining.CoffeeMaker.Models;

public class OrderViewModel
{
    public DateTime CreatedAt { get; set; }

    public string Product { get; set; }

    public DateTime? FinishedAt { get; set; }
}