using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopWallet
{
    public class TransactionProcessor : ScriptableObject
    {
        #region Public Actions
        public Func<Dictionary<CurrencyType, float>, ShopMerchandiseSO, bool> PriceCheck =>
            (holdings, merch) => CanAffordPayment(holdings, merch);

        public Action<Wallet, ShopMerchandiseSO, Action<ShopMerchandiseSO>, Action<ShopMerchandiseSO>> AttemptPurchase =>
            (wallet, merch, successCallback, failureCallback) => 
            AttemptPayment(wallet.walletHoldings, merch, wallet.MakePaymentCallback.Invoke, successCallback, failureCallback);
        #endregion

        #region Internal Logic
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

        private void AttemptPayment(Dictionary<CurrencyType, float> holdings, ShopMerchandiseSO merch,
            Action<Dictionary<CurrencyType, float>> paymentCallback, Action<ShopMerchandiseSO> successCallback, Action<ShopMerchandiseSO> failureCallback)
        {
            if (PriceCheck(holdings, merch))
            {
                paymentCallback(StockValueConversion(merch.merchCost));
                successCallback(merch);
            }
            else
            {
                failureCallback(merch);
            }
        }

        private bool CanAffordPayment(Dictionary<CurrencyType, float> holdings, ShopMerchandiseSO merchandise)
        {
            bool canPurchase = false;
            var merchCost = StockValueConversion(merchandise.merchCost);

            foreach (KeyValuePair<CurrencyType, float> item in merchCost)
            {
                if (!holdings.ContainsKey(item.Key))
                    continue;

                if (holdings[item.Key] >= item.Value)
                {
                    canPurchase = true;
                }
                else if (holdings[item.Key] < item.Value)
                {
                    Debug.Log("We do not have enough: " + item.Key + " we needed: " + merchCost[item.Key] + " but we only had: " + item.Value);
                    canPurchase = false;
                    break;
                }
            }

            return canPurchase;
        }
        #endregion
    }
}