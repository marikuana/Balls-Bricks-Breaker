using System;

[Serializable]
public abstract class LevelItems : LevelObjects
{
    public abstract ItemType ItemType { get; }
}
