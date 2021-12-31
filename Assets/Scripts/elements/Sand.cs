using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sand: element 
{
    public Sand(Vector2Int pos, Vector2? speed = null) : base (pos, speed)
    {
        byte off = (byte) Mathf.Round(Random.Range(70, 130));
        this.color = new Color32(255, 244, off, 255);

        this.matter = Matter.Solid;

    }

    public override Vector3Int Step()
    {
        // // // Vector3 newpos = this._position + this.speed;

        //for now, just go down. 
        Vector2Int candidate = (this.position - new Vector2Int(0, 1));
        // Debug.Log(Chunks.GetCell(candidate).matter);
        if (Chunks.GetCell(candidate).matter == Matter.None) {
            // Debug.Log("Swap"+ candidate+ "curpors" + this.position);
            // this.color = new Color32(10, 244, 200, 255);

            return (Vector3Int) candidate;
        } 
        candidate = this.position - new Vector2Int(-1, 1);
        if (Chunks.GetCell(candidate).matter == Matter.None) {
            return (Vector3Int) candidate;
        }
        candidate = this.position - new Vector2Int(1, 1);
        if (Chunks.GetCell(candidate).matter == Matter.None) {
            return (Vector3Int) candidate;
        }
        

        return Vector3Int.one;

        //if under is nothing, add gravity

    }

}


