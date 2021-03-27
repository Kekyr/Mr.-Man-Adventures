using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextLocalizer : MonoBehaviour
{
    public static string CurrentLanguage = "english";//язык по дефолту- английский

    public static TMP_FontAsset CurrentFont;

    public static int CurrentFontSize;

    public TMP_FontAsset russian;

    public TMP_FontAsset english;


    private static Dictionary<string, Dictionary<string, string>> Translations = new Dictionary<string, Dictionary<string, string>>()
    {
        ["russian"] = new Dictionary<string, string>()
        {
            ["menu_startgame"] = "Начать игру",
            ["menu_settings"] = "Настройки",
            ["menu_music"] = "Музыка",
            ["menu_sfx"] = "Звуки",
            ["menu_language"] = "Русский",
            ["menu_back"] = "Назад",
            ["menu_credits"] = "Титры",
            ["menu_quit"] = "Выйти",
            ["menu_main"] = "Главное меню",
            ["menu_continue"] = "Продолжить",
            ["menu_restart"] = "Начать заново",
            ["menu_gameover"] = "Игра окончена",
            ["menu_ending"] = "Поздравляю! Ты сделал это! Судя по всему ты ас в играх." +
            " Буду очень благодарен за отзыв! И спасибо за игру!",



            ["introduction_0"] = "Это был самый обычный день. Mr.Man и " +
            "Twiggy просто стояли у дерева и болтали.",
            ["introduction_1"] = "Внезапно, Mr. Man почувствовал запах " +
            "чего-то вкусного и пошел на него. Twiggy была немного удивлена.",
            ["introduction_2"] = "Затем Mr.Man увидел к чему ведет " +
            "этот прекрасный запах - к стопке вкусных блинчиков с маслицем.",
            ["introduction_3"] = "Twiggy была в шоке от происходящего.",
            ["introduction_4"] = "Вдруг из кустов вышел орк. Он был очень рад встрече с Twiggy...",
            ["introduction_5"] = "Когда Mr.Man вернулся к дереву, он наткнулся на табличку, Twiggy написала на ней, " +
            "что ее похитил злобный орк.",
            ["introduction_6"] = "В начале ему стало грустно и обидно.",
            ["introduction_7"] = "Но через какое-то время он воспрял духом и " +
            "решил во что бы то ни стало спасти Twiggy!",

            ["tutorial_0"] = "Используйте кнопки A и D, чтобы перемещаться по миру игры.",
            ["tutorial_1"] = "Нажмите на SPACE, чтобы подпрыгнуть.",
            ["tutorial_2"] = "Нажмите на E, чтобы ударить противника.",
            ["tutorial_3"] = "Вы можете убивать противника двумя способами: ударом кулака и прыжком.",
            ["tutorial_4"] = "Чтобы купить еду в конце уровня нажмите на Q.",
        },

        ["english"] = new Dictionary<string, string>()
        {
            ["menu_startgame"] = "start game",
            ["menu_settings"] = "settings",
            ["menu_music"] = "music",
            ["menu_sfx"] = "sfx",
            ["menu_language"] = "english",
            ["menu_back"] = "back",
            ["menu_credits"] = "credits",
            ["menu_quit"] = "quit",
            ["menu_main"] = "main menu",
            ["menu_continue"] = "resume",
            ["menu_restart"] = "restart",
            ["menu_gameover"] = "game over",
            ["menu_ending"] = "Congratulations! You did this! You are a true hardcore gamer." +
            " Besides,thank you for playing! I will appreciate any feedback.",

            ["introduction_0"] = "It was a usual day. Mr.Man and Twiggy were walking in " +
            "the forest and talking.",
            ["introduction_1"] = "Suddenly Mr. Man felt some delicious smell " +
            "and went to it. Twiggy was a bit surprised. ",
            ["introduction_2"] = "Then Mr.Man saw where it leads - " +
            "to a stack of pancakes with butter.",
            ["introduction_3"] = "Twiggy was shocked by what was happening.",
            ["introduction_4"] = "At once an orc came out of the bushes. " +
            "He was very glad to meet Twiggy...",
            ["introduction_5"] = "When Mr.Man returned to the tree, " +
            "he found a plate, it said \"I was kidnapped\".",

            ["introduction_6"] = "At first, he felt sad and hurt.",
            ["introduction_7"] = "But after some time, he perked up and " +
            "decided at all costs to save Twiggy.",

            ["tutorial_0"] = "Use the A, D buttons to move around the game world.",
            ["tutorial_1"] = "Use SPACE to jump.",
            ["tutorial_2"] = "Use E to punch enemy.",
            ["tutorial_3"] = "You can kill opponents in two ways: with a punch and a jump.",
            ["tutorial_4"] = "Use Q to buy food at the end of the level.",
        }
    };

    private void Awake()
    {
        CheckFont();
    }

    public static string ResolveStringValue(string id)
    {
        return Translations[CurrentLanguage][id];
    }

    private void CheckFont()
    {
        if (TextLocalizer.CurrentLanguage == "russian")
        {
            CurrentFont = russian;

            if (SceneManager.GetActiveScene().buildIndex >= 3)
            {
                TextLocalizer.CurrentFontSize = 73;
            }
            else
            {
                TextLocalizer.CurrentFontSize = 85;
            }
        }
        else
        {
            CurrentFont = english;

            TextLocalizer.CurrentFontSize = 120;
        }
    }


}

