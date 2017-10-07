// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;

namespace Avalonia.Controls.ColorBlender
{
    public partial class Swatch : UserControl
    {
        public Swatch()
        {
            this.InitializeComponent();
            this.InitializeNames();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

    public partial class Swatch : UserControl
    {
        public Rectangle col;

        private void InitializeNames()
        {
            col = this.FindControl<Rectangle>("col");
        }
    }
}
