public class WaitBall : LaunchStatus
{
    public WaitBall(Launcher launcher) : base(launcher)
    {
    }

    public override void Update()
    {
        if (Launcher.Balls == 0)
            launcher.status = new ReadyToLaunch(launcher);
    }
}
