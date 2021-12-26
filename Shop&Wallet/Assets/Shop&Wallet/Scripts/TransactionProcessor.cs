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

        private Func<List<StockValue>, Dictionary<CurrencyType, float>> StockValueConversion =>
            (currencyList) =>
            {
                Dictionary<CurrencyType, float> dict = new Dictionary<CurrencyType, float>();
                
                foreach (var currency in currencyList)
                {
                    try
                    {
                        dict.Add(currency.type, currency.value);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                }

                return dict;
            };

        protected void AttemptPayment(Dictionary<CurrencyType, float> holdings, ShopMerchandiseSO merch,
            Action<Dictionary<CurrencyType, float>> paymentCallback)
        {
            if (PriceCheck(holdings, merch) && onSuccessfulPurchase != null)
            {
                paymentCallback(StockValueConversion(merch.merchCost));
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
            var merchCost = StockValueConversion(merchandise.merchCost);

            foreach (KeyValuePair<CurrencyType, float> item in holdings)
            {
                if (!merchCost.ContainsKey(item.Key))
                    continue;

                if (item.Value >= merchCost[item.Key])
                {
                    canPurchase = true;
                }
                else if (item.Value < merchCost[item.Key])
                {
                    Debug.Log("We do not have enough: " + item.Key + " we needed: " + merchCost[item.Key] + " but we only had: " + item.Value);
                    canPurchase = false;
                    break;
                }
            }

            return canPurchase;
        }
    }
}