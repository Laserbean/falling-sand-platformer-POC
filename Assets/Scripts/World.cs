using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// using System.Linq; //for the foreach loop thing




public class World : MonoBehaviour
{
    // Global constants
    // // // const int CHUNK_AREA = CHUNK_SIZE* CHUNK_SIZE;

    public Tilemap colorTileMap;
    public Tilemap solidTileMap;

    public Tile basictile;
    // TileBase[] tileArray = new TileBase[Constants.CHUNK_SIZE* Constants.CHUNK_SIZE];

    // public static Dictionary<Vector2Int, bool[,]> doneChunks; 
    // public static Dictionary<Vector2Int, TileBase[]> world_dict;
    public static Dictionary<Vector2Int, element[]> world_dict;
    public static Dictionary<Vector2Int, int> chunkstate_dict;
    public static List<List<Vector2>> list_o_collider_points;

    public static Dictionary<Vector2Int, Vector2Int> execution_dict;
    public static Dictionary<Vector2Int, List<List<Vector2>>> chunkHitbox_dict;



    public const int minx = -5, maxx  =5;
    public const int miny = -5, maxy  =5;
    
    // public const int minx = 0, maxx  =1;
    // public const int miny = 0, maxy  =1;

    // public const int minx = -1, maxx  =3;
    // public const int miny = -1, maxy  =3;
    public PolygonCollider2D thispolygon; 
    TilePolygon Tilepoly; 
    void Start()
    {
        TileManager.init(basictile, solidTileMap);
        // colorTileMap = GameObject.Find("solid").GetComponent<Tilemap>();
        World.world_dict = new Dictionary<Vector2Int, element[]>(); 
        World.chunkstate_dict = new Dictionary<Vector2Int, int>(); 
        World.execution_dict = new Dictionary<Vector2Int, Vector2Int>();
        World.chunkHitbox_dict = new Dictionary<Vector2Int, List<List<Vector2>>>();


        list_o_collider_points = new List<List<Vector2>>();

        thispolygon = this.gameObject.GetComponent<PolygonCollider2D>();
        Tilepoly = new TilePolygon();

        // chunkgen(new Vector2Int(0, 0));
        Vector2Int curpos;
        for (int i = minx; i < maxx; i++) {
            for (int j = miny; j < maxy; j++) {
                curpos = new Vector2Int(i * Constants.CHUNK_SIZE, j* Constants.CHUNK_SIZE);
                chunkgen(curpos);

                Chunks.fillChunk(curpos, colorTileMap, basictile); //fills with basic tile
                Chunks.drawChunk(curpos, colorTileMap);

                World.chunkstate_dict.Add(curpos, 1); 
                // Chunks.fillSolidChunk(curpos, solidTileMap, basictile);
            }
        }
        // Chunks.drawChunk(Vector2Int.zero, colorTileMap);
        // for(int jj =0;jj < Constants.CHUNK_SIZE; jj++) {
        //     for(int ii =0;ii < Constants.CHUNK_SIZE; ii++) {
        //         Debug.Log("Hmmmm");
        //         colorTileMap.SetTile( new Vector3Int(ii, jj, 0),basictile);
        //     }
        // }
        // Vector3Int[] positions = new Vector3Int[Constants.CHUNK_SIZE * Constants.CHUNK_SIZE];

        // for(int ii =0;ii < Mathf.Pow(Constants.CHUNK_SIZE, 2); ii++) {
        //     positions[ii] = new Vector3Int(ii % Constants.CHUNK_SIZE, ii / Constants.CHUNK_SIZE, 0);
        // }
        // colorTileMap.SetTiles(positions, fish);
        World.print("done");
        // float period = 1f/60f; //time update

        // InvokeRepeating("MyUpdate", Constants.PERIOD, Constants.PERIOD);
 
    }

    //worldget
    void chunkgen(Vector2Int chunkpos)
    {
        element[] fish = new element[(int)Mathf.Pow(Constants.CHUNK_SIZE, 2)];

        // List<Vector2Int> vlist = Chunks.GetLinearList(chunkpos+ new Vector2Int(8,8), chunkpos + new Vector2Int(14,15));
        float val = 0.5f; 
        if (chunkpos.y < 0|| chunkpos.x != 0) { //  ){ // 
            val = 1f; 
        }
        for(int ii =0;ii < Mathf.Pow(Constants.CHUNK_SIZE, 2); ii++) {
            // // // cur index in chunk is [ii + ii * Constants.CHUNK_SIZE]
            if (Random.Range(0f, 1f) > val ){//&& Chunks.mod(ii,Constants.CHUNK_SIZE) == 0) {
                fish[ii] = new Sand(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            } else {
                fish[ii] = new element(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            }
            // if (ii < Constants.CHUNK_SIZE) {
            //     fish[ii] = new Bedrock(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            // } 
            // if(ii == 0 ||ii == 2 || ii == 3 || ii == 0 || ii == 4 || ii == 6 || ii == 7) { //(int)Mathf.Pow(Constants.CHUNK_SIZE, 2) /2

            // if(ii == 0) { //(int)Mathf.Pow(Constants.CHUNK_SIZE, 2) /2
            //     fish[ii] = new Sand(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE), new Vector2(3,3));
            // }else {
            //     fish[ii] = new element(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            // }



            // Vector2Int curpoint = chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE);

            // if (vlist.Contains(curpoint)){
            //     fish[ii] = new Sand(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));

            // }

        }


        // if (Mathf.Round(Random.Range(0, 1)) > 0.5) {
        //         fish[ii] = basictile
        //     }
 
        World.world_dict.Add(chunkpos, fish);
        // // // // // // // // // // // // // // // World.chunkHitbox_dict.Add(chunkpos, Chunks.GetChunkMesh(chunkpos));
        // // // Debug.LogError(chunkpos);

    }

    void Update() {
        // // // // Chunks.drawChunk(Vector2Int.zero, colorTileMap);
        // Vector2Int curpos;
        // for (int i = minx; i < maxx; i++) {
        //     for (int j = miny; j < maxy; j++) {
        //         curpos = new Vector2Int(i * Constants.CHUNK_SIZE, j* Constants.CHUNK_SIZE);
        //         // // // Chunks.drawChunk(curpos, colorTileMap); //set's tile color // NOTE Moved to swap thing
        //         UpdateChunk(curpos); //will be chunks later. 
        //     }
        // }
        float starttime = Time.realtimeSinceStartup;
        int result = 0;
        List<Vector2Int> keyList = new List<Vector2Int>(World.chunkstate_dict.Keys);
        foreach (Vector2Int key in keyList) {
            if (World.chunkstate_dict[key] == 1 || World.chunkstate_dict[key] == 2) {
                drawChunkBox(key, new Color(1, 0, 0, 0.3f));

                // World.chunkstate_dict[key] = 

                result = UpdateChunk(key); //will be chunks later. 
                // 0 means no changes, 1 means changes
                if (result == 0) { 
                    if (World.chunkstate_dict[key] == 1) {
                        result = 2; 
                    }
                }
                if (result != -1) {
                    World.chunkstate_dict[key] = result;
                }
                // list_o_collider_points.AddRange(Chunks.GetChunkMesh(Chunks.GetChunkPos(key))); 

                if (World.chunkHitbox_dict.ContainsKey(key)) {
                    World.chunkHitbox_dict[key] = Chunks.GetChunkMesh(key);
                } else {
                    World.chunkHitbox_dict.Add(key, Chunks.GetChunkMesh(key));
                }

            }
        }
        // while (i < 98) {
        //     thispolygon.SetPath(i, new Vector2[0]);
        // }
        Debug.Log("First"+(Time.realtimeSinceStartup - starttime)); 
        ExecuteSwaps();
        Debug.Log("Execute swaps"+(Time.realtimeSinceStartup - starttime)); 

        // int ii = 0;
        list_o_collider_points.Clear();

        Vector2Int curpos;
        for (int i = minx; i < maxx; i++) {
            for (int j = miny; j < maxy; j++) {
                curpos = Chunks.GetChunkPos(new Vector2Int(i * Constants.CHUNK_SIZE, j* Constants.CHUNK_SIZE));
                if (World.chunkHitbox_dict.ContainsKey(curpos)) {
                    list_o_collider_points.AddRange(World.chunkHitbox_dict[curpos]);
                }
            }
        }
        Debug.Log("Prepare collider"+(Time.realtimeSinceStartup - starttime)); 


        Tilepoly.CreateLevelCollider(list_o_collider_points, thispolygon);
        Debug.Log("CreateCollider"+(Time.realtimeSinceStartup - starttime)); 

        

        // Chunks.drawChunk(new Vector2Int(0, -Constants.CHUNK_SIZE), colorTileMap);
    }

    int UpdateChunk(Vector2Int cpos) {
        Vector3Int curcandidate;
        int curreturn = 0; //no changes
        if (!World.world_dict.ContainsKey(cpos)) {
            return -1;
        }
        for(int ii =0;ii < Mathf.Pow(Constants.CHUNK_SIZE, 2); ii++) {
            curcandidate = World.world_dict[cpos][ii].Step();

            //  World.world_dict[cpos][ii].position   //original
            //  curcandidate  //destination
            if (curcandidate.z == 0) {
                if (World.execution_dict.ContainsKey((Vector2Int)curcandidate)){
                    if (Random.Range(0f, 1f) > 0.5f) {
                        World.execution_dict[(Vector2Int) curcandidate] = World.world_dict[cpos][ii].position;
                    }
                    // Debug.Break();
                } else {
                    World.execution_dict.Add((Vector2Int) curcandidate, World.world_dict[cpos][ii].position);
                }
                    // Debug.LogError("Hmm" + World.world_dict[cpos][ii].position + " : " + curcandidate);
                curreturn = 1; //changes
            }
        }
        return curreturn; 
    }
    void ExecuteSwaps() {
        List<Vector2Int> keyList = new List<Vector2Int>(World.execution_dict.Keys);
        foreach (Vector2Int fish in keyList) { //fish is destination, value is origin.
            Chunks.Swap(fish, World.execution_dict[fish], colorTileMap);
            // World.chunkstate_dict[key] = 1;
            
            // Chunks.Edge curedge = 
            Vector2Int key = World.execution_dict[fish]; //should be called value

            Chunks.Edge edge1, edge2;
            (edge1, edge2) = Chunks.EdgeType(key);
            World.chunkstate_dict[Chunks.GetChunkPos(fish)] = 1;
            if (Chunks.GetCell(fish+ Vector2Int.left).IsFreeFalling <1) {
                Chunks.TryWakeCell(fish + Vector2Int.left);
            }
            if (Chunks.GetCell(fish+ Vector2Int.right).IsFreeFalling <1) {
                Chunks.TryWakeCell(fish + Vector2Int.right);
            }
            if (Chunks.GetCell(fish+ Vector2Int.down).IsFreeFalling <1) {
                Chunks.TryWakeCell(fish + Vector2Int.down);
            }

            if (Chunks.GetCell(fish+ Vector2Int.left +Vector2Int.up).IsFreeFalling <1) {
                Chunks.TryWakeCell(fish + Vector2Int.left +Vector2Int.up);
            }
            if (Chunks.GetCell(fish+ Vector2Int.right +Vector2Int.up).IsFreeFalling <1) {
                Chunks.TryWakeCell(fish + Vector2Int.right +Vector2Int.up);
            }




            if (edge1 == Chunks.Edge.up) {
                if (World.chunkstate_dict.ContainsKey(Chunks.GetChunkPos(key)+ new Vector2Int(0,Constants.CHUNK_SIZE))) { //up
                    // drawChunkBox(Chunks.GetChunkPos(key) + new Vector2Int(0,Constants.CHUNK_SIZE), new Color(0, 1, 0, 1));
                    World.chunkstate_dict[Chunks.GetChunkPos(key) + new Vector2Int(0,Constants.CHUNK_SIZE)] = 1;
                }
            } else if (edge1 == Chunks.Edge.down) {
                if (World.chunkstate_dict.ContainsKey(Chunks.GetChunkPos(key) - new Vector2Int(0,Constants.CHUNK_SIZE))) {
                    World.chunkstate_dict[Chunks.GetChunkPos(key) - new Vector2Int(0,Constants.CHUNK_SIZE)] = 1;
                }
            }

            if (edge2 == Chunks.Edge.left) {
                if (World.chunkstate_dict.ContainsKey(Chunks.GetChunkPos(key) - new Vector2Int(Constants.CHUNK_SIZE, 0))) {
                    World.chunkstate_dict[Chunks.GetChunkPos(key) - new Vector2Int(Constants.CHUNK_SIZE, 0)] = 1;
                }
            } else if (edge2 == Chunks.Edge.right) {
                if (World.chunkstate_dict.ContainsKey(Chunks.GetChunkPos(key) + new Vector2Int(Constants.CHUNK_SIZE, 0))) {
                    World.chunkstate_dict[Chunks.GetChunkPos(key) + new Vector2Int(Constants.CHUNK_SIZE, 0)] = 1;
                }
            }
            
        }
        World.execution_dict.Clear();
        // Debug.Log("Clear");
    }

    void drawChunkBox(Vector2Int pos1, Color color) {
        // Color color = new Color(0.7f, 0.1f, 0.1f);
        // Color color = new Color(1, 0, 0, 1);
        Vector3 pos =((Vector3) (Vector3Int) pos1) * Constants.PIXEL_SCALE;
        Vector3 pos2 = new Vector3(pos.x + Constants.CHUNK_SIZE * Constants.PIXEL_SCALE, pos.y +Constants.CHUNK_SIZE * Constants.PIXEL_SCALE, 0);


        Debug.DrawLine(new Vector3(pos.x, pos.y, 0),new Vector3(pos2.x, pos.y, 0), color, Constants.PERIOD); 
        Debug.DrawLine(new Vector3(pos2.x, pos.y, 0),new Vector3(pos2.x, pos2.y, 0), color, Constants.PERIOD); 
        Debug.DrawLine(new Vector3(pos2.x, pos2.y, 0),new Vector3(pos.x, pos2.y, 0), color, Constants.PERIOD); 
        Debug.DrawLine(new Vector3(pos.x, pos2.y, 0),new Vector3(pos.x, pos.y, 0), color, Constants.PERIOD); 
        // Gizmos.color = new Color(1, 0, 0, 0.5f);
        // Gizmos.DrawCube((Vector3)(Vector3Int) pos, new Vector3(Constants.CHUNK_SIZE, Constants.CHUNK_SIZE, 1));
    }

    public void MouseEvent(Vector3 pos1) {
        int radius = (int) pos1.z; 
        Vector2Int pos = new Vector2Int((int)(pos1.x *8) ,(int) (pos1.y *8));
        Vector2Int curpos = Vector2Int.zero; 
        for (int j = (int) pos.y - radius; j < pos.y + radius; j++) {
            for (int i = (int) pos.x - radius; i < pos.x + radius; i++) {
                curpos.x = i;
                curpos.y = j; 
                if (isInCircle(curpos, new  Vector2Int((int) pos.x, (int) pos.y), radius)) {
                    AddCell(curpos);
                }
            }
        }

    }

    public void AddCell(Vector2Int pos) {
        // Debug.Log(pos);
        World.chunkstate_dict[Chunks.GetChunkPos(pos)] = 1;
        Chunks.AddCell(new Sand(pos), colorTileMap); 
    }


    bool isInCircle(Vector2Int pos, Vector2Int center, int radius) { 
        return ((pos - center).magnitude <= radius);
        // if (pos.x > center.x + radius || pos.x < center.x - radius || 
        // pos.y > center.y + radius || pos.y < center.y - radius) {
        //     return false; 
        // } else {
        //     pos.x = pos.x > center.x? pos.x : 2*center.x - pos.x;
        //     pos.y = pos.y > center.y? pos.y : 2*center.y - pos.y;
        //     float y = Mathf.Sqrt(radius*radius - Mathf.Pow(pos.x-center.x,2))+center.y; 
        //     return (pos.y <= y);
        // }
    }


}
