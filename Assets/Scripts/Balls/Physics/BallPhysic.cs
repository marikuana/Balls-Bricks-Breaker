using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BallPhysic : BallPhysicBase
{
    public BallPhysic(BallBase ball) : base(ball)
    {
    }

    public override Vector3 CalculatePosition()
    {
        float distance = Time.deltaTime * ball.speed * Controller.Instance.SimulateSpeed;
        float ballRadius = ball.transform.localScale.x / 2f;
        Vector3 position = ball.transform.position;

        RaycastHit2D hit = Raycast(ball.transform.position, ball.velocity, distance, ballRadius);
        if (hit != default(RaycastHit2D))
        {
            distance -= hit.distance;
            position += (Vector3)ball.velocity * (hit.distance);
            ball.velocity = Vector3.Reflect(ball.velocity, hit.normal);
        }

        return position + ((Vector3)ball.velocity * distance);
    }

    private RaycastHit2D Raycast(Vector2 origin, Vector2 velocity, float distance, float offset = 0f)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, velocity, distance + offset);
        if (hit != default(RaycastHit2D))
        {
            hit.distance -= offset;
        }
        return hit;
    }

    private RaycastHit2D MultiRayCasts(Vector3 origin, Vector3 velocity, float distance, float ballRadius)
    {
        RaycastHit2D mainHit = Raycast(origin, velocity, distance, ballRadius);
        RaycastHit2D leftHit = Raycast((Vector3.Cross(velocity, Vector3.forward) * ballRadius) + origin, velocity, distance);
        RaycastHit2D rightHit = Raycast((Vector3.Cross(velocity, Vector3.back) * ballRadius) + origin, velocity, distance);
        if (mainHit != default(RaycastHit2D))
            return mainHit;
        if (leftHit != default(RaycastHit2D))
            return leftHit;
        if (rightHit != default(RaycastHit2D))
            return rightHit;
        return rightHit;
    }
}

