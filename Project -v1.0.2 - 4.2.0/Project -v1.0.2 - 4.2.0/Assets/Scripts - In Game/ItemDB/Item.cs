using UnityEngine;
using System.Collections;
using System;

public class Item {
	
	//Item Member Variables and Properties-----------------------------------
	public int ID;

	public string Name;
	public float Health;
	public float Armour;
	public float Damage = 0;
	
	public float Speed = 0;
	public float RotationSpeed = 0;
	public float Acceleration = 0;
	
	public GameObject Prefab;
	
	public int Cost;
	public float BuildTime;
	
	public int TypeIdentifier;
	public int TeamIdentifier;
	
	private Texture2D m_ItemImage;
	public Texture2D ItemImage
	{
		get
		{
			return m_ItemImage;
		}
		set
		{
			//Whenver the Item image is set, create the hover image
			m_ItemImage = value;
			CreateHoverImage ();
		}
	}
	
	public Texture2D ItemImageHover
	{
		get;
		private set;
	}
	
	public int SortOrder;
	public int[] RequiredBuildings;
	public GameObject Explosion;
	public Type ObjectType;
	
	//-------------------------------------------------------------
	
	//Item building variables------------------------------------------
	
	private bool m_Paused = false;
	private bool m_Building = false;
	private bool m_Finished = false;
	
	private float m_Timer = 0;
	private IManager m_Manager;
	private float m_CostPerSecond;
	private float m_Progress = 0;
	
	public bool Paused
	{
		get
		{
			return m_Paused;
		}
	}
	
	public bool IsBuilding
	{
		get
		{
			return m_Building;
		}
	}
	
	public bool IsFinished
	{
		get
		{
			return m_Finished;
		}
	}
	
	//-----------------------------------------------------------------
	
	//GUI Style variales ------------------------------------------
	
	private GUIStyle m_ButtonStyle;
	
	public GUIStyle ButtonStyle
	{
		get
		{
			return m_ButtonStyle;
		}
	}
	
	//------------------------------------------------------------------
	
	//Constructors ---------------------------------------------------
	public Item()
	{
		
	}
	
	public Item(Item item)
	{
		ID = item.ID;
		Name = item.Name;
		Health = item.Health;
		Armour = item.Armour;
		Damage = item.Damage;
		Speed = item.Speed;
		RotationSpeed = item.RotationSpeed;
		Prefab = item.Prefab;
		Cost = item.Cost;
		BuildTime = item.BuildTime;
		TypeIdentifier = item.TypeIdentifier;
		TeamIdentifier = item.TeamIdentifier;
		m_ItemImage = item.ItemImage;
		ItemImageHover = item.ItemImageHover;
		SortOrder = item.SortOrder;
		RequiredBuildings = item.RequiredBuildings;
		Explosion = item.Explosion;
		m_Manager = ManagerResolver.Resolve<IManager>();
		m_CostPerSecond = ((float)Cost)/BuildTime;
		m_ButtonStyle = item.ButtonStyle;
		ObjectType = item.ObjectType;
	}
	//--------------------------------------------------------------------------
	
	public void Initialise()
	{
		m_ButtonStyle = new GUIStyle();
		m_ButtonStyle.normal.background = m_ItemImage;
		m_ButtonStyle.hover.background = ItemImageHover;
	}
	
	//Methods------------------------------------------------------
	private void CreateHoverImage()
	{
		ItemImageHover = new Texture2D(m_ItemImage.width, m_ItemImage.height, TextureFormat.RGB24, false);
		
		for (int i=0; i<m_ItemImage.width; i++)
		{
			for (int j=0; j<m_ItemImage.height; j++)
			{
				Color pixelColor = ItemImage.GetPixel (i, j);
				pixelColor.r += 0.2f;
				pixelColor.g += 0.2f;
				pixelColor.b += 0.2f;
				ItemImageHover.SetPixel (i, j, pixelColor);
			}
		}
		
		ItemImageHover.Apply ();
	}
	
	public void Update(float frameTime)
	{
		//Are we paused?
		if (!m_Paused)
		{
			//Have we finished the build?
			if (m_Progress != 1.0f)
			{
				//Do we have enough money to continue the build?
				if (m_Manager.CostAcceptable (m_CostPerSecond*frameTime))
				{
					//Add the money back before removing it, this is to stop floating point precision errors
					m_Manager.AddMoneyInstant (m_Timer*m_CostPerSecond);
					
					m_Timer += frameTime;
					
					//Make sure we don't charge more by going over the build time
					if (m_Timer >= BuildTime)
					{
						m_Timer = BuildTime;
					}
					
					m_Manager.RemoveMoneyInstant (m_Timer*m_CostPerSecond);
					
					m_Progress = m_Timer/BuildTime;
				}
			}
			else
			{
				//Is this a building or a unit?
				if (TypeIdentifier == Const.TYPE_Building || TypeIdentifier == Const.TYPE_Support)
				{
					//Wait for user to place building before finishing
					m_Finished = true;
				}
				else
				{
					//Create unit straight away and finish build
					
					FinishBuild ();
				}
				
				GUIEvents.ItemUpdateTimer -= Update;
			}
		}
	}
	
	public Item DeepClone()
	{
		return new Item(this);
	}
	
	public void StartBuild()
	{
		GUIEvents.ItemUpdateTimer += Update;
		m_Timer = 0;
		m_Progress = 0;
		m_Building = true;
		m_Finished = false;
	}
	
	public void PauseBuild()
	{
		Pause ();
	}
	
	public void UnPauseBuild()
	{
		UnPause ();
	}
	
	public void CancelBuild()
	{
		if (!m_Finished) 
		{
			GUIEvents.ItemUpdateTimer -= Update;
		}
		
		UnPause ();
		m_Building = false;
		m_Finished = false;
		m_Manager.AddMoney (m_Timer*m_CostPerSecond);
	}
	
	public void FinishBuild()
	{
		UnPause ();
		m_Building = false;
		m_Finished = false;
		m_Progress = 0;
		m_Timer = 0;
	}
	
	public void AbortBuild()
	{
		if (!m_Finished) 
		{
			GUIEvents.ItemUpdateTimer -= Update;
		}
		
		UnPause ();
		m_Building = false;
		m_Finished = false;
	}
	
	public float GetProgress()
	{
		return m_Progress;
	}
	
	private void Pause()
	{
		m_Paused = true;
	}
	
	private void UnPause()
	{
		m_Paused = false;
	}
}
