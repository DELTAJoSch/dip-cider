using System.Windows.Forms;

namespace CIDER.LoadIO
{
    public class FolderSelector : FolderSelectionInterface
    ///Summary
    ///This class implements the Folderselectioninterface. This class can show a Userinterface allowing the user to select a folder.
    ///If the user exits the dialog without selecting a folder, an exception will be thrown
    {
        private string _lastSelected;
        public string LastSelected { get { return _lastSelected; } private set { _lastSelected = value; } }
        //last selected path

        public string SelectFolder()
        // Shows the Dialog and lets the user select the file
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            browserDialog.Description = "Select CIDER Folder";
            browserDialog.ShowNewFolderButton = false;

            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                return browserDialog.SelectedPath;
            }
            else
            {
                throw new FileDialogExitedException();
            }
        }
    }

    public interface FolderSelectionInterface
    ///Summary
    ///The Interface for the FolderSelcetion. Can be used as a seam for unit testing
    {
        string SelectFolder();

        string LastSelected { get; }
    }
}