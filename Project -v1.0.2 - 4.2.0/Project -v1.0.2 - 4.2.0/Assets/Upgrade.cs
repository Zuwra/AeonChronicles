using UnityEngine;
using System.Collections;


public abstract class Upgrade : MonoBehaviour {

	public string Name;
	[TextArea(2,10)]
	public string Descripton;

	public Material iconPic;
	public AbstractCost myCost;

	//public GameObject UIButton;

	public  abstract void applyUpgrade (GameObject obj);




}
