// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorBlender;

namespace Avalonia.Controls.ColorBlender
{
    public partial class ColorBlender : UserControl
    {
        private bool _updatingSliders = false;
        private ColorMatch _vm;

        public ColorBlender()
        {
            this.InitializeComponent();
            this.InitializeNames();

            _vm = new ColorMatch(213, 46, 49);

            DataContext = _vm;

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRGB();
            UpdateSliderHSV();

            this.InitializeEventHandlers();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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
            _vm.Update();

            UpdateVariations();
            UpdateSwatches();
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

    public partial class ColorBlender : UserControl
    {
        public DropDown algorithm;
        public Slider sliderR;
        public Slider sliderG;
        public Slider sliderB;
        public Slider sliderH;
        public Slider sliderS;
        public Slider sliderV;
        public Rectangle rgbvar1;
        public Rectangle rgbvar2;
        public Rectangle rgbvar3;
        public Rectangle rgbvar4;
        public Rectangle rgbvar5;
        public Rectangle rgbvar6;
        public Rectangle rgbvar7;
        public Rectangle hsvvar1;
        public Rectangle hsvvar2;
        public Rectangle hsvvar3;
        public Rectangle hsvvar4;
        public Rectangle hsvvar5;
        public Rectangle hsvvar6;
        public Rectangle hsvvar7;
        public Rectangle hsvvar8;
        public Rectangle hsvvar9;
        public Swatch swatch1;
        public Swatch swatch2;
        public Swatch swatch3;
        public Swatch swatch4;
        public Swatch swatch5;
        public Swatch swatch6;

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
    }
}
