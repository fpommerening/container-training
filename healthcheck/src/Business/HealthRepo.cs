namespace FP.ContainerTraining.Healthcheck.Business;

public class HealthRepo : IHealthRepo
{
    public HealthRepo()
    {
        LastChangeUtc = DateTime.UtcNow;
        IsHealthy = true;
    }

    private bool _isHealthy;

    public bool IsHealthy
    {
        get => _isHealthy;
        set
        {
            _isHealthy = value;
            LastChangeUtc = DateTime.UtcNow;
        }
    }

    public DateTime LastChangeUtc { get; private set; }
}