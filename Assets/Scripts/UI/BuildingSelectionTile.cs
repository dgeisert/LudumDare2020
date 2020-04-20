using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSelectionTile : MonoBehaviour
{
    public Building newBuilding;
    public Image outline, backing;
    public TextMeshProUGUI cost;
    public Color selectColor;
    public List<BuildingSelectionTile> otherButtons;
    public void Start()
    {
        cost.text = "$" + newBuilding.cost.ToString();
        if(newBuilding.type == BuildingType.MONEY)
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        Game.Instance.clickBuilding = newBuilding;
        foreach (BuildingSelectionTile bst in otherButtons)
        {
            bst.Deselect();
        }
        backing.color = selectColor;
        outline.enabled = true;
    }

    public void Deselect()
    {
        outline.enabled = false;
        backing.color = Color.white;
    }
}