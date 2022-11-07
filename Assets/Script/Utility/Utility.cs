using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Treeplla
{
    public class Utility
    {
        public static Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            Vector2 movePos;

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, null, out movePos);
            //Convert the local point to world point
            return parentCanvas.transform.TransformPoint(movePos);
        }

        public static Vector2 worldToLocalUISpace(Transform parentT, Vector3 worldPos)
        {
            //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            Vector2 movePos;

            //Convert the screenpoint to ui rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentT as RectTransform, screenPos, null, out movePos);
            //Convert the local point to world point
            
            return movePos;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp, bool _local = false)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            if(_local)
            {
                return origin.AddSeconds(timestamp).ToLocalTime();
            }
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private static char[] NumberChar = 
        {
            'a','b','c','d','e','f','g','h','i','j','k',
            'l','m','n','o','p','q','r','s','t','u','v',
            'w','x','y','z'
        };


        public static string CalculateMoneyToString(System.Numerics.BigInteger _Long)
        {	
            var targetString = _Long.ToString();
            var targetLen = targetString.Length - 1;
            if(targetLen == 0)
                targetLen = 1;
            var front =  targetLen / 3;
            var back = targetLen % 3;
            if(front == 0)
            {
                return _Long.ToString();
            }
            var top = targetString.Substring(0,back + 1);
            var top_back = targetString.Substring(back + 1, 1);

            var front_copy = front;
            if(front > 1378) // 26 + 26 * 26 + 26 * 26 + 26 * 26
            {
                front_copy = front_copy - 1378;
            }
            else if(front > 702) // 26 + 26 * 26
            {
                front_copy = front_copy - 702;
            }
            else if(front > 26)
            {
                front_copy = front_copy - 26;
            }
           
            var front_front = front_copy / 26;
            var front_second = front_copy % 26;
            
            char frontChar;
            if(front_front == 26)
                frontChar = 'z';
            else if(front_front >= 0 && front_front <= 26)
                frontChar = NumberChar[front_front];
            else
                frontChar = (char)0;

            char secondChar;
            if(front_second == 0)
                secondChar = 'z';
            else if(front_second > 0 && front_second < 26)
                secondChar = NumberChar[front_second - 1];
            else
                secondChar = (char)0;

            string final_numTostr = string.Empty;

            if(front > 1378) // 26 + 26 * 26 + 26 * 26 + 26 * 26
                final_numTostr = $"{char.ToUpper(frontChar)}{char.ToUpper(secondChar)}";
            else if(front > 702) // 26 + 26 * 26 + 26 * 26 + 26 * 26
                final_numTostr = $"{char.ToUpper(frontChar)}{secondChar}";
            else if(front > 26)            
                final_numTostr = $"{frontChar}{secondChar}";            
            else            
                final_numTostr = $"{secondChar}";
                
            return string.Format("{0}.{1}{2}",top,top_back,final_numTostr);
        }

        public static string GetTimeStringFormattingBoost(int seconds)
        {
            var time = new TimeSpan(0, 0, seconds);
                return $"{time.Seconds}s";
        }

        public static string GetTimeStringFormattingLongWithUnit(int seconds, string[] unit)
        {
            var time = new TimeSpan(0, 0, seconds);
            if (seconds >= 86400)
                return $"{time.Days:D2}{unit[0]}{time.Hours:D2}{unit[1]}{time.Minutes:D2}{unit[2]}{time.Seconds:D2}{unit[3]}";
            else if (seconds >= 3600)
                return $"{time.Hours:D2}{unit[1]}{time.Minutes:D2}{unit[2]}{time.Seconds:D2}{unit[3]}";
            //else if(seconds > 60)
            else
                return $"{time.Minutes:D2}{unit[2]}{time.Seconds:D2}{unit[3]}";
        }

        public static string GetTimeStringFormattingShort(int seconds) 
        {
            var time = new TimeSpan(0, 0, seconds);
            if (seconds >= 86400)
                return $"{time.Days}d {time.Hours}h";
            else if (seconds >= 3600)
                return $"{time.Hours}h {time.Minutes}m";
            else if (seconds > 59)
                return $"{time.Minutes}m {time.Seconds}s";
            else
                return $"{time.Seconds}s";      
        }

        public static string GetTimeStringFormattingStageClearSkill(int seconds)
        {
            var time = new TimeSpan(0, 0, seconds);
            if (seconds >= 86400)
                return $"{time.Days}.{time.Hours}d";
            else if (seconds >= 3600)
                return $"{time.Hours}.{time.Minutes}h";
            else if (seconds > 59)
                return $"{time.Minutes}m {time.Seconds}s";
            else
                return $"{time.Seconds}s";
        }


        public static string GetTimeStringFormattingOne(int seconds)
        {
            var time = new TimeSpan(0, 0, seconds);
            if (seconds >= 86400)
                return $"{time.Days}d";
            else if (seconds >= 3600)
                return $"{time.Hours}h";
            else if (seconds > 59)
                return $"{time.Minutes}m";
            else
                return $"{time.Seconds}s";
        }

        public static string GetTimeHour(int seconds)
        {
            var time = new TimeSpan(0, 0, seconds);
            return $"{time.Hours}h";
        }

        public static string GetTimeStringFormattingLong(int seconds)
        {
            var time = new TimeSpan(0, 0, seconds);
            if(seconds >= 86400)
                return $"{time.Days}d {time.Hours}h {time.Minutes}m {time.Seconds}s";
            else if(seconds >= 3600)
                return $"{time.Hours}h {time.Minutes}m {time.Seconds}s";
            //else if(seconds > 60)
            else
                return $"{time.Minutes}m {time.Seconds}s";
        }

        public static void SetActiveCheck(GameObject target, bool value)
        {
            if(target != null)
            {
                if(value && !target.activeSelf)
                    target.SetActive(true);
                else if(!value && target.activeSelf)
                    target.SetActive(false);
            }
        }


        public static void SetInterActiveCheck(Button target , bool value)
        {
            if(target != null)
            {
                if (value && !target.IsInteractable())
                    target.interactable = true;
                else if (!value && target.IsInteractable())
                    target.interactable = false;
            }
        }

        public static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            Debug.Log("Getting device inches: " + diagonalInches);

            return diagonalInches;
        }
    }
}
