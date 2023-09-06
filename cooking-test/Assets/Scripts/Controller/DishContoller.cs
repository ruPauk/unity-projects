using UnityEngine;
using UnityEngine.EventSystems;

public class DishContoller : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Button {this.color} is pressed");

    }

    //private delegate void OnPointerClickHandler(PointerEventData eventData);

    private DishModel dishModel;
    [SerializeField] public DishModel.DishColor color;

    public DishContoller()
    {
        //dishModel = new DishModel(color);
    }


}
