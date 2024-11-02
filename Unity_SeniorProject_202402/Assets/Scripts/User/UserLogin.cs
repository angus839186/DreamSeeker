using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace MUI
{
    public class UserLogin : MonoBehaviour
    {
        public static UserLogin instance;
        #region 參數
        public UserDataManager dataManager;
        public Button createAccountBtn;
        public Button nextBtn;
        public Button signInBtn;
        public Button saveBtn;
        public Button loadBtn;
        public TMP_InputField userAccount;
        public TMP_InputField userPassword;
        public TMP_InputField createUserAccount;
        public TMP_InputField createUserPassword;
        public TMP_InputField test;
        public TextMeshProUGUI loadText;

        public Image createAccountCanvas;
        #endregion
        [SerializeField]
        UserData data;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            dataManager = dataManager.GetComponent<UserDataManager>();
            createAccountBtn.onClick.AddListener(SetCreateAccountCanvas);
            saveBtn.onClick.AddListener(SaveData);
            loadBtn.onClick.AddListener(LoadData);
        }

        public void SaveData()
        {
            data.userName = userAccount.text;
            data.userPassword = userPassword.text;
            data.test = test.text;
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(Application.streamingAssetsPath + "/test.json", FileMode.Create);
            bf.Serialize(s, JsonUtility.ToJson(data));
            s.Close();

            Debug.Log("Save Success");
        }

        public void LoadData()
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(Application.streamingAssetsPath + "/test.json", FileMode.Open);
            data = JsonUtility.FromJson<UserData>((string)bf.Deserialize(s));
            s.Close();
            loadText.text = data.userName + "/" + data.userPassword + "/" + data.test;

            Debug.Log("Load Success");
        }

        private void SetCreateAccountCanvas()
        { 
            createAccountCanvas.gameObject.SetActive(true);
        }

        private void CancelBtn()
        {
            createAccountCanvas.gameObject.SetActive(false);
        }
        /// <summary>
        /// 使用者登入
        /// </summary>

        //1.登入成功
        //2.密碼輸入錯誤
        //3.該帳號不存在
        //4.帳號不能為空
        //5.密碼不能為空

        public void SignInAccount()
        {
            string id = userAccount.text;
            string password = userPassword.text;

            string idData = dataManager.accountID;
            string passwordData = dataManager.password;

            if (id != "")
            {
                if (password != "")
                {
                    if (id == idData)
                    {
                        if (password == passwordData)
                        {
                            Debug.Log("登入成功");
                        }
                        else
                        {
                            Debug.Log("密碼輸入錯誤");
                        }
                    }
                    else
                    {
                        Debug.Log("該帳號不存在");
                    }
                }
                else
                {
                    Debug.Log("密碼不能為空");
                }
            }
            else
            {
                Debug.Log("帳號不能為空");
            }
            createAccountCanvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void NextCreateAccount()
        {
            string ID = dataManager.accountID;
            string password = dataManager.password;
            if (ID != "")
            {
                if (password != "")
                {
                    
                }
            }
            else
            {

            }
            createAccountCanvas.gameObject.SetActive(false);
        }
        
        [System.Serializable]
        public class UserData
        {
            public string userName;
            public string userPassword;
            public string test;
        }
    }
}

