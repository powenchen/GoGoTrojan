using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapMark : MonoBehaviour {
    public Transform MyCar;
    private bool carExist = false;

    // Use this for initialization
    private void OnDrawGizmos()
    {

        if (MyCar != null)
        {
            transform.position =
            new Vector3(MyCar.position.x, transform.position.y, MyCar.position.z);
            transform.rotation = Quaternion.Euler(
                new Vector3(
                    transform.rotation.eulerAngles.x,
                    MyCar.rotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z
                    )
            );
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (MyCar != null)
        {
            carExist = true;
            transform.position =
                new Vector3(MyCar.position.x, transform.position.y, MyCar.position.z);
            transform.rotation = Quaternion.Euler(
                new Vector3(
                    transform.rotation.eulerAngles.x,
                    MyCar.rotation.eulerAngles.y,
                    transform.rotation.eulerAngles.z
                    )
            );
        }
        else if (carExist)
        {
            //car exist once but gone now
            Destroy(gameObject);
        }
    }
}
