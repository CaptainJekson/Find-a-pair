using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Card _card;

    private GridLayoutGroup _gridLayoutGroup;

    private void Awake()
    {
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _levelConfig.Height;

        CreateLevel();
    }

    private void CreateLevel()
    {
        ChangeCellSize();
        
        foreach (var item in _levelConfig.LevelField)
        {
            var newCard = Instantiate(_card, transform.position, Quaternion.identity);

            newCard.transform.SetParent(transform, false);

            if (item == false)
            {
                newCard.IsEmpty = true;
                newCard.GetComponent<Image>().enabled = false;
                newCard.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void ChangeCellSize()    //TODO
    {
        var reducer = _levelConfig.Height > _levelConfig.Width ? _levelConfig.Height : _levelConfig.Width;
        var numberOfReductions = reducer / 2; //TODO

        for (var i = 0; i < numberOfReductions - 1; i++)
        {
            Debug.Log(numberOfReductions);
            
            _gridLayoutGroup.cellSize *= 0.5f;
        }
    }
}