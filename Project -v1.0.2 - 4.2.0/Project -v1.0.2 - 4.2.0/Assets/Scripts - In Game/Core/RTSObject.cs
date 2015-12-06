using UnityEngine;
using System.Collections;

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
	public abstract void SetDeselected();
	public abstract void AssignToGroup(int groupNumber);
	public abstract void RemoveFromGroup();
	public abstract void ChangeTeams(int team);
	
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
