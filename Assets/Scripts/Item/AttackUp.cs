using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : Item
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

        player.At += 5;
        player.BulletScale = new Vector3(player.BulletScale.x + 0.2f, player.BulletScale.y + 0.2f, player.BulletScale.z + 0.2f);
    }
}
