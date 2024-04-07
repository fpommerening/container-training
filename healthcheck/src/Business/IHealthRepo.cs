namespace FP.ContainerTraining.Healthcheck.Business;

public interface IHealthRepo
{
    bool IsHealthy { get; set; }
    DateTime LastChangeUtc { get; }
}