using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShopWallet
{
    public class Storefront : MonoBehaviour
    {
        public delegate void OnTransactionEvent(ShopMerchandiseSO merch);
        public OnTransactionEvent eventSuccess;
        public OnTransactionEvent eventFailed;

        [SerializeField] private TransactionProcessor _transactionProcessor;
        [SerializeField] private List<ShopMerchandiseSO> _shopStock = new List<ShopMerchandiseSO>();
        public List<ShopMerchandiseSO> shopStock => _shopStock;

        private Action<ShopMerchandiseSO> eventSuccessCallback =>
            (merch) => eventSuccess(merch);
        private Action<ShopMerchandiseSO> eventFailedCallback =>
            (merch) => eventFailed(merch);

        public Action<ShopMerchandiseSO, Wallet> AttemptPurchase =>
            (merch, wallet) =>
            {
                _transactionProcessor.AttemptPurchase(wallet, merch, eventSuccessCallback, eventFailedCallback);
            };

        private void Awake()
        {
            if (!_transactionProcessor)
                _transactionProcessor = (TransactionProcessor)AssetDatabase.LoadAssetAtPath("Assets/Shop&Wallet/TransactionProcessor.asset", typeof(TransactionProcessor));
        }
    }
}