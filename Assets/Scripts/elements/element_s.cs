﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct element_s
{
    /*
    powder will be the base class. 
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
    public string element; 


    public int IsFreeFalling{get; set;} //0 sleeping, 1 and 2 are awake
    public float inertialResistance; 
    public float friction;
    public float airResistance;

    public element_s(Vector2Int pos, Vector2? speed = null)
    {
        this.IsFreeFalling = 2;
        this.position = pos; 
        if (speed == null) {
            this.speed = Vector2.zero;
        } else {
            this.speed = (Vector2)speed;
        }
        this.element = "nothing"; 
        this.matter = Matter.None;
        this.color = new Color32(100,100,100,100);
        this.friction = 0.5f;
        this.inertialResistance = 0f; 
        this.airResistance = 0.5f; 
    }

}

public class e_gen {
    public static element_s Sand(Vector2Int pos, Vector2? speed = null) {
        Vector2 tspeed; 
        if (speed == null) {
            tspeed = Vector2.zero;
        } else {
            tspeed = (Vector2)speed;
        }
        element_s sand = new element_s(pos, tspeed); 

        sand.matter = Matter.Powder;
        sand.element = "sand";

        byte off = (byte) Mathf.Round(Random.Range(50, 150));
        sand.color = new Color32(255, 244, off, 255);
        sand.inertialResistance = 0.2f;
        sand.friction = 0.1f; 
        return sand; 
    }

        public static element_s Bedrock(Vector2Int pos, Vector2? speed = null) {
        Vector2 tspeed; 
        if (speed == null) {
            tspeed = Vector2.zero;
        } else {
            tspeed = (Vector2)speed;
        }
        element_s thisp = new element_s(pos, tspeed); 

        thisp.matter = Matter.Bedrock;
        thisp.element = "bedrock";

        byte off = (byte) Mathf.Round(Random.Range(50, 150));
        thisp.color = new Color32(0,0,0,0);
        thisp.inertialResistance = 1f;
        return thisp; 
    }

}

public static class e_step {
    public static int TryWakeCell(element_s thisp) {
        if (thisp.IsFreeFalling >0) { 
            return thisp.IsFreeFalling;
        }
        return Random.Range(0f, 1f) >= thisp.inertialResistance ? 2: 0;
        // this.IsFreeFalling = true;
        // return thisp;
    }


    

    public static Vector3Int PowderStep(element_s powder)
    {
        /*
                Calculates the destination to swap or whatever of powder element. 
                Returns a vector where x and y are the coordinates and z is the behaviour. 
                Currently z = 0  : swap
                        z = 1  : do nothing/ignore
                        z = -1 : delete/replace with nothing element. 
                      
        */
        // // // Vector3 newpos = powder._position + powder.speed;
        Vector2Int curspeed = new Vector2Int((int) (Mathf.Abs(powder.speed.x) >= 1 ? powder.speed.x: Mathf.Round(Random.Range(0f, powder.speed.x))),
                            (int) (Mathf.Abs(powder.speed.y) >= 1 ? powder.speed.y: Mathf.Round(Random.Range(0f, powder.speed.y))));

        Vector2Int end = powder.position + curspeed; 
        
        Vector2Int candidate;
        // powder.color = new Color32(200, 200, 50, 255);

        if (Chunks.GetCell((powder.position + new Vector2Int(0, -1))).matter == Matter.None) {  //can go down
            // powder.color = new Color32(255, 50, 50, 255);//red
            candidate = (powder.position + new Vector2Int(0, -1));
            powder.speed = new Vector2(powder.speed.x, powder.speed.y - Constants.GRAVITY);
            for (int i = -2; i >= curspeed.y; i--) {
                if (Chunks.GetCell(powder.position + new Vector2Int(0, i)).matter == Matter.None) {
                } else { 
                    break;
                }
                candidate = (powder.position + new Vector2Int(0, i));

            }
            // powder.color = new Color32(10, 244, 200, 255);
            if(powder.speed.y != 0f && Chunks.GetCell(candidate - new Vector2Int(0, 1)).matter != Matter.None) {
                float absY = Mathf.Min(Mathf.Abs(powder.speed.y) * powder.airResistance, 3);
                if (Mathf.Abs(powder.speed.x) < 0.01f) {
                    powder.speed = new Vector2(Mathf.Sign(Random.Range(-1f, 1f))*absY, 0f);
                } else {
                    powder.speed = new Vector2(Mathf.Sign(powder.speed.x)*absY, 0f);
                }
            }
            if (Chunks.GetCell(candidate).matter != Matter.None) {
                Debug.LogError("error");
            }
            
            Chunks.SetCell(powder);
            return (Vector3Int) candidate;
        } else {
                // powder.color = new Color32(40, 205, 255, 255); //turquoise maybe
            candidate = (powder.position);
            if (Mathf.Abs(curspeed.x) > 0) {
                powder.speed =new Vector2 ( Chunks.GetCell(powder.position-new Vector2Int(0, 1)).friction * powder.friction * powder.speed.x, 0f);
                if (Mathf.Abs(powder.speed.x) < 0.01) {
                    powder.IsFreeFalling -=1;
                }
                // powder.color = new Color32(40, 255, 50, 255); //green
                for (int i = 1; i < Mathf.Abs(curspeed.x); i++) {
                    if (Chunks.GetCell(powder.position + new Vector2Int((int)Mathf.Sign(curspeed.x)*i,0)).matter == Matter.None) {
                    } else { 
                        powder.speed = Vector2.zero; 
                        if (Chunks.GetCell(powder.position + new Vector2Int((int)Mathf.Sign(curspeed.x)*i,0)).matter != powder.matter) {
                                    //powder thing doesn't work.
                        }
                        break;
                    }
                    candidate = (powder.position + new Vector2Int((int)Mathf.Sign(curspeed.x)*i,0));

                }
                // powder.color = new Color32(40, 30, 50, 255); 
                
                if (candidate== powder.position) {
                     //pass
                } else {
                    if (Chunks.GetCell(candidate).matter != Matter.None) {
                        Debug.LogError("error");
                    }
                    Chunks.SetCell(powder);
                    
                    return (Vector3Int) candidate;
                }
            } 
            // powder.color = new Color32(10, 244, 200, 255);
            
        }
        bool botleft, botright;
        botleft = Chunks.GetCell(powder.position + new Vector2Int(-1, -1)).matter == Matter.None;
        botright = Chunks.GetCell(powder.position + new Vector2Int(1, -1)).matter == Matter.None;

        if (powder.IsFreeFalling == 0) {
            Chunks.SetCell(powder);

            return Vector3Int.one;
        }
        
        if (Random.Range(0f, 1f) <= powder.inertialResistance) {
            powder.IsFreeFalling = 0;
            Chunks.SetCell(powder);

            return Vector3Int.one;
        }


        if (botleft && botright) {
            if (Random.Range(0f, 1f) > 0.5f) { 
                powder.speed = new Vector2(-Constants.GRAVITY*0.3f, powder.speed.y);
                return (Vector3Int)(powder.position + new Vector2Int(-1, -1));
            } else {
                powder.speed = new Vector2(Constants.GRAVITY*0.3f, powder.speed.y);
                return (Vector3Int)(powder.position + new Vector2Int(1, -1));
            }
        }
        else {
            if (botright) {
                // powder.speed = new Vector2(Constants.GRAVITY*0.3f, powder.speed.y  -Constants.GRAVITY*0.5f);
                powder.speed = new Vector2(Constants.GRAVITY*0.3f, powder.speed.y);
                return (Vector3Int)(powder.position + new Vector2Int(1, -1));
            }
            if(botleft) {
                // powder.speed = new Vector2(-Constants.GRAVITY*0.3f, powder.speed.y  -Constants.GRAVITY*0.5f);
                powder.speed = new Vector2(-Constants.GRAVITY*0.3f, powder.speed.y);
                return (Vector3Int)(powder.position + new Vector2Int(-1, -1));
            }
        }

        // if(powder.speed.y != 0f && Chunks.GetCell(powder.position - new Vector2Int(0, 1)).speed.y == 0f) {
        //     float absY = Mathf.Max(powder.speed.y * airResistance, 10);
        //     powder.speed = new Vector2(powder.speed.x< 0 ? -absY : absY, 0f);
        //     powder.color = new Color32(40, 255, 50, 255);
        // }
        // powder.speed =new Vector2 ( Chunks.GetCell(powder.position - new Vector2Int(0, 1)).friction * powder.friction * powder.speed.x, 0f);

        powder.IsFreeFalling -=1; 
        Chunks.SetCell(powder);
        return Vector3Int.one;

        //if under is nothing, add gravity

    }



}
