using UnityEngine;

public abstract class PhysicBase
{
    protected BallBase ball;

    public PhysicBase(BallBase ball) =>
        this.ball = ball;

    public abstract Vector3 CalculatePosition();
}