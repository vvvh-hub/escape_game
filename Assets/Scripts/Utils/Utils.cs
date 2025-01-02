using UnityEngine;

public static class Utils {
    /// <summary>
    /// 移除对象名后面的"(clone)"
    /// </summary>
    /// <param name="input">对象名</param>
    /// <returns></returns>
    public static string RemoveClone(string input) {
        int len = input.IndexOf('(');
        if (len == -1) {
            return input;
        } else {
            return input.Substring(0, len);
        }
    }

    /// <summary>
    /// 检查一个范围是否在另一个范围内
    /// </summary>
    /// <param name="a">给定范围第一个数</param>
    /// <param name="b">给定范围第二个数</param>
    /// <param name="min">另一个范围最小值</param>
    /// <param name="max">另一个范围最大值</param>
    /// <returns></returns>
    public static bool CheckInputAvailable(float a, float b, float min, float max) {
        if (a < min || a > max)
            return false;
        if (b < min || b > max)
            return false;
        return a != b;
    }


    /// <summary>
    /// 将子弹角度调整为合适的角度
    /// </summary>
    /// <param name="direction">传入的方向</param>
    /// <returns>应该设置成为的角度</returns>
    public static Quaternion GetRotation(Vector2 direction) {
        float k = direction.y / direction.x;
        float angle = -Mathf.Atan(k) * Mathf.Rad2Deg;
        if ((direction.y < 0 && direction.x < 0) || (direction.y < 0 && direction.x > 0)) {
            angle += 180;
        }
        return Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// 将子弹调整到合适角度后加上以偏移量
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="extra">偏移量</param>
    /// <returns></returns>
    public static Quaternion GetRotation(Vector3 direction, float extra) {
        float angle;
        if (!Mathf.Approximately(direction.x, 0)) {
            float k = direction.y / direction.x;
            //Debug.Log(k);
            angle = -Mathf.Atan(k) * Mathf.Rad2Deg;
        } else {
            angle = 90;
        }

        if ((direction.y < 0 && direction.x < 0) || (direction.y < 0 && direction.x > 0)) {
            angle += 180;
        }
        angle += extra;
        var rtn = Quaternion.Euler(new Vector3(0, 0, angle));
        if (Quaternion.Dot(rtn, rtn) < Quaternion.kEpsilon)
            rtn = Quaternion.identity;
        return rtn;
    }

    public static Vector2 EluerToVector(Vector3 eluer) {
        float z = eluer.z;
        return new Vector3(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad), 0);
    }

    /// <summary>
    /// 判断一个数是否大约等于另一个数
    /// </summary>
    /// <param name="number">要判断的数</param>
    /// <param name="another">另一数</param>
    /// <param name="range">大约范围</param>
    /// <returns></returns>
    public static bool Approximately(float number, float another, float range = 0.01f) {
        return (number > another - range && number < another + range);
    }
}
