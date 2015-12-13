using UnityEngine;
using System.Collections;

public class WorkerMain : MonoBehaviour {

    public const int WAITING = 0;
    public const int GATHERING = 1;
    public const int TRAVELING = 2;
    public const int RETURNING = 3;

    public double gatherRate;//how many resources it gathers PER FRAME
    public int capacity;
    public int workerState;

    private RaceManager manager;
    private int nextActionTime = 0;

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("GameRaceManager").GetComponent<RaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += 1;
            switch (workerState)
            {
                case WAITING:
                    break;
                case GATHERING:
                    //attempt to gather
                    //check to see if we've exhausted the resource
                        //if yes, get next resource target and change to TRAVELING and end
                    //check to see if we've filled our capacity
                        //if yes, switch state to RETURNING
                    break;
                case TRAVELING:
                    //check to see if we've arrived at the gatherlocation
                        //if yes, switch state to GATHERING
                    break;
                case RETURNING:
                    //check to see if we've reached a resourceDropoff
                    //if yes, unload and switch state to TRAVELING
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        //detect new resource sites
        IResource resource = otherObj.GetComponent<IResource>();
        if(resource)
        {
            manager.updateGatherLocations(resource);
        }
    }
}
