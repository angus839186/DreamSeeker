using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static InteractableItem;

namespace MUI
{
    public class PasswordLockController : MonoBehaviour
    {
        public TextMeshProUGUI[] passwordNumText;
        [SerializeField]
        private string password;

        public GameObject passwordLocker;
        public GameObject passwordAnime;
        public Animator animator;

        public Dialogues _dialogues;

        private void Start()
        {
            for (int i = 0; i < passwordNumText.Length; i++)
            {
                passwordNumText[i].text = "0";
            }
        }

        public void UpButtinClick(int _index)
        {
            int value;
            switch ( _index)
            {
                case 1:
                    value = Int32.Parse(passwordNumText[0].text);
                    if (value < 9)
                    { 
                        value++;
                    }
                    else
                    {
                        value = 0;
                    }
                    passwordNumText[0].text = value.ToString();
                    break;
                case 2:
                    value = Int32.Parse(passwordNumText[1].text);
                    if (value < 9)
                    {
                        value++;
                    }
                    else
                    {
                        value = 0;
                    }
                    passwordNumText[1].text = value.ToString();
                    break;
                case 3:
                    value = Int32.Parse(passwordNumText[2].text);
                    if (value < 9)
                    {
                        value++;
                    }
                    else
                    {
                        value = 0;
                    }
                    passwordNumText[2].text = value.ToString();
                    break;
                case 4:
                    value = Int32.Parse(passwordNumText[3].text);
                    if (value < 9)
                    {
                        value++;
                    }
                    else
                    {
                        value = 0;
                    }
                    passwordNumText[3].text = value.ToString();
                    break;

            }
            CheckClick();
        }

        public void DownButtinClick(int _index)
        {
            int value;
            switch (_index)
            {
                case 1:
                    value = Int32.Parse(passwordNumText[0].text);
                    if (value > 0)
                    {
                        value--;
                    }
                    else
                    {
                        value = 9;
                    }
                    passwordNumText[0].text = value.ToString();
                    break;
                case 2:
                    value = Int32.Parse(passwordNumText[1].text);
                    if (value > 0)
                    {
                        value--;
                    }
                    else
                    {
                        value = 9;
                    }
                    passwordNumText[1].text = value.ToString();
                    break;
                case 3:
                    value = Int32.Parse(passwordNumText[2].text);
                    if (value > 0)
                    {
                        value--;
                    }
                    else
                    {
                        value = 9;
                    }
                    passwordNumText[2].text = value.ToString();
                    break;
                case 4:
                    value = Int32.Parse(passwordNumText[3].text);
                    if (value > 0)
                    {
                        value--;
                    }
                    else
                    {
                        value = 9;
                    }
                    passwordNumText[3].text = value.ToString();
                    break;
            }
            CheckClick();
        }

        public void CheckClick()
        {
            string value = 
                passwordNumText[0].text +
                passwordNumText[1].text +
                passwordNumText[2].text +
                passwordNumText[3].text;
            if (value != password)
            {
                return;
            }
            else
            {
                GameManager.Instance.CompleteMiniGame("Password");
                CloseSafeCase();
                StartCoroutine(UnlockSafeRoutine());
            }
        }

        public void OpenSafeCase()
        {
            passwordLocker.SetActive(true);
            PlayerMovement.instance.canMove = false;
        }
        public void CloseSafeCase()
        {
            passwordLocker.SetActive(false);
            PlayerMovement.instance.canMove = true;
            for (int i = 0; i < passwordNumText.Length; i++)
            {
                passwordNumText[i].text = "0";
            }
        }
        public IEnumerator UnlockSafeRoutine()
        {
            passwordAnime.SetActive(true);
            animator.Play("open");

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            yield return new WaitForSeconds(1f);

            passwordAnime.SetActive(false);

            DialogueManager.instance.StartDialogue(_dialogues, 0, null);

            ItemManager.instance.TakeItem("StorageroomKey");
        }
    }
}

