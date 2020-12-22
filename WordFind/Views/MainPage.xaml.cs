using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WordFind.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            CreateGame();
            //var grid = new Grid();
            //for (int r = 0; r < 11; r++)
            //{
            //    grid.RowDefinitions.Add(new RowDefinition());
            //    grid.ColumnDefinitions.Add(new ColumnDefinition());
            //}
            //grid.Parent = this;
            //for (int r = 0; r < 11; r++)
            //{
            //    var boxView = new BoxView()
            //    {
            //        BackgroundColor = Color.CornflowerBlue,
            //        HorizontalOptions = LayoutOptions.FillAndExpand,
            //        VerticalOptions = LayoutOptions.FillAndExpand
            //    };
            //    grid.Children.Add(boxView, r, r);
            //    boxVi
            //}
        }

        void CreateGame()
        {
            string[] list = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            var grid = new Grid();
            grid.BackgroundColor = Color.Red;
            grid.HorizontalOptions = LayoutOptions.FillAndExpand;
            grid.SetBinding(Button.HeightRequestProperty, new Binding("Width", source: grid));


            int count = (int)Math.Sqrt(list.Length);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });

                    var Rowbutton = new Button { Text = list[i * count + j] };
                    Rowbutton.HorizontalOptions = LayoutOptions.FillAndExpand;
                    Rowbutton.VerticalOptions = LayoutOptions.FillAndExpand;
                    Rowbutton.SetBinding(Button.HeightRequestProperty, new Binding("Width", source: Rowbutton));
                    grid.Children.Add(Rowbutton, j, i);
                }
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            //this.CurrentPage
            //=
            //    new StackLayout { Children = { grid }, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
        }

    }
}
