// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorBlender;

namespace ColorBlenderWPF.Controls
{
    public partial class ColorBlender : UserControl
    {
        private bool _updatingSliders = false;
        private ColorMatch _vm;

        public ColorBlender()
        {
            this.InitializeComponent();

            _vm = new ColorMatch(213, 46, 49);

            DataContext = _vm;

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
            UpdateSliderHSV();

            this.InitializeEventHandlers();
        }

        private void UpdateVariations()
        {
            rgbvar1.Fill = _vm.VariationsRGB[0].ToSolidColorBrush();
            rgbvar2.Fill = _vm.VariationsRGB[1].ToSolidColorBrush();
            rgbvar3.Fill = _vm.VariationsRGB[2].ToSolidColorBrush();
            rgbvar4.Fill = _vm.VariationsRGB[3].ToSolidColorBrush();
            rgbvar5.Fill = _vm.VariationsRGB[4].ToSolidColorBrush();
            rgbvar6.Fill = _vm.VariationsRGB[5].ToSolidColorBrush();
            rgbvar7.Fill = _vm.VariationsRGB[6].ToSolidColorBrush();

            hsvvar1.Fill = _vm.VariationsHSV[0].ToSolidColorBrush();
            hsvvar2.Fill = _vm.VariationsHSV[1].ToSolidColorBrush();
            hsvvar3.Fill = _vm.VariationsHSV[2].ToSolidColorBrush();
            hsvvar4.Fill = _vm.VariationsHSV[3].ToSolidColorBrush();
            hsvvar5.Fill = _vm.VariationsHSV[4].ToSolidColorBrush();
            hsvvar6.Fill = _vm.VariationsHSV[5].ToSolidColorBrush();
            hsvvar7.Fill = _vm.VariationsHSV[6].ToSolidColorBrush();
            hsvvar8.Fill = _vm.VariationsHSV[7].ToSolidColorBrush();
            hsvvar9.Fill = _vm.VariationsHSV[8].ToSolidColorBrush();
        }

        private void UpdateSwatches()
        {
            swatch1.col.Fill = _vm.CurrentBlend.Colors[0].ToSolidColorBrush();
            swatch2.col.Fill = _vm.CurrentBlend.Colors[1].ToSolidColorBrush();
            swatch3.col.Fill = _vm.CurrentBlend.Colors[2].ToSolidColorBrush();
            swatch4.col.Fill = _vm.CurrentBlend.Colors[3].ToSolidColorBrush();
            swatch5.col.Fill = _vm.CurrentBlend.Colors[4].ToSolidColorBrush();
            swatch6.col.Fill = _vm.CurrentBlend.Colors[5].ToSolidColorBrush();
        }

        private void UpdateSliderRGB()
        {
            _updatingSliders = true;

            sliderR.Value = _vm.CurrentRGB.R;
            sliderG.Value = _vm.CurrentRGB.G;
            sliderB.Value = _vm.CurrentRGB.B;

            _updatingSliders = false;
        }

        private void UpdateSliderHSV()
        {
            _updatingSliders = true;

            sliderH.Value = _vm.CurrentHSV.H;
            sliderS.Value = _vm.CurrentHSV.S;
            sliderV.Value = _vm.CurrentHSV.V;

            _updatingSliders = false;
        }

        private void InitializeEventHandlers()
        {
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
        }

        private void Algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.Update();

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
            _vm.CurrentRGB = b.Color.ToRGB();
            _vm.CurrentHSV = _vm.CurrentRGB.ToHSV();

            _vm.Update();

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
            UpdateSliderHSV();
        }

        private void HandleSliderValueChangedRGB()
        {
            _vm.CurrentRGB.R = sliderR.Value;
            _vm.CurrentRGB.G = sliderG.Value;
            _vm.CurrentRGB.B = sliderB.Value;

            _vm.CurrentHSV = _vm.CurrentRGB.ToHSV();
            _vm.CurrentRGB = _vm.CurrentHSV.ToRGB();

            _vm.Update();

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderHSV();
        }

        private void HandleSliderValueChangedHSV()
        {
            _vm.CurrentHSV.H = sliderH.Value;
            _vm.CurrentHSV.S = sliderS.Value;
            _vm.CurrentHSV.V = sliderV.Value;

            _vm.CurrentRGB = _vm.CurrentHSV.ToRGB();

            _vm.Update();

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
        }
    }
}
