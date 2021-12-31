using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sand: Powder 
{
    public Sand(Vector2Int pos, Vector2? speed = null) : base (pos, speed)
    {
        byte off = (byte) Mathf.Round(Random.Range(70, 130));
        this.color = new Color32(255, 244, off, 255);
    }


}

