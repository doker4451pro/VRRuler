using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HTCComponentRuler : ComponentRulerBase
{
    [SerializeField]
    private SteamVR_Input_Sources steamVRHandType;
    [SerializeField]
    private SteamVR_Action_Boolean ClickAction;

    private void Update()
    {
        if (ClickAction.GetStateDown(steamVRHandType)) 
        {
            SendMessageReady();
        }

        if (ClickAction.GetStateUp(steamVRHandType))
        {
            SendMessageNotReady();
        }
    }
}
