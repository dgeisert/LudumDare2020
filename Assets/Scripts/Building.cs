using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuildingType
{
    ATTACK,
    HEAL,
    SLOW,
    WATER,
    MONEY,
    EMPTY
}

public class Building : MonoBehaviour
{
    public float cost, water, waterConsumptionRate, growTime, minViableSize, startSize;
    public float rate, amount, lastActivation;
    public Transform growObject;
    public Vector2Int pos;
    public Sprite icon;
    public List<Building> upgrades;
    public BuildingType type;
    public Building targetBuilding;
    public Enemy targetEnemy;

    public void Init(Vector2Int pos)
    {
        growObject.localScale = Vector3.one * startSize;
        this.pos = pos;
    }

    void Update()
    {
        if (growObject.localScale.x < 1 && waterConsumptionRate * Time.deltaTime < water)
        {
            water -= waterConsumptionRate * Time.deltaTime;
            growObject.localScale += Vector3.one * (Time.deltaTime / growTime * (1 - startSize));
        }
        if (type != BuildingType.WATER && (growObject.localScale.x < minViableSize || lastActivation + rate > Time.time))
        {
            return;
        }
        switch (type)
        {
            case BuildingType.ATTACK:
                if (!Attack())
                {
                    return;
                }
                break;
            case BuildingType.WATER:
                Water();
                break;
            case BuildingType.MONEY:
                Money();
                break;
        }
        lastActivation = Time.time;
    }

    bool Attack()
    {
        if (targetEnemy == null)
        {
            return false;
        }
        return true;
    }
    void Water()
    {
        if (water > 0)
        {
            foreach (Building b in Game.Instance.GetNeighbors(this))
            {
                b.water += amount;
                water -= amount;
            }
        }
        else
        {
            //replace with empty
        }
    }
    void Money()
    {
        Game.Money += amount;
    }

    void OnClick()
    {

    }

}