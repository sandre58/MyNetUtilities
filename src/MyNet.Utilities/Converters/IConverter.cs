// -----------------------------------------------------------------------
// <copyright file="IConverter.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MyNet.Utilities.Converters;

public interface IConverter<TFrom, TTo>
{
    TTo Convert(TFrom item);

    TFrom ConvertBack(TTo item);
}
