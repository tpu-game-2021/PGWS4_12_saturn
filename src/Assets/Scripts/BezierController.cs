using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierController : MonoBehaviour
{
    [SerializeField] private GameObject p0;
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject p3;
    // Start is called before the first frame update

    float count = 0.0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 5秒でリセット・
        count += Time.deltaTime;
        if(5.0f < count){
            count = 0.0f;
        }
        // ５秒かけて0-1に変わって目的に飛んでいく
        float t = count / 5.0f;

        // ベジェ曲線を使って位置を指定
        this.transform.position = Bezier(
            p0.transform.position,
            p1.transform.position,
            p2.transform.position,
            p3.transform.position,
            t
        );

        // 飛んでいく方向にオブジェクトを向ける
        this.transform.LookAt(Bezier(
            p0.transform.position,
            p1.transform.position,
            p2.transform.position,
            p3.transform.position,
            t+0.01f// ちょっと先
        ));
        // カプセル形状と飛んでいく方向が９０度違っていたので補正
        this.transform.Rotate(90,0,0);
    }

    private Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p12 = Vector3.Lerp(p0, p1, t);
        Vector3 p23 = Vector3.Lerp(p2, p3, t);
        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        return Vector3.Lerp(p012, p123, t);
    }
}
