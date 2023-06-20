public class CMBallTypeButton : CMGenericButton
{
    protected override void Awake()
    {
        base.Awake();
        button.onClick.AddListener(() => GameEvents.OnSelectedBallType?.Invoke(value));
    }
}
