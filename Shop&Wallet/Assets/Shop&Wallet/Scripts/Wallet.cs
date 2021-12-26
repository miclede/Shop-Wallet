using System.Collections.Generic;
using UnityEngine;
using System;

namespace ShopWallet
{
    public enum CurrencyType
    {
        Energy,
        Food,
        Minerals,
        ConsumerGoods,
        Alloy
    }

    public class Wallet : MonoBehaviour
    {
        public Action<Dictionary<CurrencyType, float>> MakePaymentCallback => (cost) => MakePayment(cost);

        protected Dictionary<CurrencyType, float> _walletHoldings = new Dictionary<CurrencyType, float>();
        public Dictionary<CurrencyType, float> walletHoldings => _walletHoldings;

        protected virtual void Awake()
        {
            InitializeHoldings();
        }

        public void ReceivePayment(Dictionary<CurrencyType, float> payment)
        {
            foreach (KeyValuePair<CurrencyType, float> currency in payment)
            {
                _walletHoldings[currency.Key] += currency.Value;
            }
        }

        protected void InitializeHoldings()
        {
            foreach (CurrencyType currency in Enum.GetValues(typeof(CurrencyType)))
            {
                _walletHoldings.Add(currency, 0);
            }
        }

        private void MakePayment(Dictionary<CurrencyType, float> cost)
        {
            foreach (KeyValuePair<CurrencyType, float> currency in cost)
            {
                if (_walletHoldings[currency.Key] - cost[currency.Key] >= 0)
                    _walletHoldings[currency.Key] -= cost[currency.Key];
                else break;
            }
        }
    }
}