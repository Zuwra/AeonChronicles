using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cursor : MonoBehaviour {

	public List<Texture2D> CursorPicture;
	public float animationTime = 0.1f;
	private float animationCounter = 0;
	private int index = 0;
	public int ID;
	public bool CenterTexture = true;
	
	private bool isAnimated = false;
	
	public bool IsAnimated
	{
		get { return this.isAnimated; }
	}
	
	void Awake()
	{
		if (CursorPicture.Count == 0)
		{
			Destroy (this);
		}
		
		if (CursorPicture.Count > 1)
		{
			isAnimated = true;
		}
	}
	
	public virtual void Animate(float deltaTime)
	{
		animationCounter += deltaTime;
		
		if (animationCounter >= animationTime)
		{
			animationCounter = 0;
			index++;
			if (index >= CursorPicture.Count) 
			{
				index = 0;
			}
		}
	}
	
	public Texture2D GetCursorPicture()
	{
		return CursorPicture[index];
	}
}
