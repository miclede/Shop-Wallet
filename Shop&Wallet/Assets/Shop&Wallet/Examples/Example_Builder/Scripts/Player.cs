using System.Collections.Generic;
using UnityEngine;

namespace ShopWallet.Example_Builder
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Wallet _playerWallet;
        public Wallet playerWallet => _playerWallet;

        [SerializeField] private float incomeAmount;

        public void PrintMoney()
        {
            Dictionary<CurrencyType, float> currencyToMint = new Dictionary<CurrencyType, float>();

            currencyToMint.Add(CurrencyType.Alloy, incomeAmount);
            currencyToMint.Add(CurrencyType.ConsumerGoods, incomeAmount);
            currencyToMint.Add(CurrencyType.Energy, incomeAmount);
            currencyToMint.Add(CurrencyType.Food, incomeAmount);
            currencyToMint.Add(CurrencyType.Minerals, incomeAmount);

            _playerWallet.ReceivePaymentCallback(currencyToMint);
        }
    }
}