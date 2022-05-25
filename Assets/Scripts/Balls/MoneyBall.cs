using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBall : BallBase
{
    public override void Impact(Block block)
    {
        Manager.Instance.ProgressData.AddMoney(1);
    }
}
