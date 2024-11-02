using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask interactableLayer;
    public float interactionDistance;
    public GameObject interactPrompt;

    public GameObject playerBody;

    private void Update()
    {
        if (DialogueManager.instance.isDialoguing)
            return;
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(playerBody.transform.position, interactionDistance, interactableLayer);
        if (hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                InteractableItem interactable = hit.GetComponent<InteractableItem>();
                if (interactable != null)
                {
                    interactPrompt.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        interactable.Interact();
                    }
                    break; 
                }
                else
                {
                    interactPrompt.SetActive(false);
                }
            }
        }
    }

    // Optionally, you can add a Gizmos function to visualize the circle in the Unity editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerBody.transform.position, interactionDistance);
    }
}
