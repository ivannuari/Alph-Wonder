using System;
using UnityEngine;

namespace GaweDeweStudio
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState currentState = GameState.Menu;

        public LevelData1 levelData1;
        public LevelData2 levelData2;
        public LevelData3 levelData3;

        public bool isFromGame = false;

        private AudioManager _sound;

        public delegate void GameStateDelegate(GameState newState);
        public event GameStateDelegate OnStateChanged;

        public event Action OnWidgetOpened;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);

            _sound = GetComponent<AudioManager>();
        }

        private void Start()
        {
            levelData2.Load();
        }

        public void ChangeState(GameState newState)
        {
            if (newState == currentState) { return; }
            currentState = newState;

            OnStateChanged?.Invoke(currentState);
        }

        public void ShowWidget()
        {
            OnWidgetOpened?.Invoke();
        }

        public AudioManager GetSound() { return _sound; }
    }
}



[System.Serializable]
public class LevelData1
{
    [System.Serializable]
    public class Data1
    {
        public int level;
        public int star;
    }

    public Data1[] saveData = new Data1[8];

    public void Load()
    {
        if (PlayerPrefs.HasKey("DataLevel1"))
        {
            string json = PlayerPrefs.GetString("DataLevel1");
            var _d = JsonUtility.FromJson<LevelData1>(json);
            saveData = _d.saveData;
        }
    }

    public void Save(int nomor, int star)
    {
        saveData[nomor].star = star;

        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("DataLevel1", json);
    }
}





[System.Serializable]
public class LevelData2
{
    [System.Serializable]
    public class Data2
    {
        public int level;
        public int star;
    }

    public Data2[] saveData = new Data2[26];

    public void Load()
    {
        if (PlayerPrefs.HasKey("DataLevel2"))
        {
            string json = PlayerPrefs.GetString("DataLevel2");
            var _d = JsonUtility.FromJson<LevelData2>(json);
            saveData = _d.saveData;
        }
    }

    public void Save(int nomor,int star)
    {
        saveData[nomor].star = star;

        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("DataLevel2",json);
    }
}





[System.Serializable]
public class LevelData3
{
    [System.Serializable]
    public class Data3
    {
        public int level;
        public int star;
    }

    public Data3[] saveData = new Data3[8];

    public void Load()
    {
        if (PlayerPrefs.HasKey("DataLevel3"))
        {
            string json = PlayerPrefs.GetString("DataLevel3");
            var _d = JsonUtility.FromJson<LevelData3>(json);
            saveData = _d.saveData;
        }
    }

    public void Save(int nomor, int star)
    {
        saveData[nomor].star = star;

        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("DataLevel3", json);
    }
}