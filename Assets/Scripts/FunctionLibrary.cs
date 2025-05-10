using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;
public static class FunctionLibrary 
{
    public enum FunctionName { Wave, MultiWave, Ripple, Sphere , Torus}
    static Function[] functions = { Wave, MultiWave, Ripple , Sphere, Torus};
    //这意味着Function可以引用任何符合这个签名的方法，比如Wave，MultiWave和Ripple
	//委托的意思是说可以把一个方法当做参数传递给另一个方法
	//Function是一个委托类型，它接受两个float参数并返回一个float值
	//这意味着Function可以引用任何符合这个签名的方法，比如Wave，MultiWave和Ripple
    public delegate Vector3 Function (float u, float v, float t);
	
	public static Function GetFunction (FunctionName name){
		return functions[(int)name];
	}

    public static FunctionName GetNextFunctionName (FunctionName name) {
		return (int)name < functions.Length - 1 ? name + 1 : 0;
	}
    public static FunctionName GetRandomFunctionNameOtherThan (FunctionName name) {
		var choice = (FunctionName)Random.Range(1, functions.Length);
		return choice == name ? 0 : choice;
	}
    public static Vector3 Morph (
		float u, float v, float t, Function from, Function to, float progress
	) {
		return Vector3.LerpUnclamped(
			from(u, v, t), to(u, v, t), SmoothStep(0f, 1f, progress)
		);
	}
    public static Vector3 Wave (float u, float v, float t) {
		Vector3 p;
		p.x = u;
		p.y = Sin(PI * (u + v + t));
		p.z = v;
		return p;
	}
    public static Vector3 MultiWave (float u, float v, float t) {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + 0.5f * t));
        p.y += 0.5f * Sin(2f * PI * (v + t));
        p.y += Sin(PI * (u + v + 0.25f * t));
        p.z = v;
        return p * (1f / 2.5f);
    }
    public static Vector3 Ripple (float u, float v, float t) {
        Vector3 p;
        float d = Sqrt(u * u + v * v);
        p.x = u;
        p.y = Sin(PI * (4f * d - t));
        p.y /= (1f + 10f * d);
        p.z = v;
        return p;
    }
    public static Vector3 Sphere (float u, float v, float t) {
        //r是一个动态值，基于参数 u、v 和时间 t 的正弦波变化。
        //0.9f 是半径的基准值。
        //0.1f * Sin(...) 是一个小幅度的波动，用于让半径随时间和 u、v 的变化产生动态效果。
        //6f * u + 4f * v + t 控制正弦波的频率和相位，产生复杂的动态效果。
		float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        //s是一个动态值，基于参数 u 和 v 的正弦波变化。
        //Cos(0.5f * PI * v) 用于在球体上创建一个圆形的截面。
        //r * Cos(0.5f * PI * v) 是计算球体上每个点的实际半径。
		float s = r * Cos(0.5f * PI * v);
		Vector3 p;
        //p是一个三维向量，表示球体上每个点的坐标。
        //p.x、p.y 和 p.z 分别表示 x、y 和 z 坐标。
        //s * Sin(PI * u) 和 s * Cos(PI * u) 用于计算球体上每个点的 x 和 z 坐标。
        //r * Sin(0.5f * PI * v) 用于计算球体上每个点的 y 坐标。
		p.x = s * Sin(PI * u);
		p.y = r * Sin(0.5f * PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}
    public static Vector3 Torus (float u, float v, float t) {
        //r1是环面（Torus）的主半径（主圆的半径），它是一个动态值，基于参数 u 和时间 t 的正弦波变化。
        //0.7f 是主半径的基准值。
        //0.1f * Sin(...) 是一个小幅度的波动，用于让主半径随时间和 u 的变化产生动态效果。
        //6f * u 控制正弦波的频率，0.5f * t 控制波动随时间的变化。
		float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        //r2是环面（Torus）的副半径（小圆的半径），它是一个动态值，基于参数 u、v 和时间 t 的正弦波变化。
        //0.15f 是副半径的基准值。
        //0.05f * Sin(...) 是一个小幅度的波动，用于让副半径随时间和 u、v 的变化产生动态效果。
        //8f * u + 4f * v + 2f * t 控制正弦波的频率和相位，产生复杂的动态效果。
		float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));
        //s是环面（Torus）的半径，它是主半径和副半径的组合。
        //r1 + r2 * Cos(PI * v) 是计算环面上每个点的实际半径。
        //Cos(PI * v) 用于在环面上创建一个圆形的截面。
		float s = r1 + r2 * Cos(PI * v);
		Vector3 p;
        //p是一个三维向量，表示环面上每个点的坐标。
        //p.x、p.y 和 p.z 分别表示 x、y 和 z 坐标。
        //s * Sin(PI * u) 和 s * Cos(PI * u) 用于计算环面上每个点的 x 和 z 坐标。
        //r2 * Sin(PI * v) 用于计算环面上每个点的 y 坐标。
		p.x = s * Sin(PI * u);
		p.y = r2 * Sin(PI * v);
		p.z = s * Cos(PI * u);
		return p;
	}

}
