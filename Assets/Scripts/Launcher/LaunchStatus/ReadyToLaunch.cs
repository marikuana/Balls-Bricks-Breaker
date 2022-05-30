using UnityEngine;
using UnityEngine.EventSystems;

public class ReadyToLaunch : LaunchStatus
{
    private int pointerId = -1;
    public ReadyToLaunch(Launcher launcher) : base(launcher)
    {
#if !UNITY_EDITOR
        pointerId = 0;
#endif
        launcher.RenderLine(false);
    }


    public override void Update()
    {
        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject(pointerId))
        {
            launcher.status = new Aiming(launcher);
        }
    }

    

    public override void Restart()
    {
    }
}
