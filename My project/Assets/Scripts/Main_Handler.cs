using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

// when another script needed use singletons!!!

//NOTE!!: MAKE SURE EACH SOLDIER KILLS ENEMY THEY THEMSELVES CHOOSE AND MAKE THEIR GUNS TRACK THE ENEMY
public class Main_Handler : MonoBehaviour
{
    public static Main_Handler instance{get; private set;}
    private void Awake() {instance = this;}
    public Data data;
    [SerializeField] private TMP_Text day_TEXT;
    [SerializeField] private TMP_Text gold_TEXT;

    [SerializeField] private Button build_B;

    [SerializeField] private Button recruit_B;

    
    [SerializeField] private Button recruit2_B;


    [SerializeField] GameObject soldier_GO;
    [SerializeField] GameObject miner_GO;

    [SerializeField] GameObject rain_GO;

    [SerializeField] Transform soldier_spawnpoint;

    [SerializeField] Transform miner_spawnpoint;

    [SerializeField] Transform rain_spawnpoint;

    
    private float time_MW;
    private float time_RS;
    void Start()
    {
        data = new Data();
    }

    
    void Update()
    {
        
        Miners_Work();
        Rain_Spawner();
        
    }

    public void Manual_Mine_Button()
    {
        data.gold += 1;
        gold_TEXT.text = "Gold: " + data.gold;
    }

    public void Barracks_Button()
    {
        if(build_B.isActiveAndEnabled && recruit_B.isActiveAndEnabled && recruit2_B.isActiveAndEnabled)
        {
            build_B.gameObject.SetActive(false);
            recruit_B.gameObject.SetActive(false);
            recruit2_B.gameObject.SetActive(false);
        }
        else
        {
            build_B.gameObject.SetActive(true);
            recruit_B.gameObject.SetActive(true);
            recruit2_B.gameObject.SetActive(true);
        }
    }
     
    private float offset1;
    public void Recruit_Button()
    {
        
        if(data.gold >= 10) // replace 10 with cost var
        {
            data.gold -= 10;
            data.soldier_count++;
            gold_TEXT.text = "Gold: " + data.gold;
            offset1 = Random.Range(-1f,1f);
            Instantiate(soldier_GO, new Vector3(soldier_spawnpoint.position.x + offset1,soldier_spawnpoint.position.y,0), Quaternion.identity);
            

        }
        else
        {
            // print insufficient gold
        }
    }

    public void Recruit_Button_miner()
    {
          if(data.gold >= 10) // replace 10 with cost var
        {
            data.gold -= 10;
            data.miner_count++;
            gold_TEXT.text = "Gold: " + data.gold;
            offset1 = Random.Range(-1f,1f);
            Instantiate(miner_GO, new Vector3(miner_spawnpoint.position.x + offset1,miner_spawnpoint.position.y,0), Quaternion.identity);
            

        }
        else
        {
            // print insufficient gold
        }
    }    

    private void Miners_Work()
    {
        if(data.miner_count >=1){time_MW += Time.deltaTime;}
        
        if(data.miner_count*data.miner_wc*time_MW >= 1 && data.miner_count >= 1)
        {
         
         float golds_added = data.miner_count*data.miner_wc*time_MW;

         data.gold += (int)golds_added;
         gold_TEXT.text = "Gold: " + data.gold;
        time_MW = 0;

        }
        else
        {

        }

    }

    private void Rain_Spawner()
    {
        
        time_RS += Time.deltaTime;
        if(time_RS >= 1/data.rain_hz)
        {
            var offset2 = Random.Range(-4.3f,4.3f);
            
            var rain_clone = Instantiate(rain_GO, new Vector3(rain_spawnpoint.position.x + offset2, rain_spawnpoint.position.y, 0), Quaternion.identity);
            time_RS = 0;
            data.rain_count++;
            data.rains[data.rains.Length - data.rain_count] = rain_clone;
            
            Debug.Log(data.rain_count);

            
            
        }


    }
}
