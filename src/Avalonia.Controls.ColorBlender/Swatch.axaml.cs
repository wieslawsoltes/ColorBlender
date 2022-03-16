using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;

namespace Avalonia.Controls.ColorBlender
{
    public partial class Swatch : UserControl
    {
        public Rectangle col;

        public Swatch()
        {
            this.InitializeComponent();

            col = this.FindControl<Rectangle>("col");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
