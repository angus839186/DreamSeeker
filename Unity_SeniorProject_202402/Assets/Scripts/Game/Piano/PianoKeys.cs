using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoKeys : MonoBehaviour
{
    public Image keys;

    private void Awake()
    {
        keys.GetComponent<Image>();
        keys.alphaHitTestMinimumThreshold = 0.1f;
    }
}
