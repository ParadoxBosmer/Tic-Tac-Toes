using System.IO;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
  
        public static DataHandler Instance;

        public Sprite themeIndexX;
        public Sprite themeIndexO;
        public string savePath;

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
            
            
            savePath = Path.Combine(Application.persistentDataPath, "matches.txt");
                if (!File.Exists(savePath))
                {
                
                    File.WriteAllText(savePath, ""); 
            
                    Debug.Log("Created new match file at: " + savePath);
                }
                else
                {
                    Debug.Log("Match file already exists at: " + savePath);
                }
        }
    
}
