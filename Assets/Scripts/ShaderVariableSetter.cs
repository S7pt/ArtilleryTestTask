using UnityEngine;

public class ShaderVariableSetter : MonoBehaviour
{
    void Start()
    {
        Debug.Log("_g_cl before: " +Shader.GetGlobalVector("_g_cl"));
        Shader.SetGlobalVector("_g_cl", new Vector4(1,1,0,1));
        Debug.Log("_g_cl after: " + Shader.GetGlobalVector("_g_cl"));
        Debug.Log("_g_dir before: " + Shader.GetGlobalVector("_g_dir"));
        Shader.SetGlobalVector("_g_dir", new Vector3(0, 1, 1));
        Debug.Log("_g_dir after: " + Shader.GetGlobalVector("_g_dir"));
        Debug.Log("_g_scl before: " + Shader.GetGlobalFloat("_g_scl"));
        Shader.SetGlobalFloat("_g_scl", 1);
        Debug.Log("_g_scl after: " + Shader.GetGlobalFloat("_g_scl"));
        Debug.Log("_g_sat before: " + Shader.GetGlobalFloat("_g_sat"));
        Shader.SetGlobalFloat("_g_sat", 1);
        Debug.Log("_g_sat after: " + Shader.GetGlobalFloat("_g_sat"));
    }
}
