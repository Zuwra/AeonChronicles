using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoicePack : MonoBehaviour {


	public string voicePackName;
	public int UnlockNumber;
	public List<AudioClip> packSelected;

	public List<AudioClip> Ore;
	public List<AudioClip> supply;
	public List<AudioClip> maxSupply;
	public List<AudioClip> troopsAttacked;
	public List<AudioClip> baseAttacked;
	public List<AudioClip> cooldown;
	public List<AudioClip> buildingPlacement;
	public List<AudioClip> EnemyWave;
	public List<AudioClip> EnemyDreadNaughts;
	public List<AudioClip> EnemyBunnies;
	public List<AudioClip> ResearchFinished;
	public List<AudioClip> OreDepleted;
	public List<AudioClip> OreOccupied;
	public List<AudioClip> BuildingComplete;
	public List<AudioClip> UltTwoComp;
	public List<AudioClip> UltFourComp;


	public AudioClip getVoicePackLine()
	{
		return packSelected [Random.Range (0, packSelected.Count - 1)];
	}

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

	public AudioClip getOreDepletedLine()
	{
		return OreDepleted[Random.Range (0,OreDepleted.Count - 1)];
	}

	public AudioClip getOreOccupiedLine()
	{
		return OreOccupied[Random.Range (0,OreOccupied.Count - 1)];
	}

	public AudioClip getDreadnaughtsLine()
	{
		return EnemyDreadNaughts[Random.Range (0,EnemyDreadNaughts.Count - 1)];
	}


	public AudioClip getBuildingCompleteLine()
	{
		return BuildingComplete[Random.Range (0,BuildingComplete.Count - 1)];
	}

	public AudioClip getUltTwoComplete()
	{
		return UltTwoComp[Random.Range (0,UltTwoComp.Count - 1)];
	}

	public AudioClip getUltFourComplete()
	{
		return UltFourComp[Random.Range (0,UltFourComp.Count - 1)];
	}


	public AudioClip getEnemyWaveLine( WaveContainer.waveWarningType waveType)
	{
		if (waveType == WaveContainer.waveWarningType.Bunny && EnemyBunnies.Count > 0) {
			return EnemyBunnies [Random.Range (0,EnemyBunnies.Count - 1)];
		}
		else if (waveType == WaveContainer.waveWarningType.DreadNaught && EnemyDreadNaughts.Count > 0) {
			return EnemyDreadNaughts [Random.Range (0,EnemyDreadNaughts.Count - 1)];
		}

		return EnemyWave [Random.Range (0,EnemyWave.Count - 1)];
	}
}
