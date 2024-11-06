using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Data 
{
    public int gold, day, miner_count,soldier_count, rain_count, rain_arraypos,rain_killcount;
    public float miner_wc, rain_speed, rain_hz, soldier_firerate;

    public GameObject[] rains; 

    public Data()
    {
        gold = 100;
        day = 1;
        miner_count = 0;
        soldier_count = 0;
        miner_wc = 0.5f;
        rain_hz = 4f;
        rain_speed = 0.1f;
        rain_count = 0;
        rain_arraypos = 0;
        rain_killcount = 0;
        rains = new GameObject[1000];
        soldier_firerate = 1f;
    }
   
}
