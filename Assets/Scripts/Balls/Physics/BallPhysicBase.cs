using UnityEngine;

public abstract class BallPhysicBase
{
    protected BallBase ball;

    public BallPhysicBase(BallBase ball) =>
        this.ball = ball;

    public abstract Vector3 CalculatePosition();
}
