Shader "CruduxCruo/Glass CubeMap" 
{
    Properties 
	{
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
        _Cube ("Reflection Cubemap", Cube) = "_Skybox" { TexGen CubeReflect }
        _ReflectStrength( "Reflection Strength" , Range(0,3)) = 1.5
		_Alpha ("Alpha", Range (0.0, 1)) = 1
    }   
	
	SubShader 
	{
           
		Tags { "Queue"="Transparent"  "RenderType" = "Transparent" } 

		CGPROGRAM
			#pragma surface surf Lambert alpha

			struct Input  
			{
				float2 uv_MainTex;
				float3 viewDir;
				float3 worldRefl;
			};

			uniform sampler2D _MainTex;
			uniform float4 _Color;
			uniform samplerCUBE _Cube;
			uniform float4 _ReflectColor;
			uniform float _ReflectStrength;
			uniform float _Fresnel;
			half _Alpha;

			void surf (Input IN, inout SurfaceOutput o)
			{
               
				float4 texcol = tex2D(_MainTex, IN.uv_MainTex);
				texcol.rgb *=_Color.rgb;
				texcol.a = texcol.a * _Color.a;
       
				float4 reflectCol = texCUBE( _Cube , IN.worldRefl );
               
				reflectCol *= _ReflectColor;
				reflectCol *= _ReflectStrength;
				
				texcol.rgb += reflectCol.rgb;
       
				o.Albedo = texcol.rgb;
				o.Alpha = _Alpha * _Color.a;
			}
		ENDCG
	}
       
	FallBack "Transparent/Diffuse"
}