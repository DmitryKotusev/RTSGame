using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentOnSelect : MonoBehaviour
{
    [SerializeField]
    GameObject selectedShader;
    [SerializeField]
    GameObject healthBarCanvas;

    bool isSelected = false;
    GameManager gameManager;
    private void Start()
    {
        isSelected = false;
        selectedShader.SetActive(isSelected);
        // healthBarCanvas.SetActive(isSelected);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.GetSelectableObjects().Add(gameObject);
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
            selectedShader.SetActive(isSelected);
            // healthBarCanvas.SetActive(isSelected);
        }
        else
        {
            selectedShader.SetActive(isSelected);
            // healthBarCanvas.SetActive(isSelected);
        }
    }
}
