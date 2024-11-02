using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MUI
{
    public class Tooltips : MonoBehaviour
    {
        public TextMeshProUGUI detailText;

        public void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowTooltip()
        {
            gameObject.SetActive(true);
        }
        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        public void UpdateTooltip(string _detailText)
        {
            detailText.text = _detailText;
        }

        public void SetPosition(Vector2 _pos)
        {
            transform.localPosition = _pos;
        }
    }
}
