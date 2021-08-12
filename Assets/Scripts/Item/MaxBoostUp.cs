using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBoostUp : Item
{


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        player.MaxBoost += 0.5f;
    }
}
