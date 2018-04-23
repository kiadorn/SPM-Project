using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance; //Instansierar Objektet.

    [Header("Spelarstats")] //Spelarens stats
    public int HealthPoints;
    public int Currency = 0;
    public bool HasSword;
    public bool HasShield;

    [Header("UI element")]
    public GameObject SwordIcon;


    public CheckPoint Current;

    public void SwordObtain()
    {
        SwordIcon.SetActive(true);
        HasSword = true;
    }

    //public void ShieldObtain()
    //{
    //    ShieldIcon.SetActive(true);
    //    HasShield = true;
    //}

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

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("n"))
        {
            SavePlayerStats();
        }
        if (Input.GetKeyDown("b"))
        {
            LoadPlayerStats();
        }
        if (Input.GetKeyDown("escape"))
        {
            SavePlayer();
              //  MainMenu();                                   //SKAPA MAINMENU
        }
    }
        public void SavePlayerStats()
        {
            SavePlayer();
        }
        public void LoadPlayerStats()
        {
            LoadPlayer();


        }
    private void OnApplicationQuit()
    {
        SavePlayer();
    }
    public static void SavePlayer() //Sparfunktion
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
            PlayerData data = new PlayerData();
            data.HealthPoints = instance.HealthPoints;
            data.Currency = instance.Currency;
            data.HasSword = instance.HasSword;
            //data.HasShield = instance.HasShield;
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
                instance.HasSword = data.HasSword;
                //instance.HasShield = data.HasShield;
                Debug.Log("Loaded");
        }


    }
}

[Serializable]
public class PlayerData //Serializerbara egenskaper (De som går att föra över binärt till sparfilsenkryption)
{
    public int HealthPoints;
    public int Currency;
    public bool HasSword;
    //public bool HasShield;
}
