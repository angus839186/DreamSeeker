using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MUI
{
    public class Scenefader : MonoBehaviour
    {
        public Image BlackImage;
        [SerializeField] private float alpha;

        private void Start()
        {
            StartCoroutine(FadeIn());
        }

        public void FadeTo(string _sceneName)
        {
            StartCoroutine(FadeOut(_sceneName));
        }

        IEnumerator FadeIn()
        {
            alpha = 1;

            while (alpha > 0)
            {
                alpha -= Time.deltaTime;
                BlackImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
        }
        IEnumerator FadeOut(string sceneName)
        {
            alpha = 0;

            while (alpha < 1)
            {
                alpha += Time.deltaTime;
                BlackImage.color = new Color(0, 0, 0, alpha);
                yield return null;
            }
            SceneManager.LoadScene(sceneName);
        }


    }
}
