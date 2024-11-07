namespace ConsignmentIntegration.Logic.Interfaces
{
    public interface ISftpService
    {
        Task<string> RetrieveConsignment(string fileName);
    }
}
