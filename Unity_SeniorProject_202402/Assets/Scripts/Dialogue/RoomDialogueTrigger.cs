using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDialogueTrigger : MonoBehaviour
{
    public Dialogues _dialogue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DialogueManager.instance.StartDialogue(_dialogue, 0, null);
            this.gameObject.SetActive(false);
        }
    }
}
