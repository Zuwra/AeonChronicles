    using UnityEngine;
using System.Collections;


public class bobble : MonoBehaviour {

   
        public float amplitude;          //Set in Inspector 
        public float speed;                  //Set in Inspector 
     

	float rand;

	void Start()
	{
		rand = Random.value;
	}
        // Update is called once per frame
        void Update()
        {
          
 
		transform.position = transform.position + Vector3.up *amplitude * Mathf.Sin(speed * Time.time + rand) ;
            
        }






    }
