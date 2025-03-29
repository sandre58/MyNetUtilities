// -----------------------------------------------------------------------
// <copyright file="SentenceGenerator.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyNet.Utilities.Generator;

public static class SentenceGenerator
{
    public const string LoremIpsum = @"lorem ipsum amet, pellentesque mattis accumsan maximus etiam mollis ligula non iaculis ornare mauris efficitur ex eu rhoncus aliquam in hac habitasse platea dictumst maecenas ultrices, purus at venenatis auctor, sem nulla urna, molestie nisi mi a ut euismod nibh id libero lacinia, sit amet lacinia lectus viverra donec scelerisque dictum enim, dignissim dolor cursus morbi rhoncus, elementum magna sed, sed velit consectetur adipiscing elit curabitur nulla, eleifend vel, tempor metus phasellus vel pulvinar, lobortis quis, nullam felis orci congue vitae augue nisi, tincidunt id, posuere fermentum facilisis ultricies mi, nisl fusce neque, vulputate integer tortor tempus praesent proin quis nunc massa congue, quam auctor eros placerat eros, leo nec, sapien egestas duis feugiat, vestibulum porttitor, odio sollicitudin arcu, et aenean sagittis ante urna fringilla, risus et, vivamus semper nibh, eget finibus est laoreet justo commodo sagittis, vitae, nunc, diam ac, tellus posuere, condimentum enim tellus, faucibus suscipit ac nec turpis interdum malesuada fames primis quisque pretium ex, feugiat porttitor massa, vehicula dapibus blandit, hendrerit elit, aliquet nam orci, fringilla blandit ullamcorper mauris, ultrices consequat tempor, convallis gravida sodales volutpat finibus, neque pulvinar varius, porta laoreet, eu, ligula, porta, placerat, lacus pharetra erat bibendum leo, tristique cras rutrum at, dui tortor, in, varius arcu interdum, vestibulum, magna, ante, imperdiet erat, luctus odio, non, dui, volutpat, bibendum, quam, euismod, mattis, class aptent taciti sociosqu ad litora torquent per conubia nostra, inceptos himenaeos suspendisse lorem, a, sem, eleifend, commodo, dolor, cursus, luctus, lectus,";

    public static string Words(int wordCount, bool includePunctuation = false) => Words(wordCount, wordCount, includePunctuation);

    public static string Words(int wordCountMin, int wordCountMax, bool includePunctuation = false) => string.Join(" ", RandomWordsInLoremIpsum(includePunctuation).Take(RandomGenerator.Int(wordCountMin, wordCountMax)));

    public static string Sentence(int wordCount) => Sentence(wordCount, wordCount);

    public static string Sentence(int wordCountMin, int wordCountMax) => string.Format(CultureInfo.CurrentCulture, "{0}.", Words(wordCountMin, wordCountMax, true)).Replace(",.", ".", StringComparison.OrdinalIgnoreCase).Replace("..", string.Empty, StringComparison.OrdinalIgnoreCase).ToSentence();

    public static string Paragraph(int wordCount, int sentenceCount) => Paragraph(wordCount, wordCount, sentenceCount, sentenceCount);

    public static string Paragraph(int wordCountMin, int wordCountMax, int sentenceCount) => Paragraph(wordCountMin, wordCountMax, sentenceCount, sentenceCount);

    public static string Paragraph(int wordCountMin, int wordCountMax, int sentenceCountMin, int sentenceCountMax)
    {
        var source = string.Join(" ", Enumerable.Range(0, RandomGenerator.Int(sentenceCountMin, sentenceCountMax)).Select(_ => Sentence(wordCountMin, wordCountMax)));

        return source.Remove(source.Length - 1);
    }

    public static IEnumerable<string> Paragraphs(int wordCount, int sentenceCount, int paragraphCount) => Paragraphs(wordCount, wordCount, sentenceCount, sentenceCount, paragraphCount, paragraphCount);

    public static IEnumerable<string> Paragraphs(int wordCountMin, int wordCountMax, int sentenceCount, int paragraphCount) => Paragraphs(wordCountMin, wordCountMax, sentenceCount, sentenceCount, paragraphCount, paragraphCount);

    public static IEnumerable<string> Paragraphs(int wordCountMin, int wordCountMax, int sentenceCountMin, int sentenceCountMax, int paragraphCount) => Paragraphs(wordCountMin, wordCountMax, sentenceCountMin, sentenceCountMax, paragraphCount, paragraphCount);

    public static IEnumerable<string> Paragraphs(int wordCountMin, int wordCountMax, int sentenceCountMin, int sentenceCountMax, int paragraphCountMin, int paragraphCountMax) => [.. Enumerable.Range(0, RandomGenerator.Int(paragraphCountMin, paragraphCountMax)).Select(_ => Paragraph(wordCountMin, wordCountMax, sentenceCountMin, sentenceCountMax))];

    public static string ToSentence(this string input) => input.Length >= 1 ? string.Concat(input[..1].ToUpper(CultureInfo.CurrentCulture), input[1..]) : input.ToUpper(CultureInfo.CurrentCulture);

    private static IEnumerable<string> RandomWordsInString(string words) => words.Split(' ').OrderBy(_ => RandomGenerator.Int());

    private static IEnumerable<string> RandomWordsInLoremIpsum(bool includePunctuation) => includePunctuation ? RandomWordsInString(LoremIpsum) : RandomWordsInString(LoremIpsum.Replace(",", string.Empty, StringComparison.OrdinalIgnoreCase));
}
