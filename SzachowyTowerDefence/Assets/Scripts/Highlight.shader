Shader "Custom/Carton"{
	Properties{
		_AlbedoColor("Color", Color) = (0,0,0,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("Outline Width", Range(.002, 0.1)) = .005
	}

		SubShader{

			Stencil
			{
				Ref 1
				Comp always
				Pass replace
			}

			Tags { "Queue" = "Transparent" }
			LOD 100

			ZWrite off

			CGPROGRAM
				#pragma surface surf Lambert vertex:vert

			struct Input {
				float2 uv_MainTex;
			};

			float _OutlineWidth;
			float4 _OutlineColor;

			void vert(inout appdata_full v) {
				v.vertex.xyz += v.normal * _OutlineWidth;
			}

			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = _OutlineColor.rgb;
				o.Albedo = _OutlineColor.rgb;
			}

			ENDCG

			ZWrite On

			CGPROGRAM
			#pragma surface surf Lambert

			struct Input {
				float2 uv_MainTex;
			};
			float4 _AlbedoColor;


			void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = _AlbedoColor.rgb;
			}

			ENDCG
		}
			Fallback "Diffuse"
}