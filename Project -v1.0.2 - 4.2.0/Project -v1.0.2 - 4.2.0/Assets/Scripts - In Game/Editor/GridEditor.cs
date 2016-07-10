using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Grid))]
public class GridEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical();
		
		EditorGUILayout.LabelField ("Show Grid");
		EditorGUILayout.LabelField ("Show Open Tiles");
		EditorGUILayout.LabelField ("Show Closed Tiles");

		
		EditorGUILayout.LabelField ("Tile Size");
		EditorGUILayout.LabelField ("Grid Width");
		EditorGUILayout.LabelField ("Grid Length");
		EditorGUILayout.LabelField ("Width Offset");
		EditorGUILayout.LabelField ("Length Offset");
		EditorGUILayout.LabelField ("Max Steepness");
		EditorGUILayout.LabelField ("Block Indent");
		EditorGUILayout.LabelField ("Passable Height");
		
		EditorGUILayout.EndVertical();
		
		EditorGUILayout.BeginVertical();
		if (!Grid.main) {
			Grid.main.Initialise ();
		}

		Grid.main.ShowGrid = EditorGUILayout.Toggle(Grid.main.ShowGrid);
		Grid.main.ShowOpenTiles = EditorGUILayout.Toggle(Grid.main.ShowOpenTiles);
		Grid.main.ShowClosedTiles = EditorGUILayout.Toggle(Grid.main.ShowClosedTiles);
		//Grid.ShowBridgeTiles = EditorGUILayout.Toggle(Grid.ShowBridgeTiles);
		//Grid.ShowTunnelTiles = EditorGUILayout.Toggle(Grid.ShowTunnelTiles);
		
		Grid.main.TileSize = EditorGUILayout.FloatField (Grid.main.TileSize);
		Grid.main.Width = EditorGUILayout.IntField(Grid.main.Width);
		Grid.main.Length = EditorGUILayout.IntField (Grid.main.Length);
		Grid.main.WidthOffset = EditorGUILayout.FloatField (Grid.main.WidthOffset);
		Grid.main.LengthOffset = EditorGUILayout.FloatField (Grid.main.LengthOffset);
		Grid.main.MaxSteepness = EditorGUILayout.FloatField (Grid.main.MaxSteepness);
		Grid.main.BlockIndent = EditorGUILayout.IntField (Grid.main.BlockIndent);
		Grid.main.PassableHeight = EditorGUILayout.FloatField (Grid.main.PassableHeight);
		
		
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal ();
		
		if(GUILayout.Button("Evaluate Terrain"))
		{
			Grid.main.Initialise ();
		}
	}
}
