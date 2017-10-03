// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorBlender;

namespace ColorBlenderAvalonia
{
    public class MainWindow : Window
    {
        private RGB rgb;
        private HSV hsv;
        private Blend z;
        private RGB[] vRGB = new RGB[7];
        private RGB[] vHSV = new RGB[9];
        private bool updatingSliders = false;
        private DropDown algorithm;
        private Slider sliderR;
        private Slider sliderG;
        private Slider sliderB;
        private Slider sliderH;
        private Slider sliderS;
        private Slider sliderV;
        private Rectangle rgbvar1;
        private Rectangle rgbvar2;
        private Rectangle rgbvar3;
        private Rectangle rgbvar4;
        private Rectangle rgbvar5;
        private Rectangle rgbvar6;
        private Rectangle rgbvar7;
        private Rectangle hsvvar1;
        private Rectangle hsvvar2;
        private Rectangle hsvvar3;
        private Rectangle hsvvar4;
        private Rectangle hsvvar5;
        private Rectangle hsvvar6;
        private Rectangle hsvvar7;
        private Rectangle hsvvar8;
        private Rectangle hsvvar9;
        private Swatch swatch1;
        private Swatch swatch2;
        private Swatch swatch3;
        private Swatch swatch4;
        private Swatch swatch5;
        private Swatch swatch6;

        private string Algorithm
        {
            get { return (algorithm.SelectedItem as DropDownItem).Content.ToString(); }
        }

        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();

            algorithm = this.FindControl<DropDown>("algorithm");
            sliderR = this.FindControl<Slider>("sliderR");
            sliderG = this.FindControl<Slider>("sliderG");
            sliderB = this.FindControl<Slider>("sliderB");
            sliderH = this.FindControl<Slider>("sliderH");
            sliderS = this.FindControl<Slider>("sliderS");
            sliderV = this.FindControl<Slider>("sliderV");
            rgbvar1 = this.FindControl<Rectangle>("rgbvar1");
            rgbvar2 = this.FindControl<Rectangle>("rgbvar2");
            rgbvar3 = this.FindControl<Rectangle>("rgbvar3");
            rgbvar4 = this.FindControl<Rectangle>("rgbvar4");
            rgbvar5 = this.FindControl<Rectangle>("rgbvar5");
            rgbvar6 = this.FindControl<Rectangle>("rgbvar6");
            rgbvar7 = this.FindControl<Rectangle>("rgbvar7");
            hsvvar1 = this.FindControl<Rectangle>("hsvvar1");
            hsvvar2 = this.FindControl<Rectangle>("hsvvar2");
            hsvvar3 = this.FindControl<Rectangle>("hsvvar3");
            hsvvar4 = this.FindControl<Rectangle>("hsvvar4");
            hsvvar5 = this.FindControl<Rectangle>("hsvvar5");
            hsvvar6 = this.FindControl<Rectangle>("hsvvar6");
            hsvvar7 = this.FindControl<Rectangle>("hsvvar7");
            hsvvar8 = this.FindControl<Rectangle>("hsvvar8");
            hsvvar9 = this.FindControl<Rectangle>("hsvvar9");
            swatch1 = this.FindControl<Swatch>("swatch1");
            swatch2 = this.FindControl<Swatch>("swatch2");
            swatch3 = this.FindControl<Swatch>("swatch3");
            swatch4 = this.FindControl<Swatch>("swatch4");
            swatch5 = this.FindControl<Swatch>("swatch5");
            swatch6 = this.FindControl<Swatch>("swatch6");

            hsv = new HSV(213, 46, 49);
            rgb = new RGB(hsv);
            z = ColorMatch.Match(hsv, Algorithm);

            UpdateSliderRGB();
            UpdateSliderHSV();
            UpdateSwatches();
            UpdateVariations();

            sliderR.GetObservable(Slider.ValueProperty).Subscribe(value => SliderRGB_ValueChanged());
            sliderG.GetObservable(Slider.ValueProperty).Subscribe(value => SliderRGB_ValueChanged());
            sliderB.GetObservable(Slider.ValueProperty).Subscribe(value => SliderRGB_ValueChanged());
            sliderH.GetObservable(Slider.ValueProperty).Subscribe(value => SliderHSV_ValueChanged());
            sliderS.GetObservable(Slider.ValueProperty).Subscribe(value => SliderHSV_ValueChanged());
            sliderV.GetObservable(Slider.ValueProperty).Subscribe(value => SliderHSV_ValueChanged());

            rgbvar1.PointerPressed += Rectangle_PointerPressed;
            rgbvar2.PointerPressed += Rectangle_PointerPressed;
            rgbvar3.PointerPressed += Rectangle_PointerPressed;
            rgbvar4.PointerPressed += Rectangle_PointerPressed;
            rgbvar5.PointerPressed += Rectangle_PointerPressed;
            rgbvar6.PointerPressed += Rectangle_PointerPressed;
            rgbvar7.PointerPressed += Rectangle_PointerPressed;
            hsvvar1.PointerPressed += Rectangle_PointerPressed;
            hsvvar2.PointerPressed += Rectangle_PointerPressed;
            hsvvar3.PointerPressed += Rectangle_PointerPressed;
            hsvvar4.PointerPressed += Rectangle_PointerPressed;
            hsvvar5.PointerPressed += Rectangle_PointerPressed;
            hsvvar6.PointerPressed += Rectangle_PointerPressed;
            hsvvar7.PointerPressed += Rectangle_PointerPressed;
            hsvvar8.PointerPressed += Rectangle_PointerPressed;
            hsvvar9.PointerPressed += Rectangle_PointerPressed;
            swatch1.col.PointerPressed += Rectangle_PointerPressed;
            swatch2.col.PointerPressed += Rectangle_PointerPressed;
            swatch3.col.PointerPressed += Rectangle_PointerPressed;
            swatch4.col.PointerPressed += Rectangle_PointerPressed;
            swatch5.col.PointerPressed += Rectangle_PointerPressed;
            swatch6.col.PointerPressed += Rectangle_PointerPressed;
            algorithm.SelectionChanged += Algorithm_SelectionChanged;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            z = ColorMatch.Match(hsv, Algorithm);
            UpdateSwatches();
            UpdateVariations();
        }

        private void SliderRGB_ValueChanged()
        {
            if (updatingSliders == false)
            {
                HandleSliderValueChangedRGB();
            }
        }

        private void SliderHSV_ValueChanged()
        {
            if (updatingSliders == false)
            {
                HandleSliderValueChangedHSV();
            }
        }

        private void Rectangle_PointerPressed(object sender, PointerPressedEventArgs e)
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
