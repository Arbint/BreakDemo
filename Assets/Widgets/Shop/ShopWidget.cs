using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopWidget : Widget
{
    [SerializeField] private ShopSystem shopSystem;
    [SerializeField] ShopItemWidget shopItemWidgetPrefab;
    [SerializeField] private RectTransform shopList;
    [SerializeField] private Button backButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI creditsText;


    List<ShopItemWidget> _shopItemWidgets = new List<ShopItemWidget>();
    private CreditComponent _ownerCreditComponent;
    private ShopItemWidget _currentSelectedShopItemWidget;

    private void Awake()
    {
        backButton.onClick.AddListener(SwithOffShop);
        buyButton.onClick.AddListener(TryPurchase);
    }

    private void SwithOffShop()
    {
        _currentSelectedShopItemWidget = null;
        GameplayWidget.Instance.SwitchToGameplay();
    }

    private void TryPurchase()
    {
        if (!_currentSelectedShopItemWidget || !shopSystem.TryPurchase(_currentSelectedShopItemWidget.GetItem(), _ownerCreditComponent))
            return;
        
        Destroy(_currentSelectedShopItemWidget.gameObject);
        _shopItemWidgets.Remove(_currentSelectedShopItemWidget);
        _currentSelectedShopItemWidget = null;
    }

    public override void SetOwner(GameObject newOwner)
    {
        base.SetOwner(newOwner);
        _ownerCreditComponent = newOwner.GetComponent<CreditComponent>();
        _ownerCreditComponent.OnCreditChanged += UpdateCredit;
        UpdateCredit(_ownerCreditComponent.Credits);
        InitShopItems();
    }

    private void UpdateCredit(int newCredit)
    {
        creditsText.text = $"{newCredit}";
        RefreshShopItemWidgets(newCredit);
    }

    private void RefreshShopItemWidgets(int newCredit)
    {
        foreach (ShopItemWidget shopItemWidget in _shopItemWidgets)
        {
            shopItemWidget.Refresh(newCredit);
        }
    }

    private void InitShopItems()
    {
        foreach (ShopItem shopItem in shopSystem.GetItems())
        {
            AddShopItem(shopItem);
        }
    }

    private void AddShopItem(ShopItem shopItem)
    {
        ShopItemWidget shopItemWidget = Instantiate(shopItemWidgetPrefab, shopList);
        shopItemWidget.Init(shopItem, _ownerCreditComponent.Credits);
        shopItemWidget.OnItemSelected += ItemSelected;
        _shopItemWidgets.Add(shopItemWidget);
    }

    private void ItemSelected(ShopItemWidget shopItemWidget)
    {
        _currentSelectedShopItemWidget = shopItemWidget;
    }
}

