using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{
    public GameObject menuScreen;
    public bool showOnLoad;


    private void Start()
    {
        if (showOnLoad) Show();
        else Hide();
    }

    public void Show()
    {
        menuScreen.SetActive(true);

    }

    public void Hide()
    {
        menuScreen.SetActive(false);
    }
}
