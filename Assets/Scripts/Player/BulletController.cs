using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float delTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        delTime += Time.deltaTime;

        if (delTime > 3.0f)
        {
            delTime = 0;
            gameObject.SetActive(false);
        }
    }
}
