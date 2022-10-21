Shader "Treeplla/SpriteOutline"
{
    Properties{
    _MainTex("MainTex",2D) = "white"{} 
    _OutlineWidth("Outline Size", Float) = 0.01
    _OutlineColor("Outline Color", Color) = (1,1,1,1)
    _BackGroundMode("BackGroundMode", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Pass
        {
            Cull Off 
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
                
        

            float4 vec4(float x,float y,float z,float w){return float4(x,y,z,w);}
            float4 vec4(float x){return float4(x,x,x,x);}
            float4 vec4(float2 x,float2 y){return float4(float2(x.x,x.y),float2(y.x,y.y));}
            float4 vec4(float3 x,float y){return float4(float3(x.x,x.y,x.z),y);}


            float3 vec3(float x,float y,float z){return float3(x,y,z);}
            float3 vec3(float x){return float3(x,x,x);}
            float3 vec3(float2 x,float y){return float3(float2(x.x,x.y),y);}

            float2 vec2(float x,float y){return float2(x,y);}
            float2 vec2(float x){return float2(x,x);}

            float vec(float x){return float(x);}
            
            

            struct VertexInput {
                float4 vertex : POSITION;
                float2 uv:TEXCOORD0;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                //VertexInput
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv:TEXCOORD0;
                //VertexOutput
            };
            sampler2D _MainTex; 
            float4 _MainTex_ST;
            fixed _OutlineWidth;
            fixed4 _OutlineColor;            
            fixed _BackGroundMode;
            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            
            fixed4 frag(VertexOutput vertex_output) : SV_Target
            {
                float2 u_textureRes = vertex_output.uv;
                //OUTLINE
                float outlineAlpha = 0.0;
                float angle = 0.0;
                for( int i=0; i < 32; ++i ){
                    angle += 1.0/(float(32)/2.0) * 3.14159265359;
                    float2 testPoint = float2( _OutlineWidth * cos(angle), _OutlineWidth * sin(angle) );
                    testPoint = vertex_output.uv + testPoint;
                    float sampledAlpha = tex2D( _MainTex,  testPoint ).a;
                    outlineAlpha = max( outlineAlpha, sampledAlpha );
                }
                fixed4 fragColor = lerp( float4(0.0,0.0,0.0,0.0), _OutlineColor, outlineAlpha );

                //fixed4 col = tex2D(_MainTex, i.uv);
                //TEXTURE
                float4 tex0 = tex2D( _MainTex, u_textureRes );
                fixed4 result = lerp( fragColor, tex0, tex0.a );
                result.rgb = lerp(result.rgb,float3(1.0,1.0,1.0), 1.0);
                    
                return result;

            }
            ENDCG
        }
    }
}
