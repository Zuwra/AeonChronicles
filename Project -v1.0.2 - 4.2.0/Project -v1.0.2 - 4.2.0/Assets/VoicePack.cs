using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoicePack : MonoBehaviour {


	public string voicePackName;


	public List<AudioClip> Ore;
	public List<AudioClip> supply;
	public List<AudioClip> maxSupply;
	public List<AudioClip> troopsAttacked;
	public List<AudioClip> baseAttacked;
	public List<AudioClip> cooldown;
	public List<AudioClip> buildingPlacement;


	public AudioClip getOreLine()
	{
		return Ore [Random.Range (0, Ore.Count - 1)];
	}

	public AudioClip getSupplyLine()
	{
		return supply [Random.Range (0, supply.Count - 1)];
	}

	public AudioClip getMaxSupplyLine()
	{
		return maxSupply [Random.Range (0, maxSupply.Count - 1)];
	}

	public AudioClip getTroopAttackLine()
	{
		return troopsAttacked [Random.Range (0, troopsAttacked.Count - 1)];
	}

	public AudioClip getbaseAttackedLine()
	{
		return baseAttacked [Random.Range (0, baseAttacked.Count - 1)];
	}

	public AudioClip getCooldownLine()
	{
		return cooldown [Random.Range (0, cooldown.Count - 1)];
	}

	public AudioClip getBadBuildingLine()
	{
		return buildingPlacement [Random.Range (0,buildingPlacement.Count - 1)];
	}


}
