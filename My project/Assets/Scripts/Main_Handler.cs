using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class Main_Handler : MonoBehaviour
{
    public static Main_Handler instance{get; private set;}
    private void Awake() {instance = this;}
    public Data data;

    private Upgrade upg1, upg2, upg3, upg4, upg5, upg6, upg7;
    
    [SerializeField] private TMP_Text day_TEXT;

    [SerializeField] private TMP_Text day_gameover_TEXT;

    [SerializeField] private Canvas gameover_canv;
    [SerializeField] private TMP_Text gold_TEXT;
    [SerializeField] private TMP_Text goldtimer_TEXT;
    [SerializeField] private TMP_Text minercount_TEXT;
    [SerializeField] private TMP_Text soldiercount_TEXT;
    [SerializeField] private TMP_Text soldierspawntime_TEXT;

    [SerializeField] private TMP_Text recruit_miner_TEXT;
    [SerializeField] private TMP_Text recruit_soldier_TEXT;
    [SerializeField] private TMP_Text accumgold_TEXT;

    //cost texts
    [SerializeField] private TMP_Text minercap_upg_cost_TEXT;
    [SerializeField] private TMP_Text soldiercap_upg_cost_TEXT;
    [SerializeField] private TMP_Text minereff_upg_cost_TEXT;
    [SerializeField] private TMP_Text soldierfr_upg_cost_TEXT;
    [SerializeField] private TMP_Text soldierspawntime_upg_cost_TEXT;
    [SerializeField] private TMP_Text minerretrievetime_upg_cost_TEXT;

    [SerializeField] private TMP_Text clickpower_upg_cost_TEXT;
    [SerializeField] private TMP_Text skyclear_TEXT;

    [SerializeField] private TMP_Text Quarry_Lvl_TEXT;
    [SerializeField] private TMP_Text Foundry_Lvl_TEXT;

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

    
    private float time, time_MW, time_RS, time_SS = 0;

    private int spawnqueue = 0;

    public bool gameover;
    
    
    void Start()
    {
        gameover = false;

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

        upg7 = new Upgrade();
        upg7.cost = 200;



        
        gold_TEXT.text = "Gold: " + data.gold;
        minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        StartCoroutine(skyclear());
        

        
    }

    
    void Update()
    {
        
        Miners_Work();
        Rain_Spawner();
        Soldier_Spawner();
        Day_Counter();
        
        Gameover();
        

        if(data.miner_count>0){goldtimer_TEXT.text = (int)(data.miner_retrievetime - time_MW) + "s";}
        
        
    }

    private void Gold_Text_Update()
    {
        gold_TEXT.text = "Gold: " + (int)data.gold;
    }

    public void Barracks_Button()
    {
        if(recruit_B.isActiveAndEnabled && recruit2_B.isActiveAndEnabled)
        {
            
            recruit_B.gameObject.SetActive(false);
            recruit2_B.gameObject.SetActive(false);
        }
        else
        {
            
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
        else
        {
            Techtree_GO.gameObject.SetActive(false);
        }
       
        
    }




  
    private bool quarryenabled, foundryenabled, productiontree_open;

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
        
        if(data.gold >= data.soldier_cost && data.soldier_count < data.soldier_cap && spawnqueue < data.soldier_cap - data.soldier_count) 
        {
            data.gold -= data.soldier_cost;
            Gold_Text_Update();
            spawnqueue++;
        }

    
        else
        {
            // print insufficient gold
        }
    }

    public void Clicker_Button()
    {

        data.gold += data.click_power;
        Gold_Text_Update();
    }

    public void Clicker_upg_Button()
    {

        if(data.gold >= upg7.cost)
        {
             
            data.gold -= upg7.cost;
            Gold_Text_Update();

            data.click_power++;
            upg7.cost = upg7.cost*data.click_power;

            clickpower_upg_cost_TEXT.text = upg7.cost +"G";

        }
       



    }

    private void Soldier_Spawner()
    {
            if(spawnqueue > 0)
        {
            time_SS += Time.deltaTime;

            soldierspawntime_TEXT.text = (int)(data.soldier_spawntime - time_SS) + "s x " + spawnqueue;

           

            if(time_SS >= data.soldier_spawntime)
            {
                data.soldier_count++;
                soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;
                offset1 = UnityEngine.Random.Range(-1f,1f);
                Instantiate(soldier_GO, new Vector3(soldier_spawnpoint.position.x + offset1,soldier_spawnpoint.position.y,0), Quaternion.identity);

                spawnqueue--;
                time_SS = 0;
                if(spawnqueue == 0)
                {
                    soldierspawntime_TEXT.text = "";
                }
            }


        }
    }

    public void Recruit_Button_miner()
    {
          if(data.gold >= data.miner_cost && data.miner_count < data.miner_cap) 
        {
            data.gold -= data.miner_cost;
            data.miner_count++;
            
            Gold_Text_Update();
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
        accumgold_TEXT.text = (int)accum_gold + "G";

        }
        

        if(time_MW >= data.miner_retrievetime && data.miner_count >= 1)
        {
         
        data.gold += (int)accum_gold;
        Gold_Text_Update();
        time_MW = 0;
        accum_gold = 0;

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

    IEnumerator skyclear()
    {
        
            if(data.rain_count == 0)
            {
                skyclear_TEXT.text = "Sky is Clear";
                yield return new WaitForSeconds(3);
                skyclear_TEXT.text = "HELL RAINS HEAVIER";
                yield return new WaitForSeconds(3);
                skyclear_TEXT.text = "";

                data.rain_hz += data.soldier_count*data.soldier_firerate*0.5f; 


            }
            


    }

    private void Day_Counter()
    {
        time += Time.deltaTime;

        if(time >= 50 && !gameover)
        {
            data.day++;
            time = 0;
            day_TEXT.text = "Day: " + data.day;

            data.rain_hz += data.day*0.8f;
        }

        

    }





    // LAB RESEARCH & UPGRADE BUTTON FUNCTIONS

    public void Minercap_UPG()
    {
        if(data.gold >= upg1.cost){
        data.gold -= upg1.cost;
        data.miner_cap += (int)upg1.effect;
        
        Gold_Text_Update();
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
        Gold_Text_Update();

        data.miner_wc = data.miner_wc*upg2.effect;
        data.miner_cost = data.miner_cost*upg2.effect;
        recruit_miner_TEXT.text = "Recruit: " + (int)data.miner_cost + "G";

        upg2.cost = (int)(upg2.cost*Mathf.Pow(1.5f,upg2.level));

        minereff_upg_cost_TEXT.text = upg2.cost + "G";
        }
    }
       public void Minerretrieve_UPG()
    {
        if(data.gold >= upg3.cost)
        {
        data.gold -= upg3.cost;
        Gold_Text_Update();

        data.miner_retrievetime = data.miner_retrievetime*upg3.effect;

        upg3.cost = (int)(upg3.cost*Mathf.Pow(1.5f,upg3.level));

        upg3.level++;

        minerretrievetime_upg_cost_TEXT.text = upg3.cost + "G";
        }
    }
       public void Soldiercap_UPG()
    {
        if(data.gold >= upg4.cost){
        data.gold -= upg4.cost;
        data.soldier_cap += (int)upg4.effect;
        
        Gold_Text_Update();
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        upg4.cost = 100*upg4.level;
        upg4.level++;

        

        

        soldiercap_upg_cost_TEXT.text = upg4.cost + "G";
        }
    }
       public void Soldierfr_UPG()
    {
        if(data.gold >= upg5.cost)
        {
        
        data.gold -= upg5.cost;
        Gold_Text_Update();

        data.soldier_firerate = data.soldier_firerate*upg5.effect;
        data.soldier_cost = data.soldier_cost*upg5.effect;
        recruit_soldier_TEXT.text = "Recruit: " + (int)data.soldier_cost + "G";

        upg5.cost = (int)(upg5.cost*Mathf.Pow(1.5f,upg5.level));

        soldierfr_upg_cost_TEXT.text = upg5.cost + "G";


        }
    }
       public void Soldierspawntime_UPG()
    {
        if(data.gold >= upg6.cost)
        {
        
        data.gold -= upg6.cost;
        Gold_Text_Update();

        data.soldier_spawntime = data.soldier_spawntime*upg6.effect;
        
        upg6.cost = (int)(upg6.cost*Mathf.Pow(1.5f,upg6.level));
        upg6.level++;

        soldierspawntime_upg_cost_TEXT.text = upg6.cost + "G";


    }
    }

    private void Gameover()
    {

        if(gameover)
        {


            gameover_canv.gameObject.SetActive(true);
            day_gameover_TEXT.text = "Days Survived: " + data.day;

            

        
        }


    }

    public void Restart_Button()
    {

        gameover = false;

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

        upg7 = new Upgrade();
        upg7.cost = 200;
        
        Gold_Text_Update();
        minercount_TEXT.text = "Miners: " + data.miner_count + "/" + data.miner_cap;
        soldiercount_TEXT.text = "Soldiers: " + data.soldier_count + "/" + data.soldier_cap;

        time = 0;

        StartCoroutine(skyclear());

       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        


    }
}
