using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countdown
{
    //Set all default constants too maintain style throughout the app
    internal class DefaultConstants
    {

        public const int PRIMARY_TYPE = 1,
                         SECONDARY_TYPE = 2,
                         TERTIARY_TYPE = 3;

        public const String LM_BACKGROUND = "#b3a794",

                            LM_BUTTON_BACKGROUND_COLOR_PRIMARY = "#cfc8b8",
                            LM_BUTTON_BACKGROUND_COLOR_SECONDARY = "#d9caa3",
                            LM_BUTTON_BACKGROUND_COLOR_TERTIARY = "#c7c4bd",

                            LM_BLUE = "#3a97e8";

        public const String DM_BACKGROUND = "#7a684b",

                            DM_BUTTON_BACKGROUND_COLOR_PRIMARY = "#7d7b74",
                            DM_BUTTON_BACKGROUND_COLOR_SECONDARY = "#6b6659",
                            DM_BUTTON_BACKGROUND_COLOR_TERTIARY = "#6b6b69",

                            DM_BLUE = "#097dab";

    public static string GetBackgroundColor()
    {
         if (IsDarkMode())
         {
            return DM_BACKGROUND;
         }

        return LM_BACKGROUND;
     }

     public static string GetButtonBackgroundColor(int type)
     {

         switch (type)
         {
            case PRIMARY_TYPE:
            if (IsDarkMode())
            {
                return DM_BUTTON_BACKGROUND_COLOR_PRIMARY;
            }

            return LM_BUTTON_BACKGROUND_COLOR_PRIMARY;
            
            case SECONDARY_TYPE:
            if (IsDarkMode())
            {
                return DM_BUTTON_BACKGROUND_COLOR_SECONDARY;
            }

             return LM_BUTTON_BACKGROUND_COLOR_SECONDARY;

            case TERTIARY_TYPE:
            if (IsDarkMode())
            {
                return DM_BUTTON_BACKGROUND_COLOR_TERTIARY;
            }

            return LM_BUTTON_BACKGROUND_COLOR_TERTIARY;
         }

            if (IsDarkMode())
            {
                return DM_BUTTON_BACKGROUND_COLOR_PRIMARY;
            }

            return LM_BUTTON_BACKGROUND_COLOR_PRIMARY;
     }
        public static bool IsDarkMode()
        {
            return Preferences.Default.Get("dark_mode", false);
        }
    }
}
