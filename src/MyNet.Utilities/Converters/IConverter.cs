// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyNet.Utilities.Converters
{
    internal interface IConverter<TFrom, TTo>
    {
        TTo Convert(TFrom item);

        TFrom ConvertBack(TTo item);
    }
}
