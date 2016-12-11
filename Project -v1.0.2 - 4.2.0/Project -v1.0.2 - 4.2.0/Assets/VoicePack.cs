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
	public List<AudioClip> EnemyWave;
	public List<AudioClip> EnemyDreadNaughts;
	public List<AudioClip> ResearchFinished;
	public List<AudioClip> OreDepleted;

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

	public AudioClip getResearchLine()
	{
		return ResearchFinished[Random.Range (0,ResearchFinished.Count - 1)];
	}

	public AudioClip getEnemyWaveLine()
	{
		return EnemyWave [Random.Range (0,EnemyWave.Count - 1)];
	}

	public AudioClip geOreDepletedLine()
	{
		return OreDepleted[Random.Range (0,OreDepleted.Count - 1)];
	}

	public AudioClip getDreadnaughtsLine()
	{
		return EnemyDreadNaughts[Random.Range (0,EnemyDreadNaughts.Count - 1)];
	}


}
