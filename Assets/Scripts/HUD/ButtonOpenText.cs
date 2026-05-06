using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOpenText : MonoBehaviour
{
    public GameObject PanelPeces;

    public void OpenPanel()
    {
        if (PanelPeces != null)
        {
            bool isActive = PanelPeces.activeSelf;

            PanelPeces.SetActive(!isActive);
        }
    }
}
