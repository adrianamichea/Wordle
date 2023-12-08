using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Wordle.ViewModels;

namespace Wordle.View
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private readonly GameViewModel viewModel;

        public GameWindow()
        {
            InitializeComponent();
            viewModel = new GameViewModel();
            DataContext = viewModel;

            Debug.WriteLine("GameWindow constructor called. DataContext set.");

        }

       
    }

}
