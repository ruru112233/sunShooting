using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSearch : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        LookAtDrop();
    }

    // ˆê”Ô‹ß‚­‚É‚ ‚é…“H‚ÌŒü‚«‚ÉŒü‚­
    void LookAtDrop()
    {
        GameObject obj = SearchScript.FindDrop(player.transform);
        if (obj != null)
        {
            this.transform.LookAt(obj.transform);
        }
        
    }
}
