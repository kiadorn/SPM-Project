﻿using System.Collections;
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
    public int Currency;
    public bool Level1Done;
    public bool Level2Done;
    public int deathCounter;

    
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

    public void AddDeathToCounter()
    {
        deathCounter++;
    }

    public int GetDeathCounter()
    {
        return deathCounter;
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
            data.Deaths = instance.deathCounter;
            bf.Serialize(stream, data);
            stream.Close();

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
                instance.deathCounter = data.Deaths;
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
    public int Deaths;
    public PlayerData(GameManager instance) {
     HealthPoints = instance.HealthPoints;
     Currency = instance.Currency;
     Level1Done = instance.Level1Done;
     Level2Done = instance.Level2Done;
     Deaths = instance.deathCounter;
    }
}
