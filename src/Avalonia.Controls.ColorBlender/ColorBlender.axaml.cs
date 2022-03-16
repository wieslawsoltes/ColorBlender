using System;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using ColorBlender;
using ColorBlender.Colors;

namespace Avalonia.Controls.ColorBlender
{
    public class ColorBlender : UserControl
    {
        private readonly Slider _sliderR;
        private readonly Slider _sliderG;
        private readonly Slider _sliderB;
        private readonly Slider _sliderH;
        private readonly Slider _sliderS;
        private readonly Slider _sliderV;
        private readonly Rectangle _rgbvar1;
        private readonly Rectangle _rgbvar2;
        private readonly Rectangle _rgbvar3;
        private readonly Rectangle _rgbvar4;
        private readonly Rectangle _rgbvar5;
        private readonly Rectangle _rgbvar6;
        private readonly Rectangle _rgbvar7;
        private readonly Rectangle _hsvvar1;
        private readonly Rectangle _hsvvar2;
        private readonly Rectangle _hsvvar3;
        private readonly Rectangle _hsvvar4;
        private readonly Rectangle _hsvvar5;
        private readonly Rectangle _hsvvar6;
        private readonly Rectangle _hsvvar7;
        private readonly Rectangle _hsvvar8;
        private readonly Rectangle _hsvvar9;
        private readonly Swatch _swatch1;
        private readonly Swatch _swatch2;
        private readonly Swatch _swatch3;
        private readonly Swatch _swatch4;
        private readonly Swatch _swatch5;
        private readonly Swatch _swatch6;
        private bool _updatingSliders;

        public ColorBlender()
        {
            this.InitializeComponent();

            var algorithm = this.FindControl<ComboBox>("algorithm");
            _sliderR = this.FindControl<Slider>("sliderR");
            _sliderG = this.FindControl<Slider>("sliderG");
            _sliderB = this.FindControl<Slider>("sliderB");
            _sliderH = this.FindControl<Slider>("sliderH");
            _sliderS = this.FindControl<Slider>("sliderS");
            _sliderV = this.FindControl<Slider>("sliderV");
            _rgbvar1 = this.FindControl<Rectangle>("rgbvar1");
            _rgbvar2 = this.FindControl<Rectangle>("rgbvar2");
            _rgbvar3 = this.FindControl<Rectangle>("rgbvar3");
            _rgbvar4 = this.FindControl<Rectangle>("rgbvar4");
            _rgbvar5 = this.FindControl<Rectangle>("rgbvar5");
            _rgbvar6 = this.FindControl<Rectangle>("rgbvar6");
            _rgbvar7 = this.FindControl<Rectangle>("rgbvar7");
            _hsvvar1 = this.FindControl<Rectangle>("hsvvar1");
            _hsvvar2 = this.FindControl<Rectangle>("hsvvar2");
            _hsvvar3 = this.FindControl<Rectangle>("hsvvar3");
            _hsvvar4 = this.FindControl<Rectangle>("hsvvar4");
            _hsvvar5 = this.FindControl<Rectangle>("hsvvar5");
            _hsvvar6 = this.FindControl<Rectangle>("hsvvar6");
            _hsvvar7 = this.FindControl<Rectangle>("hsvvar7");
            _hsvvar8 = this.FindControl<Rectangle>("hsvvar8");
            _hsvvar9 = this.FindControl<Rectangle>("hsvvar9");
            _swatch1 = this.FindControl<Swatch>("swatch1");
            _swatch2 = this.FindControl<Swatch>("swatch2");
            _swatch3 = this.FindControl<Swatch>("swatch3");
            _swatch4 = this.FindControl<Swatch>("swatch4");
            _swatch5 = this.FindControl<Swatch>("swatch5");
            _swatch6 = this.FindControl<Swatch>("swatch6");

            _sliderR.GetObservable(RangeBase.ValueProperty).Subscribe(_ => SliderRGB_ValueChanged());
            _sliderG.GetObservable(RangeBase.ValueProperty).Subscribe(_ => SliderRGB_ValueChanged());
            _sliderB.GetObservable(RangeBase.ValueProperty).Subscribe(_ => SliderRGB_ValueChanged());
            _sliderH.GetObservable(RangeBase.ValueProperty).Subscribe(_ => SliderHSV_ValueChanged());
            _sliderS.GetObservable(RangeBase.ValueProperty).Subscribe(_ => SliderHSV_ValueChanged());
            _sliderV.GetObservable(RangeBase.ValueProperty).Subscribe(_ => SliderHSV_ValueChanged());

            _rgbvar1.PointerPressed += Rectangle_PointerPressed;
            _rgbvar2.PointerPressed += Rectangle_PointerPressed;
            _rgbvar3.PointerPressed += Rectangle_PointerPressed;
            _rgbvar4.PointerPressed += Rectangle_PointerPressed;
            _rgbvar5.PointerPressed += Rectangle_PointerPressed;
            _rgbvar6.PointerPressed += Rectangle_PointerPressed;
            _rgbvar7.PointerPressed += Rectangle_PointerPressed;

            _hsvvar1.PointerPressed += Rectangle_PointerPressed;
            _hsvvar2.PointerPressed += Rectangle_PointerPressed;
            _hsvvar3.PointerPressed += Rectangle_PointerPressed;
            _hsvvar4.PointerPressed += Rectangle_PointerPressed;
            _hsvvar5.PointerPressed += Rectangle_PointerPressed;
            _hsvvar6.PointerPressed += Rectangle_PointerPressed;
            _hsvvar7.PointerPressed += Rectangle_PointerPressed;
            _hsvvar8.PointerPressed += Rectangle_PointerPressed;
            _hsvvar9.PointerPressed += Rectangle_PointerPressed;

            _swatch1._col.PointerPressed += Rectangle_PointerPressed;
            _swatch2._col.PointerPressed += Rectangle_PointerPressed;
            _swatch3._col.PointerPressed += Rectangle_PointerPressed;
            _swatch4._col.PointerPressed += Rectangle_PointerPressed;
            _swatch5._col.PointerPressed += Rectangle_PointerPressed;
            _swatch6._col.PointerPressed += Rectangle_PointerPressed;

            algorithm.SelectionChanged += Algorithm_SelectionChanged;

            this.AttachedToVisualTree += UserControl_AttachedToVisualTree;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void UserControl_AttachedToVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRgb();
            UpdateSliderHsv();
        }

        private void UpdateVariations()
        {
            if (DataContext is ColorMatch vm)
            {
                _rgbvar1.Fill = vm.VariationsRGB[0].ToSolidColorBrush();
                _rgbvar2.Fill = vm.VariationsRGB[1].ToSolidColorBrush();
                _rgbvar3.Fill = vm.VariationsRGB[2].ToSolidColorBrush();
                _rgbvar4.Fill = vm.VariationsRGB[3].ToSolidColorBrush();
                _rgbvar5.Fill = vm.VariationsRGB[4].ToSolidColorBrush();
                _rgbvar6.Fill = vm.VariationsRGB[5].ToSolidColorBrush();
                _rgbvar7.Fill = vm.VariationsRGB[6].ToSolidColorBrush();

                _hsvvar1.Fill = vm.VariationsHSV[0].ToSolidColorBrush();
                _hsvvar2.Fill = vm.VariationsHSV[1].ToSolidColorBrush();
                _hsvvar3.Fill = vm.VariationsHSV[2].ToSolidColorBrush();
                _hsvvar4.Fill = vm.VariationsHSV[3].ToSolidColorBrush();
                _hsvvar5.Fill = vm.VariationsHSV[4].ToSolidColorBrush();
                _hsvvar6.Fill = vm.VariationsHSV[5].ToSolidColorBrush();
                _hsvvar7.Fill = vm.VariationsHSV[6].ToSolidColorBrush();
                _hsvvar8.Fill = vm.VariationsHSV[7].ToSolidColorBrush();
                _hsvvar9.Fill = vm.VariationsHSV[8].ToSolidColorBrush();
            }
        }

        private void UpdateSwatches()
        {
            if (DataContext is ColorMatch vm)
            {
                _swatch1._col.Fill = vm.CurrentBlend.Colors[0].ToSolidColorBrush();
                _swatch2._col.Fill = vm.CurrentBlend.Colors[1].ToSolidColorBrush();
                _swatch3._col.Fill = vm.CurrentBlend.Colors[2].ToSolidColorBrush();
                _swatch4._col.Fill = vm.CurrentBlend.Colors[3].ToSolidColorBrush();
                _swatch5._col.Fill = vm.CurrentBlend.Colors[4].ToSolidColorBrush();
                _swatch6._col.Fill = vm.CurrentBlend.Colors[5].ToSolidColorBrush();
            }
        }

        private void UpdateSliderRgb()
        {
            _updatingSliders = true;

            if (DataContext is ColorMatch vm)
            {
                _sliderR.Value = vm.CurrentRGB.R;
                _sliderG.Value = vm.CurrentRGB.G;
                _sliderB.Value = vm.CurrentRGB.B;
            }

            _updatingSliders = false;
        }

        private void UpdateSliderHsv()
        {
            _updatingSliders = true;

            if (DataContext is ColorMatch vm)
            {
                _sliderH.Value = vm.CurrentHSV.H;
                _sliderS.Value = vm.CurrentHSV.S;
                _sliderV.Value = vm.CurrentHSV.V;
            }

            _updatingSliders = false;
        }

        private void Algorithm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ColorMatch vm)
            {
                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
        }

        private void SliderRGB_ValueChanged()
        {
            if (_updatingSliders == false)
            {
                HandleSliderValueChangedRgb();
            }
        }

        private void SliderHSV_ValueChanged()
        {
            if (_updatingSliders == false)
            {
                HandleSliderValueChangedHsv();
            }
        }

        private void Rectangle_PointerPressed(object sender, PointerPressedEventArgs e)
        {
            HandleRectangleClick((sender as Rectangle)?.Fill as SolidColorBrush);
        }

        private void HandleRectangleClick(SolidColorBrush b)
        {
            if (DataContext is ColorMatch vm)
            {
                vm.CurrentRGB = b.Color.ToRGB();
                vm.CurrentHSV = vm.CurrentRGB.ToHSV();

                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRgb();
            UpdateSliderHsv();
        }

        private void HandleSliderValueChangedRgb()
        {
            if (DataContext is ColorMatch vm)
            {
                vm.CurrentRGB = new RGB(_sliderR.Value, _sliderG.Value, _sliderB.Value);
                vm.CurrentHSV = vm.CurrentRGB.ToHSV();
                vm.CurrentRGB = vm.CurrentHSV.ToRGB();

                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderHsv();
        }

        private void HandleSliderValueChangedHsv()
        {
            if (DataContext is ColorMatch vm)
            {
                vm.CurrentHSV = new HSV(_sliderH.Value, _sliderS.Value, _sliderV.Value);
                vm.CurrentRGB = vm.CurrentHSV.ToRGB();

                vm.Update();
            }

            UpdateVariations();
            UpdateSwatches();
            UpdateSliderRgb();
        }
    }
}
