using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class ItemScript : MonoBehaviour
{
    public GameObject particle;
    public Camera cam;
    Transform trans;

    public Tile thistile;
    public Tilemap tilemap;
    GameObject thisprojectile;
    
    void Start()
    {
        trans = transform;
        // cam = GetComponent<Camera>();
    }

    public GameEvent spwn; 

    void Update()
    {
        cam.WorldToScreenPoint(trans.position);
        Vector3 screenPos = cam.WorldToScreenPoint(trans.position);

        Debug.DrawLine(Input.mousePosition, Input.mousePosition + new Vector3(4, 4, 0), Color.red);
        string s = string.Format("{0}: {1}, {2}", "MousePos", Input.mousePosition.x.ToString(), Input.mousePosition.y.ToString());
        string s2= string.Format("{0}: {1}, {2}", "Playerpos", screenPos.x.ToString(), screenPos.y.ToString());

        screenPos.z = 0;

        Vector3 mdir = Input.mousePosition - screenPos;
        // Debug.Log(string.Format("{0}\n{1}", s, s2)); 
        // Debug.Log(Vector3.Normalize(mdir));


        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("projectile fired");
            //for 3d:
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // if (Physics.Raycast(ray))

            // float angle = 0.0f;
            // Vector3 axis = Vector3.zero;
            // transform.rotation.ToAngleAxis(out angle, out axis);
            Vector2 sposVect = cam.ScreenToWorldPoint(Input.mousePosition);
            
            // // add cell here
            Vector3 posvec = sposVect;
            posvec.z = 5; 
            // World.AddCell(new Vector2Int((int)sposVect.x, (int)sposVect.y));
            spwn.Raise2Vector3(posvec);

            // thisprojectile = (Instantiate(particle, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1), Input.mousePosition - screenPos)));
            // thisprojectile.GetComponent<ProjectileScript>().projectileHit += handleProjectileHit;
        }

    }

    // void handleProjectileHit(Vector3 pos) {
    //     thisprojectile.GetComponent<ProjectileScript>().projectileHit -= handleProjectileHit;
    //     Vector3Int posInt = new Vector3Int((int) pos.x, (int) pos.y, (int) pos.z);
    //     Debug.Log("posx and posy are supposed to be " + pos.x + " " + pos.y);
    //     // tilemap.SetTile(posInt, thistile);

    //     // int try1 = 0; 
    //     // while (Chungus.GetCellW(pos.x, pos.y).CellMatter != MatterType.nothing &&) {

    //     // }
    //     Chungus.AddCellWorld(posInt.x, posInt.y, new SandCell(new Vector2(posInt.x, posInt.y) ,Vector2.zero ));
    // }
    // private void OnDrawGizmos() {
    //     Gizmos.DrawSphere(Input.mousePosition, 1);
    // }
}