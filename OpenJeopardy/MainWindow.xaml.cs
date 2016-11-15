using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Win32;

using CH = ConfigHandler;

namespace OpenJeopardy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const Int32 SupportedFormatVersion = 1;

        // REFINE This should be localizable, and tied to the default used in the XAML
        private String NoFileSelectedText = "Please select a file";

        public MainWindow()
        {
            if (!Directory.Exists(CH.ConfigHandler.DEFAULT_QUESTION_DIRECTORY))
            {
                try
                {
                    Directory.CreateDirectory(CH.ConfigHandler.DEFAULT_QUESTION_DIRECTORY);
                }
                catch (Exception ex)
                {
                    // Should be able to continue without the directory, though UX may suffer
                    Debug.WriteLine(ex);
                }
            }

            InitializeComponent();

            // TODO Have the program remember the last question set used and automatically select it
        }

        private void SelectQuestions(Object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                DefaultExt = ".json",
                Filter = "JSON|*.json",
                InitialDirectory = CH.ConfigHandler.DEFAULT_QUESTION_DIRECTORY
            };

            Boolean? result = fileDialog.ShowDialog();

            // This explicit comparison is needed since 'result' is nullable
            if (result == true)
            {
                SetSelectedQuestions(fileDialog.FileName);
            }
        }

        private void SetSelectedQuestions(String filename)
        {
            if (File.Exists(filename))
            {
                StartGame.IsEnabled = true;
                SelectedFileName.Text = filename;
            }
            else
            {
                StartGame.IsEnabled = false;
                SelectedFileName.Text = NoFileSelectedText;
            }
        }

        private void LoadGame(Object sender, RoutedEventArgs e)
        {
            // Ensure that a file is selected
            // (Yes, we should be able to trust our own code, but...)
            if (String.Equals(NoFileSelectedText, SelectedFileName.Text, StringComparison.OrdinalIgnoreCase) ||
                !File.Exists(SelectedFileName.Text))
            {
                MessageBox.Show("A question file must be selected to begin.", "Question File Not Selected",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                StartGame.IsEnabled = false;
                return;
            }

            // Deserialize the file
            CH.ConfigWrapper questions;
            try
            {
                questions = CH.ConfigHandler.DeserializeStream(SelectedFileName.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                ReportQuestionLoadError();
                return;
            }

            if (questions == null)
            {
                ReportQuestionLoadError();
                return;
            }

            // Make sure the questions aren't from a newer version of the tool
            if (questions.FormatRevision > SupportedFormatVersion)
            {
                MessageBox.Show("The questions were generated with a newer version" + Environment.NewLine
                                + "of the software.  Please upgrade to the latest version.");
                return;
            }

            // Questions were loaded, and we support the version; set up the game board.
            PrepareGameBoard(questions);
        }

        private void PrepareGameBoard(CH.ConfigWrapper questionConfig)
        {
            // Get the number of rows and categories
            List<CH.QuestionCategory> categories = questionConfig.Categories;
            Int32[] points = questionConfig.PointValues;

            // TODO Define the game window class
        }

        private static void ReportQuestionLoadError()
        {
            MessageBox.Show("An error ocurred while loading the questions." + Environment.NewLine
                            + "Please try again.  If the issue continues," + Environment.NewLine
                            + "contact support.", "Error Loading Questions", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
        }
    }
}