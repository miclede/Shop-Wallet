using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShopWallet.Example_Builder
{
    public class BobsStore : MonoBehaviour
    {
        [SerializeField] private Storefront _myStorefront;
        [SerializeField] private List<BobStoreButton> _storeButtons;
        [SerializeField] private Player _targetPlayer;

        private void OnEnable()
        {
            _myStorefront.eventSuccess += BuildStructure;
            _myStorefront.eventFailed += FailedBuilding;
        }

        private void Start()
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            if (_storeButtons.Count == _myStorefront.shopStock.Count)
            {
                int i = 0;
                foreach (var item in _myStorefront.shopStock)
                {
                    _storeButtons[i].InitializeButton(_myStorefront.AttemptPurchase, item, _targetPlayer);
                    i++;
                }
            }
            else throw new Exception("The button list count is not equal to the item count in: " + _myStorefront.shopStock);
        }

        public void BuildStructure(ShopMerchandiseSO merch)
        {
            Debug.Log("We have built: " + merch.merchName);
        }

        public void FailedBuilding(ShopMerchandiseSO merch)
        {
            Debug.Log("We cannot build: " + merch.merchName);
        }

        private void OnDisable()
        {
            _myStorefront.eventSuccess -= BuildStructure;
            _myStorefront.eventFailed -= FailedBuilding;
        }
    }
}