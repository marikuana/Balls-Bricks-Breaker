using System.Linq;

public class WaitBall : LaunchStatus
{
    public WaitBall(Launcher launcher) : base(launcher)
    {
    }

    public override void Update()
    {
        if (launcher.GetLaunchedBall().Count() == 0)
            launcher.status = new ReadyToLaunch(launcher);
    }
}
