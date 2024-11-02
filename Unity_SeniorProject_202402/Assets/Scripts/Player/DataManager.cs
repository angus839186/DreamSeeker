using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    private static DataManager instance;
    public UserDataExcelSaveLoad userDataSaveLoad;
    public PlayerData currentPlayerData = new PlayerData();


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

        userDataSaveLoad = GameObject.Find("UserDataExcelSaveLoad").GetComponent<UserDataExcelSaveLoad>();
    }

    private void Start()
    {
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        if (GameObject.Find("UserExcelLogin"))
        {
            UserExcelLogin excelLogin = GameObject.Find("UserExcelLogin").GetComponent<UserExcelLogin>();
            currentPlayerData.userID = excelLogin.currentPlayerData.userID;
            currentPlayerData.userName = excelLogin.currentPlayerData.userName;
            currentPlayerData.useraccountID = excelLogin.currentPlayerData.useraccountID;
            currentPlayerData.userpassword = excelLogin.currentPlayerData.userpassword;
            currentPlayerData.playerPosition = excelLogin.currentPlayerData.playerPosition;
            currentPlayerData.itemList = excelLogin.currentPlayerData.itemList;
            currentPlayerData.itemNumber = excelLogin.currentPlayerData.itemNumber;
            currentPlayerData.survivePoint = excelLogin.currentPlayerData.survivePoint;
            currentPlayerData.interest = excelLogin.currentPlayerData.interest;
            currentPlayerData.forgotPassword = excelLogin.currentPlayerData.forgotPassword;
        }
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
