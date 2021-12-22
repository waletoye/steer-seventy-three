using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;

namespace Steer73.FormsApp.ViewModels
{
    public class UsersViewModel : ViewModel, INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public UsersViewModel(IUserService userService, IMessageService messageService)
        {
            _userService = userService;
            _messageService = messageService;
        }

        public async Task Initialize()
        {
            IEnumerable<User> users = new List<User>();

            try
            {
                DetailedUsers = new ObservableCollection<UserPlus>();

                IsBusy = true;

                users = await _userService.GetUsers();


                users = users.OrderBy(x => x.FirstName).OrderBy(y => y.LastName);


                foreach (var user in users)
                {
                    DetailedUsers.Add(new UserPlus
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                    });
                }

                DetailedUsers = new ObservableCollection<UserPlus>(DetailedUsers.OrderBy(x => x.FullName).ToList());

            }
            catch (Exception ex)
            {
                await _messageService.ShowError(ex.Message);
            }
            finally
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetailedUsers"));
                IsBusy = false;
            }
        }

        internal void RemoveUser(object context)
        {
            var user = DetailedUsers.Where(x => x == context).FirstOrDefault();

            if (user == null)
                return;

            DetailedUsers.Remove(user);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetailedUsers"));
        }

        internal void RemoveFirstUser()
        {
            //remove first element
            DetailedUsers.RemoveAt(0);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetailedUsers"));
        }

        public bool IsBusy { get; set; }

        //original data
        public ObservableCollection<UserPlus> DetailedUsers { get; set; }

        //source, sorted
        public ObservableCollection<UserPlus> UserSource { get; set; }


        //public ICollection<User> Users { get; } = new List<User>();
        //public ICollection<UserPlus> DetailedUsers { get; set; } = new List<UserPlus>();

        public override event PropertyChangedEventHandler PropertyChanged;

        string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                DetailedUsers = new ObservableCollection<UserPlus>(DetailedUsers.Where(x => x.FullName.Contains(searchText)).ToList());
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DetailedUsers"));
            }
        }
    }
}
