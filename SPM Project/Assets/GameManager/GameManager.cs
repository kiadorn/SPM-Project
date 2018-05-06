using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance; //Instansierar Objektet.

    [Header("Spelarstats")] //Spelarens stats
    public int HealthPoints;
    public int Currency = 0;
    public bool Level1Done;
    public bool Level2Done;
    Scene currentScene;

    public CheckPoint Current;
    
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start() {
     currentScene = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("b"))
        {
            LoadPlayerStats();
        }
        if (Input.GetKeyDown("escape"))
        {
            SavePlayer();
        }
    }
    public void LoadPlayerStats()
        {
            LoadPlayer();


        }
    public void ChangeHealth(int i)
    {
        HealthPoints += i;
        CheckIfDead();
    }
    private void CheckIfDead()
    {
        if (HealthPoints < 1)
        {
            //Kill player-move to respawn;
           // this.HealthUI.text = "Dead";
        }
        else
        {
           // UpdateHealth();
        }
    }
    private void OnApplicationQuit()
    {
    }
    public static void SavePlayer() //Sparfunktion
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
            PlayerData data = new PlayerData(instance);
            data.HealthPoints = instance.HealthPoints;
            data.Currency = instance.Currency;
            data.Level1Done = instance.Level1Done;
            data.Level2Done = instance.Level2Done;
            bf.Serialize(stream, data);
            stream.Close();
            Debug.Log("Saved");

        }

        public static void LoadPlayer() //Laddfunktion
        {

            if (File.Exists(Application.persistentDataPath + "/player.sav"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

                PlayerData data = (PlayerData)bf.Deserialize(stream);

                stream.Close();
                instance.Currency = data.Currency;
                instance.HealthPoints = data.HealthPoints;
                instance.Level1Done = data.Level1Done;
                instance.Level2Done = data.Level2Done;
                Debug.Log("Loaded");
        }


    }
}

[Serializable]
public class PlayerData //Serializerbara egenskaper (De som går att föra över binärt till sparfilsenkryption)
{
    public int HealthPoints;
    public int Currency = 0;
    public bool Level1Done;
    public bool Level2Done;
    public PlayerData(GameManager instance) {
     HealthPoints = instance.HealthPoints;
     Currency = instance.Currency;
     Level1Done = instance.Level1Done;
     Level2Done = instance.Level2Done;
    }
}
