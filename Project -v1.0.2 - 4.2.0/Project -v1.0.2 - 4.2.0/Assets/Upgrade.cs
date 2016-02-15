using UnityEngine;
using System.Collections;


public abstract class Upgrade : MonoBehaviour {

	//these 

	//[ToolTip("Only fill these in if this upgrade replaces another one")]
	public string Name;
	[TextArea(2,10)]
	public string Descripton;

	public Material iconPic;
	public AbstractCost myCost;
    public float buildTime;
	//public GameObject UIButton;

	public  abstract void applyUpgrade (GameObject obj);




}
