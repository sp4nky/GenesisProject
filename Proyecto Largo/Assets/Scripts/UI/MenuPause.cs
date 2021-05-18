using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public MenuAnimationSettings animSettings;
    bool opened = false;

    // Update is called once per frame
    void Update()
    {
        bool open = Input.GetButtonDown("Cancel");;
        if (open && !opened)
        {
            OpenMenu();
        }
        else
        {
            if (open && opened)
            {
                CloseMenu();
            }
        }
    }

    private void OpenMenu()
    {
        animSettings.Show();
        animSettings.OpenMenuAnimation();
        opened = true;
        GameManagement.instance.Pause();
    }

    public void CloseMenu()
    {
        animSettings.CloseMenuAnimation();
        opened = false;
        GameManagement.instance.Unpause();
    }


}
