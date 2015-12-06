using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ItemDB {
	
	private static GameObject m_SmallExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	private static GameObject m_MediumExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	private static GameObject m_LargeExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	private static GameObject m_GiantExplosion = Resources.Load ("", typeof(GameObject)) as GameObject;
	
	private static List<Item> AllItems = new List<Item>();

	public static Item GRIStandardTank = new Item
	{
		ID = 0,
		TypeIdentifier = Const.TYPE_Vehicle,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Tank",
		Health = 100.0f,
		Armour = 3.0f,
		Damage = 30.0f,
		Speed = 10.0f,
		RotationSpeed = 80.0f,
		Acceleration = 2.0f,
		Explosion = m_SmallExplosion,
		Prefab = Resources.Load ("Models/GRI/Units/Vehicles/Standard Tank/StandardTank", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Units/Vehicles/Standard Tank/StandardTank", typeof(Texture2D)) as Texture2D,
		SortOrder = 0,
		RequiredBuildings = new int[] { 40 },
		Cost = 700,
		BuildTime = 10.0f,
	};
	
	public static Item GRIMCV = new Item
	{
		ID = 0,
		TypeIdentifier = Const.TYPE_Vehicle,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Tank",
		Health = 100.0f,
		Armour = 3.0f,
		Damage = 30.0f,
		Speed = 10.0f,
		RotationSpeed = 80.0f,
		Acceleration = 2.0f,
		Explosion = m_SmallExplosion,
		Prefab = Resources.Load ("Models/GRI/Units/Vehicles/Standard Tank/StandardTank", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Units/Vehicles/Standard Tank/StandardTank", typeof(Texture2D)) as Texture2D,
		SortOrder = 0,
		RequiredBuildings = new int[] { 40 },
		Cost = 700,
		BuildTime = 10.0f,
	};
	
	public static Item GRIConstructionYard = new Item
	{
		ID = 0,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Construction Yard",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Construction Yard/test", typeof(GameObject)) as GameObject,
		//ItemImage = Resources.Load ("", typeof(Texture2D)) as Texture2D,
		SortOrder = 100,
		RequiredBuildings = new int[] { 7, 6, 100 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_ConYard),
	};
	
	public static Item GRIPowerPlant = new Item
	{
		ID = 1,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Power Plant",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Power Plant/PowerPlantReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Power Plant/PowerPlant", typeof(Texture2D)) as Texture2D,
		SortOrder = 0,
		RequiredBuildings = new int[] { 0 },
		Cost = 700,
		BuildTime = 2.0f,
		ObjectType = typeof(GRI_PowerPlant),
	};
	
	public static Item GRIBarracks = new Item
	{
		ID = 2,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Barracks",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Barracks/BarracksReady", typeof(GameObject)) as GameObject,
		//Prefab = Resources.Load ("Models/GRI/Buildings/Power Plant/PowerPlantReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Barracks/Barracks", typeof(Texture2D)) as Texture2D,
		SortOrder = 1,
		RequiredBuildings = new int[] { 0 },
		Cost = 700,
		BuildTime = 3.0f,
		ObjectType = typeof(GRI_Barracks),
	};
	
	public static Item GRIRefinery = new Item
	{
		ID = 3,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Refinery",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Refinery/RefineryReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Refinery/Refinery", typeof(Texture2D)) as Texture2D,
		SortOrder = 2,
		RequiredBuildings = new int[] { 1 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_Refinery),
	};
	
	public static Item GRIWarFactory = new Item
	{
		ID = 4,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "War Factory",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/War Factory/WarFactoryReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/War Factory/WarFactory", typeof(Texture2D)) as Texture2D,
		SortOrder = 3,
		RequiredBuildings = new int[] { 2, 3 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_WarFactory),
	};
	
	public static Item GRIRadar = new Item
	{
		ID = 5,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Radar",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Radar/RadarReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Radar/Radar", typeof(Texture2D)) as Texture2D,
		SortOrder = 4,
		RequiredBuildings = new int[] { 3 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_Radar),
	};
	
	public static Item GRIRepairPad = new Item
	{
		ID = 6,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Repair Pad",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Repair Pad/RepairPadReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Repair Pad/RepairPad", typeof(Texture2D)) as Texture2D,
		SortOrder = 5,
		RequiredBuildings = new int[] { 5 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_RepairPad),
	};
	
	public static Item GRITechCeter = new Item
	{
		ID = 7,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Tech Center",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Tech Center/TechCenterReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Tech Center/TechCenter", typeof(Texture2D)) as Texture2D,
		SortOrder = 6,
		RequiredBuildings = new int[] { 4, 5 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_TechCenter),
	};
	
	public static Item GRISuperWeapon = new Item
	{
		ID = 8,
		TypeIdentifier = Const.TYPE_Building,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Super Weapon",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_GiantExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Super Weapon/SuperWeaponReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Super Weapon/SuperWeapon", typeof(Texture2D)) as Texture2D,
		SortOrder = 7,
		RequiredBuildings = new int[] { 7 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_SuperWeapon),
	};
	
	public static Item GRISupportGun = new Item
	{
		ID = 30,
		TypeIdentifier = Const.TYPE_Support,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "Rifle Turret",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Support/Gun/GunReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Support/Gun/Gun", typeof(Texture2D)) as Texture2D,
		SortOrder = 1,
		RequiredBuildings = new int[] { 1 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_Support_Gun),
	};
	
	public static Item GRISupportSAM = new Item
	{
		ID = 31,
		TypeIdentifier = Const.TYPE_Support,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "SAM Site",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_LargeExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Support/SAM/SAMReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Support/SAM/SAM", typeof(Texture2D)) as Texture2D,
		SortOrder = 2,
		RequiredBuildings = new int[] { 5 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_Support_SAM),
	};
	
	public static Item GRISupportRPG = new Item
	{
		ID = 32,
		TypeIdentifier = Const.TYPE_Support,
		TeamIdentifier = Const.TEAM_GRI,
		Name = "RPG",
		Health = 100.0f,
		Armour = 3.0f,
		Explosion = m_MediumExplosion,
		Prefab = Resources.Load ("Models/GRI/Buildings/Support/RPG/RPGReady", typeof(GameObject)) as GameObject,
		ItemImage = Resources.Load ("Item Images/GRI/Buildings/Support/RPG/RPG", typeof(Texture2D)) as Texture2D,
		SortOrder = 3,
		RequiredBuildings = new int[] { 7 },
		Cost = 700,
		BuildTime = 10.0f,
		ObjectType = typeof(GRI_Support_RPG),
	};
	
	public static void Initialise()
	{
		InitialiseItem (GRIStandardTank);
		InitialiseItem (GRIConstructionYard);
		InitialiseItem (GRIPowerPlant);
		InitialiseItem (GRIBarracks);
		InitialiseItem (GRIRefinery);
		InitialiseItem (GRIWarFactory);
		InitialiseItem (GRIRadar);
		InitialiseItem (GRITechCeter);
		InitialiseItem (GRIRepairPad);
		InitialiseItem (GRISuperWeapon);
		
		InitialiseItem (GRISupportGun);
		InitialiseItem (GRISupportRPG);
		InitialiseItem (GRISupportSAM);
	}
	
	private static void InitialiseItem(Item item)
	{
		item.Initialise ();
		AllItems.Add (item);
	}
	
	public static List<Item> GetAvailableItems(int ID, List<Building> CurrentBuildings)
	{
		List<Item> valueToReturn = AllItems.FindAll(x => 
		{
			if (x.RequiredBuildings.Length == 1)
			{
				if (x.RequiredBuildings[0] == ID)
				{
					return true;
				}
			}
			else
			{
				bool otherBuildingsPresent = true;
				
				//Does this item require the added building ID?
				if (x.RequiredBuildings.Contains (ID))
				{
					//If so do we have the other required ID's?
					foreach (int id in x.RequiredBuildings)
					{
						if (id != ID && CurrentBuildings.FirstOrDefault(building => building.ID == id) == null)
						{
							otherBuildingsPresent = false;
							break;
						}
					}
				}
				else
				{
					otherBuildingsPresent = false;
				}
				
				return otherBuildingsPresent;
			}
			
			
			return false;
		});
		
		return valueToReturn;
	}
}
