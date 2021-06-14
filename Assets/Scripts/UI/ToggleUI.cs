using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    [SerializeField] GameObject objectToEnable;
    [SerializeField] GameObject objectToDisable;

    public void EnableObject()
    {
        objectToEnable.SetActive(true);
    }

    public void DisableObject()
    {
        objectToDisable.SetActive(false);
    }
}
