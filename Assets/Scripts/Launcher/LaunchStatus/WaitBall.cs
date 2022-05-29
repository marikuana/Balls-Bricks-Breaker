using System.Collections.Generic;
using System.Linq;

public class WaitBall : LaunchStatus
{
    private IEnumerable<BallBase> balls;

    public WaitBall(Launcher launcher, List<BallBase> balls) : base(launcher)
    {
        this.balls = balls;
    }

    public override void Update()
    {
        if (balls.Where(ball => ball != null).Count() == 0)
            launcher.status = new ReadyToLaunch(launcher);
    }

    public override void Restart()
    {
        foreach (var ball in balls.ToList())
        {
            if (ball != null)
                ball.Destroy();
        }
    }
}
