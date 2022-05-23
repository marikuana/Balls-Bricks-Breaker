public class Launching : LaunchStatus
{
    private bool launching;

    public Launching(Launcher launcher) : base(launcher)
    {
        launching = true;
        launcher.RenderLine(true);
        
    }

    public override void Update()
    {
        if (launching == false)
        {
            launcher.RenderLine(false);
            launcher.status = new WaitBall(launcher);
        }
    }

    public void EndLaunch()
    {
        launching = false;
    }
}
