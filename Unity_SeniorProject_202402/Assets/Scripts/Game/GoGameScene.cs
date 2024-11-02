using UnityEngine;
using UnityEngine.SceneManagement;

namespace MUI
{
    public class GoGameScene : MonoBehaviour
    {
        public PlayerDataManager playerDataManager;
        public GameObject player;
        private Vector3 playerPos;
        public bool checkPos;
        public string gameName;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            if (checkPos && player != null)
            {
                player.transform.position = playerDataManager.playerLoadPosition;
                checkPos = false;
            }
            if (playerDataManager == null)
            { 
                playerDataManager = GameObject.Find("Scene01_DataManager").GetComponent<PlayerDataManager>();
            }
        }

        public void GoToGameScene(string _sceneName)
        {
            if (player != null && playerDataManager != null)
            {
                playerDataManager.playerLoadPosition = player.transform.position;
                checkPos = true;
                SceneManager.LoadScene(_sceneName);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                GoToGameScene(gameName);
            }
        }
    }
}

