using System.Linq;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private string _typeName = "default";


    public string GetTypeName() { return _typeName; }

    private void Start()
    {
        DontDestroyOnLoad[] similarObjects = FindObjectsOfType<DontDestroyOnLoad>().ToArray();
        foreach (var item in similarObjects)
        {
            if (item != this && item.GetTypeName() == this.GetTypeName())
            {
                this.gameObject.SetActive(false);
                return;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
