using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Buff : MonoBehaviour {

	public Sprite HelpIcon;
	[TextArea(3,10)]
	public string toolDescription;
	public bool stacks;

	private UnitManager myManager;
	public GameObject source;

	void Awake()
	{
		myManager = GetComponent<UnitManager> ();
	}

	public void applyBuff()
	{
		myManager.myStats.addBuff (this, stacks);



	}

	public void applyDebuff()
	{
		myManager.myStats.addDebuff (this, true);
	}

	public void removeDebuff()
	{
		myManager.myStats.removeDebuff(this, true);
	}



	public void removeBuff()
	{
		myManager.myStats.removeBuff(this);

	}
}
