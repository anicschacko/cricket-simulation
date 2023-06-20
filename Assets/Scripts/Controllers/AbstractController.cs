using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
    public abstract void AddListeners();
    public abstract void RemoveListeners();
    public abstract void FetchConfig();
    public abstract void Init();

    void Awake()
    {
        FetchConfig();
        AddListeners();
    }

    void Start()
    {
        Init();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }
}
