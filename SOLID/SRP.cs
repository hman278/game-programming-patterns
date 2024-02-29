// Single-responsiblity

public class PlayerInput() : MonoBehaviour
{
    // Code ...
}

public class PlayerMover() : MonoBehaviour
{
    // Code ...
}

public class PlayerAudio() : MonoBehaviour
{
    // Code ...
}

[RequireComponent(typeof(PlayerInput), typeof(PlayerMover), typeof(PlayerAudio))]
public class Player() : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerAudio _playerAudio;
}
