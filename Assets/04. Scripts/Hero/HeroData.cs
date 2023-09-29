using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable/HeroData", fileName = "Hero Data")]
public class HeroData : ScriptableObject
{
    public int heroIndex = 0;
    public Material heroImage;
    public string heroName = "";
    public string heroAbility = "";
    public int heroLevel = 1;
    public SnowMan snowMan;
    public SnowManData snowManData;

}
