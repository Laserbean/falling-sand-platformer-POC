using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powder: element 
{
    public float InertialResistance = 0.5f;  
    public float airResistance = 0.3f;
    public Powder(Vector2Int pos, Vector2? speed = null) : base (pos, speed)
    {
        this.color = new Color32(100, 100, 100, 255);
        this.matter = Matter.Solid;
        this.friction = 0.9f;
    }

    public override Vector3Int Step()
    {
        // // // Vector3 newpos = this._position + this.speed;
        // // // Vector2Int candidate = (this.position - new Vector2Int(0, 1));

        Vector2Int cur = this.position;
        // Vector2Int end = this.position + this.speedInt; 
        Vector2Int end = new Vector2Int(this.position.x + this.speedInt.x, this.position.y + (Mathf.Abs(this.speedInt.y) >=1 ? this.speedInt.y: -1));
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
                    this.speed = new Vector2(0, this.speed.y);
                    return  (Vector3Int) candidate; 
                }
            }
        }  
        if (candidate == end) {
            this.color = new Color32(100, 100, 100, 255);

            return  (Vector3Int) candidate; 
        }
        
        candidate = (this.position - new Vector2Int(0, 1));

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
                this.speed = new Vector2(-Constants.GRAVITY*0.3f, this.speed.y);
            this.color = new Color32(255, 50, 50, 255);

                return (Vector3Int)(this.position + new Vector2Int(-1, -1));
            } else {
                this.color = new Color32(50, 50, 255, 255);

                this.speed = new Vector2(Constants.GRAVITY*0.3f, this.speed.y);
                return (Vector3Int)(this.position + new Vector2Int(1, -1));
            }
        }
        else {
            if (botright) {
                // this.speed = new Vector2(Constants.GRAVITY*0.3f, this.speed.y  -Constants.GRAVITY*0.5f);
                this.color = new Color32(50, 50, 255, 255);

                this.speed = new Vector2(Constants.GRAVITY*0.3f, this.speed.y);
                return (Vector3Int)(this.position + new Vector2Int(1, -1));
            }
            if(botleft) {
                // this.speed = new Vector2(-Constants.GRAVITY*0.3f, this.speed.y  -Constants.GRAVITY*0.5f);
                this.color = new Color32(255, 50, 50, 255);

                this.speed = new Vector2(-Constants.GRAVITY*0.3f, this.speed.y);
                return (Vector3Int)(this.position + new Vector2Int(-1, -1));
            }
        }

        if(this.speed.y != 0f && Chunks.GetCell(this.position - new Vector2Int(0, 1)).speed.y == 0f) {
            float absY = Mathf.Max(this.speed.y * airResistance, 10);
            this.speed = new Vector2(this.speed.x< 0 ? -absY : absY, 0f);
            this.color = new Color32(40, 255, 50, 255);

        }
        this.speed =new Vector2 ( Chunks.GetCell(this.position - new Vector2Int(0, 1)).friction * this.friction * this.speed.x, 0f);

        this.IsFreeFalling = false; 
        return Vector3Int.one;

        //if under is nothing, add gravity

    }

}


