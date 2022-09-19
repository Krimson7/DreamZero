using UnityEngine;

public interface I_PlayerFormListener{
    void OnEnable();
    void OnDisable();
    void OnPlayerFormChange(Player player);
}