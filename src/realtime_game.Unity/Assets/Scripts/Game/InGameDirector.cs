using UnityEngine;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using MagicOnion.Client;
using MagicOnion;
using realtime_game.Shared.Interfaces.Services;
using realtime_game.Shared.Models.Contexts;
using Shared.Interfaces.StreamingHubs;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class InGameDirector : MonoBehaviour
{
    [SerializeField] GameObject me;
    [SerializeField] GameObject otherCharacterPrefab;
    [SerializeField] Button join;
    [SerializeField] Button leave;
    //[SerializeField] RoomModel roomModel;
    RoomModel roomModel;
    UserModel userModel;
    PlayerManager pm;
    Dictionary<Guid, GameObject> characterList = new Dictionary<Guid, GameObject>();
    public TMP_InputField roomNameInput;
    public TMP_InputField inputId;
    public Button leaveButton;
    private int myUserId = 4;
    private bool ready = false;
    User myself;
    [SerializeField] CameraPlayerTracker mt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 自分以外のユーザーの移動を反映
    private void OnMoveUser(Guid connectionId, Vector3 pos, Quaternion rot)
    {
        // いない人は移動できない
        if (!characterList.ContainsKey(connectionId))
        {
            return;
        }

        // DOTweenを使うことでなめらかに動く！
        characterList[connectionId].transform.DOMove(pos, 0.07f);
        characterList[connectionId].transform.position = pos;
        //roomModel.OnMoveCharacter = null;
    }
}
