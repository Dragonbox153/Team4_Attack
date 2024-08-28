using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject prefabLevel;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject ScoreCanvas;

    public void OnStart()
    {
        var level = Instantiate(prefabLevel);
        level.transform.SetParent(transform.parent);
        level.transform.position = Vector2.zero;
        ScoreCanvas.SetActive(true);
        menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStart();
        }
    }
}
