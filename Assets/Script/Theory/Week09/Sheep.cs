using System;
using UnityEngine;

public class Sheep 
{
    public int SheepMember;
    public static int _totalSheepCount;

    private static readonly int _initialPopulation = 5;
    static Sheep()
    {
        _totalSheepCount = _initialPopulation;

    }
    public int ASheepisNumber
    {
        get { return SheepMember; }
    }

    public static int TotalSheepCount
    {
        get { return _totalSheepCount; }
    }

    public Sheep(int sheepMember)
    {
        SheepMember = sheepMember;
        _totalSheepCount++;
    }

    public int AskNumber()
    {
        return SheepMember;
    }

    public static int GetAllSheep()
    {
        return _totalSheepCount;
    }

    public void SetNumber(int number)
    {
        SheepMember = number;
    }

    public static void RemoveSheep(int count)
    {
        _totalSheepCount -= count; 
    }
    
    public void Jump()
    {
        Debug.Log("Sheep is jump Sheep get gravity " + FarmUtils.Gravity);
    }
}
