using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveManagerLibrary
{
    public class SaveManager
    {
        // Сохранение данных
        public static void Save(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public static void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        public static void Save(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        // Выгрузка данных
        public static int LoadInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
        public static float LoadFloat(string key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
        public static string LoadString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
        public static int LoadInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public static float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        public static string LoadString(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        // Удаление данных по ключу
        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        // Сброс всех данных
        public static void DeleteKeyAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
