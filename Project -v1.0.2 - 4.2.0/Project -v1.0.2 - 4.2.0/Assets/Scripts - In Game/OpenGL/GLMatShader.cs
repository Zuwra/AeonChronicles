using UnityEngine;
using System.Collections;

public static class GLMatShader {

	public static Material GetGLMaterial()
	{	
		string shaderText = 
		"Shader \"Lines/Colored Blended\" {" +
            "SubShader { Pass { " +
            "    Blend SrcAlpha OneMinusSrcAlpha " +
            "    ZWrite Off Cull Off Fog { Mode Off } " +
            "    BindChannels {" +
            "      Bind \"vertex\", vertex Bind \"color\", color }" +
            "} } }" ;
		
		Material mat = new Material(shaderText);
		mat.color = Color.white;
		
		return mat;
	
	}
}
