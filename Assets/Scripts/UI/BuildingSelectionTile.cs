using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelectionTile : MonoBehaviour
{
    public Building newBuilding;
    public Image image;
    public TextMeshProUGUI cost, growTime;
    public void Init(Building newBuilding)
    {
        this.newBuilding = newBuilding;
        cost.text = newBuilding.cost.ToString();
        growTime.text = newBuilding.growTime.ToString();
        image.sprite = newBuilding.icon;
    }

    public void OnClick()
    {
        Game.Instance.ChangeBuilding(newBuilding);
    }
}
