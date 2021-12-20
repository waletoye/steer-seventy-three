using System;
using Xamarin.Forms;

namespace Steer73.FormsApp.Model
{
    public class UserPlus : User
    {
        public string Initials => $"{FirstName[0]}{LastName[0]}";

        public string FullName => $"{FirstName} {LastName}";

        public string Email
        {
            get
            {
                string email = $"{FirstName[0]}.{LastName}@steer73.com";
                return email.ToLower();
            }
        }

        public Color Color => Utilities.Functions.GetRandomColor();

        public bool IsMarked { get; internal set; }
    }
}
