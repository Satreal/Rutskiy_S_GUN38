using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/CellPaletteSettings")]
public class CellPaletteSettings : ScriptableObject
{
    [field:SerializeField]
    [field:Tooltip("Клетка выбрана")]
    public Material SelectMaterial { get; private set; }

    [field: SerializeField]
    [field: Tooltip("Клетка для передвижения/удара")]
    public Material MoveMaterial { get; private set; }

    


    [field: SerializeField]
    [field: Tooltip("Клетка для передвижения И удара")]
    public Material MoveAndAtackMaterial {  get; private set; }


}
