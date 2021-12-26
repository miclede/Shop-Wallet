using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace ShopWallet.Example_Builder
{
    public class BobStoreButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Button _myButton;

        private ShopMerchandiseSO _myMerchandise;

        public void InitializeButton(Action<ShopMerchandiseSO, Wallet> purchaseCallback, 
            ShopMerchandiseSO myMerch, Player targetPlayer)
        {
            _myMerchandise = myMerch;
            _myButton.onClick.AddListener(() => purchaseCallback(_myMerchandise, targetPlayer.playerWallet));
            _buttonText.text = _myMerchandise.merchName;
            gameObject.name = myMerch.name;
        }
    }
}