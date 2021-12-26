using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopWallet
{
    public class TransactionProcessor : ScriptableObject
    {
        public delegate void TransactionCallback(ShopMerchandiseSO merch);
        public TransactionCallback onSuccessfulPurchase;
        public TransactionCallback onFailedPurchase;

        public Func<Dictionary<CurrencyType, float>, ShopMerchandiseSO, bool> PriceCheck =>
            (holdings, merch) => CanAffordPayment(holdings, merch);

        public Action<Wallet, ShopMerchandiseSO> AttemptPurchase =>
            (wallet, merch) => AttemptPayment(wallet.walletHoldings, merch, wallet.MakePaymentCallback);

        protected void AttemptPayment(Dictionary<CurrencyType, float> holdings, ShopMerchandiseSO merch,
            Action<Dictionary<CurrencyType, float>> paymentCallback)
        {
            if (PriceCheck(holdings, merch) && onSuccessfulPurchase != null)
            {
                paymentCallback(merch.merchCost);
                onSuccessfulPurchase(merch);
            }
            else if (onFailedPurchase != null)
            {
                onFailedPurchase(merch);
            }
            else if (onSuccessfulPurchase == null || onFailedPurchase == null)
            {
                throw new Exception("Attempted to purchase merchandise without opening store.");
            }
        }

        protected bool CanAffordPayment(Dictionary<CurrencyType, float> holdings, ShopMerchandiseSO merchandise)
        {
            bool canPurchase = false;

            foreach (KeyValuePair<CurrencyType, float> item in holdings)
            {
                if (item.Value >= merchandise.merchCost[item.Key])
                {
                    canPurchase = true;
                }
                else if (item.Value < merchandise.merchCost[item.Key])
                {
                    Debug.Log("We do not have enough: " + item.Key + " we needed: " + merchandise.merchCost[item.Key] + " but we only had: " + item.Value);
                    canPurchase = false;
                    break;
                }
            }

            return canPurchase;
        }
    }
}