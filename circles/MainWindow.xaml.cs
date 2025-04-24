using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace circles;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //bool canPlaceCircle = true;
    int n = 8;
    double squareW;
    double squareH;
    double radius;
    Canvas canvas;

    Ellipse? currentEllipse = null;

    public MainWindow()
    {
        InitializeComponent();

        canvas = new Canvas();
        this.AddChild(canvas);
        canvas.Height = 800;
        canvas.Width = 800;
        canvas.Background = Brushes.AliceBlue;
        canvas.MouseLeftButtonDown += new MouseButtonEventHandler(placeCircle);
        canvas.MouseRightButtonDown += new MouseButtonEventHandler(removeCircle);
        //canvas.KeyDown += new KeyEventHandler(moveCircle);
        //canvas.MouseDoubleClick += new MouseButtonEventHandler(addTeleport);


        double cWidth = canvas.Width;
        double cHeight = canvas.Height;
        squareW = cWidth / n;
        squareH = cHeight / n;
        radius = Math.Min(squareW, squareH) / 2;

        for (int i = 0; i <= n; i++)
        {
            var xStart = 0;
            var xEnd = cHeight;
            var y = i * squareW;

            Line line = new Line { X1 = xStart, X2 = xEnd, Y1 = y, Y2 = y, Stroke = Brushes.Red };
            canvas.Children.Add(line);
        }
        for (int i = 0; i <= n; i++)
        {
            var yStart = 0;
            var yEnd = cWidth;
            var x = i * squareH;

            Line line = new Line { X1 = x, X2 = x, Y1 = yStart, Y2 = yEnd, Stroke = Brushes.Red };
            canvas.Children.Add(line);
        }
    }


    private void placeCircle(object sender, MouseButtonEventArgs e)
    {



        var position = e.GetPosition(canvas);
        var posX = (int)((int)(position.X / squareW) * squareW);
        var posY = (int)((int)(position.Y / squareH) * squareH);

        var elements = canvas.Children;
        foreach (var element in elements)
        {
            if (element is Ellipse)
            {
                var elem = (Ellipse)element;
                if (elem.Margin.Left == posX & elem.Margin.Top == posY)
                {
                    currentEllipse = elem;
                    return;
                }
            }
        }


        var ellipse = new Ellipse();


        ellipse.Height = 2 * radius;
        ellipse.Width = 2 * radius;
        ellipse.Fill = Brushes.Red;

        //int x = (int)(position.X / squareW);
        //int y = (int)(position.Y / squareH);
        // MessageBox.S
        ellipse.Margin = new Thickness(posX, posY, 0, 0);


        canvas.Children.Add(ellipse);
        currentEllipse = ellipse;
        //canPlaceCircle = false;
    }

    private void setNewCirclePosition(int x, int y)
    {
        if (currentEllipse == null) return;

        var coord = currentEllipse.Margin;
        var newPosX = coord.Left + x * squareW;
        var newPosY = coord.Top + y * squareH;



        if (newPosX >= canvas.Width) newPosX = 0;
        if (newPosX < 0) newPosX = canvas.Width - squareW;

        if (newPosY >= canvas.Height) newPosY = 0;
        if (newPosY < 0) newPosY = canvas.Height - squareH;

        if (!clearWay(newPosX, newPosY)) return;


        currentEllipse.Margin = new Thickness(newPosX, newPosY, 0, 0);

        //canvas.Children.Add(ellipse);
    }

    bool clearWay(double x, double y)
    {
        var elements = canvas.Children;
        foreach (var element in elements)
        {
            if (element is Ellipse)
            {
                var elem = (Ellipse)element;
                if (elem.Margin.Left == x && elem.Margin.Top == y)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void moveCircle(object sender, KeyEventArgs e)
    {
        //MessageBox.Show(e.Key.ToString());

        switch (e.Key)
        {
            case Key.Up:
                setNewCirclePosition(0, -1);
                break;
            case Key.Down:
                setNewCirclePosition(0, 1);
                break;

            case Key.Right:
                setNewCirclePosition(1, 0);
                break;
            case Key.Left:
                setNewCirclePosition(-1, 0);
                break;


        }

    }

    private void removeCircle(object sender, MouseButtonEventArgs e)
    {
        var position = e.GetPosition(canvas);
        var posX = (int)((int)(position.X / squareW) * squareW);
        var posY = (int)((int)(position.Y / squareH) * squareH);

        var elements = canvas.Children;
        foreach (var element in elements)
        {
            if (element is Ellipse)
            {
                var elem = (Ellipse)element;
                if (elem.Margin.Left == posX && elem.Margin.Top == posY)
                {
                    canvas.Children.Remove(elem);
                    return;
                }
            }
        }
    }

    private void addTeleport(object sender, MouseButtonEventArgs e)
    {
        MessageBox.Show("qwe");
    }
}