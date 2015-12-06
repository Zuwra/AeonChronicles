using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WaterController : MonoBehaviour {
	/*


	private bool paused;
	public float speed = 1;
	public bool High; // false = low, true = high;
	public List<GameObject> list;
	public float raiseHeight;
	private InteractableComponent interact;
	public GameObject handle;
	private float nearTop;
	public float MaxNumberUsed;
	public string CantUseText;
	private float timesUsed = 0;


	public GameObject activate;
	private bool thingActivated = false;


	public float cameraDelay;
	private float timer;


	
	public GameObject SplashEffect;
	public List<GameObject> boilers;
	private List<GameObject> newBoilers;


	private Dictionary<GameObject, float[]> map;

	// Use this for initialization
	void Start () {
		newBoilers = new List<GameObject> ();
		interact = GetComponent<InteractableComponent> ();
		interact.OnNotify +=handleOnNotify;
		interact.OnInteract += HandleOnInteract;
		map = new Dictionary<GameObject,float[] > ();
		High = !High;
		foreach (GameObject obj in  list) {
		if(obj.GetComponentInChildren<FloatingPlatform>() != null)
			{
				obj.GetComponentInChildren<FloatingPlatform>().controls = this;
			}
		}

	}
	

	
	public void handleOnNotify(InteractableNotifyEventData data){
		GameUI.DisplayInstructionTextArea("[J] to Pull Lever", 3f);
	}
	
	public void waterALternate(bool oneway)
	{

		WaterChunkBehavior chunk = list [0].GetComponentInChildren<WaterChunkBehavior> ();


		if ((chunk.originalPlace == true && oneway) || !oneway) {
						HandleOnInteract (null);
				}


		}

	public void HandleOnInteract (InteractableInteractEventData data)
	{
		if (gameObject.GetComponent<SoundMachine> () != null) {
						this.gameObject.GetComponent<SoundMachine> ().playSound ();
				}
				WaterChunkBehavior chunk = list [0].GetComponentInChildren<WaterChunkBehavior> ();

				if (!chunk.isMoving) {
						chunk.originalPlace = ! chunk.originalPlace;
						chunk.isMoving = true;

						
								foreach (GameObject stuff in list) {
				
										foreach (FloatingPlatform obj in stuff.GetComponentsInChildren<FloatingPlatform>()) {
												obj.controls = this.gameObject.GetComponent<WaterController> ();
										}
								}






								if (!paused) {
										if (this.gameObject.GetComponent<SoundMachine> ()) {

												this.gameObject.GetComponent<SoundMachine> ().playSound ();
										}
		
										if ((MaxNumberUsed > timesUsed) || MaxNumberUsed == 0) {
												timesUsed++;

												if (activate != null && !thingActivated) {
														thingActivated = true;

														activate.GetComponent<CutSceneAdvanced> ().activate ();
												}


					foreach(GameObject obj in newBoilers)
					{
						
						Destroy(obj);
					
						
					}

					newBoilers.Clear();


					foreach(GameObject obj in boilers)
						{
						
							GameObject boilNewInstance = (GameObject)Instantiate (SplashEffect,obj.transform.position,  Quaternion.identity);
							boilNewInstance.transform.LookAt(new Vector3 (0,1000,0));
							newBoilers.Add(boilNewInstance);

						}




						





												High = !High;



												foreach (GameObject obj in  list) {
														if (obj != null) {

																Vector3 top;
																Vector3 bottom;
				
																Vector3 temp = obj.transform.position;
																if (High) {
																		top = obj.transform.position;
					
																		temp.y -= (raiseHeight);
																		bottom = temp;
																} else {
																		bottom = obj.transform.position;
					
																		temp.y += (raiseHeight);
																		top = temp;
																}
																float[] limits = new float[2];
				
																limits [0] = top.y;
																limits [1] = bottom.y;
							if(map != null && obj != null)
							{map.Remove (obj);
								map.Add (obj, limits);}
				
														}
												}
										} else {
												interact.disablenotification ();
												GameUI.DisplayInstructionTextArea (CantUseText, 3f);
										}
		
				
										pauseTransports ();


								}
						
				}
		}


	public void pauseTransports()
	{
		paused = true;
	
	}



	// Update is called once per frame
	void Update () {

		if (paused) {
						timer += Time.deltaTime;
						if (handle != null) {
								if (!High) {
										handle.transform.Rotate (new Vector3 (.25f, 0, 0));
								} else {
										handle.transform.Rotate (new Vector3 (-.25f, 0, 0));
								}

						}


						if (timer > cameraDelay) {
			
			
								if (map != null) {
										foreach (KeyValuePair<GameObject, float[]> pair in map) {
												float[] temp = pair.Value;
												Vector3 bobbleVector = pair.Key.transform.position;

												if (!High && bobbleVector.y < temp [0]) {

														bobbleVector.y += speed * 0.007f * (temp [0] - bobbleVector.y) + .008f;
														pair.Key.transform.position = bobbleVector;
												} else if (High && bobbleVector.y > temp [1]) {	
														bobbleVector.y -= speed * 0.007f * (bobbleVector.y - temp [1]) + .008f;
														pair.Key.transform.position = bobbleVector;
												} else {

							foreach(GameObject obj in newBoilers)
							{

								obj.GetComponent<ParticleSystem>().emissionRate = 0;

							}


														paused = false;
														timer = 0;
														WaterChunkBehavior chunk = list [0].GetComponentInChildren<WaterChunkBehavior> ();

														chunk.isMoving = false;

						
												}
						
			
			
										}
								}

		
						}
				} 
		

		


	
	
	}*/
}