using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

	// Update is called once per frame
	void Update ()
	{
	    if (!GameManager.Instance.IsGameEnded)
	    {
	        var newPos = Target.position + Offset;
	        //newPos.x = transform.position.x;


	        transform.position = newPos;
	    }
	}
}
