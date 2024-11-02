using UnityEngine;
using UnityEngine.Events;

    [CreateAssetMenu(menuName = "ScriptableObjects/Item", fileName ="New Item")]
    public class item : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite itemSprite;
        public bool canRemove;  // 保留這個屬性，用來決定是否能移除物品
        public bool existsInScene;  // 標記物品是否存在於第一關場景中
    }
