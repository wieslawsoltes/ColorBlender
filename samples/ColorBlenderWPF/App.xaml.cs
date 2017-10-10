// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Windows;
using ColorBlender;

namespace ColorBlenderWPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            new MainWindow()
            {
                DataContext = new ColorMatch(213, 46, 49)
            }
            .ShowDialog();
        }
    }
}
