using UnityEngine;
using System.Collections.Generic;

public interface IPurchaseListener
{
    public bool HandlePurchase(Object newPurchase);
}

public class CreditComponent : MonoBehaviour
{
    [SerializeField] int credits = 100;
   
    public int Credits
    {
        get => credits;
        private set => credits = value;
    }

    public delegate void OnCreditChangedDelegate(int newCredit);
    public event OnCreditChangedDelegate OnCreditChanged;

    List<IPurchaseListener> _listeners = new List<IPurchaseListener>();

    private void Awake()
    {
        foreach(IPurchaseListener listener in GetComponents<IPurchaseListener>())
        {
            Debug.Log($"found purchase listner: {listener}");
            _listeners.Add(listener);            
        }
    }

    private void BroadcastPurchase(Object item)
    {
        foreach(IPurchaseListener listner in _listeners)
        {
            if(listner.HandlePurchase(item))
                return;
        }
    }

    public bool Purchase(int price, Object item)
    {
        if (Credits < price) return false;

        ChangeCredit(-price);
        BroadcastPurchase(item);

        return true;
    }

    public void ChangeCredit(int rewardAmt)
    {
        Credits += rewardAmt;
        if (Credits <= 0)
        {
            Credits = 0;
        }
        
        OnCreditChanged?.Invoke(Credits); 
    }
}
