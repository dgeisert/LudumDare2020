using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText, moneyText, waterText, healthText;
    public Transform buttonHolder;
    public BuildingSelectionTile selectionTile;

    public void UpdateScore(float val)
    {
        scoreText.text = "Score: " + val.ToString("#,#");
    }
    public void UpdateHealth(float val)
    {
        healthText.text = "Health: " + val.ToString("#,#");
    }
    public void UpdateMoney(float val)
    {
        moneyText.text = "Money: " + val.ToString("#,#");
    }
    public void UpdateWater(float val)
    {
        waterText.text = "Water: " + val.ToString("#,#");
    }
    public void EndGame(bool victory)
    {
        gameObject.SetActive(false);
    }

    public void Populate(Building building)
    {
        //put buttons in
    }
}