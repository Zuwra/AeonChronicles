using UnityEngine;
using System.Collections;

public class IResource : MonoBehaviour {

    public string type;
    public double amountRemaining;
    protected double regenRate = .1;
    public int uniqueID;
    public bool known = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void attemptHarvest(WorkerMain worker) {
        //todo -- add code to remove resources based on worker's gatherRate
    }

    public override bool Equals(object o)
    {
        if (o is IResource)
            return uniqueID == (o as IResource).uniqueID;
        return false;
    }


	public override int GetHashCode()
	{return this.GetHashCode ();}


}



