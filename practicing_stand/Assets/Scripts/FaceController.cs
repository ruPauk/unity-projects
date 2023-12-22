using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour, IStateMachineOwner
{
    [SerializeField] private Sprite _neutralSprite;
    [SerializeField] private Sprite _happySprite;
    [SerializeField] private Sprite _sadSprite;
    private Dictionary<FaceStateEnum, Sprite> _faceSpritesStates = new();
    private SpriteRenderer _faceSpriteRenderer;

    private void Awake()
    {
        _faceSpriteRenderer = GetComponent<SpriteRenderer>();
        _faceSpritesStates.Add(FaceStateEnum.Neutral, _neutralSprite);
        _faceSpritesStates.Add(FaceStateEnum.Happy, _happySprite);
        _faceSpritesStates.Add(FaceStateEnum.Sad, _sadSprite);
    }

    public void SetFace(FaceStateEnum faceState)
    {
        _faceSpriteRenderer.sprite = _faceSpritesStates[faceState];
    }

    public float GetDistance()
    {
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var distance = Vector2.Distance(position, gameObject.transform.position);
        return distance;
    }
}
