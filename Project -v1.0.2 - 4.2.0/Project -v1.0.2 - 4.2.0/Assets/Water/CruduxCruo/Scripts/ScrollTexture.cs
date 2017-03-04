using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour 
{
	//типы движения:
	//по прямой, по кругу, по восьмерке
	//type of movement
	public enum AnimType
	{
		line,
		circle,
		Lissajous
	}
	private float t = 0;	//время/угол  time/angle
	private float x;
	private float y;
	private Vector2 uvOffset;

	public int[] materialNums = new int[1];					//номер материала для обработки, по умолчанию - 0  
															//number of the material for processing, default - 0
	public string textureName = "_MainTex";	
	public float speedAnimation = 0.1f;		
	public float radius = 0.2f;								//радиус движения по кругу или восьмерке
															//range of motion in a circle or figure Lissajous
	public Vector2 vectorLine = new Vector2(0.2f, 0.1f);	//вектор движения по прямой
															//a motion vector in a straight line
	public AnimType _animtype;
	
	void LateUpdate() 
	{		
		if (t < 2 * Mathf.PI)
			t += Time.deltaTime * speedAnimation;
		else
			t = 0;
		
		switch (_animtype) 
		{
			case AnimType.line:
				if (Mathf.Abs(x) < 1)
					x += Time.deltaTime * speedAnimation * vectorLine.x;
				else
					x = 0;
				if (Mathf.Abs(y) < 1)
					y += Time.deltaTime * speedAnimation *vectorLine.y;
				else
					y = 0;			
			break;

			case AnimType.circle:
				x = radius * Mathf.Cos (t);
				y = radius * Mathf.Sin (t);
			break;

			case AnimType.Lissajous:
				x = radius * Mathf.Cos (t);
				y = radius * Mathf.Sin (2 * t);
			break;
		}

		uvOffset = new Vector2 (x, y);	
		for (int i = 0; i < materialNums.Length; i++)
			GetComponent<Renderer>().materials[materialNums[i]].SetTextureOffset( textureName, uvOffset );
	}
}
