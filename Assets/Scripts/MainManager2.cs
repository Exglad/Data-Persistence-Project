using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager2 : MonoBehaviour
{
    public static MainManager2 Instance;
    public string playerName;
    public string bestplayerName;
    public int playerScore = 0;
    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public string bestplayerName;
        public int playerScore;
    }
    public void SaveBestPlayerName()
    {
        SaveData data = new SaveData();
        data.bestplayerName = bestplayerName;
        data.playerScore = playerScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadBestPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestplayerName = data.bestplayerName;
            playerScore = data.playerScore;
        }
    }
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);       
    }

   
}
