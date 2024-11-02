using UnityEngine;
using UnityEngine.Events;

    [CreateAssetMenu(menuName = "ScriptableObjects/Item", fileName ="New Item")]
    public class item : ScriptableObject
    {
        public string itemName;
        public string description;
        public Sprite itemSprite;
        public bool canRemove;  // �O�d�o���ݩʡA�ΨӨM�w�O�_�ಾ�����~
        public bool existsInScene;  // �аO���~�O�_�s�b��Ĥ@��������
    }
