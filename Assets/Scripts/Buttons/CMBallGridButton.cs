public class CMBallGridButton : CMGenericButton
{
    protected override void Awake() 
    {
        base.Awake();
        button.onClick.AddListener(() => GameEvents.OnSelectedBallGrid?.Invoke(value));
    }
}
