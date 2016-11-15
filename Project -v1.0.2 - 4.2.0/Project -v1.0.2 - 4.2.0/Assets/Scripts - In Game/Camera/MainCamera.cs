using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour, ICamera {
	

		//Singleton
		public static MainCamera main;

		//Camera Variables
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

		void Awake()
		{
			main = this;
		}

		// Use this for initialization
		void Start () 
		{	if (StartPoint == null) {
				StartPoint = GameObject.FindObjectOfType<sPoint> ().gameObject;
			}
			//Set up camera position
			if (StartPoint != null)
			{goToStart ();
				transform.position = new Vector3(StartPoint.transform.position.x, HeightAboveGround, StartPoint.transform.position.z-AngleOffset);
			}
			AngleOffset = 45 -((HeightAboveGround - m_MinFieldOfView) / m_MaxFieldOfView) * 45;
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
			} else if (Input.GetMouseButtonUp (2)) {
				if (CursorManager.main.getMode () == 6) {
					CursorManager.main.normalMode ();
				}
				middleMouseDown = false;
			} else if (middleMouseDown) {
				CursorManager.main.MouseDragMode ();
				if (Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width - 2 && Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height - 2) {


					transform.Translate ((middleStartPos.x - Input.mousePosition.x) * avgDeltaTime * HeightAboveGround / 15, 0, (middleStartPos.y - Input.mousePosition.y) * Time.deltaTime* HeightAboveGround /14, Space.World);
					middleStartPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
					CheckEdgeMovement ();
				}
			}

		}

		public void goToStart()
		{
			if (StartPoint != null) {
				transform.position = new Vector3 (StartPoint.transform.position.x, HeightAboveGround, StartPoint.transform.position.z - AngleOffset);
			}
		}
		public void generalMove(Vector3 input){
			transform.position = new Vector3 (input.x, this.gameObject.transform.position.y, input.z - AngleOffset/45 * HeightAboveGround);
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
				float totalSpeed = e.duration*ScrollAcceleration;
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

				if (transform.position.z < m_Boundries.yMin)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, m_Boundries.yMin);
				}
				else if (transform.position.z > m_Boundries.yMax)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y, m_Boundries.yMax);
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
			Ray r1 = Camera.main.ViewportPointToRay (new Vector3(0,1,0));
			//Ray r2 = Camera.main.ScreenPointToRay (new Vector3(Screen.width,Screen.height-1,0));
			//Ray r2 = Camera.main.ScreenPointToRay (new Vector3(Screen.width-1,Screen.height-1,0));
			Ray r3 = Camera.main.ViewportPointToRay (new Vector3(0,0,0));
			Ray r4 = Camera.main.ScreenPointToRay (new Vector3(Screen.width-1,0,0));

			float left, right, top, bottom;

			RaycastHit h1;

			Physics.Raycast (r1, out h1, Mathf.Infinity, 1<< 16);		
			left = h1.point.x;
			top = h1.point.z;

			Physics.Raycast (r4, out h1, Mathf.Infinity, 1<< 16);
			right = h1.point.x;

			Physics.Raycast (r3, out h1, Mathf.Infinity, 1<< 16);
			bottom = h1.point.z;

			if (left < m_Boundries.xMin)
			{
				Camera.main.transform.Translate (new Vector3(m_Boundries.xMin-left,0,0), Space.World);
			}
			else if (right > m_Boundries.xMax)
			{
				//Debug.Log ("hit right side");
				//Camera.main.transform.Translate (new Vector3(m_Boundries.xMax-right,0,0), Space.World);
			}

			if (bottom < m_Boundries.yMin)
			{
				Camera.main.transform.Translate (new Vector3(0,0,m_Boundries.yMin-bottom), Space.World);
			}
			else if (top > m_Boundries.yMax)
			{
				Camera.main.transform.Translate (new Vector3(0,0,m_Boundries.yMax-top), Space.World);
			}
		}

		public void Zoom(object sender, ScrollWheelEventArgs e)
		{float x = 0;
			float z = 0;
			//Debug.Log ("Zooming");
			if (HeightAboveGround > m_MaxFieldOfView || HeightAboveGround < m_MinFieldOfView) {
				return;}

			HeightAboveGround -= e.ScrollValue * ZoomRate * avgDeltaTime * 10;


			if (HeightAboveGround < m_MinFieldOfView) {
				HeightAboveGround = m_MinFieldOfView;
			} else if (HeightAboveGround > m_MaxFieldOfView) {
				HeightAboveGround = m_MaxFieldOfView;
			} else {
				if (e.ScrollValue > 0 && Input.GetKey (KeyCode.LeftShift)) {//if shift do this
					Debug.Log ("LEFT SHIFT DOWN");
					x = (Screen.width / 2 - Input.mousePosition.x) * .03f;
					z = -(Screen.height / 2 - Input.mousePosition.y) * .001f * AngleOffset;

				} else if (e.ScrollValue > 0) {
					x = (Screen.width / 2) * .03f;
					z = -(Screen.height / 2) * .001f * AngleOffset;
				}

			}


			transform.position = new Vector3 (this.gameObject.transform.position.x - x, HeightAboveGround, this.gameObject.transform.position.z + z);


			AngleOffset = 45 -((HeightAboveGround - m_MinFieldOfView) / m_MaxFieldOfView) * 45;



			//AngleOffset += e.ScrollValue * Time.deltaTime * 700;

			if (AngleOffset > 90) {
				AngleOffset = 90;
			} else if (AngleOffset < 0) {
				AngleOffset = 0;
			}
			transform.rotation = Quaternion.Euler (90 - AngleOffset, 0, 0);

			if (HeightAboveGround < m_MaxFieldOfView) {
				CheckEdgeMovement ();
			}

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
			m_Boundries.yMin = minY+1;
			m_Boundries.yMax = maxY;


		}

		public Rect getBoundries()
		{
			return m_Boundries;
		}


	}