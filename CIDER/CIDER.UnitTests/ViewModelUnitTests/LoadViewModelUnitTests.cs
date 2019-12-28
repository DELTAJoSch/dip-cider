using CIDER.LoadIO;
using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDER.UnitTests.ViewModelUnitTests
{
    public class LoadViewModelUnitTests
    {
        [Test]
        public void LoadViewModel_OnSelectClicked_CallsFolderSelector()
        {
            //Arrange
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            FolderChecker folderChecker = Substitute.For<FolderChecker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO, Factories.GetMainWindowViewModelStub());

            //Act
            viewModel.SelectClickCommand.Execute(this);

            //Assert
            manager.Received().SelectFolder();
        }

        [Test]
        public void LoadViewModel_OnSelectClickedError_ReceivesSelectorError()
        {
            //Arrange
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            FolderChecker folderChecker = Substitute.For<FolderChecker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO, Factories.GetMainWindowViewModelStub());

            //Act
            viewModel.SelectClickCommand.Execute(this);

            manager.ThrowError = true;

            viewModel.SelectClickCommand.Execute(this);

            //Assert
            Assert.AreEqual("", viewModel.PathText);
        }

        [Test]
        public void LoadViewModel_OnSelectClicked_ReceivesStringOfSelectedFile()
        {
            //Arrange
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            FolderChecker folderChecker = Substitute.For<FolderChecker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO, Factories.GetMainWindowViewModelStub());

            //Act
            viewModel.SelectClickCommand.Execute(this);

            //Assert
            Assert.AreEqual("return", manager.LastSelected);
        }

        [Test]
        public void LoadViewModel_OnSelectClicked_CallsFolderChecker()
        {
            //Arrange
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            Checker folderChecker = Substitute.For<Checker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO, Factories.GetMainWindowViewModelStub());

            //Act
            viewModel.SelectClickCommand.Execute(this);

            //Assert
            folderChecker.Received().IsCorrectFolder("return");
        }

        [Test]
        public void LoadViewModel_OnSelectClickekCorrectFolder_SetsCheckmark()
        {
            //Arrange
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            Checker folderChecker = Substitute.For<Checker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO, Factories.GetMainWindowViewModelStub());

            //Act
            viewModel.SelectClickCommand.Execute(this);

            //Assert
            Assert.AreEqual(@"~\..\..\Icons\success.png", viewModel.CheckImage);
        }

        [Test]
        public void LoadViewModel_OnSelectClickekWrongFolder_SetsBlocked()
        {
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            Checker folderChecker = Substitute.For<Checker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO, Factories.GetMainWindowViewModelStub());

            folderChecker.ReturnTrue = false;

            viewModel.SelectClickCommand.Execute(this);

            Assert.AreEqual(@"~\..\..\Icons\forbidden.png", viewModel.CheckImage);
        }

        public FolderManager GetFolderManager()
        {
            FolderManager manager = new FolderManager();

            return manager;
        }
    }

    public class FolderManager : IFolderSelector
    {
        public FolderManager()
        {
            ThrowError = false;
            LastSelected = null;
        }

        public string LastSelected { get; private set; }

        public bool ThrowError { get; set; }
        public string SelectFolder()
        {
            if(ThrowError == false)
            {
                LastSelected = "return";
                return "return";
            }
            else
            {
                throw new FileDialogExitedException();
            }
        }
    }

    public class Checker : IChecker
    {
        public Checker()
        {
            ReturnTrue = true;
        }
        public bool ReturnTrue { get; set; }
        public bool IsCorrectFolder(string Path)
        {
            if (ReturnTrue)
                return true;
            return false;
        }
    }
}
