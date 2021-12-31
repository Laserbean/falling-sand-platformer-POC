using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// using System.Linq; //for the foreach loop thing

public class World : MonoBehaviour
{
    // Global constants
    // // // const int CHUNK_AREA = CHUNK_SIZE* CHUNK_SIZE;

    public Tilemap solidTilemap;
    public Tile basictile;
    // TileBase[] tileArray = new TileBase[Constants.CHUNK_SIZE* Constants.CHUNK_SIZE];

    // public static Dictionary<Vector2Int, bool[,]> doneChunks; 
    // public static Dictionary<Vector2Int, TileBase[]> world_dict;
    public static Dictionary<Vector2Int, element[]> world_dict;
    public static Dictionary<Vector2Int, Vector2Int> execution_dict;


    public const int minx = -5, maxx  =5;
    public const int miny = -5, maxy  =5;
    
    void Start()
    {
        World.world_dict = new Dictionary<Vector2Int, element[]>(); 
        World.execution_dict = new Dictionary<Vector2Int, Vector2Int>(); 

        // chunkgen(new Vector2Int(0, 0));
        Vector2Int curpos;
        for (int i = minx; i < maxx; i++) {
            for (int j = miny; j < maxy; j++) {
                curpos = new Vector2Int(i * Constants.CHUNK_SIZE, j* Constants.CHUNK_SIZE);
                chunkgen(curpos);
                Chunks.fillChunk(curpos, solidTilemap, basictile); //fills with basic tile
                Chunks.drawChunk(curpos, solidTilemap);

            }
        }
        // Chunks.drawChunk(Vector2Int.zero, solidTilemap);
        // for(int jj =0;jj < Constants.CHUNK_SIZE; jj++) {
        //     for(int ii =0;ii < Constants.CHUNK_SIZE; ii++) {
        //         Debug.Log("Hmmmm");
        //         solidTilemap.SetTile( new Vector3Int(ii, jj, 0),basictile);
        //     }
        // }
        // Vector3Int[] positions = new Vector3Int[Constants.CHUNK_SIZE * Constants.CHUNK_SIZE];

        // for(int ii =0;ii < Mathf.Pow(Constants.CHUNK_SIZE, 2); ii++) {
        //     positions[ii] = new Vector3Int(ii % Constants.CHUNK_SIZE, ii / Constants.CHUNK_SIZE, 0);
        // }
        // solidTilemap.SetTiles(positions, fish);
        World.print("done");
        float period = 1f/60f; //time update
        InvokeRepeating("MyUpdate", 0f, period);
 
    }

    //worldget
    void chunkgen(Vector2Int chunkpos)
    {
        element[] fish = new element[(int)Mathf.Pow(Constants.CHUNK_SIZE, 2)];
        for(int ii =0;ii < Mathf.Pow(Constants.CHUNK_SIZE, 2); ii++) {
            // // // cur index in chunk is [ii + ii * Constants.CHUNK_SIZE]
            if (Random.Range(0f, 1f) > 0.5f) {
                fish[ii] = new Sand(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            } else {
                fish[ii] = new element(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            }
            // if (ii < Constants.CHUNK_SIZE) {
            //     fish[ii] = new Bedrock(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            // } 
            // else if(ii == 63) { //(int)Mathf.Pow(Constants.CHUNK_SIZE, 2) /2
            //     fish[ii] = new Sand(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            // }else {
            //     fish[ii] = new element(chunkpos + new Vector2Int((int)ii % Constants.CHUNK_SIZE, (int)ii/Constants.CHUNK_SIZE));
            // }

        }

        // if (Mathf.Round(Random.Range(0, 1)) > 0.5) {
        //         fish[ii] = basictile
        //     }
 
        World.world_dict.Add(chunkpos, fish);
        Debug.LogError(chunkpos);

    }

    void MyUpdate() {
        // Chunks.drawChunk(Vector2Int.zero, solidTilemap);
        Vector2Int curpos;
        for (int i = minx; i < maxx; i++) {
            for (int j = miny; j < maxy; j++) {
                curpos = new Vector2Int(i * Constants.CHUNK_SIZE, j* Constants.CHUNK_SIZE);
                Chunks.drawChunk(curpos, solidTilemap);

                UpdateChunk(curpos); //will be chunks later. 
            }
        }
        ExecuteSwaps();

        // Chunks.drawChunk(new Vector2Int(0, -Constants.CHUNK_SIZE), solidTilemap);
    }

    void UpdateChunk(Vector2Int cpos) {
        Vector3Int curcandidate;
        for(int ii =0;ii < Mathf.Pow(Constants.CHUNK_SIZE, 2); ii++) {
            curcandidate = World.world_dict[cpos][ii].Step();
            if (curcandidate.z == 0) {
                try {
                    World.execution_dict.Add(World.world_dict[cpos][ii].position, (Vector2Int) curcandidate);
                }
                catch {
                    Debug.LogError("Hmm" + World.world_dict[cpos][ii].position + " : " + curcandidate);
                    World.execution_dict[World.world_dict[cpos][ii].position] = (Vector2Int) curcandidate;
                    // World.execution_dict.Add(World.world_dict[cpos][ii].position, (Vector2Int) curcandidate);

                    // Debug.Break();
                }
            }
        }
    }
    void ExecuteSwaps() {
        List<Vector2Int> keyList = new List<Vector2Int>(World.execution_dict.Keys);
        foreach (Vector2Int key in keyList) {
            Chunks.Swap(key, World.execution_dict[key]);
        }
        World.execution_dict.Clear();
        // Debug.Log("Clear");
    }


}
