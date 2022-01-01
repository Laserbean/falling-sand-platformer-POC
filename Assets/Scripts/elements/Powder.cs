using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powder: element 
{
    public float InertialResistance = 0.5f;  
    public Powder(Vector2Int pos, Vector2? speed = null) : base (pos, speed)
    {
        this.color = new Color32(100, 100, 100, 255);
        this.matter = Matter.Solid;
    }

    public override Vector3Int Step()
    {
        // // // Vector3 newpos = this._position + this.speed;
        // // // Vector2Int candidate = (this.position - new Vector2Int(0, 1));

        Vector2Int cur = this.position;
        Vector2Int end = this.position + this.speedInt; 
        Vector2Int candidate = cur;


        this.speed = new Vector2(this.speed.x, this.speed.y - Constants.GRAVITY);
        
        List<Vector2Int> vlist = Chunks.GetLinearList(cur, end);  
        foreach(Vector2Int pos in vlist) {
            if (pos == cur) { //skips the initial position
                continue;
            }
            if (Chunks.GetCell(pos).matter == Matter.None) { //if current is go-able. 
                candidate = pos; 
            } else {
                // this.speed = new Vector2(0f, this.speed.y);
                if (candidate == cur) {  //if the next spot is not go-able
                    break;
                } else {
                    return  (Vector3Int) candidate; 
                }
            }
        }  

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
                this.speed = new Vector2(-Constants.GRAVITY*0.5f, this.speed.y  -Constants.GRAVITY*0.5f);
                return (Vector3Int)(this.position + new Vector2Int(-1, -1));
            } else {
                this.speed = new Vector2(Constants.GRAVITY*0.5f, this.speed.y  -Constants.GRAVITY*0.5f);
                return (Vector3Int)(this.position + new Vector2Int(1, -1));
            }
        }
        else {
            if (botright) {
                this.speed = new Vector2(Constants.GRAVITY*0.5f, this.speed.y  -Constants.GRAVITY*0.5f);
                return (Vector3Int)(this.position + new Vector2Int(1, -1));
            }
            if(botleft) {
                this.speed = new Vector2(-Constants.GRAVITY*0.5f, this.speed.y  -Constants.GRAVITY*0.5f);
                return (Vector3Int)(this.position + new Vector2Int(-1, -1));
            }
        }
        this.speed = Vector2.zero; 
        this.IsFreeFalling = false; 
        return Vector3Int.one;

        //if under is nothing, add gravity

    }

}


