using System;

public static class GameEvents
{
    public static Action<bool> OnClickPlay;
    public static Action<int> OnSelectedBallType;

    public static Action<int> OnSelectedBallGrid;

    public static Action<int> OnSelectedRunsGrid;

    public static Action OnBallPhaseComplete;

    public static Action<int, int, bool> OnBatPhaseComplete;

}