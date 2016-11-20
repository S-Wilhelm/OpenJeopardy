using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace OpenJeopardy
{
    /// <summary>
    /// Interaction logic for GameBoardTemp.xaml
    /// </summary>
    public partial class GameBoardTemp : Window
    {
        public GameBoardTemp()
        {
            InitializeComponent();
        }

        // FIXME This class is a temporary fix to get a working product to the user
        public GameBoardTemp(GameModel model) : this()
        {
            this.model = model;
            DrawGrid();
        }

        private GameModel model { get; set; }

        private void DrawGrid()
        {
            Int32 rowCount = model.Categories.Max(category => category.QuestionEntries.Count);
            Int32 columnCount = model.Categories.Count;

            for (var i = 0; i < rowCount + 1; i++)
            {
                GameGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (var i = 0; i < columnCount; i++)
            {
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            GameGrid.UpdateLayout();

            for (var c = 0; c < columnCount; c++)
            {
                var columnHeader = new Label()
                {
                    Content = model.Categories[c].Heading,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    FontSize = 48
                };
                Grid.SetColumn(columnHeader, c);
                Grid.SetRow(columnHeader, 0);
                GameGrid.Children.Add(columnHeader);

                for (var r = 0; r < rowCount; r++)
                {
                    ExtendedQuestionEntry questionEntry = model.Categories[c].QuestionEntries[r];
                    var rowLabel = new Label()
                    {
                        Content = questionEntry.PointValue,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 48
                    };
                    rowLabel.MouseDown += (sender, args) =>
                    {
                        // This needs to show the question text, then, upon click, show the answer, before returning to the main screen and hiding this label
                        var questionDisplay = new QuestionDisplayTemp(questionEntry.Question, questionEntry.Answer);
                        questionDisplay.ShowDialog();
                        rowLabel.Visibility = Visibility.Hidden;
                        // TODO This should update the 'answered' property of the question entry
                    };
                    Grid.SetColumn(rowLabel, c);
                    Grid.SetRow(rowLabel, r + 1);
                    GameGrid.Children.Add(rowLabel);
                }
            }
        }

        private void GameBoardTemp_OnPreviewKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
    }
}