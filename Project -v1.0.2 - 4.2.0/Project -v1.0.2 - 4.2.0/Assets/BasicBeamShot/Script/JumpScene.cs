using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class JumpScene : MonoBehaviour {

	public int scene_index = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void ChangeScene(){
		print("SceneChange:"+scene_index);
		SceneManager.LoadScene (scene_index);
		//Application.LoadLevel(scene_index);
	}
}
