using UnityEngine;
using UnityEngine.EventSystems;

public class ReadyToLaunch : LaunchStatus
{
    private Vector2 launchVector = Vector2.up;
    public ReadyToLaunch(Launcher launcher) : base(launcher)
    {
    }


    public override void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetButtonUp("Fire1")) && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        launchVector = (cursorPos - (Vector2)launcher.transform.position).normalized;

        float lineLenght = Mathf.Clamp(Vector3.Distance(launcher.transform.position, cursorPos), 2, 7);
        launcher.SetLinePoistion(launchVector * lineLenght);


        launcher.RenderLine(Input.GetButton("Fire1"));

        if (CanShoot())
        {
            if (Input.GetButtonUp("Fire1"))
                launcher.status = new Launch(launcher, launchVector);
            launcher.SetLineColor(Color.yellow);
        }
        else
            launcher.SetLineColor(Color.red);
    }

    private bool CanShoot() =>
        launchVector.y > 0.3f;

    public override void Restart()
    {

    }
}
