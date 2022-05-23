public abstract class LaunchStatus
{
    protected Launcher launcher;

    public LaunchStatus(Launcher launcher)
    {
        this.launcher = launcher;
    }

    public abstract void Update();
}
