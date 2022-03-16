using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;

namespace Avalonia.Controls.ColorBlender
{
    public class Swatch : UserControl
    {
        internal readonly Rectangle _col;

        public Swatch()
        {
            this.InitializeComponent();

            _col = this.FindControl<Rectangle>("col");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
