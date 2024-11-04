using System.Collections;
using System.Collections.Generic;


public class Data 
{
    public int gold, day, miner_count,soldier_count;
    public float miner_wc, rain_speed, rain_hz;

    public Data()
    {
        gold = 0;
        day = 1;
        miner_count = 0;
        soldier_count = 0;
        miner_wc = 0.5f;
        rain_hz = 4f;
        rain_speed = 0.1f;
    }
   
}
