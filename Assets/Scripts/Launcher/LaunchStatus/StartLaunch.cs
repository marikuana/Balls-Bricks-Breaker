using UnityEngine;

public class StartLaunch : LaunchStatus
{
    private Vector3 shootDirection;

    public StartLaunch(Launcher launcher, Vector3 shootDirection) : base(launcher)
    {
        this.shootDirection = shootDirection;
    }

    public override void Update()
    {
        Launching launching = new Launching(launcher);
        launcher.StartLaunch(shootDirection, launching.EndLaunch);
        launcher.status = launching;
    }
}