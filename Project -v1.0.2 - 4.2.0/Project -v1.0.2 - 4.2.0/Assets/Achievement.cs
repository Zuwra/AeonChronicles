﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Achievement : MonoBehaviour {

	public string Title;
	public string Description;
	public Sprite myIcon;

	public enum Earnings{all, unearned, earned}
	public enum Level{all, campaign, one, two,three, four,five, six,seven,eight,nine,ten,eleven,twelve,thirteen, anyLevel}
	public abstract void CheckBeginning ();
	public abstract string GetDecription ();
	public Level myLevel;
	public int TechReward = 2;

	public abstract void CheckEnd ();

	public string getRewardText()
	{
		if (TechReward > 0) {
			return "Reward: +" + TechReward + " Tech Credits"; 
		} else {
			return "New Announcer Voice Pack";
		}
	}

	public bool HasBeenRewarded()
	{
		if (PlayerPrefs.GetInt (Title + "Reward", 0) == 0) {
			return false;
		}
		return true;
	}

	public void Reward()
	{
		PlayerPrefs.SetInt(Title + "Reward", 1);
	}

	public bool IsAccomplished()
	{
		
		if (PlayerPrefs.GetInt (Title, 0) == 0) {
			return false;
		}
		return true;
	}

	protected void Accomplished()
	{
		PlayerPrefs.SetInt (Title, 1);
	}

	public virtual void Reset()
	{
		PlayerPrefs.SetInt (Title, 0);
	}
}
