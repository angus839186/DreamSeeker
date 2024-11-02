using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpecialDialogue : MonoBehaviour
{
    public Dialogues _dialogue;

    public CanvasGroup FadeIOgroup;
    public float TimeToFade;

    public string NextLevelName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.Instance.GameEnd)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(NextLevel());
            }
            else
            {
                DialogueManager.instance.StartDialogue(_dialogue, 0, null);
            }
        }
    }
    IEnumerator NextLevel()
    {
        bool fadein = true;
        while (fadein)
        {
            FadeIOgroup.alpha += TimeToFade * Time.deltaTime;
            if (FadeIOgroup.alpha >= 1)
            {
                fadein = false;
                SceneManager.LoadScene(NextLevelName);
            }
            yield return null;
        }
    }
}
