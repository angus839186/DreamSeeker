using Ink.Parsed;
using UnityEngine;

namespace MUI
{
    public class NPC_EnvoyEvent : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Collider2D collider;
        private InkDialogueSystem ink;

        public bool isTouched;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            ink = GameObject.Find("InkDialogueSystem").GetComponent<InkDialogueSystem>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player" && !isTouched)
            {
                isTouched = true;
                //ink.StartDialogue(ink._inkAssets); 
            }
        }
    }
}

