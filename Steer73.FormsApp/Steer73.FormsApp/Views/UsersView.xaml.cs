using System.Threading.Tasks;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using Steer73.FormsApp.ViewModels;
using Xamarin.Forms;

namespace Steer73.FormsApp.Views
{
    public partial class UsersView : ContentPage
    {
        UsersViewModel vm;

        public UsersView()
        {
            InitializeComponent();

            frmLoader.IsVisible = false;
            BindingContext = vm = new UsersViewModel(new UserService(), new MessageService());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            IsLoading = true;
            lblError.Text = "Loading users directory...";

            await vm.Initialize();

            IsLoading = false;
            lblError.Text = "No users available...";
        }

        void OnDeleteSwipeItemInvoked(System.Object sender, System.EventArgs e)
        {
            var context = (sender as SwipeItem).BindingContext;
            vm.RemoveUser(context);
        }

        private bool isLoading;
        private bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                if (value)
                {
                    frmLoader.IsVisible = true;
                    actInd.IsRunning = true;
                    //filterIcon.IsVisible = false;
                }
                else
                {
                    frmLoader.IsVisible = false;
                    actInd.IsRunning = false;
                    //filterIcon.IsVisible = true;
                }
            }
        }
    }
}
