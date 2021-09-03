using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenObjectRuler : ChosenObjectBase
{
    [SerializeField]
    private Ruler ruler;
    public override void MakeChosen() 
    {
        ruler.SetTurn();
    }
}
