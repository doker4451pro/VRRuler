using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Ruler : MonoBehaviour
{
    [SerializeField] private GameObject leftSphere;
    [SerializeField] private GameObject rightSphere;
    [SerializeField] private TextMeshPro textObject;
    [SerializeField] private Camera cameraAtLook;

    private ComponentRulerBase firstComponent;
    private ComponentRulerBase secondComponent;
    private LineRenderer line;
    private bool canFirstDraw=false;
    private bool canDraw = false;

    private void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        DrawRuler();
        TextLookAtCamera();
    }

    public void GetReadyMessageFrom(ComponentRulerBase componentRuler) 
    {
        if (componentRuler.isLeftHand)
            firstComponent = componentRuler;
        else
            secondComponent = componentRuler;

        //можем рисовать только когда в первый раз есть оба компонента
        if (!canFirstDraw && firstComponent != null && secondComponent != null)
        {
            canFirstDraw = true;
            SetTurn(true);
        }
    }

    public void SetTurn(bool flag) 
    {
        canDraw = flag;
        leftSphere.SetActive(flag);
        rightSphere.SetActive(flag);
        textObject.gameObject.SetActive(flag);
        line.enabled = flag;
    }

    public void SetTurn() 
    {
        canDraw = !canDraw;
        leftSphere.SetActive(canDraw);
        rightSphere.SetActive(canDraw);
        textObject.gameObject.SetActive(canDraw);
        line.enabled = canDraw;
    }


    public void GetNotReadyMessageFrom(ComponentRulerBase componentRuler) 
    {
        if (componentRuler.isLeftHand)
            firstComponent = null;
        else
            secondComponent = null;
    }
   
    private void DrawRuler() 
    {
        if (canDraw)
        {
            Vector3 pointA = firstComponent != null ? firstComponent.PointToDraw.transform.position : leftSphere.transform.position;
            Vector3 pointB = secondComponent != null ? secondComponent.PointToDraw.transform.position: rightSphere.transform.position;


            line.SetPosition(0, pointA);
            line.SetPosition(1, pointB);


            leftSphere.transform.position = pointA;
            rightSphere.transform.position = pointB;

            //Round
            var distance = Mathf.Round(Vector3.Distance(pointA, pointB) * 10000) / 100;

            textObject.text = distance.ToString() + " см";
            textObject.transform.position = (pointA + pointB) / 2;
        }
    }

    private void TextLookAtCamera() =>
        textObject.transform.rotation = Quaternion.LookRotation(textObject.transform.position - cameraAtLook.transform.position);
}
