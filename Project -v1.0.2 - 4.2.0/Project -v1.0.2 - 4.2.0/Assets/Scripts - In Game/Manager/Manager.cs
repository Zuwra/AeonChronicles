using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Manager : MonoBehaviour, IManager {
	
	//Singleton
	public static Manager main;
	
	//Member Variables-------------------------------------------------

	private List<Unit> m_Units = new List<Unit>();
	
	private List<Item> m_AvailableItems = new List<Item>();
	
	private IGUIManager m_GUIManager;
	
	private int m_UniqueIDCount = 0;
	
	private float m_Money = 4000;
	
	private float m_MoneyToAdd = 0;
	private float m_MoneyAddRate = 1000.0f;
	
	//Properties ----------------------------------------------------
	public int Money
	{
		get
		{
			return (int)m_Money;
		}
	}
	
	//Methods ----------------------------------------------------------
	void Awake ()
	{
		main = this;
		
		//Initialise everything
		Initialise();
	}

	// Use this for initialization
	void Start () 
	{
		m_GUIManager = ManagerResolver.Resolve<IGUIManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Slowly add money
		if (m_MoneyToAdd > 0)
		{
			float tempMoney = m_MoneyAddRate*Time.deltaTime;
			
			if (tempMoney > m_MoneyToAdd)
			{
				tempMoney = m_MoneyToAdd;
			}
			
			AddMoneyInstant (tempMoney);
			m_MoneyToAdd -= tempMoney;
		}
		
		if (Input.GetKeyDown ("a"))
		{
			AddMoney (1000);
		}
	}
	
	private void Initialise()
	{
		//Perform all initialisation here
	
		ItemProgressTextures.Initialise ();
		GUIStyles.Initialise ();
	}



	public void UnitAdded(Unit unit)
	{
		m_Units.Add (unit);
	}
	

	public int GetUniqueID ()
	{
		m_UniqueIDCount++;
		return m_UniqueIDCount - 1;
	}

	public void AddMoney (float money)
	{
		m_MoneyToAdd += money;
	}

	public void AddMoneyInstant (float money)
	{
		m_Money += money;
	}

	public void RemoveMoneyInstant (float money)
	{
		m_Money -= money;
	}
	
	public bool CostAcceptable(float cost)
	{
		return Money-cost >= 0;
	}
}
