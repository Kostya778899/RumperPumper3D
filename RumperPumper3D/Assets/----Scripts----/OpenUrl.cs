using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    [SerializeField] private string _url = "https://default";


    public void OpenUrl_() => Application.OpenURL(_url);
}
