using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BanpoFri
{
    public static class LocalizeExtention
    {
        public static string GetString(this BanpoFri.Localize tableData, string key , bool IsEmptyReturn = false)
        {
            var data = tableData.GetData(key);
            if(data == null && !IsEmptyReturn) 
            {
                return $"empty!!{key}";
            }
            else if(data == null && IsEmptyReturn)
            {
                return "";
            }
            switch(GameRoot.Instance.UserData.Language)
            {
                case Config.Language.en:
                    return data.en;
                case Config.Language.ko:
                    return data.ko;
                case Config.Language.ja:
                    return data.ja;
                case Config.Language.th:
                    return data.th;
                case Config.Language.de:
                    return data.de;
                case Config.Language.fr:
                    return data.fr;
                default:
                    return data.en;
            }
        }

        public static string GetFormat(this BanpoFri.Localize tableData, string key, params object[] args)
        {
            var str = GetString(tableData, key);
            return string.Format(str, args);
        }
    }
}