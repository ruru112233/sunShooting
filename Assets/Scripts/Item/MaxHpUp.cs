using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxHpUp : Item
{
    Slider hpSlider = null;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        hpSlider = GameObject.FindWithTag("HpSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        player.Hp += 20;
        hpSlider.maxValue = player.Hp;
    }
}
