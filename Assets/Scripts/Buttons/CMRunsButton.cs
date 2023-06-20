public class CMRunsButton : CMGenericButton
{
    protected override void Awake()
    {
        base.Awake();
        button.onClick.AddListener(() => GameEvents.OnSelectedRunsGrid?.Invoke(value));
    }
}
