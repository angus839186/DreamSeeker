using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance;
    public Vector3 playerLoadPosition;
    private Vector3 playerCurrentPosition;
    private GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            SavePlayerplayerCurrentPosition();
        }
    }

    public void SavePlayerplayerCurrentPosition()
    {
        playerCurrentPosition = player.gameObject.transform.position;
        playerLoadPosition = playerCurrentPosition;
    
        DataManager dataManager = GameObject.Find("DataManager").GetComponent<DataManager>();
        UserDataExcelSaveLoad userDataExcel = GameObject.Find("UserDataExcelSaveLoad").GetComponent<UserDataExcelSaveLoad>();
        dataManager.currentPlayerData.playerPosition = playerLoadPosition;
        userDataExcel.SavePlayerDataIO();
    }


}
