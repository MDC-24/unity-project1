using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldier : MonoBehaviour
{
    private Data data;

    private float time;
    void Start()
    {
        data = Main_Handler.instance.data;
        time = 0;
               
    }

    
    void Update()
    {
        Shooting();
    }

    private void Shooting()
    {

        time += Time.deltaTime;

        if(time >= 1/data.soldier_firerate && data.rain_count >=1)
        {
            Destroy(data.rains[data.rains.Length - data.rain_count]);
            data.rain_count--;

            time = 0;

            
            Debug.Log(data.rain_count);


        }



    }
}
