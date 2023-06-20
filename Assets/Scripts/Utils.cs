using System;
using System.Threading.Tasks;

public static class Utils
{

    #region WaitMethod
    public static async void WaitForSeconds(float seconds, Action OnComplete)
    {
        await Task.Delay((int)seconds * 1000);
        OnComplete?.Invoke();
    }
    #endregion
}
