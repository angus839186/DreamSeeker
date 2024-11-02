using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : InteractableItem
{
    public CanvasGroup FadeIOgroup;
    public float TimeToFade;
    public Transform NextPosition;
    public Transform newCameraPosition;

    public GameObject chair;
    public Vector2 chairOriginPos;
    public GameObject can;
    public Vector2 canOriginPos;

    private void Start()
    {
        chairOriginPos = chair.transform.localPosition;
        canOriginPos = can.transform.localPosition;
    }

    public override void Interact()
    {
        StartCoroutine(FadeInAndOut());
    }

    IEnumerator FadeInAndOut()
    {
        bool fadein = true;
        bool fadeout = false;
        PlayerMovement.instance.canMove = false;

        while (fadein)
        {
            FadeIOgroup.alpha += TimeToFade * Time.deltaTime;
            if (FadeIOgroup.alpha >= 1)
            {
                fadein = false;
                fadeout = true;
                Teleport();
            }
            yield return null; 
        }
        while (fadeout)
        {
            FadeIOgroup.alpha -= TimeToFade * Time.deltaTime;
            if (FadeIOgroup.alpha <= 0)
            {
                fadeout = false;
            }
            yield return null;
        }
    }

    public void Teleport()
    {
        PlayerMovement.instance.canMove = true;
        PlayerMovement.instance.transform.position = NextPosition.transform.position;
        CameraManager.instance.UpdateCameraPosition(newCameraPosition);

        if(!GameManager.Instance.IsMiniGameCompleted("Sokobanbox"))
        {
            ResetSokobanBox();
        }
    }
    public void ResetSokobanBox()
    {
        chair.transform.localPosition = new Vector2(chairOriginPos.x, chairOriginPos.y);
        can.transform.localPosition = new Vector2(canOriginPos.x, canOriginPos.y);
    }
}
