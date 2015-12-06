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
		EditorGUILayout.LabelField ("Show Bridge Tiles");
		EditorGUILayout.LabelField ("Show Tunnel Tiles");
		
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
		
		Grid.ShowGrid = EditorGUILayout.Toggle(Grid.ShowGrid);
		Grid.ShowOpenTiles = EditorGUILayout.Toggle(Grid.ShowOpenTiles);
		Grid.ShowClosedTiles = EditorGUILayout.Toggle(Grid.ShowClosedTiles);
		Grid.ShowBridgeTiles = EditorGUILayout.Toggle(Grid.ShowBridgeTiles);
		Grid.ShowTunnelTiles = EditorGUILayout.Toggle(Grid.ShowTunnelTiles);
		
		Grid.TileSize = EditorGUILayout.FloatField (Grid.TileSize);
		Grid.Width = EditorGUILayout.IntField(Grid.Width);
		Grid.Length = EditorGUILayout.IntField (Grid.Length);
		Grid.WidthOffset = EditorGUILayout.FloatField (Grid.WidthOffset);
		Grid.LengthOffset = EditorGUILayout.FloatField (Grid.LengthOffset);
		Grid.MaxSteepness = EditorGUILayout.FloatField (Grid.MaxSteepness);
		Grid.BlockIndent = EditorGUILayout.IntField (Grid.BlockIndent);
		Grid.PassableHeight = EditorGUILayout.FloatField (Grid.PassableHeight);
		
		
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndHorizontal ();
		
		if(GUILayout.Button("Evaluate Terrain"))
		{
			Grid.Initialise ();
		}
	}
}
