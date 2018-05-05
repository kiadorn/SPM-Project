using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Load_Save {

    //public static void SavePlayer(PlayerStats PlayerStats)
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);
    //    PlayerData data = new PlayerData(PlayerStats);
    //    bf.Serialize(stream, data);
    //    stream.Close();
    //    Debug.Log("Saved");

    //}

    //public static void LoadPlayer(PlayerStats stats)
    //{
    //    if (File.Exists(Application.persistentDataPath + "/player.sav"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

    //        PlayerData data = bf.Deserialize(stream) as PlayerData;
    //        stats.Currency = data.Currency;
    //        stats.HealthPoints = data.HealthPoints;
    //        stats.HasSword = data.HasSword;
    //        PlayerStats.HasShield = data.HasShield;
    //        stream.Close();
    //        Debug.Log("Loaded");



    //    }
    //}

}
//[Serializable]
//public class PlayerLUL
//{
//    public PlayerData(PlayerStats PlayerStats)
//    {
//        HealthPoints = PlayerStats.HealthPoints;
//        Currency = PlayerStats.Currency;
//        HasSword = PlayerStats.HasSword;
//        HasShield = PlayerStats.HasShield;

//    }

//    public int HealthPoints;
//    public int Currency;
//    public bool HasSword;
//    public bool HasShield;
//}
