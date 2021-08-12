using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : Enemy
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        EnemyHp = 10;
        EnemyAt = 5;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
