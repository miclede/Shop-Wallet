using ShopWallet;
using System;
using UnityEditor;

namespace SerializedDictionaries
{
    [Serializable] public class SerializedDictionary<TKey, TValue> : UnitySerializedDictionary<TKey, TValue> { }

    [CustomPropertyDrawer(typeof(SerializedDictionary<CurrencyType, float>))]
    public class CurrencyDictionaryDrawer : DictionaryDrawer<CurrencyType, float> { }
}