using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Factories/Ball Factory")]
public class BallFactory : ScriptableObject
{
    [SerializeField]
    private DefauldBall prefDefault;
    [SerializeField]
    private MoneyBall prefMoney;
    [SerializeField]
    private SuicideBall prefSuicide;

    public BallBase GetBallPref(BallType type) => type switch
    {
        BallType.Defauld => prefDefault,
        BallType.Money => prefMoney,
        BallType.Suicide => prefSuicide,
        _ => throw new NotImplementedException(),
    };

}
