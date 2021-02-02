using Binaries;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;

namespace MakeFrameRGreatAgain_
{
    //Main part
    public partial class MainWindow : Window
    {
        #region Fields

        Decorate decorator = new Decorate();
        ProcessesClass workProcess = new ProcessesClass();
        HardwareClass workHardware = new HardwareClass();
        UserClass workUser = new UserClass();

        #endregion
        public MainWindow()
        {
            InitializeComponent();
            decorator.GradientGrid(ParentGrid);
            decorator.GradientField(ProcessList, ModulesList);

            initiateLoad();

            workProcess.Init(ProcessList);
            workHardware.FillIn(Accessories);

            workUser.setprops(workUser.Reader());
        }

        private void ProcessList_DoubleClick(object sender, System.EventArgs e) //Double Click event
        {
            string Selected = ProcessList.SelectedItem.ToString();
            workProcess.ProcessKill(Selected);
        }

        private void GetInfo_Click(object sender, RoutedEventArgs e) 
        {
            string Selected = ProcessList.SelectedItem.ToString();
            ModulesList.Text = workProcess.GetInfo(Selected);
        }

        private void ProcessesKillButton_Click(object sender, RoutedEventArgs e) //Kill Button event
        {
            string Selected = ProcessList.SelectedItem.ToString();
            workProcess.ProcessKill(Selected);
        }

        #region Menu events

        private void Process_Menu_Click(object sender, RoutedEventArgs e) => SetVisibility(0); 
        private void Hardware_Menu_Click(object sender, RoutedEventArgs e) => SetVisibility(1);
        private void Settings_Menu_Click(object sender, RoutedEventArgs e) => SetVisibility(2);

        #endregion

        private void Accessories_Selected(object sender, RoutedEventArgs e) //PC part selected (one-click)
        {
            string toTransport = (Accessories.SelectedItem as TreeViewItem).Header.ToString();
            workHardware.ComponentInfo(AccessoriesDisplay, toTransport);
        }

        #region Settings Events

        private void DoubleClickInteraction_Enable(object sender, RoutedEventArgs e) => interactionColorChange(DoubleClickButton, 0);
        private void GifLoader_Enable(object sender, RoutedEventArgs e) => interactionColorChange(GifLoaderButton, 1);

        #endregion
    }

    //Optimization part
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Set visibility of one chosen grid.
        /// </summary>
        private void SetVisibility(int index)
        {
            Grid[] grids = new Grid[3] { ProcGrid, HardGrid, SettingsGrid };

            for(int i = 0; i < grids.Length; i++)
            {
                if(i != index && grids[i].Visibility == Visibility.Visible)
                {
                    grids[i].Visibility = Visibility.Hidden;
                }
                else if(i == index && grids[i].Visibility == Visibility.Hidden)
                {
                    grids[i].Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Set property and initiate color change of button
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="innerIndex"></param>
        private void interactionColorChange(ContentControl uiElement, int innerIndex)
        {
            bool[] temp = workUser.Reader();

            if (workUser.Reader()[innerIndex])
                temp[innerIndex] = false;
            else
                temp[innerIndex] = true;

            workUser.setprops(temp);

            decorator.InitiateColor(uiElement, innerIndex);
        }

        /// <summary>
        /// Initial load of components that are linked with settings
        /// </summary>
        private void initiateLoad()
        {
            workUser.createPath();

            decorator.InitiateColor(DoubleClickButton, 0);
            decorator.InitiateColor(GifLoaderButton, 1);

            if (!workUser.Reader()[0])
                ProcessesKillButton.Visibility = Visibility.Visible;
            else
                ProcessesKillButton.Visibility = Visibility.Hidden;

            if (workUser.Reader()[1])
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri("Visual/GifLogo.gif", UriKind.Relative);
                image.EndInit();
                ImageBehavior.SetAnimatedSource(LogoImage, image);
            }
        }
    }

}
