using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Steer73.FormsApp.Utilities
{
    public class Functions
    {
        internal static bool CheckNetwork()
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                return false;

            return true;
        }


        public static Color GetRandomColor()
        {
            Color[] colors = {
                Color.Accent,
                Color.AliceBlue,
                Color.LightGreen,
                Color.AntiqueWhite,
                Color.PaleVioletRed,
                Color.DarkOrange,
                Color.Pink,
                Color.Tan,
                Color.BurlyWood,
                Color.Purple,
                Color.LightGray,
            };

            Random rnd = new Random();
            int index = rnd.Next(0, colors.Length - 1);

            return colors[index];
        }

    }
}
