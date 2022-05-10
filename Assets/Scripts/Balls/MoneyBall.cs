using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBall : BallBase
{
    public override void Impact(Block block)
    {
        Controller.Instance.ProgressData.AddMoney(1);
    }
}
