using CIDER.LoadIO;
using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDER.UnitTests
{
    public class LoadViewModelUnitTests
    {
        [Test]
        public void LoadViewModel_OnSelectClicked_CallsFolderSelector()
        {
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            FolderChecker folderChecker = Substitute.For<FolderChecker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO);

            viewModel.SelectClickCommand.Execute(this);

            manager.Received().SelectFolder();
        }

        [Test]
        public void LoadViewModel_OnSelectClickedError_ReceivesSelectorError()
        {
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            FolderChecker folderChecker = Substitute.For<FolderChecker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO);

            viewModel.SelectClickCommand.Execute(this);

            manager.ThrowError = true;

            viewModel.SelectClickCommand.Execute(this);

            Assert.AreEqual("", viewModel.PathText);
        }

        [Test]
        public void LoadViewModel_OnSelectClicked_ReceivesStringOfSelectedFile()
        {
            FolderManager manager = Substitute.For<FolderManager>();
            DataProvider dataProvider = Substitute.For<DataProvider>();
            FolderChecker folderChecker = Substitute.For<FolderChecker>();
            FileIO fileIO = Substitute.For<FileIO>();
            LoadViewModel viewModel = new LoadViewModel(dataProvider, folderChecker, manager, fileIO);

            viewModel.SelectClickCommand.Execute(this);

            Assert.AreEqual("return", manager.LastSelected);
        }

        public FolderManager GetFolderManager()
        {
            FolderManager manager = new FolderManager();

            return manager;
        }
    }

    public class FolderManager : IFolderSelectionInterface
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
}
