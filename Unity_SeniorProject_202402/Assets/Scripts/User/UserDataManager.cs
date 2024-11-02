using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System;

namespace MUI
{
    public class UserDataManager : MonoBehaviour
    {
        private string gasLink = "https://script.google.com/macros/s/AKfycbyyhPyB9YMUcGl_Y6OUEIulOVxzA-HwbIJsnuSwRmO5sGx1fprWkJJ_7iSHaCexOCQ4Nw/exec";
        //private Button BtnGetData;
        public Button btnAccountSignIn;
        public Button btnAccountCreate;
        public Button btnAccountDelete;

        private WWWForm form;
        public TextMeshProUGUI textUserName;
        public TMP_InputField inputField;

        public TMP_InputField loginAccountID;
        public TMP_InputField loginPassword;

        [SerializeField]
        public string accountID;
        public string password;
        public string accountIDValue;

        public string createName;
        public string createAccountID;
        public string createPassword;
        public string createAccountIDValue;

        public TMP_InputField deleteInputField;
        public string deleteID;

        public string[] userData;

        private void Start()
        {
            //BtnGetData = GameObject.Find("���W").GetComponent<Button>();
            //textUserName = GameObject.Find("Test").GetComponent<TextMeshProUGUI>();
            //BtnGetData.onClick.AddListener(GetGASData);
            btnAccountSignIn.onClick.AddListener(() => { UserSignIn(); }) ;
            btnAccountCreate.onClick.AddListener(() => { CreateAP(); });
            btnAccountDelete.onClick.AddListener(() => { DeleteAccount(); });
            inputField.onEndEdit.AddListener(SetGASData);
        }

        //���oGAS���
        #region �x�sŪ�����
        private void GetGASData()
        { 
            form = new WWWForm();
            form.AddField("method", "���o");

            StartCoroutine(StartGetGASData());
        }

        private void SetGASData(string value)
        {
            form = new WWWForm();
            form.AddField("method", "�]�w");
            form.AddField("textUserName", inputField.text);
            StartCoroutine(StartSetGASData());
        }
        private IEnumerator StartGetGASData()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink,form))
            {
                yield return www.SendWebRequest();
                textUserName.text = inputField.text;
                print(www.downloadHandler.text);
            }
        }

        private IEnumerator StartSetGASData()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                yield return www.SendWebRequest();
                
                print(www.downloadHandler.text);
            }
        }
        #endregion

        #region �n�J
        private void UserSignIn()
        {
            UserSignInAccount();
            //UserSignInPassword();
        }

        public void UserSignInAccount()
        {
            form = new WWWForm();
            form.AddField("method", "�n�J");
            form.AddField("account",accountID);
            form.AddField("password", password);
            StartCoroutine (StartUserSignInAccount());
        }

        public IEnumerator StartUserSignInAccount()
        {
            accountID = loginAccountID.text;
            password = loginPassword.text;
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink,form))
            {
                yield return www.SendWebRequest();
                accountIDValue = www.downloadHandler.text;
                if (accountIDValue.Contains("�b���αK�X���~"))
                {
                    //loginAccountID.text = "";
                    //loginPassword.text = "";
                    Debug.Log("�b���αK�X���~");                 
                }
                else
                {
                    userData = accountIDValue.Split(new Char[] { ',' });
                }
            }
        }
        #endregion

        #region ���U
        private void CreateAP()
        { 
            CreateAPAccount();
        }

        //���U�b��
        public void CreateAPAccount()
        {
            form = new WWWForm();
            form.AddField("method", "���U�b��");
            form.AddField("createname", createName);
            form.AddField("createaccount", createAccountID);
            form.AddField("createpassword", createPassword);
            StartCoroutine(StartUserCreateAccount());
        }

        public IEnumerator StartUserCreateAccount()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                yield return www.SendWebRequest();
                createAccountIDValue = www.downloadHandler.text;
                Debug.Log(createAccountIDValue);
            }
        }
        #endregion

        #region ���P
        public void DeleteAccount()
        {
            form = new WWWForm();
            form.AddField("method", "�R���b��");
            form.AddField("deleteID",(int.Parse( deleteInputField.text) + 1).ToString());
            StartCoroutine(StartUserDeleteAccount());
        }

        public IEnumerator StartUserDeleteAccount()
        {
            using (UnityWebRequest www = UnityWebRequest.Post(gasLink, form))
            {
                yield return www.SendWebRequest();
                deleteInputField.text = "";
                Debug.Log(www.downloadHandler.text);
            }
        }
        #endregion

        
    }
}

