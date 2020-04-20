using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Highlighters;

public class TestPointerMgr : MonoBehaviour {

    public Color EnterColor, MarkerSetColor;

    private VRTK_Pointer pointer;

    private void Awake()
    {
        pointer = GetComponent<VRTK_Pointer>();
        pointer.DestinationMarkerEnter += Pointer_DestinationMarkerEnter;
        pointer.DestinationMarkerExit += Pointer_DestinationMarkerExit;
        pointer.DestinationMarkerSet += Pointer_DestinationMarkerSet;
    }
    private void OnDestroy()
    {
        pointer.DestinationMarkerEnter -= Pointer_DestinationMarkerEnter;
        pointer.DestinationMarkerExit -= Pointer_DestinationMarkerExit;
        pointer.DestinationMarkerSet -= Pointer_DestinationMarkerSet;
    }
    private void Pointer_DestinationMarkerSet(object sender, DestinationMarkerEventArgs e)
    {
        HighLight(e.target, MarkerSetColor);
    }

    private void Pointer_DestinationMarkerExit(object sender, DestinationMarkerEventArgs e)
    {
        HighLight(e.target, Color.clear);
    }

    private void Pointer_DestinationMarkerEnter(object sender, DestinationMarkerEventArgs e)
    {
        HighLight(e.target, EnterColor);
    }
    private void HighLight(Transform target, Color color)
    {
        VRTK_BaseHighlighter highlighter = (target != null ? target.GetComponent<VRTK_BaseHighlighter>() : null);
        if (highlighter != null)
        {
            highlighter.Initialise();
            if (color != Color.clear)
            {
                highlighter.Highlight(color);
            }
            else
            {
                highlighter.Unhighlight();
            }
        }
    }


}
