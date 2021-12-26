using UnityEngine;
using System;
using System.Collections.Generic;

namespace ShopWallet
{
    [CreateAssetMenu(fileName = "ShopMerchandiseSO", menuName = "ShopWallet/Merchandise")]
    public class ShopMerchandiseSO : ScriptableObject
    {
        [SerializeField] private string _merchName;
        public string merchName => _merchName;

        [SerializeField] private Sprite _merchImage;
        public Sprite merchImage => _merchImage;

        [SerializeField] private int _merchID;
        private int merchID => _merchID;

        [SerializeField] private List<StockValue> _merchCost = new List<StockValue>();
        public List<StockValue> merchCost => _merchCost;
    }

    [Serializable]
    public struct StockValue
    {
        [SerializeField] private CurrencyType _type;
        public CurrencyType type => _type;
        [SerializeField] private float _value;
        public float value => _value;
    }
}