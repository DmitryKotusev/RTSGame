using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentOnSelect : MonoBehaviour
{
    [SerializeField]
    Material selected;
    [SerializeField]
    Material unselected;

    bool isSelected = false;
    private void Start()
    {
        isSelected = false;
        Camera.main.transform.parent.GetComponent<AgentsSelector>().selectableObjects.Add(gameObject);
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void OnSelect()
    {
        isSelected = !isSelected;
        if (isSelected)
        {
            GetComponent<Renderer>().material = selected;
        }
        else
        {
            GetComponent<Renderer>().material = unselected;
        }
    }
}
