// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorBlender;
using ColorBlender.Colors;

namespace WPF.Controls.ColorBlender
{
    public partial class ColorBlender : UserControl
    {
        private bool _updatingSliders = false;

        public ColorBlender()
        {
            this.InitializeComponent();

            sliderR.ValueChanged += SliderRGB_ValueChanged;
            sliderG.ValueChanged += SliderRGB_ValueChanged;
            sliderB.ValueChanged += SliderRGB_ValueChanged;
            sliderH.ValueChanged += SliderHSV_ValueChanged;
            sliderS.ValueChanged += SliderHSV_ValueChanged;
            sliderV.ValueChanged += SliderHSV_ValueChanged;

            rgbvar1.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rgbvar2.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rgbvar3.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rgbvar4.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rgbvar5.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rgbvar6.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            rgbvar7.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

            hsvvar1.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar2.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar3.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar4.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar5.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar6.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar7.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar8.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            hsvvar9.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

            swatch1.col.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            swatch2.col.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            swatch3.col.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            swatch4.col.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            swatch5.col.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
            swatch6.col.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;

            algorithm.SelectionChanged += Algorithm_SelectionChanged;

            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
            UpdateSliderHSV();
        }

        private void UpdateVariations()
        {
            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                rgbvar1.Fill = vm.VariationsRGB[0].ToSolidColorBrush();
                rgbvar2.Fill = vm.VariationsRGB[1].ToSolidColorBrush();
                rgbvar3.Fill = vm.VariationsRGB[2].ToSolidColorBrush();
                rgbvar4.Fill = vm.VariationsRGB[3].ToSolidColorBrush();
                rgbvar5.Fill = vm.VariationsRGB[4].ToSolidColorBrush();
                rgbvar6.Fill = vm.VariationsRGB[5].ToSolidColorBrush();
                rgbvar7.Fill = vm.VariationsRGB[6].ToSolidColorBrush();

                hsvvar1.Fill = vm.VariationsHSV[0].ToSolidColorBrush();
                hsvvar2.Fill = vm.VariationsHSV[1].ToSolidColorBrush();
                hsvvar3.Fill = vm.VariationsHSV[2].ToSolidColorBrush();
                hsvvar4.Fill = vm.VariationsHSV[3].ToSolidColorBrush();
                hsvvar5.Fill = vm.VariationsHSV[4].ToSolidColorBrush();
                hsvvar6.Fill = vm.VariationsHSV[5].ToSolidColorBrush();
                hsvvar7.Fill = vm.VariationsHSV[6].ToSolidColorBrush();
                hsvvar8.Fill = vm.VariationsHSV[7].ToSolidColorBrush();
                hsvvar9.Fill = vm.VariationsHSV[8].ToSolidColorBrush();
            }
        }

        private void UpdateSwatches()
        {
            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                swatch1.col.Fill = vm.CurrentBlend.Colors[0].ToSolidColorBrush();
                swatch2.col.Fill = vm.CurrentBlend.Colors[1].ToSolidColorBrush();
                swatch3.col.Fill = vm.CurrentBlend.Colors[2].ToSolidColorBrush();
                swatch4.col.Fill = vm.CurrentBlend.Colors[3].ToSolidColorBrush();
                swatch5.col.Fill = vm.CurrentBlend.Colors[4].ToSolidColorBrush();
                swatch6.col.Fill = vm.CurrentBlend.Colors[5].ToSolidColorBrush();
            }
        }

        private void UpdateSliderRGB()
        {
            _updatingSliders = true;

            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                sliderR.Value = vm.CurrentRGB.R;
                sliderG.Value = vm.CurrentRGB.G;
                sliderB.Value = vm.CurrentRGB.B;
            }

            _updatingSliders = false;
        }

        private void UpdateSliderHSV()
        {
            _updatingSliders = true;

            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                sliderH.Value = vm.CurrentHSV.H;
                sliderS.Value = vm.CurrentHSV.S;
                sliderV.Value = vm.CurrentHSV.V;
            }

            _updatingSliders = false;
        }

        private void Algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
        }

        private void SliderRGB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_updatingSliders == false)
            {
                HandleSliderValueChangedRGB();
            }
        }

        private void SliderHSV_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_updatingSliders == false)
            {
                HandleSliderValueChangedHSV();
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HandleRectangleClick((sender as Rectangle).Fill as SolidColorBrush);
        }

        private void HandleRectangleClick(SolidColorBrush b)
        {
            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                vm.CurrentRGB = b.Color.ToRGB();
                vm.CurrentHSV = vm.CurrentRGB.ToHSV();

                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
            UpdateSliderHSV();
        }

        private void HandleSliderValueChangedRGB()
        {
            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                vm.CurrentRGB = new RGB(
                    sliderR.Value,
                    sliderG.Value,
                    sliderB.Value);

                vm.CurrentHSV = vm.CurrentRGB.ToHSV();
                vm.CurrentRGB = vm.CurrentHSV.ToRGB();

                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderHSV();
        }

        private void HandleSliderValueChangedHSV()
        {
            var vm = DataContext as ColorMatch;
            if (vm != null)
            {
                vm.CurrentHSV = new HSV(
                    sliderH.Value,
                    sliderS.Value,
                    sliderV.Value);

                vm.CurrentRGB = vm.CurrentHSV.ToRGB();

                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
        }
    }
}
