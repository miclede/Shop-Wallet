using System;
using UnityEngine;
using UnityEngine.Events;

namespace ShopWallet
{
    [Serializable] public class TransactionEvent : UnityEvent<ShopMerchandiseSO> { }

    public class Storefront : MonoBehaviour
    {
        public delegate void OnTransactionEvent(ShopMerchandiseSO merch);
        public OnTransactionEvent eventSuccess;
        public OnTransactionEvent eventFailed;

        [SerializeField] protected TransactionProcessor _transactionProcessor;
        [SerializeField] protected ShopCatalogSO _shopCatalog;
        public ShopCatalogSO shopCatalog => _shopCatalog;

        //TransactionEvent will handle the argument passing during event Invoke()
        private TransactionEvent onSuccessfulTransaction;
        private TransactionEvent onFailedTransaction;

        private bool _isOpen;

        public Action<ShopMerchandiseSO, Wallet> AttemptPurchase =>
            (merch, wallet) =>
            {
                if (_isOpen)
                    _transactionProcessor.AttemptPurchase(wallet, merch);
                else Debug.Log("You need to open the store to buy from it.");
            };


        private void Awake()
        {
            if (onSuccessfulTransaction == null)
                onSuccessfulTransaction = new TransactionEvent();
            if (onFailedTransaction == null)
                onFailedTransaction = new TransactionEvent();

            _isOpen = false;
        }

        private void Start()
        {
            onSuccessfulTransaction.AddListener((merch) => eventSuccess(merch));
            onFailedTransaction.AddListener((merch) => eventFailed(merch));
        }

        public void OpenStorefront()
        {
            _isOpen = true;
            _transactionProcessor.onSuccessfulPurchase += (merch) => onSuccessfulTransaction.Invoke(merch);
            _transactionProcessor.onFailedPurchase += (merch) => onFailedTransaction.Invoke(merch);
        }

        public void CloseStorefront()
        {
            _isOpen = false;
            _transactionProcessor.onSuccessfulPurchase -= (merch) => onSuccessfulTransaction.Invoke(merch);
            _transactionProcessor.onFailedPurchase -= (merch) => onFailedTransaction.Invoke(merch);
        }
    }
}