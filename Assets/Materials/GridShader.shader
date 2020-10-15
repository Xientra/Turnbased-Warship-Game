Shader "Custom/GridShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_CellSize("Cell Size", Float) = 0
		_LineWidth("Line Width", Float) = 0
	}
		SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		LOD 100

		Cull Off
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _LineWidth;
			float _CellSize;

			v2f vert(appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{

				float2 unityCell = (_ScreenParams.xy / (unity_OrthoParams * 2));

				fixed4 col = float4(1, 1, 1, 0);

				if ((i.vertex.x % unityCell.x * _CellSize.x) <= _LineWidth * unityCell.x || (i.vertex.y % unityCell.y * _CellSize) <= _LineWidth * unityCell.y)
					col = tex2D(_MainTex, i.uv); // sample the texture
					

				return col * i.color;
			}
			ENDCG
		}
	}
}
