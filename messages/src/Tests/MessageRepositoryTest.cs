using FP.ContainerTraining.Messages.Data;
using Xunit;

namespace FP.ContainerTraining.Messages.Tests;

public class MessageRepositoryTest
{
    [Fact]
    public async Task SaveMessage_Should_WriteToRealDatabase_And_GetMessages_ShouldReadIt()
    {
        var connectionString = "mongodb://localhost";

        var inMemorySettings = new Dictionary<string, string> {
            {"ConnectionStrings:MessageDB", connectionString}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        var repository = new MessageRepository(configuration);
        var testUser = "IntegrationTester";
        var testContent = "Dies ist ein Test in einer echten DB";

        var savedId = await repository.SaveMessage(testUser, testContent);

        var messages = await repository.GetMessages();

        Assert.NotNull(savedId);
        Assert.NotEmpty(savedId);

        var loadedMessage = messages.FirstOrDefault();
        Assert.NotNull(loadedMessage);
        
        Assert.Equal(testUser, loadedMessage.User);
        Assert.Equal(testContent, loadedMessage.Content);
        Assert.Equal(savedId, loadedMessage.Id.ToString());
    }
}