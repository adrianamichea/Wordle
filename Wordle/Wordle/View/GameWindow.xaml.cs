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
using Wordle.Interfaces;
using Wordle.Strategies;
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
            var gameEntityFactory = new GameEntityFactory();
            var wordGenerationStrategy = new RandomWordGenerationStrategy();
            viewModel = new GameViewModel(gameEntityFactory, wordGenerationStrategy);
            DataContext = viewModel;

            Debug.WriteLine("GameWindow constructor called. DataContext set.");

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
