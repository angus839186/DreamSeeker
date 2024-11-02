using System;
using System.Collections;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UserDataExcelSaveLoad;

public class UserExcelLogin : MonoBehaviour
{
    #region �Ѽ�
    public UserDataExcelSaveLoad saveLoad;
    public StartScenesCanvas scenesCanvas;
    public DataManager dataManager;
    public PlayerDataList playerDataList = new PlayerDataList();
    [SerializeField]
    public PlayerData currentPlayerData = new PlayerData();

    public Button signInBtn;
    public Button createAccountBtn;
    private bool isPasswordToSee;

    //�n�J
    public TMP_InputField userAccount;
    public TMP_InputField userPassword;

    public TextMeshProUGUI errorWord;
    //���U
    public TMP_InputField createUserName;
    public TMP_InputField createUserAccount;
    public TMP_InputField createUserPassword;
    public TMP_InputField createUserConfirmPassword;

    public TMP_InputField createUserForgotPassword;
    public TextMeshProUGUI createErrorWord;
    //�ק�K�X
    public TMP_InputField changeUserOldPassword;
    public TMP_InputField changeUserNewPassword;
    public TMP_InputField changeUserConfirmPassword;

    private int changeIndex;
    //�e��canvas
    public GameObject loginCanvas;
    public GameObject caCanvas;
    public GameObject changePasswordCanvas;
    //�n�J��ϥΪ̸��
    public TextMeshProUGUI UserName;
    public Button changePasswordBtn;
    public Button deleteAccountBtn;
    public Button logOffBtn;

    public TextMeshProUGUI forgotPasswordText;
    #endregion

    private void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        saveLoad = GameObject.Find("UserDataExcelSaveLoad").GetComponent<UserDataExcelSaveLoad>();
        TakeUserData();

        changePasswordBtn.gameObject.SetActive(false);
        deleteAccountBtn.gameObject.SetActive(false);
        logOffBtn.gameObject.SetActive(false);
    }

    public void TakeUserData()
    {
        playerDataList = saveLoad.playerDataList;
    }

    public void Login()
    {
        string id = userAccount.text;
        string password = userPassword.text;

        string checkIdData;
        string checkPasswordData;
        for (int i = 0; i < playerDataList.data.Count; i++)
        {
            checkIdData = playerDataList.data[i].useraccountID;
            checkPasswordData = playerDataList.data[i].userpassword;

            if (id != "")
            {
                if (password != "")
                {
                    if (id == checkIdData)
                    {
                        if (password == checkPasswordData)
                        {
                            Debug.Log("�n�J���\");
                            currentPlayerData.userID = playerDataList.data[i].userID;
                            currentPlayerData.userName = playerDataList.data[i].userName;
                            currentPlayerData.useraccountID = playerDataList.data[i].useraccountID;
                            currentPlayerData.userpassword= playerDataList.data[i].userpassword;
                            currentPlayerData.playerPosition = playerDataList.data[i].playerPosition;
                            currentPlayerData.itemList = playerDataList.data[i].itemList;
                            currentPlayerData.itemNumber = playerDataList.data[i].itemNumber;
                            currentPlayerData.survivePoint = playerDataList.data[i].survivePoint;
                            currentPlayerData.interest = playerDataList.data[i].interest;
                            currentPlayerData.forgotPassword = playerDataList.data[i].forgotPassword;

                            changeIndex = i;

                            SuccessLogin();
                            dataManager.LoadPlayerData();
                            loginCanvas.SetActive(false);

                            changePasswordBtn.gameObject.SetActive(true);
                            deleteAccountBtn.gameObject.SetActive(true);
                            logOffBtn.gameObject.SetActive(true);
                            return;
                        }
                        else
                        {
                            StartCoroutine(shakeText("�K�X��J���~"));
                            Debug.Log("�K�X��J���~");
                            userAccount.text = "";
                            userPassword.text = "";
                        }
                    }
                    else
                    {
                        StartCoroutine(shakeText("�ӱb�����s�b"));
                        Debug.Log("�ӱb�����s�b");
                        userAccount.text = "";
                        userPassword.text = "";
                    }
                }
                else
                {
                    StartCoroutine(shakeText("�K�X���ର��"));
                    Debug.Log("�K�X���ର��");
                    userAccount.text = "";
                    userPassword.text = "";
                }
            }
            else
            {
                StartCoroutine(shakeText("�b�����ର��"));
                Debug.Log("�b�����ର��");
                userAccount.text = "";
                userPassword.text = "";
            }
        }
    }

    private void SuccessLogin()
    {
        scenesCanvas.isLogin = true;
        UserName.text = currentPlayerData.userName;
    }

    public void CreateAccount()
    {
        string userName = createUserName.text;
        string id = createUserAccount.text;
        string password = createUserPassword.text;
        string confirmPassword = createUserConfirmPassword.text;
        string forgotPassword = createUserForgotPassword.text;

        for (int i = 0; i < playerDataList.data.Count; i++)
        {
            string checkIdData = playerDataList.data[i].useraccountID;
            string checkPasswordData = playerDataList.data[i].userpassword;

            if (id != "")
            {
                if (password != "")
                {
                    if (password == confirmPassword)
                    {
                        if (id != checkIdData)
                        {
                            if (password != checkPasswordData)
                            {
                                StartCoroutine(shakeCreateText("���U���\"));
                                Debug.Log("���U���\");
                                saveLoad.CreateAccountPlayerData(userName, id, password ,forgotPassword);
                                break;
                            }
                        }
                        else
                        {
                            StartCoroutine(shakeCreateText("�ӱb���w�s�b"));
                            
                            Debug.Log("�ӱb���w�s�b");
                            createUserAccount.text = "";
                            createUserPassword.text = "";
                            createUserConfirmPassword.text = "";
                            createUserForgotPassword.text = "";
                        }
                    }
                    else
                    {
                        StartCoroutine(shakeCreateText("�K�X�P�T�{�K�X���P�B"));
                        
                        Debug.Log("�K�X�P�T�{�K�X���P�B");
                        createUserAccount.text = "";
                        createUserPassword.text = "";
                        createUserConfirmPassword.text = "";
                        createUserForgotPassword.text = "";

                    }
                }
                else
                {
                    StartCoroutine(shakeCreateText("�K�X���ର��"));
                    
                    Debug.Log("�K�X���ର��");
                    createUserAccount.text = "";
                    createUserPassword.text = "";
                    createUserConfirmPassword.text = "";
                    createUserForgotPassword.text = "";
                }
            }
            else
            {
                StartCoroutine(shakeCreateText("�b�����ର��"));
                
                Debug.Log("�b�����ର��");
                createUserAccount.text = "";
                createUserPassword.text = "";
                createUserConfirmPassword.text = "";
                createUserForgotPassword.text = "";
            }
        } 
    }

    public void ChangePassword()
    {
        string oldPassword = changeUserOldPassword.text;
        string newPassword = changeUserNewPassword.text;
        string confirmPassword = changeUserConfirmPassword.text;

        if (oldPassword != "")
        {
            if (newPassword != "")
            {
                if (newPassword == confirmPassword)
                {
                    saveLoad.ChangePasswordPlayerData(changeIndex, newPassword);
                    Debug.Log("�ק�K�X���\");
                    changePasswordCanvas.SetActive(false);
                }
                else
                {
                    Debug.Log("�K�X�P�T�{�K�X���P�B");
                    createUserAccount.text = "";
                    createUserPassword.text = "";
                    createUserConfirmPassword.text = "";

                }
            }
            else
            {
                Debug.Log("�s�K�X���ର��");
                createUserAccount.text = "";
                createUserPassword.text = "";
                createUserConfirmPassword.text = "";
            }
        }
        else
        {
            Debug.Log("�±K�X���ର��");
            createUserAccount.text = "";
            createUserPassword.text = "";
            createUserConfirmPassword.text = "";
        }
    }

    public void LogOffAccount()
    { 
        currentPlayerData = new PlayerData();
        UserName.text = "";
        changePasswordBtn.gameObject.SetActive(false);
        deleteAccountBtn.gameObject.SetActive(false);
        logOffBtn.gameObject.SetActive(false);
    }

    public void DeleteAccount()
    {
        if (currentPlayerData.userName != "")
        {
            for (int i = 0; i < playerDataList.data.Count; i++)
            {
                if (currentPlayerData.useraccountID == playerDataList.data[i].useraccountID)
                {
                    saveLoad.DeletePlayerData(i + 2);
                    Debug.Log("�R�����\");
                }
            }
        }

        LogOffAccount();
    }

    public void OCPassword()
    {
        
        if (isPasswordToSee)
        {
            userPassword.contentType = TMP_InputField.ContentType.Standard;
            userPassword.ForceLabelUpdate();
            isPasswordToSee = false;
        }
        else
        {
            userPassword.contentType = TMP_InputField.ContentType.Password;
            userPassword.ForceLabelUpdate();
            isPasswordToSee = true;
        }
    }

    public void OpenForgotPassword()
    {
        if (userAccount.text != "")
        {
            for (int i = 0; i < playerDataList.data.Count; i++)
            {
                if (userAccount.text == playerDataList.data[i].useraccountID)
                {
                    forgotPasswordText.gameObject.SetActive(true);
                    forgotPasswordText.text = "Password:" + playerDataList.data[i].forgotPassword;
                    Debug.Log("aa");
                    break;
                }
                else
                { 
                    forgotPasswordText.gameObject.SetActive(true);
                    forgotPasswordText.text = "�b����J���ǰt��w���b��";
                    Debug.Log("bb");
                }
            }
        }
        else
        {
            forgotPasswordText.gameObject.SetActive(true);
            forgotPasswordText.text = "�b������J";
            Debug.Log("cc");
        }
    }

    IEnumerator shakeText(string _text)
    {
        errorWord.text = _text;
        errorWord.gameObject.SetActive(true);
        float _s = 5;
        Vector3 _ewp = errorWord.gameObject.transform.position;

        for (int i = 0; i < 10; i++)
        {
            errorWord.gameObject.transform.position = new Vector3(_ewp.x + _s, _ewp.y, _ewp.z);
            yield return new WaitForSeconds(0.01f);
            errorWord.gameObject.transform.position = new Vector3(_ewp.x - _s, _ewp.y, _ewp.z);
            yield return new WaitForSeconds(0.01f);
            _s--;
        }
        errorWord.gameObject.transform.position = new Vector3(980,320,0);

        errorWord.gameObject.SetActive(false);
    }

    IEnumerator shakeCreateText(string _text)
    {
        createErrorWord.text = _text;
        createErrorWord.gameObject.SetActive(true);
        float _s = 5;
        Vector3 _ewp = createErrorWord.gameObject.transform.position;

        for (int i = 0; i < 10; i++)
        {
            createErrorWord.gameObject.transform.position = new Vector3(_ewp.x + _s, _ewp.y, _ewp.z);
            yield return new WaitForSeconds(0.01f);
            createErrorWord.gameObject.transform.position = new Vector3(_ewp.x - _s, _ewp.y, _ewp.z);
            yield return new WaitForSeconds(0.01f);
            _s--;
        }
        createErrorWord.gameObject.transform.position = new Vector3(980, 320, 0);

        createErrorWord.gameObject.SetActive(false);
    }


    [Serializable]
    public class PlayerData
    {
        public int userID;
        public string userName;
        public string useraccountID;
        public string userpassword;
        public Vector3 playerPosition;
        public string itemList;
        public string itemNumber;
        public int survivePoint;
        public int interest;
        public string forgotPassword;
    }

}
