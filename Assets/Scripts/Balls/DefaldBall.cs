using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaldBall : BallBase
{
    public override void Impact(Block block)
    {
        block.Damage(Damage);
    }
}
