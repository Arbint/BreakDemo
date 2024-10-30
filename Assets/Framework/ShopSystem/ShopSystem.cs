using UnityEngine;

[CreateAssetMenu (menuName = ("Shop/ShopSystem"))]
public class ShopSystem : ScriptableObject
{
    [SerializeField] ShopItem[] shopItems;
       
    public ShopItem[] GetItems()
    {
        return shopItems;
    }

    public bool TryPurchase(ShopItem selectedItem, CreditComponent purchaser)
    {
        return purchaser.Purchase(selectedItem.price, selectedItem.item);
    }
}
