using System;
using UnityEngine;
using System.IO;

    public class SaveSystem : GenericSingleton<SaveSystem>
    {
        string filePath;
        public GameData data = new GameData();
        void Start()
        {
            DontDestroyOnLoad(this);
            filePath = Application.dataPath + "/SaveFile/SaveJson.json";
            SaveData();
        }

        public void SaveData()
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(filePath,json);
        }

        public void LoadData()
        {
            string path =  Application.dataPath + "/SaveFile/SaveJson.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path); 
                data = JsonUtility.FromJson<GameData>(json);
            }
        }
    }

