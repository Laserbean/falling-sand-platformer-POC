using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powder: element 
{
    public Powder(Vector2Int pos, Vector2? speed = null) : base (pos, speed)
    {
        this.color = new Color32(100, 100, 100, 255);
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
        bool botleft, botright;
        botleft = Chunks.GetCell(this.position + new Vector2Int(-1, -1)).matter == Matter.None;
        botright = Chunks.GetCell(this.position + new Vector2Int(1, -1)).matter == Matter.None;

        if (botleft && botright) {
            if (Random.Range(0f, 1f) > 0.5f) { 
                return (Vector3Int)(this.position + new Vector2Int(-1, -1));
            } else {
                return (Vector3Int)(this.position + new Vector2Int(1, -1));
            }
        }
        else {
            if (botright) {
                return (Vector3Int)(this.position + new Vector2Int(1, -1));
            }
            if(botleft) {
                return (Vector3Int)(this.position + new Vector2Int(-1, -1));
            }
            
        }
        

        return Vector3Int.one;

        //if under is nothing, add gravity

    }

}


