using MUI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MUI
{
    public class Scene0TriggerEvent : SerializedMonoBehaviour
    {
        private Collider2D collider;
        private InkDialogueSystem ink;

        public bool isTouched;

        //public int inkEventNumber;
        public  TextAsset inkAssets;

        private void Start()
        {
            collider = GetComponent<Collider2D>();
            ink = GameObject.Find("InkDialogueSystem").GetComponent<InkDialogueSystem>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" && !isTouched)
            {
                Player0Movement player0 = other.gameObject.GetComponent<Player0Movement>();
                player0.isMoved = false;
                isTouched = true;
                ink.StartDialogue(inkAssets);
                
            }
        }
    }

}
