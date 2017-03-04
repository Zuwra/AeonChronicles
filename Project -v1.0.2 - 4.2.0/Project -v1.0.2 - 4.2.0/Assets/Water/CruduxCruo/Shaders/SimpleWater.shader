Shader "CruduxCruo/Water" {
    Properties {
      _Color ("Main Color", Color) = (1,1,1,1)
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
      _BumpMap2 ("Bumpmap2", 2D) = "bump" {}
      _Cube ("Cubemap", CUBE) = "" {}
	  _Alpha ("Alpha", Range (0.0, 1)) = 1
    }
    SubShader {
      Tags { 
      			//"RenderType" = "Opaque" 
      			"Queue" = "Transparent" 
      			//"IgnoreProjector" = "True" 
      			"RenderType" = "Transparent"
      		}
	  //Cull Off
	  //Lighting Off
	  //LOD 200

      CGPROGRAM
      #pragma surface surf BlinnPhong alpha

      sampler2D _MainTex;
      sampler2D _BumpMap;
      sampler2D _BumpMap2;
      samplerCUBE _Cube;
	  fixed4 _Color;
	  half _Alpha;

      struct Input 
      {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float2 uv_BumpMap2;
          float3 worldRefl;
          INTERNAL_DATA
      };

      void surf (Input IN, inout SurfaceOutput o) 
      {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb * 0.25  * _Color;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          o.Normal *= UnpackNormal (tex2D (_BumpMap2, IN.uv_BumpMap2));
          o.Emission = texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)).rgb;
          o.Alpha = _Alpha * _Color.a;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }