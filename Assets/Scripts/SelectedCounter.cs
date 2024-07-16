using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{

    public clearCounter selectCC;

    public GameObject selected;

    public bool hasThing;

    private void Start()
    {
        PlayerMovement.Instance.onSelectChange += Instance_onSelectChange;
    }

    private void Instance_onSelectChange(object sender, PlayerMovement.SelectCounter e)
    {
        if (selectCC == e.CC)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        selected.SetActive(true);
    }

    private void Hide()
    {
        selected.SetActive(false);
    }
}
