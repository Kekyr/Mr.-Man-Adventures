using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLocalizer : MonoBehaviour
{
    public static string CurrentLanguage = "russian";

    private static Dictionary<string, Dictionary<int, string>> Translations = new Dictionary<string, Dictionary<int, string>>()
    {
        ["russian"] = new Dictionary<int, string>()
        {
            [0] = "Чтобы подпрыгнуть нажмите на SPACE.",
            [1] = "Чтобы ударить противника нажмите на E.",
            [2] = "Вы можете убивать противника двумя способами: ударом кулака и прыжком.",
            [3] = "Чтобы купить еду в конце уровня нажмите на Q.",
        }
        //["english"] = new Dictionary<string, string>()
        //{
        //    ["menu_resume"] = "Reanudar",
        //    ["menu_settings"] = "Configuraciones",
        //    ["menu_exit"] = "Salida",
        //}
    };

    [SerializeField] int id;

    //private void Start()
    //{
    //    GetComponent<Text>().text = ResolveStringValue(id);
    //}

    //private void OnValidate()
    //{
    //    GetComponent<Text>().text = ResolveStringValue(id);
    //}

    public static string ResolveStringValue(int id)
    {
        return Translations[CurrentLanguage][id];
    }

    
}

