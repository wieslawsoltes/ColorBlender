// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ColorBlender;
using ColorBlender.Algorithms;

namespace ColorBlenderWPF
{
    public partial class ColorBlender : UserControl
    {
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
            InitializeComponent();

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
            CurrentBlend = CurrentAlgorithm.Match(CurrentHSV);
            UpdateSwatches();
            UpdateVariationsRGB();
            UpdateVariationsHSV();
            UpdateVariations();
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
