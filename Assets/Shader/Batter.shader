Shader "MyShader/Batter"
{
    Properties
    {

        _InitColor("InitColor",COLOR) = (1,1,1,1)
        [Toggle(_True)]_IsActive("IsActive", float) = 1
        _Mix01("Mix01",2D) = "white" {}
        _Mix02("Mix02",2D) = "white" {}
        _Mix03("Mix03",2D) = "white" {}
        _Speed("Rotate Speed",float)=10
        _Process("Process",Range(0,1)) = 0

     }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


         
            float4 InitColor;
            float _IsActive;
            float4  _InitColor;

            sampler2D _Mix01;
            float4 _Mix01_ST;
             sampler2D _Mix02;
            float4 _Mix02_ST;
             sampler2D _Mix03;
            float4 _Mix03_ST;

            float _Speed;
            float _Process;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Mix01);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv.xy - float2(0.5, 0.5);
                uv = float2(uv.x * cos(_Speed * _Time.x) - uv.y * sin(_Speed * _Time.x),  
                            uv.x * sin(_Speed * _Time.x) + uv.y * cos(_Speed * _Time.x));  
                uv += float2(0.5, 0.5);  

                fixed4 mix01 = tex2D(_Mix01, uv);
                fixed4 mix02 = tex2D(_Mix02, uv);
                fixed4 mix03 = tex2D(_Mix03,i.uv.xy);

                fixed4 mixCol = lerp(mix01,mix02,_Process);

    
                fixed4 Color = lerp(_InitColor,mixCol,_IsActive);
                Color = lerp(Color,mix03,step(1,_Process));
                return Color;
            }
            ENDCG
        }
    }
}
