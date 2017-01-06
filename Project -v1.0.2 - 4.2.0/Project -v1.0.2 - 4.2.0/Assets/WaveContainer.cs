using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WaveContainer : MonoBehaviour {


	public enum EnemyWave{
		ScrapCrack, ScrapChem, DreadFleet, Bunny, CrackSkiff, ScrapSkif, BunnySKiff, ChemScrap
	}


	public WaveOption getWave(EnemyWave en)
	{
		switch (en) {
		case EnemyWave.ScrapCrack:
			return myWaveOptions [0];
			break;
		case EnemyWave.ScrapChem:
			return myWaveOptions [1];
			break;
		case EnemyWave.DreadFleet:
			return myWaveOptions [2];
			break;
		case EnemyWave.Bunny:
			return myWaveOptions [3];
			break;
		case EnemyWave.CrackSkiff:
			return myWaveOptions [4];
			break;
		case EnemyWave.ScrapSkif:
			return myWaveOptions [5];
			break;
		case EnemyWave.BunnySKiff:
			return myWaveOptions [6];
			break;
		case EnemyWave.ChemScrap:
			return myWaveOptions [7];
			break;

		}

		return myWaveOptions [0];
	}

	public List<WaveOption> myWaveOptions;

	[Serializable]
	public struct WaveOption{
		public String name;
		public List<WaveSpawner.attackWave> waveRampUp;
	}

}
