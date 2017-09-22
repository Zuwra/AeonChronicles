
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour, ICamera {


	//Singleton
	public static MainCamera main;

	//Camera Variables
    Coroutine currentFlick;
	public float HeightAboveGround = 30.0f;
	public float AngleOffset = 20.0f;
	public float m_MaxFieldOfView = 150.0f;
	public float m_MinFieldOfView = 20.0f;

	public float ScrollSpeed = 8.0f;
	public float ScrollAcceleration = 30.0f;

	public float ZoomRate = 500.0f;


	private bool canWeScroll = true;

	public GameObject StartPoint;

	private Rect m_Boundries;

	private bool ScreenSteal;
	private Vector3 StealTarget;
	private Vector3 CutSceneStart;
	private float cutsceneTime;
	private Queue<float> deltaTimes = new Queue<float>();
	private float avgDeltaTime = 0.0f;


	float sumDeltaTimes = 0;

	Vector2 middleStartPos;
	//Vector3 camStartPos;
	bool middleMouseDown;
	//TESTING
	public Vector3 TopRightBorder;
	public Vector3 BottomLeftBorder;
	void Awake()
	{
		main = this;

		SetBoundries ( BottomLeftBorder.x,BottomLeftBorder.z, TopRightBorder.x,TopRightBorder.z);


		GetComponent<FogOfWar> ().mapOffset = new Vector2 (( m_Boundries.xMin - m_Boundries.xMax)/2 + m_Boundries.xMax,(   m_Boundries.yMin -m_Boundries.yMax)/2 + m_Boundries.yMax);
		GetComponent<FogOfWar> ().mapSize = (int)Mathf.Abs (m_Boundries.xMin - m_Boundries.xMax);
		GetComponent<FogOfWar> ().Initialize ();

	}

	// Use this for initialization
	void Start () 
	{	if (StartPoint == null) {
			StartPoint = GameObject.FindObjectOfType<sPoint> ().gameObject;

		}
		//Set up camera position
		if (StartPoint != null)
		{goToStart ();
			transform.position = new Vector3(StartPoint.transform.position.x, m_MinFieldOfView + 105, StartPoint.transform.position.z-AngleOffset);
		}
		AngleOffset = 45 -((transform.position.y - m_MinFieldOfView) / m_MaxFieldOfView) * 45;
		//Set up camera rotation
		transform.rotation = Quaternion.Euler (90-AngleOffset, 0, 0);


	
	}

	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("Time scale " + Time.timeScale);
		if (deltaTimes.Count < 5 && Time.deltaTime != 0.0f) {
			deltaTimes.Enqueue (Time.deltaTime);
			avgDeltaTime = Time.deltaTime;
			sumDeltaTimes += Time.deltaTime;//==========
		} else if (deltaTimes.Count == 5 && Time.deltaTime != 0.0f) {
			sumDeltaTimes -= deltaTimes.Dequeue ();//==========
			deltaTimes.Enqueue (Time.deltaTime);
			sumDeltaTimes += Time.deltaTime;
			//sumDeltaTimes = 0;
			//foreach (float f in deltaTimes) {
			//}
			float tempAvg = sumDeltaTimes / 5.0f;//average of last 5
			if (tempAvg > avgDeltaTime * 1.1f) {
				avgDeltaTime *= 1.1f;
			} else {
				avgDeltaTime = tempAvg;
			}
		} else if (Time.deltaTime == 0) {
		} else {
			avgDeltaTime = Time.deltaTime;
		}

		//avgDeltaTime = sumDeltaTimes / 5.0f;
		//Debug.Log ("maxDelta: " + maxDelta);
		//Debug.Log ("average: " + avgDeltaTime);



		if (ScreenSteal) {
			cutsceneTime += Time.deltaTime;
			Vector3 temploc = Vector3.Lerp (CutSceneStart, StealTarget, cutsceneTime / 1.5f);
			this.transform.position = temploc;


			CheckEdgeMovement ();
			if (this.transform.position != temploc || Vector3.Distance (this.transform.position, StealTarget) < 3) {
				ScreenSteal = false;
				canWeScroll = true;
			}

		} else if (Input.GetMouseButtonDown (2)) {
			CursorManager.main.MouseDragMode ();
			middleStartPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			//camStartPos = this.transform.position;
			middleMouseDown = true;
            if (currentFlick != null)
            {
                StopCoroutine(currentFlick);
            }
		} else if (Input.GetMouseButtonUp (2)) {
			if (CursorManager.main.getMode () == 6) {
				CursorManager.main.normalMode ();
			}
            Vector2 toCoroutine = new Vector2((middleStartPos.x - Input.mousePosition.x),(middleStartPos.y - Input.mousePosition.y));
            currentFlick = StartCoroutine(flickScroll(toCoroutine));
			middleMouseDown = false;
		} else if (middleMouseDown) {
			CursorManager.main.MouseDragMode ();
			if (Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width - 2 && Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height - 2) {


				transform.Translate ((middleStartPos.x - Input.mousePosition.x) * avgDeltaTime * transform.position.y / 15, 0, (middleStartPos.y - Input.mousePosition.y) * Time.deltaTime* transform.position.y /14, Space.World);
				middleStartPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

				CheckEdgeMovement ();
			}
		}

	}
    IEnumerator flickScroll(Vector2 movementIncrement)
    {
        Vector2 currentIncrement = movementIncrement;
        while (true)
        {
            yield return 0;

            transform.Translate ((currentIncrement.x) * avgDeltaTime * transform.position.y / 15, 0, (currentIncrement.y) * Time.deltaTime* transform.position.y /14, Space.World);
            //maybe subtract min height
            currentIncrement -= currentIncrement*avgDeltaTime;

			CheckEdgeMovement ();
            if (Mathf.Abs(currentIncrement.x) + Mathf.Abs(currentIncrement.y) < 5.0f)
            {
                
                break;
            }

        }
      //  Debug.Log("END WHILE");

    }

	public void goToStart()
	{
		if (StartPoint != null) {
			transform.position = new Vector3 (StartPoint.transform.position.x, transform.position.y, StartPoint.transform.position.z - AngleOffset);
			//Debug.Log ("Start Should be " + new Vector3 (StartPoint.transform.position.x, transform.position.y, StartPoint.transform.position.z - AngleOffset));
		}
	}
	public void generalMove(Vector3 input){
		transform.position = new Vector3 (input.x, this.gameObject.transform.position.y, input.z - AngleOffset/45 * transform.position.y);
		CheckEdgeMovement ();
	}


	public void setCutScene(Vector3 vec, float cameraHeight)
	{canWeScroll = false;
		StealTarget = new Vector3 (vec.x, vec.y + cameraHeight, vec.z - cameraHeight);

		ScreenSteal = true;
		CutSceneStart = this.gameObject.transform.position;

	}


	public void Pan(object sender, ScreenEdgeEventArgs e)
	{
		if (canWeScroll)
		{
			if (currentFlick != null)
			{
				StopCoroutine(currentFlick);
				currentFlick = null;
			}


			float totalSpeed = e.duration*ScrollAcceleration *1.4f;
			float targetSpeed = totalSpeed < ScrollSpeed ? totalSpeed : ScrollSpeed;

			transform.Translate (e.x*avgDeltaTime*targetSpeed, 0, e.y*avgDeltaTime*targetSpeed, Space.World);

			//Check if we have scrolled past edge
			if (transform.position.x < m_Boundries.xMin)
			{
				transform.position = new Vector3(m_Boundries.xMin, transform.position.y, transform.position.z);
			}
			else if (transform.position.x > m_Boundries.xMax)
			{
				transform.position = new Vector3(m_Boundries.xMax, transform.position.y, transform.position.z);
			}


			if (transform.position.z < m_Boundries.yMin -10)
			{
				//transform.position = new Vector3(transform.position.x, transform.position.y, m_Boundries.yMin);
			}
			else if (transform.position.z > m_Boundries.yMax +60)
			{
				transform.position = new Vector3(transform.position.x, transform.position.y, m_Boundries.yMax+60);
			}

			CheckEdgeMovement ();
		}
	}

	public void Move(Vector3 worldPos)
	{
		transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z);
		//CheckEdgeMovement ();
	}

	private void CheckEdgeMovement()
	{
		Ray r1 = Camera.main.ScreenPointToRay (new Vector3(Screen.width/2,Screen.height-1,0)); // TOP

		Ray r3 = Camera.main.ScreenPointToRay (new Vector3(0,50,0)); //Bottom Left
		Ray r4 = Camera.main.ScreenPointToRay (new Vector3(Screen.width-1,50,0)); //Bottom Right

		float left, right, top, bottom;

		RaycastHit h1;



		Physics.Raycast (r1, out h1, Mathf.Infinity, 1<< 16);		
		top = h1.point.z;

		Physics.Raycast (r4, out h1, Mathf.Infinity, 1<< 16);
		right = h1.point.x;

		Physics.Raycast (r3, out h1, Mathf.Infinity, 1<< 16);
		bottom = h1.point.z;
		left = h1.point.x;

		if (left < m_Boundries.xMin)
		{
			Camera.main.transform.Translate (new Vector3(m_Boundries.xMin-left,0,0), Space.World);
		}
		else if (right > m_Boundries.xMax)
		{
			//Debug.Log ("hit right side");
			Camera.main.transform.Translate (new Vector3(m_Boundries.xMax-right,0,0), Space.World);
		}

		if (bottom < m_Boundries.yMin -10)
		{

			Camera.main.transform.Translate (new Vector3(0,0,(m_Boundries.yMin-10)-bottom), Space.World);
		}
		else if (top > m_Boundries.yMax +60)
		{
			Camera.main.transform.Translate (new Vector3(0,0,m_Boundries.yMax + 60-top), Space.World);
		}
	}

	public void Zoom(object sender, ScrollWheelEventArgs e)
	{

		if ((transform.position.y >= m_MaxFieldOfView && e.ScrollValue < 0) ||( transform.position.y <= m_MinFieldOfView &&e.ScrollValue > 0)) {
			return;}


		Ray rayb = Camera.main.ScreenPointToRay (new Vector2(.5f*Screen.width, .5f*Screen.height));
		RaycastHit hitb;
		Physics.Raycast (rayb, out hitb, Mathf.Infinity, 1 << 16);

		Ray rayc;
		RaycastHit hitc = new RaycastHit();
		if (Input.GetKey (KeyCode.LeftShift) && e.ScrollValue > 0) {
	
			rayc = Camera.main.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (rayc, out hitc, Mathf.Infinity, 1 << 16);

		}


		transform.Translate (Vector3.down * e.ScrollValue * 28, Space.World);
		transform.LookAt (hitb.point);

		if (Input.GetKey (KeyCode.LeftShift) && e.ScrollValue > 0) {
			transform.Translate ((hitc.point - transform.position).normalized * 40f * e.ScrollValue, Space.World);
			Vector3 moveOver = (hitc.point - transform.position).normalized;
			moveOver.y = 0;
			transform.Translate (moveOver * 20f * e.ScrollValue, Space.World);
		}
		else{
			transform.Translate ((hitb.point - transform.position).normalized * 40f * e.ScrollValue, Space.World);
	

		}

		if (transform.position.y < m_MinFieldOfView) {
			transform.position = new Vector3 (transform.position.x, m_MinFieldOfView, transform.position.z);
			transform.rotation = Quaternion.Euler (45, 0, 0);
		}

		if (transform.position.y > m_MaxFieldOfView) {
			transform.position = new Vector3 (transform.position.x, m_MaxFieldOfView, transform.position.z);

		}

		if (transform.position.y < m_MaxFieldOfView) {
			CheckEdgeMovement ();
		}

		AngleOffset = 45 -((transform.position.y - m_MinFieldOfView) / m_MaxFieldOfView) * 45;
	}




	public void minimapMove(Vector2 input)
	{

		transform.position = new Vector3((input.x ) , this.gameObject.transform.position.y ,(input.y ));
		CheckEdgeMovement ();

	}

	public void DisableScrolling()
	{
		canWeScroll = false;
	}

	public void EnableScrolling()
	{
		canWeScroll = true;
	}

	public void SetBoundries (float minX, float minY, float maxX, float maxY)
	{
		m_Boundries = new Rect();
		m_Boundries.xMin = minX;
		m_Boundries.xMax = maxX;
		m_Boundries.yMin = minY;
		m_Boundries.yMax = maxY;


	}

	public Rect getBoundries()
	{
		return m_Boundries;
	}

	/// <summary>
	/// Shakes the camera.
	/// </summary>
	/// <param name="Duration">Duration.</param>
	/// <param name="Intensity">Intensity. 8 is a good number</param>
	/// <param name="amplitude">Amplitude. .1f is a good number</param>
	public void ShakeCamera(float Duration, float Intensity, float amplitude)
	{
		StartCoroutine(CameraShake(Duration, Intensity, amplitude));
	}

	IEnumerator CameraShake(float duration, float intensity, float amplitude)
	{

		float elapsed = 0.0f;
		Vector3 totalMovement = Vector3.zero;

		while (elapsed < duration) {

	

			float MiniShake = 0;
	
			Vector3 toMove = new Vector3(Random.value  - .5f, Random.value  - .5f,Random.value - .5f) * intensity;


			while(MiniShake < amplitude)
				{MiniShake += Time.deltaTime;
				transform.Translate (toMove * Time.deltaTime);
				totalMovement += toMove * Time.deltaTime;
				yield return null;
				}
			elapsed += MiniShake;

		}

		float ReturnTime = 0.0f;
		while (ReturnTime< .1f) {
			ReturnTime += Time.deltaTime;
			transform.Translate ((totalMovement * -1) * Time.deltaTime/ .1f);
			yield return null;
		}
	}

	void OnDrawGizmos()
	{	
		Gizmos.DrawCube (TopRightBorder, Vector3.one*10);
		Gizmos.DrawCube (BottomLeftBorder, Vector3.one*10);

	}

}





