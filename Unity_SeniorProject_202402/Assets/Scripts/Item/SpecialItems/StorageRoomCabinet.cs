using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageRoomCabinet : InteractableItem
{

    public Animator anime;
    public override void Interact()
    {
        ToggleCabinet();
        var OptionPool = GetOptionPoolByName(_OptionPoolName);
        if (OptionPool != null)
        {
            DialogueManager.instance.StartDialogue(dialogue, _partIndex, OptionPool.InteractionOptionPool);
        }
    }

    public void ToggleCabinet()
    {
        anime.SetTrigger("Toggle");
    }
}
