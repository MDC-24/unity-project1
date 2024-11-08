using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Data 
{
    public int gold, day, miner_count,miner_cap,soldier_count, soldier_cap, rain_count, rain_arraypos,rain_killcount;

    //Upgrade costs
    public int minercap_upg_cost, soldiercap_upg_cost, minereff_upg_cost, soldierfr_upg_cost, soldierspawntime_upg_cost, minerretrievetime_upg_cost;
    public float miner_wc, rain_speed, rain_hz, soldier_firerate;

    public GameObject[] rains; 

    public Data()
    {
        gold = 100;
        day = 1;
        miner_count = 0;
        miner_cap = 10;
        soldier_count = 0;
        soldier_cap = 10;
        miner_wc = 0.5f;
        rain_hz = 4f;
        rain_speed = 0.1f;
        rain_count = 0;
        rain_arraypos = 0;
        rain_killcount = 0;
        rains = new GameObject[1000];
        soldier_firerate = 1f;

        minercap_upg_cost = 10;
        minereff_upg_cost = 10;
        minerretrievetime_upg_cost = 10;
        soldiercap_upg_cost = 10;
        soldierfr_upg_cost = 10;
        soldierspawntime_upg_cost = 10;
    }
   
}
