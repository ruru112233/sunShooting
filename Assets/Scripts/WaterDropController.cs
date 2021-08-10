using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropController : MeteoriteController
{
    public bool leftFlag = false;

    //float instancTime = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (leftFlag)
        {
            transform.RotateAround(sunObj.transform.position, Vector3.up, speed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(sunObj.transform.position, Vector3.up, -speed * Time.deltaTime);
        }

        //instancTime += Time.deltaTime;

        //if (instancTime > 130.0f)
        //{
        //    instancTime = 0;
        //    gameObject.SetActive(false);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("è¡Ç¶ÇΩ");
            //instancTime = 0;
            gameObject.SetActive(false);
        }
    }
}
