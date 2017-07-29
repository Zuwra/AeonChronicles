using UnityEngine;
using System.Collections;


public abstract class Upgrade : MonoBehaviour {

	//these 

	//[ToolTip("Only fill these in if this upgrade replaces another one")]
	public string Name;
	[TextArea(2,10)]
	public string Descripton;

	public Sprite iconPic;
	public AbstractCost myCost;
    public float buildTime;
	public bool Finished;
	//public GameObject UIButton;

	public  abstract void applyUpgrade (GameObject obj);

	public  abstract void unApplyUpgrade (GameObject obj);


	public void ApplySkin(GameObject obj)
	{
		SkinUnlocker unlocker = obj.GetComponent<SkinUnlocker> ();
		if (unlocker) {
			unlocker.unlockSkin (Name);
		}
	}


}
