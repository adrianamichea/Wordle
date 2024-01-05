using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Wordle.ViewModels;

namespace WordleTests
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        public void RelayCommandT_CanExecute_True()
        {
            // Arrange
            var command = new RelayCommand<string>(_ => { });

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void RelayCommandT_CanExecute_NullCanExecute_True()
        {
            // Arrange
            var command = new RelayCommand<string>(_ => { }, null);

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void RelayCommandT_Execute_CallsExecute()
        {
            // Arrange
            bool executed = false;
            var command = new RelayCommand<string>(_ => executed = true);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void RelayCommandT_RaiseCanExecuteChanged_InvokesEvent()
        {
            // Arrange
            var command = new RelayCommand<string>(_ => { });
            bool eventInvoked = false;
            command.CanExecuteChanged += (_, __) => eventInvoked = true;

            // Act
            command.RaiseCanExecuteChanged();

            // Assert
            Assert.IsTrue(eventInvoked);
        }

        [TestMethod]
        public void RelayCommand_CanExecute_True()
        {
            // Arrange
            var command = new RelayCommand(() => { });

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void RelayCommand_CanExecute_NullCanExecute_True()
        {
            // Arrange
            var command = new RelayCommand(() => { }, null);

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void RelayCommand_Execute_CallsExecute()
        {
            // Arrange
            bool executed = false;
            var command = new RelayCommand(() => executed = true);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void RelayCommand_RaiseCanExecuteChanged_InvokesEvent()
        {
            // Arrange
            var command = new RelayCommand(() => { });
            bool eventInvoked = false;
            command.CanExecuteChanged += (_, __) => eventInvoked = true;

            // Act
            command.RaiseCanExecuteChanged();

            // Assert
            Assert.IsTrue(eventInvoked);
        }
    }
}
