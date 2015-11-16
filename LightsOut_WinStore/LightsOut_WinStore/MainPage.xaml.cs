using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using LightsOut_WinStore.Common;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LightsOut_WinStore
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        private int gridWidth;      // Dimensions of the grid
        private bool[,] grid;       // Store the on/off state of the grid
        private Random rand;

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            
            rand = new Random();

            gridWidth = 3;
            CreateGrid();
            NewGame();

            //paintCanvas.Width = paintCanvas.Height;
        }

        private void CreateGrid()
        {
            // Remove all previously-existing rectangles
            paintCanvas.Children.Clear();

            grid = new bool[gridWidth, gridWidth];
            int rectSize = (int)paintCanvas.Width / gridWidth;

            SolidColorBrush black = new SolidColorBrush(Colors.Black);
            SolidColorBrush white = new SolidColorBrush(Colors.White);

            // Turn entire grid on and create rectangles to represent it
            for (int r = 0; r < gridWidth; r++)
                for (int c = 0; c < gridWidth; c++)
                {
                    grid[r, c] = true;

                    Rectangle rect = new Rectangle();
                    rect.Fill = white;
                    rect.Width = rectSize + 1;
                    rect.Height = rect.Width + 1;
                    rect.Stroke = black;

                    int x = c * rectSize;
                    int y = r * rectSize;

                    Canvas.SetTop(rect, y);
                    Canvas.SetLeft(rect, x);

                    // Add the new rectangle to the canvas' children
                    paintCanvas.Children.Add(rect);
                }
        }

        private void DrawGrid()
        {
            int index = 0;

            SolidColorBrush black = new SolidColorBrush(Colors.Black);
            SolidColorBrush white = new SolidColorBrush(Colors.White);

            // Set colors of each rectangle based on grid values
            for (int r = 0; r < gridWidth; r++)
                for (int c = 0; c < gridWidth; c++)
                {
                    Rectangle rect = paintCanvas.Children[index] as Rectangle;
                    index++;

                    if (grid[r, c])
                    {
                        // On
                        rect.Fill = white;
                        rect.Stroke = black;
                    }
                    else
                    {
                        // Off
                        rect.Fill = black;
                        rect.Stroke = white;
                    }
                }
        }

        private async void paintCanvas_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine("paintCanvas_Tapped");

            int rectSize = (int)paintCanvas.Width / gridWidth;

            // Find row, col of mouse press
            Point mousePosition = e.GetPosition(paintCanvas);
            int row = (int)(mousePosition.Y) / rectSize;
            int col = (int)(mousePosition.X) / rectSize;

            // Invert selected box and all surrounding boxes
            for (int i = row - 1; i <= row + 1; i++)
                for (int j = col - 1; j <= col + 1; j++)
                    if (i >= 0 && i < gridWidth && j >= 0 && j < gridWidth)
                        grid[i, j] = !grid[i, j];

            // Redraw the board
            DrawGrid();

            if (PlayerWon())
            {
                MessageDialog msgDialog = new MessageDialog("Congratulations!  You've won!", "Lights Out!");

                // Add an OK button
                msgDialog.Commands.Add(new UICommand("OK"));

                // Show the message box and wait aynchrously for a button press
                IUICommand command = await msgDialog.ShowAsync();

                // This executes *after* the OK button was pressed
                NewGame();
            }
        }

        private bool PlayerWon()
        {
            for (int r = 0; r < gridWidth; r++)
                for (int c = 0; c < gridWidth; c++)
                    if (grid[r, c])
                        return false;

            // All values must be false (off)
            return true;
        }

        private void NewGame()
        {
            // Fill grid with eitheir white or black
            for (int r = 0; r < gridWidth; r++)
                for (int c = 0; c < gridWidth; c++)
                    grid[r, c] = rand.Next(2) == 1;

            DrawGrid();
        }

        private void AppBarHelpButton_Click(object sender, RoutedEventArgs e)
        {
            //textBlock.Foreground = (sender as Button).Foreground;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(AboutPage));
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            Debug.WriteLine("LoadState");
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            Debug.WriteLine("SaveState");
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ChangeSize_OnClick(object sender, RoutedEventArgs e)
        {
            if (gridWidth < 7)
            {
                gridWidth += 2;
            }
            else
            {
                gridWidth = 3;
            }

            CreateGrid();
        }

        private void paintCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            paintCanvas.Width = paintCanvas.Height;
        }
    }
}
