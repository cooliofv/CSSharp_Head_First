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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        int matchCounter = 0;
        int tenthSecondsElapsed = 0;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }//MainWindow constructor

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthSecondsElapsed++;

            timerTextBlock.Text = (tenthSecondsElapsed / 10F).ToString("0.0s");

            if(matchCounter == 8)
            {
                timer.Stop();
                timerTextBlock.Text = timerTextBlock.Text + " - ИШО ?";
            }
            
        }//Timer_Tick

        public void SetUpGame()
        {
            
            List<string> listEmoji = new List<string>()
            {
                "😂", "😂",
                "😊", "😊",
                "🤣", "🤣",
                "❤", "❤",
                "💕", "💕",
                "😘", "😘",
                "👌", "👌",
                "😒", "😒"
            };

            Random random = new Random();

            int counter = listEmoji.Count;
            foreach(TextBlock cellBlock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(listEmoji.Count);

                if (counter == 0)
                    break;
                string randomEmoji = listEmoji[index];

                cellBlock.Text = randomEmoji;
                cellBlock.Visibility = Visibility.Visible;

                listEmoji.RemoveAt(index);

                counter--;
            }//foreach


            matchCounter = 0;
            tenthSecondsElapsed = 0;
            timer.Start();
        }//SetUpGame


        TextBlock lastClickedTextBlock;
        bool isMatched = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock clickedTextBlock = sender as TextBlock;

            if(isMatched == false)
            {
                clickedTextBlock.Visibility = Visibility.Hidden;
                isMatched = true;
                lastClickedTextBlock = clickedTextBlock;
            }else if (clickedTextBlock.Text == lastClickedTextBlock.Text)
            {
                clickedTextBlock.Visibility = Visibility.Hidden;
                isMatched = false;
                matchCounter++;
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                isMatched = false;
            }

        }//TextBlock_MouseDown

        private void timerTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(matchCounter == 8)
            {
                SetUpGame();
            }

        }//timerTextBlock_MouseDown
    }// class Window
}
