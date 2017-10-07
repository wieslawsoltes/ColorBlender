// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorBlender;
using ColorBlender.Algorithms;

namespace Avalonia.Controls.ColorBlender
{
    public partial class ColorBlender : UserControl
    {
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

        private bool _updatingSliders = false;

        public IList<IAlgorithm> Algorithms { get; set; }
        public IAlgorithm CurrentAlgorithm { get; set; }
        public Blend CurrentBlend { get; set; }
        public RGB CurrentRGB { get; set; }
        public HSV CurrentHSV { get; set; }
        public RGB[] VariationsRGB { get; set; }
        public RGB[] VariationsHSV { get; set; }

        public ColorBlender()
        {
            this.InitializeComponent();
            this.InitializeNames();

            Algorithms = new ObservableCollection<IAlgorithm>()
            {
                new Classic(),
                new ColorExplorer(),
                new SingleHue(),
                new Complementary(),
                new SplitComplementary(),
                new Analogue(),
                new Triadic(),
                new Square()
            };

            CurrentAlgorithm = Algorithms.FirstOrDefault();

            DataContext = this;

            CurrentHSV = new HSV(213, 46, 49);
            CurrentRGB = new RGB(CurrentHSV);

            CurrentBlend = CurrentAlgorithm.Match(CurrentHSV);
            VariationsRGB = new RGB[7];
            VariationsHSV = new RGB[9];
            UpdateVariationsRGB();
            UpdateVariationsHSV();

            UpdateSliderRGB();
            UpdateSliderHSV();
            UpdateSwatches();
            UpdateVariations();

            InitializeEventHandlers();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeNames()
        {
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
        }

        private void InitializeEventHandlers()
        {
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

        private void Algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentBlend = CurrentAlgorithm.Match(CurrentHSV);
            UpdateSwatches();
            UpdateVariationsRGB();
            UpdateVariationsHSV();
            UpdateVariations();
        }

        private void SliderRGB_ValueChanged()
        {
            if (_updatingSliders == false)
            {
                HandleSliderValueChangedRGB();
            }
        }

        private void SliderHSV_ValueChanged()
        {
            if (_updatingSliders == false)
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
            CurrentRGB = ColorExtensions.ToRGB(b.Color);
            CurrentHSV = CurrentRGB.ToHSV();

            _updatingSliders = true;
            UpdateSliderRGB();
            UpdateSliderHSV();
            _updatingSliders = false;

            CurrentBlend = CurrentAlgorithm.Match(CurrentHSV);
            UpdateSwatches();
            UpdateVariationsRGB();
            UpdateVariationsHSV();
            UpdateVariations();
        }

        private void UpdateSwatches()
        {
            swatch1.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(CurrentBlend.Colors[0]));
            swatch2.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(CurrentBlend.Colors[1]));
            swatch3.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(CurrentBlend.Colors[2]));
            swatch4.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(CurrentBlend.Colors[3]));
            swatch5.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(CurrentBlend.Colors[4]));
            swatch6.col.Fill = new SolidColorBrush(ColorExtensions.ToColor(CurrentBlend.Colors[5]));
        }

        private void UpdateSliderRGB()
        {
            sliderR.Value = CurrentRGB.R;
            sliderG.Value = CurrentRGB.G;
            sliderB.Value = CurrentRGB.B;
        }

        private void UpdateSliderHSV()
        {
            sliderH.Value = CurrentHSV.H;
            sliderS.Value = CurrentHSV.S;
            sliderV.Value = CurrentHSV.V;
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
                H = hsv.H,
                S = hsv.S,
                V = hsv.V
            };

            hsvobj.S = AddLimit(hsvobj.S, addsat, 0, 99);
            hsvobj.V = AddLimit(hsvobj.V, addval, 0, 99);

            rgbobj = hsvobj.ToRGB();

            return rgbobj;
        }

        private void UpdateVariationsRGB()
        {
            double vv = 20;
            double vw = 10;

            VariationsRGB[0] = new RGB(AddLimit(CurrentRGB.R, -vw, 0, 255), AddLimit(CurrentRGB.G, vv, 0, 255), AddLimit(CurrentRGB.B, -vw, 0, 255));
            VariationsRGB[1] = new RGB(AddLimit(CurrentRGB.R, vw, 0, 255), AddLimit(CurrentRGB.G, vw, 0, 255), AddLimit(CurrentRGB.B, -vv, 0, 255));
            VariationsRGB[2] = new RGB(AddLimit(CurrentRGB.R, -vv, 0, 255), AddLimit(CurrentRGB.G, vw, 0, 255), AddLimit(CurrentRGB.B, vw, 0, 255));
            VariationsRGB[3] = new RGB(CurrentRGB.R, CurrentRGB.G, CurrentRGB.B);
            VariationsRGB[4] = new RGB(AddLimit(CurrentRGB.R, vv, 0, 255), AddLimit(CurrentRGB.G, -vw, 0, 255), AddLimit(CurrentRGB.B, -vw, 0, 255));
            VariationsRGB[5] = new RGB(AddLimit(CurrentRGB.R, -vw, 0, 255), AddLimit(CurrentRGB.G, -vw, 0, 255), AddLimit(CurrentRGB.B, vv, 0, 255));
            VariationsRGB[6] = new RGB(AddLimit(CurrentRGB.R, vw, 0, 255), AddLimit(CurrentRGB.G, -vv, 0, 255), AddLimit(CurrentRGB.B, vw, 0, 255));
        }

        private void UpdateVariationsHSV()
        {
            double vv = 10;

            VariationsHSV[0] = HsvVariation(CurrentHSV, -vv, vv);
            VariationsHSV[1] = HsvVariation(CurrentHSV, 0, vv);
            VariationsHSV[2] = HsvVariation(CurrentHSV, vv, vv);
            VariationsHSV[3] = HsvVariation(CurrentHSV, -vv, 0);
            VariationsHSV[4] = CurrentHSV.ToRGB();
            VariationsHSV[5] = HsvVariation(CurrentHSV, vv, 0);
            VariationsHSV[6] = HsvVariation(CurrentHSV, -vv, -vv);
            VariationsHSV[7] = HsvVariation(CurrentHSV, 0, -vv);
            VariationsHSV[8] = HsvVariation(CurrentHSV, vv, -vv);
        }

        private void UpdateVariations()
        {
            rgbvar1.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[0]));
            rgbvar2.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[1]));
            rgbvar3.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[2]));
            rgbvar4.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[3]));
            rgbvar5.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[4]));
            rgbvar6.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[5]));
            rgbvar7.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsRGB[6]));

            hsvvar1.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[0]));
            hsvvar2.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[1]));
            hsvvar3.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[2]));
            hsvvar4.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[3]));
            hsvvar5.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[4]));
            hsvvar6.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[5]));
            hsvvar7.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[6]));
            hsvvar8.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[7]));
            hsvvar9.Fill = new SolidColorBrush(ColorExtensions.ToColor(VariationsHSV[8]));
        }

        private void HandleSliderValueChangedRGB()
        {
            CurrentRGB.R = sliderR.Value;
            CurrentRGB.G = sliderG.Value;
            CurrentRGB.B = sliderB.Value;

            CurrentHSV = CurrentRGB.ToHSV();
            CurrentRGB = CurrentHSV.ToRGB();

            _updatingSliders = true;
            UpdateSliderHSV();
            _updatingSliders = false;

            CurrentBlend = CurrentAlgorithm.Match(CurrentHSV);
            UpdateSwatches();
            UpdateVariationsRGB();
            UpdateVariationsHSV();
            UpdateVariations();
        }

        private void HandleSliderValueChangedHSV()
        {
            CurrentHSV.H = sliderH.Value;
            CurrentHSV.S = sliderS.Value;
            CurrentHSV.V = sliderV.Value;

            CurrentRGB = CurrentHSV.ToRGB();

            _updatingSliders = true;
            UpdateSliderRGB();
            _updatingSliders = false;

            CurrentBlend = CurrentAlgorithm.Match(CurrentHSV);
            UpdateSwatches();
            UpdateVariationsRGB();
            UpdateVariationsHSV();
            UpdateVariations();
        }
    }
}
