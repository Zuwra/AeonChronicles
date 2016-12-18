    using UnityEngine;
using System.Collections;


public class bobble : MonoBehaviour {

   
        public float amplitude;          //Set in Inspector 
        public float speed;                  //Set in Inspector 


	Vector3 StartPosition;

	float rand;

	void Start()
	{
		StartPosition = transform.localPosition;
		rand = Random.value;
	}
        // Update is called once per frame
    void Update()
        {
		transform.localPosition = StartPosition + ( Vector3.up *amplitude * Mathf.Sin(speed * Time.time + rand))  *50;
            
        }






    }
