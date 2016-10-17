using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class Buff : MonoBehaviour {

	public Sprite HelpIcon;
	[TextArea(3,10)]
	public string toolDescription;
	public bool stacks;

	private UnitManager myManager;

	void Awake()
	{
		myManager = GetComponent<UnitManager> ();
	}

	public void applyBuff()
	{
		myManager.myStats.addBuff (this, stacks);



	}


	public void removeBuff()
	{
		myManager.myStats.removeBuff(this);
		Debug.Log ("Removing");

	}
}
