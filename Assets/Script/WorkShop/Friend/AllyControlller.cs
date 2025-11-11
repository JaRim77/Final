using System.Collections.Generic;
using UnityEngine;


public class AllyController : MonoBehaviour
{
    public Ally ally;

    void Update()
    {
        if (ally == null) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            ally.FollowPlayer();
            Debug.Log("Follow Me!");
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            ally.HandleEnemy();
            Debug.Log("Attack!!");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ally.TryCollectNearbyItem();
            Debug.Log("Secrch for items");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ally.ModifyBond(10); // เพิ่มค่าความสัมพันธ์
        }
    }
}

