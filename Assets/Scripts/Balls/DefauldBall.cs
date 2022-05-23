using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefauldBall : BallBase
{
    public override void Impact(Block block)
    {
        block.Damage(Damage);
    }
}
