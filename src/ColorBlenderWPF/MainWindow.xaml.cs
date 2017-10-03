// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorBlender;

namespace ColorBlenderWPF
{
    public partial class MainWindow : Window
    {
        private RGB rgb;
        private HSV hsv;
        private Blend z;
        private RGB[] vRGB = new RGB[7];
        private RGB[] vHSV = new RGB[9];
        private bool updatingSliders = false;

        private string Algorithm
        {
            get { return (algorithm.SelectedItem as ComboBoxItem).Content.ToString(); }
        }

        public MainWindow()
        {
            InitializeComponent();

            hsv = new HSV(213, 46, 49);
            rgb = new RGB(hsv);
            z = ColorMatch.Match(hsv, Algorithm);

            UpdateSliderRGB();
            UpdateSliderHSV();
            UpdateSwatches();
            UpdateVariations();

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
            z = ColorMatch.Match(hsv, Algorithm);
            UpdateSwatches();
            UpdateVariations();
        }

        private void SliderRGB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (updatingSliders == false)
            {
                HandleSliderValueChangedRGB();
            }
        }

        private void SliderHSV_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (updatingSliders == false)
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
            rgb = ColorExtensions.ToRGB(b.Color);
            hsv = rgb.ToHSV();

            updatingSliders = true;
            UpdateSliderRGB();
            UpdateSliderHSV();
            updatingSliders = false;

            z = ColorMatch.Match(hsv, Algorithm);
            UpdateSwatches();
            UpdateVariations();
        }

        private void UpdateSwatches()
        {
            swatch1.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(z.Colors[0]));
            swatch2.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(z.Colors[1]));
            swatch3.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(z.Colors[2]));
            swatch4.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(z.Colors[3]));
            swatch5.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(z.Colors[4]));
            swatch6.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(z.Colors[5]));
        }

        private void UpdateSliderRGB()
        {
            sliderR.Value = rgb.r;
            sliderG.Value = rgb.g;
            sliderB.Value = rgb.b;
        }

        private void UpdateSliderHSV()
        {
            sliderH.Value = hsv.h;
            sliderS.Value = hsv.s;
            sliderV.Value = hsv.v;
        }

        private double AddLimit(double x, double d, double min, double max)
        {
            x = x + d;
            if (x < min) return min;
            if (x > max) return max;
            if ((x >= min) && (x <= max)) return x;

            return double.NaN;
        }

        private RGB HsvVariation(HSV hsv, double addsat, double addval)
        {
            var rgbobj = new RGB();
            var hsvobj = new HSV
            {
                h = hsv.h,
                s = hsv.s,
                v = hsv.v
            };

            hsvobj.s = AddLimit(hsvobj.s, addsat, 0, 99);
            hsvobj.v = AddLimit(hsvobj.v, addval, 0, 99);

            rgbobj = hsvobj.ToRGB();

            return rgbobj;
        }

        private void UpdateVariationsRGB()
        {
            double vv = 20;
            double vw = 10;

            vRGB[0] = new RGB(AddLimit(rgb.r, -vw, 0, 255), AddLimit(rgb.g, vv, 0, 255), AddLimit(rgb.b, -vw, 0, 255));
            vRGB[1] = new RGB(AddLimit(rgb.r, vw, 0, 255), AddLimit(rgb.g, vw, 0, 255), AddLimit(rgb.b, -vv, 0, 255));
            vRGB[2] = new RGB(AddLimit(rgb.r, -vv, 0, 255), AddLimit(rgb.g, vw, 0, 255), AddLimit(rgb.b, vw, 0, 255));
            vRGB[3] = new RGB(rgb.r, rgb.g, rgb.b);
            vRGB[4] = new RGB(AddLimit(rgb.r, vv, 0, 255), AddLimit(rgb.g, -vw, 0, 255), AddLimit(rgb.b, -vw, 0, 255));
            vRGB[5] = new RGB(AddLimit(rgb.r, -vw, 0, 255), AddLimit(rgb.g, -vw, 0, 255), AddLimit(rgb.b, vv, 0, 255));
            vRGB[6] = new RGB(AddLimit(rgb.r, vw, 0, 255), AddLimit(rgb.g, -vv, 0, 255), AddLimit(rgb.b, vw, 0, 255));
        }

        private void UpdateVariationsHSV()
        {
            double vv = 10;

            vHSV[0] = HsvVariation(hsv, -vv, vv);
            vHSV[1] = HsvVariation(hsv, 0, vv);
            vHSV[2] = HsvVariation(hsv, vv, vv);
            vHSV[3] = HsvVariation(hsv, -vv, 0);
            vHSV[4] = hsv.ToRGB();
            vHSV[5] = HsvVariation(hsv, vv, 0);
            vHSV[6] = HsvVariation(hsv, -vv, -vv);
            vHSV[7] = HsvVariation(hsv, 0, -vv);
            vHSV[8] = HsvVariation(hsv, vv, -vv);
        }

        private void UpdateVariations()
        {
            UpdateVariationsRGB();
            UpdateVariationsHSV();

            rgbvar1.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[0]));
            rgbvar2.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[1]));
            rgbvar3.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[2]));
            rgbvar4.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[3]));
            rgbvar5.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[4]));
            rgbvar6.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[5]));
            rgbvar7.Fill = new SolidColorBrush(ColorExtensions.ToColor(vRGB[6]));

            hsvvar1.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[0]));
            hsvvar2.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[1]));
            hsvvar3.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[2]));
            hsvvar4.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[3]));
            hsvvar5.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[4]));
            hsvvar6.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[5]));
            hsvvar7.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[6]));
            hsvvar8.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[7]));
            hsvvar9.Fill = new SolidColorBrush(ColorExtensions.ToColor(vHSV[8]));
        }

        private void HandleSliderValueChangedRGB()
        {
            rgb.r = sliderR.Value;
            rgb.g = sliderG.Value;
            rgb.b = sliderB.Value;

            hsv = rgb.ToHSV();
            rgb = hsv.ToRGB();

            updatingSliders = true;
            UpdateSliderHSV();
            updatingSliders = false;

            z = ColorMatch.Match(hsv, Algorithm);
            UpdateSwatches();
            UpdateVariations();
        }

        private void HandleSliderValueChangedHSV()
        {
            hsv.h = sliderH.Value;
            hsv.s = sliderS.Value;
            hsv.v = sliderV.Value;

            rgb = hsv.ToRGB();

            updatingSliders = true;
            UpdateSliderRGB();
            updatingSliders = false;

            z = ColorMatch.Match(hsv, Algorithm);
            UpdateSwatches();
            UpdateVariations();
        }
    }
}
