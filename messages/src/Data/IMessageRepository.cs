namespace FP.ContainerTraining.Messages.Data;

public interface IMessageRepository
{
    Task<string> SaveMessage(string user, string content);
    Task<List<DtoMessage>> GetMessages();
}