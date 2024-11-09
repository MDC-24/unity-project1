using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Data 
{
    public int gold, day, miner_count,miner_cap,soldier_count, soldier_cap, rain_count, rain_arraypos,rain_killcount;

    //Upgrade costs
    public float miner_wc, miner_retrievetime, rain_speed, rain_hz, soldier_firerate, soldier_spawntime;

    public GameObject[] rains; 

    public Data()
    {
        gold = 10000;
        day = 1;
        
        miner_count = 0;
        miner_cap = 10;
        miner_wc = 0.5f;
        miner_retrievetime = 30f; 
        
        soldier_count = 0;
        soldier_cap = 10;
        soldier_firerate = 1f;
        soldier_spawntime = 10f;
        
        rain_hz = 4f;
        rain_speed = 0.1f;
        rain_count = 0;
        rain_arraypos = 0;
        rain_killcount = 0;
        rains = new GameObject[1000];
        

      
    }
   
}
