using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Data 
{
    public int click_power,day, miner_count,miner_cap,  soldier_count, soldier_cap, rain_count, rain_arraypos,rain_killcount, quarry_level ,foundry_level;

    //Upgrade costs
    public float gold, miner_wc, miner_cost, miner_retrievetime, rain_speed, rain_hz, soldier_cost, soldier_firerate, soldier_spawntime;

    public GameObject[] rains; 

    public Data()
    {
        gold = 100;
        day = 1;

        click_power = 1;
        
        miner_cost = 50f;
        miner_count = 0;
        miner_cap = 10;
        miner_wc = 0.5f;
        miner_retrievetime = 30f; 
        
        soldier_cost = 50f;
        soldier_count = 0;
        soldier_cap = 10;
        soldier_firerate = 1f;
        soldier_spawntime = 10f;
        
        rain_hz = 4f;
        rain_speed = 0.1f;
        rain_count = 0;
        rain_arraypos = 0;
        rain_killcount = 0;
        rains = new GameObject[100000];

        quarry_level = 0;
        foundry_level = 0;
        

      
    }
   
}
