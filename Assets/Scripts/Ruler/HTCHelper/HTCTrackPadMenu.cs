using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HTCTrackPadMenu : MonoBehaviour
{
    [SerializeField]
    private ChosenObjectBase[] chosens;
    [SerializeField]
    private SteamVR_Action_Vector2 trackPadpositionAction;
    [SerializeField]
    private SteamVR_Input_Sources steamVRHandType;
    [SerializeField][Range(0.0f,1.0f)]
    private float deadZona = 0.5f;
    [SerializeField]
    private float distance = 10f;

    private Vector2 vectorStartLeft = Vector2.left;

    private int chosenIndexNow=-1;

    private void Start()
    {
        var vectorFinishRight = new Vector2(-vectorStartLeft.x, vectorStartLeft.y);
        var sectorAngle = Vector2.Angle(vectorStartLeft, vectorFinishRight);
        var delta = sectorAngle / (chosens.Length*2);

        for (int i = 1; i < chosens.Length * 2; i += 2)
        {
            var angle = Mathf.PI * i * delta / 180 - Mathf.PI / 2;
            chosens[(i - 1) / 2].transform.position = new Vector3(Mathf.Sin(angle) * distance, 0, Mathf.Cos(angle) * distance);
        }

    }
    private void Update()
    {
        if (trackPadpositionAction.GetAxis(steamVRHandType) == Vector2.zero) 
        {
            if (chosenIndexNow != -1)
            {
                chosens[chosenIndexNow].MakeChosen();
                chosens[chosenIndexNow].MakeNotSelected();
                chosenIndexNow = -1;
            }
            foreach (var item in chosens)
            {
                item.gameObject.SetActive(false);
            }
            return;
        }

        foreach (var item in chosens)
        {
            item.gameObject.SetActive(true);
        }

        var index = GetIndexChosenObject();

        if (chosenIndexNow != index)
        {
            if (chosenIndexNow != -1) 
            {
                chosens[chosenIndexNow].MakeNotSelected();
            }
            if (index != -1)
            {
                chosens[index].MakeSelected();
            }
            chosenIndexNow = index;
        }
    }
    private int GetIndexChosenObject()
    {
        int index = -1;
        var trackPosition = trackPadpositionAction.GetAxis(steamVRHandType);

        var angleTrackPad = Vector2.SignedAngle(trackPosition, vectorStartLeft);

        if (angleTrackPad > 0 && trackPosition.magnitude > deadZona) 
        {
            var vectorFinishRight = new Vector2(-vectorStartLeft.x, vectorStartLeft.y);
            var sectorAngle = Vector2.Angle(vectorStartLeft, vectorFinishRight);
            var delta = sectorAngle / chosens.Length;

            for (int i = 0; i * delta <= sectorAngle - delta; i++)
            {
                if (i * delta <= angleTrackPad && angleTrackPad <= (i + 1) * delta)
                    return i;
            }
        }
        return index;
    }
}
