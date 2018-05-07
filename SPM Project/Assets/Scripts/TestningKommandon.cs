using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestningKommandon : MonoBehaviour
{

    public GameObject player;
    private CheckPoint checkpointToSkipTo;
    private int i;

    void Update()
    {
        if (Input.GetKeyDown("0"))
        {
            SkipSegment();
        }

    }

    private void SkipSegment()
    {
        i = player.GetComponent<PlayerStats>().CurrentCheckPoint.transform.GetSiblingIndex();
        checkpointToSkipTo = transform.GetChild(i + 1).GetComponent<CheckPoint>();
        player.GetComponent<PlayerStats>().CurrentCheckPoint = checkpointToSkipTo;
        player.GetComponent<PlayerStats>().Death();
    }
}