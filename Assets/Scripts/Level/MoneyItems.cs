using System;

[Serializable]
public class MoneyItems : LevelItems
{
    public override ItemType ItemType => ItemType.Money;
    public int Money;
}