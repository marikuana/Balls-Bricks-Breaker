using System;
using System.Collections.Generic;
using System.Linq;

public class LevelController
{
    public Level? Level { get; private set; }

    private List<Item> items;

    public LevelController()
    {
        items = new List<Item>();
    }

    public void LoadLevel(Level level)
    {
        Level = level;
        RestartLevel();
    }

    public void RestartLevel()
    {
        UnloadLevel();

        LoadBlocks();
        LoadMoney();
    }

    public void LoadBlocks()
    {
        foreach (var obj in Level.Blocks)
        {
            Block block = Level.BlockFactory.Get(obj.Type);
            block.Initialize(obj.Position, obj.Heath);
            items.Add(block);
        }
    }

    public void LoadMoney()
    {
        foreach (var obj in Level.MoneyItems)
        {
            Money money = (Money)Level.ItemFactory.Get(obj.ItemType);
            money.Initialize(obj.Position, obj.Money);
            items.Add(money);
        }
    }


    public void UnloadLevel()
    {
        foreach (var item in items)
        {
            if (item != null)
                item.Destroy();
        }
    }

    public bool Complete()
    {
        if (Level == null)
            return false;
        // add predictate in level (enum CompliteType => destroy all blocks, collect all moneys)
        return items.Count(item => item != null && item is Block) == 0;
    }
}