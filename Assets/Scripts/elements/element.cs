using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class element 
{
    /*
    This will be the base class. 
    */
    public Vector2 speed {get; set;}
    public Vector2Int speedInt {
        get {
            return new Vector2Int((int) this.speed.x,(int) this.speed.y);
        }
    }
    public Vector2Int position {get; set;}
    public Color32 color{get; set;}
    public Matter matter{get; set;}
    public bool IsFreeFalling{get; set;}

    public element(Vector2Int pos, Vector2? speed = null)
    {
        this.IsFreeFalling = true;
        this.position = pos; 
        if (speed == null) {
            this.speed = Vector2.zero;
        } else {
            this.speed = (Vector2)speed;
        }
        this.matter = Matter.None;
        this.color = new Color32(100,100,100,100);
    }

    public virtual Vector3Int Step()
    {
        return Vector3Int.one;
        //Do nothing here. 
    }
    public virtual Vector3Int StepSimple()
    {
        return Vector3Int.one;
        //Do nothing here. 
    }



}

public enum Matter
{
    Solid,
    Liquid,
    Gas, 
    Other,
    None,
    Bedrock
}

public class Constants
{
    public const float GRAVITY = 0.5f;  //pixels per frame
    public const int CHUNK_SIZE = 16;
    public const float PIXEL_SCALE = 0.125f;
    public const float PERIOD = 0.1f;
    // public const float PERIOD = 0.5f;

}