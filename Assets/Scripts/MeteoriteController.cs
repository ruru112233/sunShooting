using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteController : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0;

    protected GameObject sunObj;

    // Start is called before the first frame update
    public virtual void Start()
    {
        sunObj = GameObject.FindWithTag("SunObj");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!GameManager.instance.gameOverFlag) 
          transform.RotateAround(sunObj.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
