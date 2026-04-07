using GaweDeweStudio;
using UnityEngine;

public class Level3Card : Level2Card
{
    protected override void PlayLevel()
    {
        Level3Controller.Instance.SetLevel(level);
        GameManager.Instance.ChangeState(GameState.Game);
    }
}
