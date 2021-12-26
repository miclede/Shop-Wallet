using UnityEngine;
using SerializedDictionaries;

namespace ShopWallet
{
    [CreateAssetMenu(fileName = "ShopMerchandiseSO", menuName = "ShopWallet/Merchandise")]
    public class ShopMerchandiseSO : ScriptableObject
    {
        [SerializeField] private string _merchName;
        public string merchName => _merchName;

        [SerializeField] private SerializedDictionary<CurrencyType, float> _merchCost = new SerializedDictionary<CurrencyType, float>();
        public SerializedDictionary<CurrencyType, float> merchCost => _merchCost;
    }
}