using UnityEngine;

public class Aiming : LaunchStatus
{
    public Aiming(Launcher launcher) : base(launcher)
    {
        SetLineEndPosition(GetCursorPosition());
        launcher.RenderLine(true);
    }

    public override void Update()
    {
        Vector2 cursorPos = GetCursorPosition();
        SetLineEndPosition(cursorPos);

        Vector2 launchDirection = GetLaunchDirection(cursorPos);

        if (CanShoot(launchDirection))
            launcher.SetLineColor(Color.yellow);
        else
            launcher.SetLineColor(Color.red);

        if (!Input.GetButton("Fire1"))
        {
            if (CanShoot(launchDirection))
                launcher.status = new Launch(launcher, launchDirection);
            else
                launcher.status = new ReadyToLaunch(launcher);
        }
    }

    private void SetLineEndPosition(Vector2 position)
    {
        float lineLenght = Mathf.Clamp(Vector3.Distance(launcher.transform.position, position), 2, 7);
        launcher.SetLinePoistion(GetLaunchDirection(position) * lineLenght);
    }

    private Vector2 GetLaunchDirection(Vector2 cursorPosition) =>
        (cursorPosition - (Vector2)launcher.transform.position).normalized;

    private Vector2 GetCursorPosition() =>
        Camera.main.ScreenToWorldPoint(Input.mousePosition);

    public override void Restart()
    {
        launcher.RenderLine(false);
    }

    private bool CanShoot(Vector2 launchDirection) =>
        launchDirection.y > 0.3f;
}
