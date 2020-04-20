using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//实现代码如下：（最外层有6个点，依次5，4，3，2，1）
//添加一个小球，沿曲线运动，使小球看向目标物体
public class Test_DrawLine : MonoBehaviour
{

    [SerializeField] private Transform points;                          //控制点父对象
    private List<Transform> point_tranList = new List<Transform>();     //控制点列表
    [SerializeField] private int pointCount = 100;                      //曲线点的个数
    private List<Vector3> line_pointList;                               //曲线点列表

    [SerializeField] private Transform lookTarget;                      //看向目标
    [SerializeField] private GameObject ball;                           //运动物体
    [SerializeField] private float time0 = 0;                           //曲线点之间移动时间间隔
    private float timer = 0;                    //计时器
    private int item = 1;                       //曲线点的索引
    private bool isTrue = false;

    //使小球沿曲线运动
    //这里不能直接在for里以Point使用差值运算，看不到小球运算效果
    //定义一个计时器，在相隔时间内进行一次差值运算。
    void Awake()
    {
        Init();
    }
    void Init()
    {
        if (null != points && point_tranList.Count == 0)
        {
            foreach (Transform item in points)
            {
                point_tranList.Add(item);
            }
        }
        line_pointList = new List<Vector3>();
        for (int i = 0; point_tranList.Count != 0 && i < pointCount; i++)
        {
            //一
            Vector3 pos1 = Vector3.Lerp(point_tranList[0].position, point_tranList[1].position, i / (float)pointCount);
            Vector3 pos2 = Vector3.Lerp(point_tranList[1].position, point_tranList[2].position, i / (float)pointCount);
            Vector3 pos3 = Vector3.Lerp(point_tranList[2].position, point_tranList[3].position, i / (float)pointCount);
            Vector3 pos4 = Vector3.Lerp(point_tranList[3].position, point_tranList[4].position, i / (float)pointCount);
            Vector3 pos5 = Vector3.Lerp(point_tranList[4].position, point_tranList[5].position, i / (float)pointCount);

            //二
            var pos1_0 = Vector3.Lerp(pos1, pos2, i / (float)pointCount);
            var pos1_1 = Vector3.Lerp(pos2, pos3, i / (float)pointCount);
            var pos1_2 = Vector3.Lerp(pos3, pos4, i / (float)pointCount);
            var pos1_3 = Vector3.Lerp(pos4, pos5, i / (float)pointCount);
            //三
            var pos2_0 = Vector3.Lerp(pos1_0, pos1_1, i / (float)pointCount);
            var pos2_1 = Vector3.Lerp(pos1_1, pos1_2, i / (float)pointCount);
            var pos2_2 = Vector3.Lerp(pos1_2, pos1_3, i / (float)pointCount);
            //四
            var pos3_0 = Vector3.Lerp(pos2_0, pos2_1, i / (float)pointCount);
            var pos3_1 = Vector3.Lerp(pos2_1, pos2_2, i / (float)pointCount);
            //五
            Vector3 find = Vector3.Lerp(pos3_0, pos3_1, i / (float)pointCount);

            line_pointList.Add(find);
        }
        if (line_pointList.Count == pointCount)
            isTrue = true;
    }

    void Update()
    {
        if (!isTrue)
            return;
        timer += Time.deltaTime;
        if (timer > time0)
        {
            timer = 0;
            if (item >= line_pointList.Count - 10)
            {
                ball.transform.LookAt(line_pointList[item]);
            }
            else
            {
                if (lookTarget)
                    ball.transform.LookAt(lookTarget);
                else
                    ball.transform.LookAt(line_pointList[item]);
            }
            ball.transform.localPosition = Vector3.Lerp(line_pointList[item - 1], line_pointList[item], 1f);
            item++;
            if (item >= line_pointList.Count)
                item = 1;
        }
    }

    //------------------------------------------------------------------------------
    //在scene视图显示
    void OnDrawGizmos()//画线
    {
        Init();
        Gizmos.color = Color.yellow;
        for (int i = 0; i < line_pointList.Count - 1; i++)
        {
            Gizmos.DrawLine(line_pointList[i], line_pointList[i + 1]);
        }
    }

}