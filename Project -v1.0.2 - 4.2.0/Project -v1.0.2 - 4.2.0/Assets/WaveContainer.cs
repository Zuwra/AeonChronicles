using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WaveContainer : MonoBehaviour {


	public enum waveWarningType
	{
		Normal, Bunny, DreadNaught,Bugs
	}

	public enum EnemyWave{
		ScrapCrack, ScrapChem, DreadFleet, Bunny, CrackSkiff, ScrapSkif, BunnySKiff, ChemScrap,ShapeLand,Wasps,Bugs,NecroSkitter
	}


	public WaveOption getWave(EnemyWave en)
	{
		switch (en) {
		case EnemyWave.ScrapCrack:
			return myWaveOptions [0];

		case EnemyWave.ScrapChem:
			return myWaveOptions [1];

		case EnemyWave.DreadFleet:
			return myWaveOptions [2];

		case EnemyWave.Bunny:
			return myWaveOptions [3];

		case EnemyWave.CrackSkiff:
			return myWaveOptions [4];

		case EnemyWave.ScrapSkif:
			return myWaveOptions [5];

		case EnemyWave.BunnySKiff:
			return myWaveOptions [6];

		case EnemyWave.ChemScrap:
			return myWaveOptions [7];
		case EnemyWave.ShapeLand:
			return myWaveOptions [8];
		
		case EnemyWave.Wasps:
			return myWaveOptions [9];
		
		case EnemyWave.Bugs:
			return myWaveOptions [10];
		

		case EnemyWave.NecroSkitter:
			return myWaveOptions [11];
		
	}

		return myWaveOptions [0];
	}

	public List<WaveOption> myWaveOptions;

	[Serializable]
	public struct WaveOption{
		public String name;
		public List<WaveSpawner.attackWave> waveRampUp;
		public waveWarningType warningType;
	}

}
