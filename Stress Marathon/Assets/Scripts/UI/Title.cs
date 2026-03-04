using UnityEngine;

public class Title : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneLoader.Instance.ConvertScene(SceneType.Race);
    }

    public void OnClickOptions()
    {
        
    }
}
