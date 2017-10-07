// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;

namespace ColorBlenderAvalonia
{
    public partial class Swatch : UserControl
    {
        public Rectangle col;

        public Swatch()
        {
            InitializeComponent();

            col = this.FindControl<Rectangle>("col");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
