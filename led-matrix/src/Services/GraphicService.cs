using FP.ContainerTraining.RaspiLedMatrix.Business;

namespace FP.ContainerTraining.RaspiLedMatrix.Services;

public class GraphicService : BackgroundService
{
    private readonly ILogger<TextWriterService> _logger;
    private readonly MatrixRepository _matrixRepository;

    public GraphicService(ILogger<TextWriterService> logger, MatrixRepository matrixRepository)
    {
        _logger = logger;
        _matrixRepository = matrixRepository;
        _logger = logger;
        _matrixRepository = matrixRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!string.IsNullOrEmpty(_matrixRepository.Graphic))
            {
                switch (_matrixRepository.Graphic.ToLowerInvariant())
                {
                    case "smiley":
                        await DrawSmiley();
                        break;
                    case "lines":
                        await DrawLines();
                        break;
                    case "tree":
                        await DrawTree();
                        break;
                    default:
                        await DrawCross();
                        break;
                }
            }

            await Task.Delay(500, stoppingToken);

        }
    }



    private Task DrawCross()
    {
        var cross = new byte[]
        {
            0x81,
            0xC3,
            0x66,
            0x3C,
            0x3C,
            0x66,
            0xC3,
            0x81
        };
        for (var digit = 0; digit < 8; digit++)
        {
            _matrixRepository[0, digit] = cross[digit];
        }

        _matrixRepository.Flush();
        return Task.CompletedTask;
    }

    private Task DrawSmiley()
    {

        var smiley = new byte[]
        {
            0x3C,
            0x42,
            0xA5,
            0x81,
            0xA5,
            0x99,
            0x42,
            0x3C
        };

        for (var digit = 0; digit < 8; digit++)
        {
            _matrixRepository[0, digit] = smiley[digit];
        }

        _matrixRepository.Flush();
        return Task.CompletedTask;
    }
    
    private Task DrawTree()
    {

        var tree = new byte[]
        {
            0x00,
            0x18,
            0x18,
            0x3C,
            0x3C,
            0x7E,
            0x5A,
            0x18
        };

        for (var digit = 0; digit < 8; digit++)
        {
            _matrixRepository[0, digit] = tree[digit];
        }

        _matrixRepository.Flush();
        return Task.CompletedTask;
    }

    private async Task DrawLines()
    {
        _matrixRepository.ClearAll();

        for (var row = 0; row < 8; row++)
        {
            byte colValue = 0x00;
            for (var col = 0; col < 8; col++)
            {
                colValue += (byte)(0x01 << col);

                _matrixRepository[0, row] = colValue;
                _matrixRepository.Flush();
                await Task.Delay(500);

                if (_matrixRepository.Graphic != "lines")
                {
                    return;
                }
            }
        }
    }
}