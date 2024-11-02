using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MUI
{
    public class PianoController : MonoBehaviour
    {
        private int _index = 0;
        [SerializeField]
        private string pianoPassword;
        [SerializeField]
        private string pianoKeyString;

        public GameObject piano;

        public Dialogues _Dialogues;
        public GameObject Photo7_Hint;
        public GameObject Photo9_Hint;



        public void KeyClick(string _key)
        {
            Debug.Log(_key);
            pianoKeyString += _key;
            if (pianoPassword == pianoKeyString)
            {
                GameManager.Instance.CompleteMiniGame("Piano");
                Debug.Log("Win");
                DialogueManager.instance.StartDialogue(_Dialogues, 0, null);
                ItemManager.instance.TakeItem("PhotoFragments8"); 
                Invoke("ClosePiano", 2f);
                Photo7_Hint.SetActive(true);
                Photo9_Hint.SetActive(true);
            }
            else
            {
                if (_index <= 5)
                {
                    _index++;
                }
                else
                {
                    _index = 0;
                    pianoKeyString = "";
                    Debug.Log("Wrong");
                }
            }                   
        }
        public void ClosePiano()
        {
            piano.SetActive(false);
            PlayerMovement.instance.canMove = true;
        }
        public void OpenPiano()
        {
            piano.SetActive(true);
            PlayerMovement.instance.canMove = false;
        }
    }
}

