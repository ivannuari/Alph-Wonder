using GaweDeweStudio;
using UnityEngine;

public class Level1Card : Level2Card
{
    protected override void PlayLevel()
    {
        Level1Controller.Instance.SetLevel(level);
        GameManager.Instance.ChangeState(GameState.Game);
    }
}
