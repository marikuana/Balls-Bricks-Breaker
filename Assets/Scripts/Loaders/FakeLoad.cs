using System.Threading.Tasks;

public class FakeLoad : ILoader
{
    private int millisecondsDelay;

    public FakeLoad(int millisecondsDelay) =>
        this.millisecondsDelay = millisecondsDelay;

    public async Task Load()
    {
        await Task.Delay(millisecondsDelay);
    }
}
