// -----------------------------------------------------------------------
// <copyright file="ProgressManager.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNet.Utilities.Progress;

public static class ProgressManager
{
    private static IProgresser? _progresser;

    public static void Initialize(IProgresser progresser) => _progresser = progresser;

    public static IProgressStep<ProgressMessage>? New() => New(1);

    public static IProgressStep<ProgressMessage>? New(IEnumerable<double> subStepDefinitions) => New(subStepDefinitions, string.Empty);

    public static IProgressStep<ProgressMessage>? New(int numberOfSteps) => New(numberOfSteps, string.Empty);

    public static IProgressStep<ProgressMessage>? New(string message, params object[] parameters)
        => New(1, message, parameters);

    public static IProgressStep<ProgressMessage>? New(int numberOfSteps, string message, params object[] parameters)
        => New([.. Enumerable.Range(1, numberOfSteps).Select(_ => 1.0D / numberOfSteps)], message, parameters);

    public static IProgressStep<ProgressMessage>? New(IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
        => _progresser?.New(subStepDefinitions, new ProgressMessage(message, parameters));

    public static IProgressStep<ProgressMessage>? NewCancellable(Action cancelAction) => NewCancellable(1, cancelAction);

    public static IProgressStep<ProgressMessage>? NewCancellable(IEnumerable<double> subStepDefinitions, Action cancelAction) => NewCancellable(subStepDefinitions, cancelAction, string.Empty);

    public static IProgressStep<ProgressMessage>? NewCancellable(int numberOfSteps, Action cancelAction) => NewCancellable(numberOfSteps, cancelAction, string.Empty);

    public static IProgressStep<ProgressMessage>? NewCancellable(Action cancelAction, string message, params object[] parameters)
        => NewCancellable(1, cancelAction, message, parameters);

    public static IProgressStep<ProgressMessage>? NewCancellable(int numberOfSteps, Action cancelAction, string message, params object[] parameters)
        => NewCancellable([.. Enumerable.Range(1, numberOfSteps).Select(_ => 1.0D / numberOfSteps)], cancelAction, message, parameters);

    public static IProgressStep<ProgressMessage>? NewCancellable(IEnumerable<double> subStepDefinitions, Action cancelAction, string message, params object[] parameters)
        => _progresser?.New(subStepDefinitions, new ProgressMessage(message, parameters), cancelAction);

    public static IProgressStep<ProgressMessage>? Start() => Start(1);

    public static IProgressStep<ProgressMessage>? Start(IEnumerable<double> subStepDefinitions) => Start(subStepDefinitions, string.Empty);

    public static IProgressStep<ProgressMessage>? Start(int numberOfSteps) => Start(numberOfSteps, string.Empty);

    public static IProgressStep<ProgressMessage>? Start(string message, params object[] parameters)
        => Start(1, message, parameters);

    public static IProgressStep<ProgressMessage>? Start(int numberOfSteps, string message, params object[] parameters)
        => Start([.. Enumerable.Range(1, numberOfSteps).Select(_ => 1.0D / numberOfSteps)], message, parameters);

    public static IProgressStep<ProgressMessage>? Start(IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
    {
        try
        {
            return _progresser?.Start(subStepDefinitions, new ProgressMessage(message, parameters));
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public static IProgressStep<ProgressMessage>? StartUncancellable() => StartUncancellable(1);

    public static IProgressStep<ProgressMessage>? StartUncancellable(IEnumerable<double> subStepDefinitions) => StartUncancellable(subStepDefinitions, string.Empty);

    public static IProgressStep<ProgressMessage>? StartUncancellable(int numberOfSteps) => StartUncancellable(numberOfSteps, string.Empty);

    public static IProgressStep<ProgressMessage>? StartUncancellable(string message, params object[] parameters)
        => StartUncancellable(1, message, parameters);

    public static IProgressStep<ProgressMessage>? StartUncancellable(int numberOfSteps, string message, params object[] parameters)
        => StartUncancellable([.. Enumerable.Range(1, numberOfSteps).Select(_ => 1.0D / numberOfSteps)], message, parameters);

    public static IProgressStep<ProgressMessage>? StartUncancellable(IEnumerable<double> subStepDefinitions, string message, params object[] parameters)
    {
        try
        {
            return _progresser?.Start(subStepDefinitions, new ProgressMessage(message, parameters), false);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
}
