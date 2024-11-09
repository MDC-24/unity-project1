using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.PackageManager;
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
    [SerializeField] private TMP_Text goldtimer_TEXT;
    [SerializeField] private TMP_Text minercount_TEXT;
    [SerializeField] private TMP_Text soldiercount_TEXT;
    [SerializeField] private TMP_Text soldierspawntime_TEXT;

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

    [SerializeField] private Button unlockfoundry_B;
    [SerializeField] private Button unlockquarry_B;


    [SerializeField] GameObject soldier_GO;
    [SerializeField] GameObject miner_GO;
    [SerializeField] GameObject rain_GO;
    [SerializeField] GameObject Techtree_GO;
    
    [SerializeField] GameObject Techtree_UNITS_GO;

    [SerializeField] GameObject Techtree_PRODUCTION_GO;

    [SerializeField] GameObject Techtree_CONSTRUCTION_GO;



    [SerializeField] Transform soldier_spawnpoint;

    [SerializeField] Transform miner_spawnpoint;

    [SerializeField] Transform rain_spawnpoint;

    
    private float time, time_MW, time_RS, time_SS = 0;

    private int spawnqueue = 0;
    
    void Start()
    {
        data = new Data();
        
        upg1 = new Upgrade();
        upg1.effect = 1;
        upg1.cost = 100;
        upg1.level = 1;

        upg2 = new Upgrade();
        upg2.effect = 1.1f;
        
        upg3 = new Upgrade();
        upg3.cost = 10;
        upg3.effect= 0.9f;

        
        upg4 = new Upgrade();
        upg4.effect = 1;
        upg4.cost = 100;
        
        upg5 = new Upgrade();
        upg5.effect = 1.1f;
        
        upg6 = new Upgrade();
        upg6.effect = 0.9f;
        upg6.cost = 10;
        
        gold_TEXT.text = "Gold: " + data.gold;
        minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        
    }

    
    void Update()
    {
        
        Miners_Work();
        Rain_Spawner();
        Soldier_Spawner();
        Day_Counter();

        if(data.miner_count>0){goldtimer_TEXT.text = (int)(data.miner_retrievetime - time_MW) + "s";}
        
        
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

    private bool quarryenabled, foundryenabled = false;
    public void Techtree_PRODUCTION_Button()
    {
        if(quarryenabled)
        {
            if(Techtree_PRODUCTION_GO.gameObject.activeSelf == false)
            {
                Techtree_PRODUCTION_GO.SetActive(true);
            }
            else
            {
                Techtree_PRODUCTION_GO.SetActive(false);
            }
        }
        else
        {
            if(unlockquarry_B.gameObject.activeSelf == false)
            {
                unlockquarry_B.gameObject.SetActive(true);
            }
            else
            {
                unlockquarry_B.gameObject.SetActive(false);
            }
        }

        if(foundryenabled)
        {
            if(Techtree_CONSTRUCTION_GO.gameObject.activeSelf == false)
            {
                Techtree_CONSTRUCTION_GO.SetActive(true);
            }
            else
            {
                Techtree_CONSTRUCTION_GO.SetActive(false);
            }
        }
        else
        {
            if(unlockfoundry_B.gameObject.activeSelf == false)
            {
                unlockfoundry_B.gameObject.SetActive(true);
            }
            else
            {
                unlockfoundry_B.gameObject.SetActive(false);
            }
        }

    }

    public void QuarryUnlock_Button()
    {
        if(data.gold >= 500)
        {
            Techtree_PRODUCTION_GO.gameObject.SetActive(true);
            

            data.gold -= 500;
            gold_TEXT.text = "Gold: " + data.gold;

            quarryenabled = true;

            unlockquarry_B.gameObject.SetActive(false);

        }
    }
      public void FoundryUnlock_Button()
    {
        if(data.gold >= 500)
        {
            Techtree_CONSTRUCTION_GO.gameObject.SetActive(true);
            

            data.gold -= 500;
            gold_TEXT.text = "Gold: " + data.gold;

            foundryenabled = true;

            unlockfoundry_B.gameObject.SetActive(false);

        }
    }
     
    private float offset1;
    public void Recruit_Button()
    {
        
        if(data.gold >= 10 && data.soldier_count < data.soldier_cap && spawnqueue < 10) // replace 10 with cost var
        {
            data.gold -= 10;
            gold_TEXT.text = "Gold: " + data.gold;
            spawnqueue++;
        }

    
        else
        {
            // print insufficient gold
        }
    }

    private void Soldier_Spawner()
    {
            if(spawnqueue > 0)
        {
            time_SS += Time.deltaTime;

            soldierspawntime_TEXT.text = (int)(data.soldier_spawntime - time_SS) + "s x " + spawnqueue;

            Debug.Log(time_SS);
            Debug.Log(spawnqueue);

            if(time_SS >= data.soldier_spawntime)
            {
                data.soldier_count++;
                soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;
                offset1 = UnityEngine.Random.Range(-1f,1f);
                Instantiate(soldier_GO, new Vector3(soldier_spawnpoint.position.x + offset1,soldier_spawnpoint.position.y,0), Quaternion.identity);

                spawnqueue--;
                time_SS = 0;
            }


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

    private float accum_gold = 0;
    private void Miners_Work()
    {
        
        if(data.miner_count >=1)
        {
        time_MW += Time.deltaTime;
        accum_gold += Time.deltaTime*data.miner_wc*data.miner_count;     
        }
        

        if(time_MW >= data.miner_retrievetime && data.miner_count >= 1)
        {
         
        data.gold += (int)accum_gold;
        gold_TEXT.text = "Gold: " + data.gold;
        time_MW = 0;
        accum_gold = 0;

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

        

    }




    private void Upgrade_varIncrementer(float cost, float effect, int level, float power)
    {
        float costx = cost*level;

        Mathf.Pow(costx, power);

        level++;


    }


    // LAB RESEARCH & UPGRADE BUTTON FUNCTIONS

    public void Minercap_UPG()
    {
        if(data.gold >= upg1.cost){
        data.gold -= upg1.cost;
        data.miner_cap += (int)upg1.effect;
        
        gold_TEXT.text = "Gold: " + data.gold;
        minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;

        
        upg1.cost = 100*upg1.level;
        upg1.level++;
        
        
       
        

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

        upg2.cost = (int)(upg2.cost*Mathf.Pow(1.5f,upg2.level));

        minereff_upg_cost_TEXT.text = upg2.cost + "G";
        }
    }
       public void Minerretrieve_UPG()
    {
        data.gold -= upg3.cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.miner_retrievetime = data.miner_retrievetime*upg3.effect;

        upg3.cost = (int)(upg3.cost*Mathf.Pow(1.5f,upg3.level));

        upg3.level++;
    }
       public void Soldiercap_UPG()
    {
        if(data.gold >= upg4.cost){
        data.gold -= upg4.cost;
        data.soldier_cap += (int)upg4.effect;
        
        gold_TEXT.text = "Gold: " + data.gold;
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        upg4.cost = 100*upg4.level;
        upg4.level++;

        

        

        soldiercap_upg_cost_TEXT.text = upg4.cost + "G";
        }
    }
       public void Soldierfr_UPG()
    {
        if(data.gold >= upg5.cost){
        data.gold -= upg5.cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.soldier_firerate = data.soldier_firerate*upg5.effect;

        upg5.cost = (int)(upg5.cost*Mathf.Pow(1.5f,upg5.level));

        soldierfr_upg_cost_TEXT.text = upg5.cost + "G";


        }
    }
       public void Soldierspawntime_UPG()
    {
        if(data.gold >= upg6.cost)
        {
        
        data.gold -= upg6.cost;
        gold_TEXT.text = "Gold: " + data.gold;

        data.soldier_spawntime = data.soldier_spawntime*upg6.effect;
        
        upg6.cost = (int)(upg6.cost*Mathf.Pow(1.5f,upg6.level));
        upg6.level++;


    }
    }
}
