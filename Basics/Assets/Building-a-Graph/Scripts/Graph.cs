using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    //序列化的意思是说再次读取Unity时序列化的变量是+有值的，不需要你再次去赋值，因为它已经被保存下来
    [SerializeField]
    Transform pointPrefab;

    [SerializeField, Range(10, 100)]
    int resolution  = 10;

    Transform[] points;
    // Start is called before the first frame update
    void Awake()
    {
       float step = 2f / resolution;
		var position = Vector3.zero;
		var scale = Vector3.one * step;
        points = new Transform[resolution];
		for (int i = 0; i < points.Length; i++) {
            Transform point = points[i] = Instantiate(pointPrefab);
			position.x = (i + 0.5f) * step - 1f;
            point.localPosition = position;
            point.localScale = scale;
            point.SetParent(transform, false);
            // SetParent(transform, false)的意思是说不需要去改变父物体的缩放和旋转
        }
       
    }

    // Update is called once per frame
    void Update () {
		float time = Time.time;
		for (int i = 0; i < points.Length; i++) {
			Transform point = points[i];
			Vector3 position = point.localPosition;
			position.y = Mathf.Sin(Mathf.PI * (position.x + time));
			point.localPosition = position;
		}
	}
}
