using UnityEngine;

namespace MUI
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        public static DontDestroyOnLoad instance;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
