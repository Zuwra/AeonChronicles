using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WaveContainer : MonoBehaviour {



	public List<WaveOption> myWaveOptions;

	[Serializable]
	public struct WaveOption{
		public String name;
		public List<WaveSpawner.attackWave> waveRampUp;
	}

}
