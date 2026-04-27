using UnityEngine;

public class DataHandler : MonoBehaviour
{
  
        public static DataHandler Instance;

        public Sprite themeIndexX;
        public Sprite themeIndexO;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); 
            }
        }
    
}
