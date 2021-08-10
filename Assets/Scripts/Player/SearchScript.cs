using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SearchScript
{
    public static GameObject FindDrop(Transform transform)
    {
        GameObject[] drops = GameObject.FindGameObjectsWithTag("WaterDrop");
        GameObject closest = null;

        float distance = Mathf.Infinity;

        Vector3 position = transform.position;

        foreach (GameObject meteo in drops)
        {
            Vector3 diff = meteo.transform.position - position;

            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = meteo;
                distance = curDistance;
            }
        }

        return closest;
    }
}
