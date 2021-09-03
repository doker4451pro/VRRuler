using EPOOutline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChosenObjectBase : MonoBehaviour
{
    private Outlinable outlinable;
    protected virtual void Awake() 
    {
        MakeOutlinable();
    }
    protected virtual void Start()
    {
        outlinable = GetComponent<Outlinable>();
        outlinable.enabled = false;
    }
    public virtual void MakeSelected() 
    {
        Highligt();
    }
    public virtual void MakeNotSelected() 
    {
        Unlight();
    }
    public abstract void MakeChosen();

    private void Highligt()
    {
        if (outlinable != null)
        {
            outlinable.enabled = true;
        }
    }

    private void Unlight()
    {
        if (outlinable != null)
        {
            outlinable.enabled = false;
        }
    }

    private void MakeOutlinable()
    {
        if (GetComponent<Outlinable>() == null)
        {
            var outlinable = this.gameObject.AddComponent<Outlinable>();
            var renderers = this.GetComponentsInChildren<MeshRenderer>(true);
            foreach (var renderer in renderers)
            {
                outlinable.OutlineTargets.Add(new OutlineTarget
                {
                    renderer = renderer,
                    BoundsMode = BoundsMode.Default,
                    CullMode = UnityEngine.Rendering.CullMode.Back,
                    DilateRenderingMode = DilateRenderMode.PostProcessing
                });
            }
        }
    }
}
