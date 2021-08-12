using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxBoostUp : Item
{
    Slider boostSlider = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        boostSlider = GameObject.FindWithTag("BoostSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        player.MaxBoost += 0.1f;
        boostSlider.maxValue = player.MaxBoost;
    }
}
