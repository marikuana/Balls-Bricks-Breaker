using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Block Factory/Default Block Factory")]
public class DefaultBlockFactory : BaseBlockFactory
{
    [SerializeField]
    private Sprite green, orange, blue, red;

    public override Block Get(BlockType blockType)
    {
        Sprite sprite = blockType switch
        {
            BlockType.Green => green,
            BlockType.Orange => orange,
            BlockType.Blue => blue,
            BlockType.Red => red,
            _ => null
        };

        Block block = Instantiate(pref).Init(sprite);
        return block;
    }
}