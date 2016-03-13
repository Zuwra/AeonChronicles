using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class RTSObject : MonoBehaviour {

	//The base class for all playable objects
	
	public string Name
	{
		get;
		private set;
	}
	
	public int ID
	{
		get;
		private set;
	}
	
	public int UniqueID
	{
		get;
		private set;
	}
	
	public int TeamIdentifier
	{
		get;
		private set;
	}
	
	private float m_Health;
	private float m_MaxHealth;
	
	public abstract void SetSelected();
	public abstract void ToggleSelected();
	public abstract void SetDeselected();
	public abstract void AssignToGroup(int groupNumber);
	public abstract void RemoveFromGroup();
	public abstract void ChangeTeams(int team);


	public List<Ability> abilityList;
	[Tooltip("Which units will appear on the first,second,third page of the Command Card when multiple are selected, Lower numbers go to the front")]
	public int AbilityPriority; //Determines which abilities display on first/second/third rows according to a  grid system q-r,a-f,z-v  any more than three units, it goes to another page
	[Tooltip("Make sure you don't run out of rows if you have more than 4 abilities. Zero indexed")]
	public int AbilityStartingRow;


	public abstract bool UseAbility (int n);


	public abstract bool UseTargetAbility(GameObject obj, Vector3 loc, int n);

	public abstract void autoCast (int n);

	public float GetHealthRatio()
	{
		return m_Health/m_MaxHealth;
	}
	
	protected void Awake()
	{
		UniqueID = ManagerResolver.Resolve<IManager>().GetUniqueID();
	}
	
	protected void AssignDetails(Item item)
	{
		Name = item.Name;
		ID = item.ID;
		TeamIdentifier = item.TeamIdentifier;
		m_MaxHealth = item.Health;
		m_Health = m_MaxHealth;
	}
	
	public void TakeDamage(float damage)
	{
		m_Health -= damage;
	}
}
