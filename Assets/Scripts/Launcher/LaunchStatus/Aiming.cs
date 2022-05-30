using UnityEngine;

public class Aiming : LaunchStatus
{
    private Vector2 launchVector = Vector2.up;

    public Aiming(Launcher launcher) : base(launcher)
    {
        launcher.RenderLine(true);
    }

    public override void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        launchVector = (cursorPos - (Vector2)launcher.transform.position).normalized;

        float lineLenght = Mathf.Clamp(Vector3.Distance(launcher.transform.position, cursorPos), 2, 7);
        launcher.SetLinePoistion(launchVector * lineLenght);

        if (CanShoot())
            launcher.SetLineColor(Color.yellow);
        else
            launcher.SetLineColor(Color.red);

        if (!Input.GetButton("Fire1"))
        {
            if (CanShoot())
                launcher.status = new Launch(launcher, launchVector);
            else
                launcher.status = new ReadyToLaunch(launcher);
        }

    }

    public override void Restart()
    {

    }

    private bool CanShoot() =>
        launchVector.y > 0.3f;
}
