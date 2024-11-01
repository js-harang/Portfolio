using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    public static KeyManager kM;

    // 입력 액션 에셋
    public InputActionAsset inputActionAsset;
    private Dictionary<string, InputAction> inputMappings = new Dictionary<string, InputAction>();

    private void Awake()
    {
        if (kM == null)
            kM = this;
        else
            Destroy(gameObject);

        // 다른씬 이동시 파괴되지 않게
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitializeInputMappings(inputActionAsset, inputMappings);
    }

    /// <summary>
    /// 입력 액션 초기화
    /// </summary>
    /// <param name="inputActions">에셋</param>
    /// <param name="mappings">매핑</param>
    public void InitializeInputMappings(InputActionAsset inputActions, Dictionary<string, InputAction> mappings)
    {
        if (inputActions)
        {
            foreach (var action in inputActions)
            {
              mappings[action.name] = action; // 모든 액션 추가
            }
        }
    }

    /// <summary>
    /// 키 변경
    /// </summary>
    /// <param name="actionName">변경할 액션 이름</param>
    /// <param name="newKey">새로 변경 할 키</param>
    public void ChangeInputBinding(string actionName, Key newKey)
    {
        if (inputMappings.TryGetValue(actionName, out var action))
        {
            action.ApplyBindingOverride("<Keyboard>/" + newKey.ToString()); // 키 변경 적용
        }
    }

    /// <summary>
    /// 특정 액션의 발동 여부 확인
    /// </summary>
    /// <param name="actionName">액션 이름</param>
    /// <returns>액션 발동 여부</returns>
    public bool IsActionTriggered(string actionName)
    {
        return inputMappings.TryGetValue(actionName, out var action) && action.triggered; // 액션 발동 확인
    }

    /// <summary>
    /// 입력 액션 상태 확인
    /// </summary>
    /// <param name="actionName">액션 이름</param>
    /// <returns>액션의 현재 상태</returns>
    public float GetActionValue(string actionName)
    {
        return inputMappings.TryGetValue(actionName, out var action) ? action.ReadValue<float>() : 0f; // 액션 값 반환
    }
}
