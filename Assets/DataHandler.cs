using UnityEngine;

public class DataHandler : MonoBehaviour
{
  
        public static DataHandler Instance;

        // The 2 fields you want to pass
        public Sprite themeIndexX;
        public Sprite themeIndexO;

        private void Awake()
        {
            // This makes the object persist across scenes
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject); // Prevent duplicates
            }
        }
    
}
