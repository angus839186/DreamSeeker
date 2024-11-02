using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

namespace MUI
{
    public class InkDialogueSystem : SerializedMonoBehaviour
    {
        private string textLabel;
        public TextMeshProUGUI diaglogueText;//textLabel
        public TextMeshProUGUI diaglogueReText;
        public Button[] btns;
        [SerializeField]
        Story story = null;
        private TextAsset inkAssets;

        public GameObject inkCanvas;
        public GameObject ReviewCanvas;
        public GameObject choiceCanvas;

        [SerializeField] private bool textFinish;
        [SerializeField] private float textSpeed;

        [Header("�Y��")]
        public GameObject face01,face02;

        private void Start()
        {
            inkCanvas.SetActive(false);
            choiceCanvas.SetActive(false);
        }

        public bool StartDialogue(TextAsset _inkAssets) 
        {
            if (story != null) { return false; }
            story = new Story(_inkAssets.text);  //�� Story ��l��
            inkAssets = _inkAssets;

            if (inkAssets.text.Contains("B"))
            {
                face02.SetActive(true);
            }
            else
            {
                face02.SetActive(false);
            }
            inkCanvas.SetActive(true);
            NextDialogue();
            return true;
        }

        public void NextDialogue()
        {
            if (story == null) return;

            ////�p�Gstory�����~�� && �S���ﶵ�A�h�N���ܵ���
            if (!story.canContinue && story.currentChoices.Count == 0)
            {
                Debug.Log("END");
                story = null;
                inkCanvas.SetActive(false);
                Player0Movement player0 = GameObject.Find("Player").GetComponent<Player0Movement>();
                player0.isMoved = true;
                return;
            }

            //���o�ثe��ܿﶵ�ƶq�A�p�G > 0 �h�]�w�ﶵ���s
            if (story.currentChoices.Count > 0) SetChoices();

            //�p�G�i�H�~��U�@�y��ܡA���� story.Continue()
            if (story.canContinue && textFinish)
            {
                StartCoroutine(SetTextUI());
                //diaglogueText.text = story.Continue();
                //diaglogueReText.text += diaglogueText.text;
            }
        }

        //�̷ӿﶵ�ƶq�A�]�m���s ��r �� Active
        private void SetChoices()
        {
            choiceCanvas.SetActive(true);
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                btns[i].gameObject.SetActive(true);
                btns[i].GetComponentInChildren<TextMeshProUGUI>().text = story.currentChoices[i].text;
            }
        }

        public void MakeChoice(int _Index)
        {
            //�ϥ� ChooseChoiceIndex ��ܷ�e�ﶵ
            story.ChooseChoiceIndex(_Index);

            //��ܧ��A�N���s����
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].gameObject.SetActive(false);
            }
            choiceCanvas.SetActive(false);
            NextDialogue();
        }

        //���L�G��
        public void SkipStory()
        {
            Debug.Log("END");
            story = null;
            Player0Movement player0 = GameObject.Find("Player").GetComponent<Player0Movement>();
            player0.isMoved = true;
            inkCanvas.SetActive(false);
        }

        //�^�U�G��
        public void OpenReviewStory()
        {
            ReviewCanvas.SetActive(true);
        }

        public void CloseReviewStory()
        {
            ReviewCanvas.SetActive(false);
        }

        //��r���ܮĪG
        IEnumerator SetTextUI()
        {
            textFinish = false;
            diaglogueText.text = "";
            int a = 0;

            
            /*
            switch (textLabel)
            {
                case "A":
                    face01.gameObject.transform.localScale = new Vector3(-1.35f,1.35f,1.35f);
                    face02.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    break;
                case "B":
                    face02.gameObject.transform.localScale = new Vector3(-1.35f, 1.35f, 1.35f);
                    face01.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    break;
            }*/

            textLabel = story.Continue();
            if (textLabel.Contains("A"))
            {
                textLabel = story.Continue();
                Debug.Log("A");
            }
            else if (textLabel.Contains("B"))
            {
                textLabel = story.Continue();
                Debug.Log("B");
            }

            for (int i = 0; i < textLabel.Length; i++)
            {
                diaglogueText.text += textLabel.Substring(a,1);
                a++;
                yield return new WaitForSeconds(textSpeed);
            }
            diaglogueReText.text += diaglogueText.text;
            textFinish = true;
        }
    }
}

