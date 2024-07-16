using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Fighter SO")]
public class FighterSO : ScriptableObject
{
  [SerializeField] public string fighterName;
  [SerializeField] public Sprite fighterIcon;
}
