using UnityEngine;
using UnityEngine.UI;

public abstract class CMGenericButton : MonoBehaviour
{
    [Tooltip("Value can either be the exact value or id of a specific data")]
    [SerializeField] protected int value;

    protected Button button;

    protected virtual void Awake()
    {
        button = this.gameObject.GetComponent<Button>();
    }
}