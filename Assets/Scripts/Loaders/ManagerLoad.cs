using System.Threading.Tasks;

public class ManagerLoad : ILoader
{
    public Task Load()
    {
        _ = Manager.Instance;
        return Task.CompletedTask;
    }
}
