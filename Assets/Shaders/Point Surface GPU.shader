Shader "Graph/Point Surface GPU" {

    Properties {
		_Smoothness ("Smoothness", Range(0,1)) = 0.5
	}
	
	SubShader {
		CGPROGRAM
        //指示着色器编译器生成具有标准照明和完整阴影支持的全局光照着色器。 ConfigureSurface 指的是一种用于配置着色器的方法
        #pragma surface ConfigureSurface Standard fullforwardshadows addshadow
		#pragma instancing_options assumeuniformscaling procedural:ConfigureProcedural
		#pragma editor_sync_compilation
		#pragma target 4.5

		#include "PointGPU.hlsl"
        struct Input {
			float3 worldPos;
		};
        
        float _Smoothness;
        //定义我们的 ConfigureSurface 方法，虽然在着色器的情况下它总是被称为函数，而不是方法。它是一个 void 函数，有两个参数。第一个是一个输入参数，它有我们刚刚定义的 Input 类型。第二个参数是表面配置数据，类型为 SurfaceOutputStandard 
		void ConfigureSurface (Input input, inout SurfaceOutputStandard surface) {
            surface.Albedo = saturate(input.worldPos * 0.5 + 0.5);
			surface.Smoothness = _Smoothness;
		}
		ENDCG
	}
	
	FallBack "Diffuse"
}