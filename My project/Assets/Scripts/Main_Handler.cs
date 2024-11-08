using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// when another script needed use singletons!!! (DONE)

//NOTE!!: MAKE SURE EACH SOLDIER KILLS ENEMY THEY THEMSELVES CHOOSE AND MAKE THEIR GUNS TRACK THE ENEMY (DONE)

// MAKE AN UPGRADE CLASS!!!!
public class Main_Handler : MonoBehaviour
{
    public static Main_Handler instance{get; private set;}
    private void Awake() {instance = this;}
    public Data data;

    private Upgrade upg1, upg2, upg3, upg4, upg5, upg6;
    [SerializeField] private TMP_Text day_TEXT;
    [SerializeField] private TMP_Text gold_TEXT;
    [SerializeField] private TMP_Text minercount_TEXT;
    [SerializeField] private TMP_Text soldiercount_TEXT;

    //cost texts
    [SerializeField] private TMP_Text minercap_upg_cost_TEXT;
    [SerializeField] private TMP_Text soldiercap_upg_cost_TEXT;
    [SerializeField] private TMP_Text minereff_upg_cost_TEXT;
    [SerializeField] private TMP_Text soldierfr_upg_cost_TEXT;
    [SerializeField] private TMP_Text soldierspawntime_upg_cost_TEXT;
    [SerializeField] private TMP_Text minerretrievetime_upg_cost_TEXT;

    [SerializeField] private Button build_B;
    [SerializeField] private Button recruit_B;
    [SerializeField] private Button recruit2_B;


    [SerializeField] GameObject soldier_GO;
    [SerializeField] GameObject miner_GO;
    [SerializeField] GameObject rain_GO;
    [SerializeField] GameObject Techtree_GO;
    
    [SerializeField] GameObject Techtree_UNITS_GO;



    [SerializeField] Transform soldier_spawnpoint;

    [SerializeField] Transform miner_spawnpoint;

    [SerializeField] Transform rain_spawnpoint;

    
    private float time, time_MW, time_RS;
    
    void Start()
    {
        data = new Data();
        
        upg1 = new Upgrade();
        upg1.effect = 1;

        upg2 = new Upgrade();
        upg2.effect = 1.1f;
        
        upg3 = new Upgrade();
        
        upg4 = new Upgrade();
        upg4.effect = 1;
        
        upg5 = new Upgrade();
        upg5.effect = 1.1f;
        
        upg6 = new Upgrade();
        
        gold_TEXT.text = "Gold: " + data.gold;
        minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        
    }

    
    void Update()
    {
        
        Miners_Work();
        Rain_Spawner();
        Day_Counter();

        
        
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

    public void Lab_Button()
    {
        if(Techtree_GO.gameObject.activeSelf == false)
        {
            Techtree_GO.gameObject.SetActive(true);
        }
        
    }

    public void Labexit_Button()
    {
        
        Techtree_UNITS_GO.SetActive(false);
        Techtree_GO.gameObject.SetActive(false);
            
       
    }

    public void Techtree_UNITS_Button()
    {
        if(Techtree_UNITS_GO.gameObject.activeSelf == false)
        {
            Techtree_UNITS_GO.SetActive(true);
        }
        else
        {
            Techtree_UNITS_GO.SetActive(false);
        }
    }
     
    private float offset1;
    public void Recruit_Button()
    {
        
        if(data.gold >= 10 && data.soldier_count < data.soldier_cap) // replace 10 with cost var
        {
            data.gold -= 10;
            data.soldier_count++;
            
            gold_TEXT.text = "Gold: " + data.gold;
            soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;
            
            offset1 = UnityEngine.Random.Range(-1f,1f);
            Instantiate(soldier_GO, new Vector3(soldier_spawnpoint.position.x + offset1,soldier_spawnpoint.position.y,0), Quaternion.identity);
            

        }
        else
        {
            // print insufficient gold
        }
    }

    public void Recruit_Button_miner()
    {
          if(data.gold >= 10 && data.miner_count < data.miner_cap) // replace 10 with cost var
        {
            data.gold -= 10;
            data.miner_count++;
            
            gold_TEXT.text = "Gold: " + data.gold;
            minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;
            
            offset1 = UnityEngine.Random.Range(-1f,1f);
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
            var offset2 = UnityEngine.Random.Range(-4.3f,4.3f);
            
            var rain_clone = Instantiate(rain_GO, new Vector3(rain_spawnpoint.position.x + offset2, rain_spawnpoint.position.y, 0), Quaternion.identity);
            time_RS = 0;
            data.rains[data.rain_arraypos] = rain_clone;
            data.rain_count++;
            data.rain_arraypos++;
            if(data.rain_arraypos >= data.rains.Length - 1){data.rain_arraypos = 0;}
            //Debug.Log(data.rain_count);
            //Debug.Log(data.rain_arraypos);

            
            
        }
    }

    private void Day_Counter()
    {
        time = Time.time;

        if(time >= data.day*60)
        {
            data.day++;

            day_TEXT.text = "Day: " + data.day;

            data.rain_hz += data.day*0.5f;
        }

        Debug.Log(time);

    }




    private void Upgrade_varIncrementer(float cost, float effect, int level, float power)
    {
        float costx = cost*level;

        cost = Mathf.Pow(costx, power);

        level++;



    }


    // LAB RESEARCH & UPGRADE BUTTON FUNCTIONS

    public void Minercap_UPG()
    {
        if(data.gold >= upg1.cost){
        data.gold -= upg1.cost;
        gold_TEXT.text = "Gold: " + data.gold;
        minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;

        data.miner_cap += (int)upg1.effect;

        Upgrade_varIncrementer(upg1.cost, upg1.effect, upg1.level, upg1.power);

        minercap_upg_cost_TEXT.text = upg1.cost + "G";
        }
    }
       public void Minereff_UPG()
    {
        if(data.gold >= upg2.cost)
        {
        data.gold -= upg2.cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.miner_wc = data.miner_wc*upg2.effect;

        Upgrade_varIncrementer(upg2.cost, upg2.effect, upg2.level, upg2.power);

        minereff_upg_cost_TEXT.text = upg2.cost + "G";
        }
    }
       public void Minerretrieve_UPG()
    {
        data.gold -= data.minercap_upg_cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.miner_cap++;
    }
       public void Soldiercap_UPG()
    {
        if(data.gold >= data.soldiercap_upg_cost){
        data.gold -= upg4.cost;
        gold_TEXT.text = "Gold: " + data.gold;
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        data.soldier_cap += (int)upg4.effect;

        Upgrade_varIncrementer(upg4.cost, upg4.effect, upg4.level, upg4.power);

        soldiercap_upg_cost_TEXT.text = upg4.cost + "G";
        }
    }
       public void Soldierfr_UPG()
    {
        if(data.gold >= upg5.cost){
        data.gold -= upg5.cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.soldier_firerate = data.soldier_firerate*upg5.effect;

        Upgrade_varIncrementer(upg5.cost, upg5.effect, upg5.level, upg5.power);

        soldierfr_upg_cost_TEXT.text = upg5.cost + "G";


        }
    }
       public void Soldierspawntime_UPG()
    {
        if(data.gold >= data.minercap_upg_cost){
        
        data.gold -= data.minercap_upg_cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.miner_cap++;
    }
    }
}
