using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class hellrains_controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Data data;
    private float time;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        data = Main_Handler.instance.data;
        time = 0;
        
    }

    
    private void Update()
    {
        
        if(time < 15f){time += Time.deltaTime;}

        else if(time >= 15f && transform.position.y > Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }

        else
        {
            
        }
    }

    private void FixedUpdate() 
    {
        rb.velocity = -data.rain_speed*transform.up*Time.deltaTime*100f;
    }


}
