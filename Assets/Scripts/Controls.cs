using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls Instance;
    public static bool Next
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.UpArrow);
        }
    }
    public static bool Pause
    {
        get
        {
            return Input.GetKeyDown(KeyCode.P);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 500.0f))
            {
                Building b = hit.transform.GetComponentInParent<Building>();
                if (b != null)
                {
                    if (Game.Instance)
                    {
                        Game.Instance.Select(b);
                    }
                }
            }
        }
    }
}