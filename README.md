# ColorBlender

[![Build status](https://ci.appveyor.com/api/projects/status/79btr6li6w4blngf/branch/master?svg=true)](https://ci.appveyor.com/project/wieslawsoltes/colorblender/branch/master)
[![Build Status](https://travis-ci.org/wieslawsoltes/ColorBlender.svg?branch=master)](https://travis-ci.org/wieslawsoltes/ColorBlender)
[![CircleCI](https://circleci.com/gh/wieslawsoltes/ColorBlender/tree/master.svg?style=svg)](https://circleci.com/gh/wieslawsoltes/ColorBlender/tree/master)

[![NuGet](https://img.shields.io/nuget/v/ColorBlender.svg)](https://www.nuget.org/packages/ColorBlender)

A .NET library for color matching and palette design.

## About

ColorBlender is a .NET library for color matching and palette design.

ColorBlender .NET version is based on sources from: http://www.colorblender.com/

The old version of ColorBlender can be found here: http://www.colormatch5k.com/
The new version of ColorBlender can be found here: http://www.colorexplorer.com/colormatch.aspx

## Algorithms

Color matching algorithms:
* classic - ColorMatch 5K Classic
* colorexplorer - ColorExplorer - "Sweet Spot Offset"
* singlehue - Single Hue
* complementary - Complementary
* splitcomplementary - Split-Complementary
* analogue - Analogue
* triadic - Triadic
* square - Square

All work is done in HSV color space, because all
calculations are based on hue, saturation and value of the working color.

The hue spectrum is divided into sections, are the matching colors are
calculated differently depending on the hue of the color.

## NuGet

ColorBlender is delivered as a NuGet package.

You can find the package [here](https://www.nuget.org/packages/ColorBlender/).

You can install the package like this:

`Install-Package ColorBlender`

## Screenshots

![](images/avalonia.png)

![](images/wpf.png)

## License

ColorBlender is licensed under the [MIT license](LICENSE.TXT).
