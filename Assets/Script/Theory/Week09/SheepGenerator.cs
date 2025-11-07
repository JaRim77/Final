using System;
using UnityEngine;

public class SheepGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sheep toro = new Sheep(0);
        Sheep alice = new Sheep(1);
        Sheep joe = new Sheep(3);

        Debug.Log("toro is number " + toro.SheepMember);
        Debug.Log("totalSheep is " + Sheep._totalSheepCount);

        Debug.Log("Alice is number " + alice.SheepMember);
        Debug.Log("All Sheep is farm " + Sheep.GetAllSheep());

        joe.SetNumber(2);
        Debug.Log("Joe is number " + joe.ASheepisNumber);
        Sheep.RemoveSheep(1);
        Debug.Log("All Sheep in farm " + Sheep._totalSheepCount);

        int WooCapacity = FarmUtils.ColcilateWooCapacity(Sheep.GetAllSheep());
        Debug.Log("WooCapacity is " + WooCapacity);
        Debug.Log("DayTime this month is " + FarmUtils.DayTime);

        //FarmUtils.WoolCapacity = 5;
    }
}
