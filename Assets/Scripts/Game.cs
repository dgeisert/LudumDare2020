﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public PauseMenu pauseMenu;
    public ScoreScreen scoreScreen;
    public InGameUI inGameUI;
    public bool active = true;
    public Transform buildingPositions;
    public Building[, ] buildings;
    public Vector2Int currentSelection;
    public Building clickBuilding;
    public static bool started = false;

    public static float Score
    {
        get
        {
            if (Instance)
            {
                return Instance.score;
            }
            return -1f;
        }
        set
        {
            if (Instance)
            {
                Instance.score = value;
                Instance.inGameUI.UpdateScore(value);
            }
        }
    }
    public static float Health
    {
        get
        {
            if (Instance)
            {
                return Instance.health;
            }
            return -1f;
        }
        set
        {
            if (Instance)
            {
                Instance.health = value;
                Instance.inGameUI.UpdateHealth(value);
                if (value > 0)
                {
                    started = true;
                }
                if (value == 0 & started)
                {
                    started = false;
                    Instance.inGameUI.EndGame(true);
                    Instance.scoreScreen.EndGame(true);
                    Instance.pauseMenu.gameObject.SetActive(false);
                }
            }
        }
    }
    public static float Money
    {
        get
        {
            if (Instance)
            {
                return Instance.money;
            }
            return -1f;
        }
        set
        {
            if (Instance)
            {
                Instance.money = value;
                Instance.inGameUI.UpdateMoney(value);
            }
        }
    }
    public float score, money, health;
    int step = 0;
    int grid = 20;
    float buildingSpacing = 5;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        pauseMenu.gameObject.SetActive(false);
        scoreScreen.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        buildings = new Building[grid, grid];
        for (int i = 0; i < buildingPositions.childCount; i++)
        {
            Building b = buildingPositions.GetChild(i).GetComponent<Building>();
            Vector2Int pos = new Vector2Int(Mathf.FloorToInt(b.transform.localPosition.x / buildingSpacing), Mathf.FloorToInt(b.transform.localPosition.z / buildingSpacing));
            buildings[pos.x, pos.y] = b;
            b.Init(pos);
        }
        for (int i = 0; i < 10; i++)
        {
            Building b = buildings[Mathf.FloorToInt(Random.value * grid), Mathf.FloorToInt(Random.value * grid)];
            if (b != null)
            {
                ChangeBuilding(b, b.waterBuilding);
            }
            if (i < 2)
            {
                Building b1 = buildings[Mathf.FloorToInt(Random.value * grid), Mathf.FloorToInt(Random.value * grid)];
                if (b1 != null)
                {
                    ChangeBuilding(b1, b1.mountainBuilding);
                }
                Building b2 = buildings[Mathf.FloorToInt(Random.value * grid), Mathf.FloorToInt(Random.value * grid)];
                if (b2 != null)
                {
                    ChangeBuilding(b2, b2.cloudBuilding);
                }
            }
        }
        Health = 0;
        Money = 10;
        Score = 0;
    }

    public List<Building> GetNeighbors(Building b)
    {
        List<Building> neighbors = new List<Building>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (b.pos.x + i >= 0 &&
                    b.pos.x + i < buildings.GetLength(0) &&
                    b.pos.y + j >= 0 &&
                    b.pos.y + j < buildings.GetLength(1) &&
                    !(i == 0 && j == 0))
                {
                    if (buildings[b.pos.x + i, b.pos.y + j] != null)
                    {
                        neighbors.Add(buildings[b.pos.x + i, b.pos.y + j]);
                    }
                }
            }
        }
        return neighbors;
    }

    public void Select(Building building)
    {
        if (Money >= clickBuilding.cost)
        {
            currentSelection = building.pos;
            ChangeBuilding(building, clickBuilding);
            Money = money - clickBuilding.cost;
        }
    }

    public void ChangeBuilding(Building oldBuilding, Building newBuilding)
    {
        Building newInstance = Instantiate(newBuilding, oldBuilding.transform.position, Quaternion.identity, buildingPositions);
        buildings[oldBuilding.pos.x, oldBuilding.pos.y] = newInstance;
        newInstance.Init(oldBuilding);
        Destroy(oldBuilding.gameObject);
    }

    public void ChangeBuilding(Building newBuilding)
    {
        if (currentSelection != null)
        {
            ChangeBuilding(buildings[currentSelection.x, currentSelection.y], newBuilding);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Controls.Pause)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }
}