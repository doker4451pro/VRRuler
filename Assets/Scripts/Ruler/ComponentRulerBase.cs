using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentRulerBase : MonoBehaviour
{
    [SerializeField]
    public bool isLeftHand;
    [SerializeField]
    public GameObject PointToDraw;
    [SerializeField]
    private Ruler ruler;

    public void SendMessageReady() 
    {
        ruler.GetReadyMessageFrom(this);
    }

    public void SendMessageNotReady() 
    {
        ruler.GetNotReadyMessageFrom(this);
    }
}
