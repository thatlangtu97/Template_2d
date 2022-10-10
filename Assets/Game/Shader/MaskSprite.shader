//////////////////////////////////////////////////////////////
/// Shadero Sprite: Sprite Shader Editor - by VETASOFT 2018 //
/// Shader generate with Shadero 1.9.3                      //
/// http://u3d.as/V7t #AssetStore                           //
/// http://www.shadero.com #Docs                            //
//////////////////////////////////////////////////////////////

Shader "Shadero Customs/MaskSprite"
{
Properties
{
[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
_NewTex_1("NewTex_1(RGB)", 2D) = "white" { }
_ThresholdSmooth_Value_1("_ThresholdSmooth_Value_1", Range(-1, 2)) = -1
_ThresholdSmooth_Smooth_1("_ThresholdSmooth_Smooth_1", Range(0, 1)) = 0.1
_SpriteFade("SpriteFade", Range(0, 1)) = 1.0

// required for UI.Mask
[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
[HideInInspector]_Stencil("Stencil ID", Float) = 0
[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
[HideInInspector]_ColorMask("Color Mask", Float) = 15

}

SubShader
{

Tags {"Queue" = "Transparent" "IgnoreProjector" = "true" "RenderType" = "Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off

// required for UI.Mask
Stencil
{
Ref [_Stencil]
Comp [_StencilComp]
Pass [_StencilOp]
ReadMask [_StencilReadMask]
WriteMask [_StencilWriteMask]
}

Pass
{

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

struct appdata_t{
float4 vertex   : POSITION;
float4 color    : COLOR;
float2 texcoord : TEXCOORD0;
};

struct v2f
{
float2 texcoord  : TEXCOORD0;
float4 vertex   : SV_POSITION;
float4 color    : COLOR;
};

sampler2D _MainTex;
float _SpriteFade;
sampler2D _NewTex_1;
float _ThresholdSmooth_Value_1;
float _ThresholdSmooth_Smooth_1;

v2f vert(appdata_t IN)
{
v2f OUT;
OUT.vertex = UnityObjectToClipPos(IN.vertex);
OUT.texcoord = IN.texcoord;
OUT.color = IN.color;
return OUT;
}


float4 UniColor(float4 txt, float4 color)
{
txt.rgb = lerp(txt.rgb,color.rgb,color.a);
return txt;
}
float4 ThresholdSmooth(float4 txt, float value, float smooth)
{
float l = (txt.x + txt.y + txt.z) * 0.33;
txt.rgb = smoothstep(value, value + smooth, l);
return txt;
}

float4 TurnAlphaToBlack(float4 txt,float fade)
{
float3 gs = lerp(txt.rgb,float3(0,0,0), 1-txt.a);
return lerp(txt,float4(gs, 1), fade);
}

float4 frag (v2f i) : COLOR
{
float4 _MainTex_1 = tex2D(_MainTex, i.texcoord);
float4 NewTex_1 = tex2D(_NewTex_1, i.texcoord);
float4 FillColor_1 = UniColor(_MainTex_1, float4(1,1,1,1));
float4 TurnAlphaToBlack_1 = TurnAlphaToBlack(FillColor_1,1);
NewTex_1.a = lerp(TurnAlphaToBlack_1.r, 1 - TurnAlphaToBlack_1.r ,0);
float4 _ThresholdSmooth_1 = ThresholdSmooth(NewTex_1,_ThresholdSmooth_Value_1,_ThresholdSmooth_Smooth_1);
_MainTex_1.a = lerp(_ThresholdSmooth_1.r * _MainTex_1.a, (1 - _ThresholdSmooth_1.r) * _MainTex_1.a,0);
float4 FinalResult = _MainTex_1;
FinalResult.rgb *= i.color.rgb;
FinalResult.a = FinalResult.a * _SpriteFade * i.color.a;
return FinalResult;
}

ENDCG
}
}
Fallback "Sprites/Default"
}
