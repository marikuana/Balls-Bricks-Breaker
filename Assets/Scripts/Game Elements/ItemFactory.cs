using UnityEngine;

[CreateAssetMenu(menuName = "Factories/Item Factory")]
public class ItemFactory : ScriptableObject
{
    [SerializeField]
    private Money moneyPref;

    public Item GetItem(ItemType itemType)
    {
        Item item = itemType switch
        {
            ItemType.Money => moneyPref,
            _ => moneyPref,
        };
        return Instantiate(item);
    }
}
