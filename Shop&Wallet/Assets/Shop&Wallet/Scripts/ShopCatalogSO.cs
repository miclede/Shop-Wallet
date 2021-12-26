using System.Collections.Generic;
using UnityEngine;

namespace ShopWallet
{
    [CreateAssetMenu(fileName = "ShopCatalogSO", menuName = "ShopWallet/Catalog")]
    public class ShopCatalogSO : ScriptableObject
    {
        [SerializeField] private List<ShopMerchandiseSO> _shopStock = new List<ShopMerchandiseSO>();
        public List<ShopMerchandiseSO> shopStock => _shopStock;
    }
}