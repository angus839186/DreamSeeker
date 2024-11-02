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
        #region �Ѽ�
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
        /// �ϥΪ̵n�J
        /// </summary>

        //1.�n�J���\
        //2.�K�X��J���~
        //3.�ӱb�����s�b
        //4.�b�����ର��
        //5.�K�X���ର��

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
                            Debug.Log("�n�J���\");
                        }
                        else
                        {
                            Debug.Log("�K�X��J���~");
                        }
                    }
                    else
                    {
                        Debug.Log("�ӱb�����s�b");
                    }
                }
                else
                {
                    Debug.Log("�K�X���ର��");
                }
            }
            else
            {
                Debug.Log("�b�����ର��");
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

