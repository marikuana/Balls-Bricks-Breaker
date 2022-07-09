using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Block Factory/Random Block Factory")]
public class RandomBlockFactory : BaseBlockFactory
{
    [SerializeField]
    protected Sprite[] sprites;

    public override Block Get(BlockType blockType)
    {
        Block block = Instantiate(pref);
        block.SetSprite(sprites[Random.Range(0, sprites.Length)]);
        return block;
    }
}
