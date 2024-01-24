// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Explode"
{
	Properties
	{
		_Color0("Color 0", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
		};

		uniform float ExplodeProgress;
		uniform float4 _Color0;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 MyCustomExpression17( float noise, float burnValue, float width, float burningRange, float4 color, float4 colorRange )
		{
			float test = noise-burnValue;
			clip(test);
			float4 finalColor = (float4)0;
			//w,高亮的区域设置为1，不是的话设置为0
			//高亮区域
			if(test<width)
			{   
			  finalColor=float4(color.rgb,1);
			  return finalColor;
			}
			//非高亮区域
			finalColor = smoothstep(burningRange,0,test)*colorRange;
			finalColor.w=0;
			return finalColor;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ase_vertexNormal * pow( ExplodeProgress , 3.0 ) );
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Color0.rgb;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float2 appendResult24 = (float2(ase_vertex3Pos.y , ase_vertex3Pos.z));
			float simplePerlin2D10 = snoise( appendResult24*2.0 );
			simplePerlin2D10 = simplePerlin2D10*0.5 + 0.5;
			float noise17 = simplePerlin2D10;
			float lerpResult28 = lerp( -1.0 , 1.0 , ExplodeProgress);
			float burnValue17 = lerpResult28;
			float width17 = 0.25;
			float burningRange17 = 0.7;
			float4 color13 = IsGammaSpace() ? float4(0.9433962,0.1548254,0.05784979,0) : float4(0.8760344,0.02073259,0.004679462,0);
			float4 color17 = color13;
			float4 color20 = IsGammaSpace() ? float4(0.8301887,0.4487063,0.1683873,0) : float4(0.6562665,0.1695977,0.02409542,0);
			float4 colorRange17 = color20;
			float4 localMyCustomExpression17 = MyCustomExpression17( noise17 , burnValue17 , width17 , burningRange17 , color17 , colorRange17 );
			o.Emission = localMyCustomExpression17.xyz;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18800
32;87;1385;720;1296.669;160.7603;1.57216;True;True
Node;AmplifyShaderEditor.PosVertexDataNode;23;-1477.467,-368.2858;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-1108.01,1.172088;Inherit;False;Constant;_Float4;Float 4;1;0;Create;True;0;0;0;False;0;False;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1396.737,199.3145;Inherit;False;Global;ExplodeProgress;ExplodeProgress;1;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1073.422,97.07385;Inherit;False;Constant;_Float5;Float 5;1;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1278.519,-104.3667;Inherit;False;Constant;_Float1;Float 1;1;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;24;-1262.081,-259.8063;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-655.9332,851.632;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-826.6759,249.448;Inherit;False;Constant;_Float2;Float 2;1;0;Create;True;0;0;0;False;0;False;0.25;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;10;-1067.834,-244.2272;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;20;-860.4175,575.4792;Inherit;False;Constant;_Color3;Color 3;2;0;Create;True;0;0;0;False;0;False;0.8301887,0.4487063,0.1683873,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-861.8835,405.1453;Inherit;False;Constant;_Color1;Color 1;1;0;Create;True;0;0;0;False;0;False;0.9433962,0.1548254,0.05784979,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;28;-867.4691,43.62042;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;3;-540.9989,506.3928;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;8;-547.4164,709.1224;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-819.5101,330.9678;Inherit;False;Constant;_Float3;Float 3;1;0;Create;True;0;0;0;False;0;False;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;17;-444.8607,35.10918;Inherit;False;float test = noise-burnValue@$clip(test)@$float4 finalColor = (float4)0@$$//w,高亮的区域设置为1，不是的话设置为0$$//高亮区域$if(test<width)${   $  finalColor=float4(color.rgb,1)@$  return finalColor@$}$$//非高亮区域$finalColor = smoothstep(burningRange,0,test)*colorRange@$finalColor.w=0@$$return finalColor@;4;False;6;True;noise;FLOAT;0;In;;Inherit;False;True;burnValue;FLOAT;0;In;;Inherit;False;True;width;FLOAT;0;In;;Inherit;False;True;burningRange;FLOAT;0;In;;Inherit;False;True;color;FLOAT4;0,0,0,0;In;;Inherit;False;True;colorRange;FLOAT4;0,0,0,0;In;;Inherit;False;My Custom Expression;True;False;0;6;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT4;0,0,0,0;False;5;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-221.4455,592.351;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;1;-645.0961,-290.3736;Inherit;False;Property;_Color0;Color 0;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;76,-117;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Explode;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;24;0;23;2
WireConnection;24;1;23;3
WireConnection;10;0;24;0
WireConnection;10;1;12;0
WireConnection;28;0;29;0
WireConnection;28;1;30;0
WireConnection;28;2;7;0
WireConnection;8;0;7;0
WireConnection;8;1;9;0
WireConnection;17;0;10;0
WireConnection;17;1;28;0
WireConnection;17;2;21;0
WireConnection;17;3;22;0
WireConnection;17;4;13;0
WireConnection;17;5;20;0
WireConnection;6;0;3;0
WireConnection;6;1;8;0
WireConnection;0;0;1;0
WireConnection;0;2;17;0
WireConnection;0;11;6;0
ASEEND*/
//CHKSM=06C4AEB67C9018FEB741E914D2B3ED8A61D795CF