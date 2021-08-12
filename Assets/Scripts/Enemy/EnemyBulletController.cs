using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EnemyBulletController : BulletController
{
    public int bulletAt = 0;
    Slider hpSlider;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GameObject.FindWithTag("HpSlider").GetComponent<Slider>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("É_ÉÅÅ[ÉW");
            hpSlider.value -= bulletAt;

            AudioManager.instance.PlaySE(6);
            
            gameObject.SetActive(false);
        }
    }
}
