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
    /// Interaction logic for QuestionDisplayTemp.xaml
    /// </summary>
    public partial class QuestionDisplayTemp : Window
    {
        public QuestionDisplayTemp()
        {
            InitializeComponent();
        }

        public QuestionDisplayTemp(String q, String a) : this()
        {
            question = q;
            answer = a;

            DisplayedText.Text = question;
        }

        private String question { get;set; }
        private String answer { get; set; }

        private Boolean showingAnswer = false;

        private void QuestionDisplayTemp_OnKeyDown(Object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }

            if (!showingAnswer)
            {
                showingAnswer = true;
                DisplayedText.Text = answer;
            }
            else
            {
                Close();
            }
        }
    }
}
