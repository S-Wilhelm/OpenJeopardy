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
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        public GameBoard()
        {
            InitializeComponent();
        }

        public GameBoard(GameModel model) : this()
        {
            DisplayGrid.ItemsSource = model.Categories;
        }

        private void DisplayGrid_OnAutoGeneratingColumn(Object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
