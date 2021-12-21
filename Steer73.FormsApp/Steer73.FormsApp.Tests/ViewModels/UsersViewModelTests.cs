using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Steer73.FormsApp.Framework;
using Steer73.FormsApp.Model;
using Steer73.FormsApp.ViewModels;

namespace Steer73.FormsApp.Tests.ViewModels
{
    [TestFixture]
    public class UsersViewModelTests
    {
        [Test]
        public async Task InitializeFetchesTheData()
        {
            var userService = new Mock<IUserService>();
            var messageService = new Mock<IMessageService>();

            var viewModel = new UsersViewModel(
                userService.Object,
                messageService.Object);

            userService
                .Setup(p => p.GetUsers())
                .Returns(Task.FromResult(Enumerable.Empty<User>()))
                .Verifiable();

            await viewModel.Initialize();

            userService.VerifyAll();
        }


        [Test]
        public async Task InitializeFetchesTheData_2()
        {
            var userService = new Mock<IUserService>();
            var messageService = new Mock<IMessageService>();

            var objectsList = new List<Model.User>() {
                new User(){FirstName = "Adewale", LastName = "Adetoye"}
                 };

            userService
                .Setup(p => p.GetUsers())
                .Returns(Task.FromResult<IEnumerable<Model.User>>(objectsList))
                .Verifiable();

            var viewModel = new UsersViewModel(
                userService.Object,
                messageService.Object);

            await viewModel.Initialize();


            userService.Verify(p => p.GetUsers());

            //should fail, since they are equal
            CollectionAssert.AreEqual(objectsList[0].FirstName, viewModel.DetailedUsers[0].FirstName);
        }


        [Test]
        public async Task InitializeShowErrorMessageOnFetchingError()
        {
            var userService = new Mock<IUserService>();
            var messageService = new Mock<IMessageService>();

            var viewModel = new UsersViewModel(
                null,
                messageService.Object);

            userService
                .Setup(p => p.GetUsers())
                .Returns(Task.FromResult(Enumerable.Empty<User>()))
                .Verifiable();

            await viewModel.Initialize();

            userService.VerifyAll();
        }
    }
}
