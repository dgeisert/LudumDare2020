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
    public Building currentSelection;
    public Transform selectionRing;

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
        selectionRing.position = building.transform.position + Vector3.up * 0.2f;
        currentSelection = building;
        inGameUI.Populate(building);
    }

    public void ChangeBuilding(Building newBuilding)
    {
        if (currentSelection != null)
        {
            Building old = currentSelection;
            currentSelection = Instantiate(newBuilding, old.transform.position, Quaternion.identity);
            buildings[old.pos.x, old.pos.y] = currentSelection;
            currentSelection.Init(old.pos);
            Destroy(old.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Controls.Next)
            {
                Next();
            }
            if (Controls.Pause)
            {
                Pause();
            }
            Score += Time.deltaTime * Random.value * 100;
        }
    }

    public void Pause()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    void Next()
    {
        if (step > 0)
        {
            step--;
            if (step == 0)
            {
                inGameUI.EndGame(true);
                scoreScreen.EndGame(true);
                pauseMenu.gameObject.SetActive(false);
            }
        }
    }
}