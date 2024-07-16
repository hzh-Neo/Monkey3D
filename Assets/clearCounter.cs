using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearCounter : MonoBehaviour
{
    [SerializeField] private GameObject something;
    [SerializeField] private GameObject TopPosition;
    public void interace()
    {
        Instantiate(something, TopPosition.transform);
        something.transform.localPosition = Vector3.zero;
    }
}
