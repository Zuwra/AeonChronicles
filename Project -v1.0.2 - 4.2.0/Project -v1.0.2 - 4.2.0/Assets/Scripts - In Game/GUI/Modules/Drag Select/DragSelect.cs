using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class DragSelect : MonoBehaviour {
	
	private GUIStyle m_DragStyle = new GUIStyle();
	
	private Vector2 m_DragLocationStart;
	private Vector2 m_DragLocationEnd;
	
	private bool m_CheckDeselect = false;
	private bool m_Dragging = false;
	
	private IGUIManager m_GuiManager;
	private UIManager uiManager;
	//private ISelectedManager m_SelectedManager;


	// Use this for initialization
	void Start () 
	{
		m_DragStyle.normal.background = TextureGenerator.MakeTexture (0.8f, 0.8f, 0.8f, 0.3f);
		m_DragStyle.border.bottom = 1;
		m_DragStyle.border.top = 1;
		m_DragStyle.border.left = 1;
		m_DragStyle.border.right = 1;
		uiManager = GameObject.FindObjectOfType<UIManager> ();
		m_GuiManager = GameObject.FindObjectOfType<GUIManager> ();
		//m_SelectedManager = ManagerResolver.Resolve<ISelectedManager>();
		
		GameObject.FindObjectOfType<EventsManager>().MouseClick += LeftButtonPressed;
		
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (m_CheckDeselect) {
			if (uiManager.allowDrag()) {
				if (Mathf.Abs (Input.mousePosition.x - m_DragLocationStart.x) > 2 && Mathf.Abs (Input.mousePosition.y - m_DragLocationStart.y) > 2) {
					m_CheckDeselect = false;
					m_Dragging = true;
					m_GuiManager.Dragging = true;
					
		
				}
			}

		}
	}
	
	void OnGUI()
	{
		if (m_Dragging)
		{
			m_DragLocationEnd = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			DragBox (m_DragLocationStart, m_DragLocationEnd, m_DragStyle);
		}
	}
	
	public void LeftButtonPressed(object sender, MouseEventArgs e)
	{
		if (e.button == 0) {

				if (!e.buttonUp && e.X < Screen.width ) {
				if (!EventSystem.current.IsPointerOverGameObject ()) {
					m_DragLocationStart = new Vector2 (e.X, e.Y);
					m_CheckDeselect = true;
				}
				} else {
					m_CheckDeselect = false;
					m_Dragging = false;
					m_GuiManager.Dragging = false;
				}

		}
	}
	
	public void DragBox(Vector2 topLeft, Vector2 bottomRight, GUIStyle style)
	{
		float minX = Mathf.Max (topLeft.x, bottomRight.x);
		float maxX = Mathf.Min (topLeft.x, bottomRight.x);
		
		float minY = Mathf.Max (Screen.height-topLeft.y, Screen.height-bottomRight.y);
		float maxY = Mathf.Min (Screen.height-topLeft.y, Screen.height-bottomRight.y);
				
		Rect rect = new Rect(minX, minY, maxX-minX, maxY-minY);
		

		m_GuiManager.DragArea = new Rect(maxX, maxY, minX-maxX, minY-maxY);
		
		GUI.Box (rect, "", style);
	}
	

}