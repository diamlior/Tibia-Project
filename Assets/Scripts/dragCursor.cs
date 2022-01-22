using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragCursor : MonoBehaviour
{
    public UnityEngine.UI.Image image;
    public void Visible(bool toHide)
    {
        image.enabled = toHide;
    }
}
