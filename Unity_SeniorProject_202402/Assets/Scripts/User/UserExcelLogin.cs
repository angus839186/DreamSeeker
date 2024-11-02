using System;
using System.Collections;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UserDataExcelSaveLoad;

public class UserExcelLogin : MonoBehaviour
{
    #region 參數
    public UserDataExcelSaveLoad saveLoad;
    public StartScenesCanvas scenesCanvas;
    public DataManager dataManager;
    public PlayerDataList playerDataList = new PlayerDataList();
    [SerializeField]
    public PlayerData currentPlayerData = new PlayerData();

    public Button signInBtn;
    public Button createAccountBtn;
    private bool isPasswordToSee;

    //登入
    public TMP_InputField userAccount;
    public TMP_InputField userPassword;

    public TextMeshProUGUI errorWord;
    //註冊
    public TMP_InputField createUserName;
    public TMP_InputField createUserAccount;
    public TMP_InputField createUserPassword;
    public TMP_InputField createUserConfirmPassword;

    public TMP_InputField createUserForgotPassword;
    public TextMeshProUGUI createErrorWord;
    //修改密碼
    public TMP_InputField changeUserOldPassword;
    public TMP_InputField changeUserNewPassword;
    public TMP_InputField changeUserConfirmPassword;

    private int changeIndex;
    //畫布canvas
    public GameObject loginCanvas;
    public GameObject caCanvas;
    public GameObject changePasswordCanvas;
    //登入後使用者資料
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
                            Debug.Log("登入成功");
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
                            StartCoroutine(shakeText("密碼輸入錯誤"));
                            Debug.Log("密碼輸入錯誤");
                            userAccount.text = "";
                            userPassword.text = "";
                        }
                    }
                    else
                    {
                        StartCoroutine(shakeText("該帳號不存在"));
                        Debug.Log("該帳號不存在");
                        userAccount.text = "";
                        userPassword.text = "";
                    }
                }
                else
                {
                    StartCoroutine(shakeText("密碼不能為空"));
                    Debug.Log("密碼不能為空");
                    userAccount.text = "";
                    userPassword.text = "";
                }
            }
            else
            {
                StartCoroutine(shakeText("帳號不能為空"));
                Debug.Log("帳號不能為空");
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
                                StartCoroutine(shakeCreateText("註冊成功"));
                                Debug.Log("註冊成功");
                                saveLoad.CreateAccountPlayerData(userName, id, password ,forgotPassword);
                                break;
                            }
                        }
                        else
                        {
                            StartCoroutine(shakeCreateText("該帳號已存在"));
                            
                            Debug.Log("該帳號已存在");
                            createUserAccount.text = "";
                            createUserPassword.text = "";
                            createUserConfirmPassword.text = "";
                            createUserForgotPassword.text = "";
                        }
                    }
                    else
                    {
                        StartCoroutine(shakeCreateText("密碼與確認密碼不同步"));
                        
                        Debug.Log("密碼與確認密碼不同步");
                        createUserAccount.text = "";
                        createUserPassword.text = "";
                        createUserConfirmPassword.text = "";
                        createUserForgotPassword.text = "";

                    }
                }
                else
                {
                    StartCoroutine(shakeCreateText("密碼不能為空"));
                    
                    Debug.Log("密碼不能為空");
                    createUserAccount.text = "";
                    createUserPassword.text = "";
                    createUserConfirmPassword.text = "";
                    createUserForgotPassword.text = "";
                }
            }
            else
            {
                StartCoroutine(shakeCreateText("帳號不能為空"));
                
                Debug.Log("帳號不能為空");
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
                    Debug.Log("修改密碼成功");
                    changePasswordCanvas.SetActive(false);
                }
                else
                {
                    Debug.Log("密碼與確認密碼不同步");
                    createUserAccount.text = "";
                    createUserPassword.text = "";
                    createUserConfirmPassword.text = "";

                }
            }
            else
            {
                Debug.Log("新密碼不能為空");
                createUserAccount.text = "";
                createUserPassword.text = "";
                createUserConfirmPassword.text = "";
            }
        }
        else
        {
            Debug.Log("舊密碼不能為空");
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
                    Debug.Log("刪除成功");
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
                    forgotPasswordText.text = "帳號輸入未匹配到已有帳號";
                    Debug.Log("bb");
                }
            }
        }
        else
        {
            forgotPasswordText.gameObject.SetActive(true);
            forgotPasswordText.text = "帳號未輸入";
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
