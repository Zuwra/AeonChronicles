using UnityEngine;
using UnityEditor;
using System.Collections;

[CanEditMultipleObjects]
[CustomEditor(typeof(MiniMapController))]
public class MiniMapControllerEditor : Editor {

	Vector2 temp;
	
	public override void OnInspectorGUI() 
	{
    	if(GUILayout.Button("Center Camera"))
		{
			Camera miniMapCamera = GameObject.FindGameObjectWithTag ("mmCamera").GetComponent<Camera>();
			temp = GetMainGameViewSize ();
			
			//Place Camera in dead center
			miniMapCamera.transform.position  = new Vector3(Terrain.activeTerrain.terrainData.size.x/2, 90, Terrain.activeTerrain.terrainData.size.z/2);
			
			//Properly configure camera viewport so it's a square and it's in the correct place regardless of resolution
			//Always want the map to appear 3/4 up the screen, with a height of 1/4.5
			float aspectRatio = temp.x/temp.y;
		
			float viewPortY = 3.0f/4.0f;
			float viewPortHeight = 1.0f/4.5f;
		
			//Figure width values based on height values
			float viewPortWidth = 1.0f/(4.5f*aspectRatio);
			float viewPortX = 1-(0.25f/aspectRatio);
		
			//Assign camera viewport
			miniMapCamera.rect = new Rect(viewPortX, viewPortY, viewPortWidth, viewPortHeight);
		}      
  	}
	
	public static Vector2 GetMainGameViewSize()
	{
    	System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
    	System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
    	System.Object Res = GetSizeOfMainGameView.Invoke(null,null);
    	return (Vector2)Res;
	}
}
