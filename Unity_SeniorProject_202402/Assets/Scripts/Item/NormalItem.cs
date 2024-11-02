using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalItem : InteractableItem
{
    public override void Interact()
    {
        var OptionPool = GetOptionPoolByName(_OptionPoolName);
        if(OptionPool!= null)
        {
            DialogueManager.instance.StartDialogue(dialogue, _partIndex, OptionPool.InteractionOptionPool);
        }
    }
}
