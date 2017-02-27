using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniMapMark : MonoBehaviour {

    public Transform[] MapMark;
    public Transform[] MiniMapMark;

    public GameObject MyCar;
    public GameObject MiniMapIcon;

    public Vector2 ox, oy, ox_2, oy_2;

    public float px, py;

    public Vector2 op;

    private void initCoord()
    {
        ox = new Vector2(MapMark[2].position.x - MapMark[0].position.x, MapMark[2].position.z - MapMark[0].position.z);
        oy = new Vector2(MapMark[1].position.x - MapMark[0].position.x, MapMark[1].position.z - MapMark[0].position.z);
        ox_2 = new Vector2(MiniMapMark[2].position.x - MiniMapMark[0].position.x, MiniMapMark[2].position.z - MiniMapMark[0].position.z);
        oy_2 = new Vector2(MiniMapMark[1].position.x - MiniMapMark[0].position.x, MiniMapMark[1].position.z - MiniMapMark[0].position.z);
    }

    // Use this for initialization
    private void OnDrawGizmos()
    {
        initCoord();
        op = new Vector2(MyCar.transform.position.x - MapMark[0].position.x, MyCar.transform.position.z - MapMark[0].position.z);
        px = Vector2.Dot(ox,op) / (ox.magnitude* ox.magnitude);
        py = Vector2.Dot(oy,op) / (oy.magnitude * oy.magnitude);

        Vector2 p_2 = px * ox_2 + py * oy_2;

        MiniMapIcon.transform.position = new Vector3(
            p_2.x + MiniMapMark[0].position.x,
            MiniMapIcon.transform.position.y,
            p_2.y + MiniMapMark[0].position.z
            );

    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        op = new Vector2(MyCar.transform.position.x - MapMark[0].position.x, MyCar.transform.position.z - MapMark[0].position.z);
        px = Vector2.Dot(ox, op) / (ox.magnitude * ox.magnitude);
        py = Vector2.Dot(oy, op) / (oy.magnitude * oy.magnitude);

        Vector2 p_2 = px * ox_2 + py * oy_2;

        MiniMapIcon.transform.position = new Vector3(
            p_2.x + MiniMapMark[0].position.x,
            MiniMapIcon.transform.position.y,
            p_2.y + MiniMapMark[0].position.z
            );
    }
}
