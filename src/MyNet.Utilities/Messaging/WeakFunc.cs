// -----------------------------------------------------------------------
// <copyright file="WeakFunc.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

namespace MyNet.Utilities.Messaging;

/// <summary>
/// Stores a Func&lt;T&gt; without causing a hard reference
/// to be created to the Function's owner. The owner can be garbage collected at any time.
/// </summary>
/// <typeparam name="TResult">The type of the result of the Func that will be stored
/// by this weak reference.</typeparam>
////[ClassInfo(typeof(WeakAction)]
public class WeakFunc<TResult>
{
    private Func<TResult?>? _staticFunc;

    public WeakFunc(Func<TResult?> func, bool keepTargetAlive = false)
        : this(func.Target, func, keepTargetAlive)
    {
    }

    public WeakFunc(object? target, Func<TResult?> func, bool keepTargetAlive = false)
    {
        if (func.Method.IsStatic)
        {
            _staticFunc = func;

            if (target != null)
                Reference = new WeakReference(target);

            return;
        }

        Method = func.Method;
        FuncReference = new WeakReference(func.Target);

        LiveReference = keepTargetAlive ? func.Target : null;
        Reference = new WeakReference(target);
    }

    protected WeakFunc()
    {
    }

    /// <summary>
    /// Gets a value indicating whether get a value indicating whether the WeakFunc is static or not.
    /// </summary>
    public bool IsStatic => _staticFunc != null;

    /// <summary>
    /// Gets the name of the method that this WeakFunc represents.
    /// </summary>
    public virtual string? MethodName => _staticFunc != null ? _staticFunc.Method.Name : Method?.Name;

    /// <summary>
    /// Gets a value indicating whether the Function's owner is still alive, or if it was collected
    /// by the Garbage Collector already.
    /// </summary>
    public virtual bool IsAlive
    {
        get
        {
            if (_staticFunc == null
                && Reference == null
                && LiveReference == null)
                return false;

            if (_staticFunc != null)
            {
                return Reference == null || Reference.IsAlive;
            }

            // Non-static action
            return LiveReference != null || Reference is { IsAlive: true };
        }
    }

    /// <summary>
    /// Gets the Function's owner. This object is stored as a
    /// <see cref="WeakReference" />.
    /// </summary>
    public object? Target => Reference?.Target;

    /// <summary>
    /// Gets or sets the <see cref="MethodInfo" /> corresponding to this WeakFunction's
    /// method passed in the constructor.
    /// </summary>
    protected MethodInfo? Method
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets a WeakReference to this WeakFunction's action's target.
    /// This is not necessarily the same as
    /// <see cref="Reference" />, for example if the
    /// method is anonymous.
    /// </summary>
    protected WeakReference? FuncReference
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets saves the <see cref="FuncReference"/> as a hard reference. This is
    /// used in relation with this instance's constructor and only if
    /// the constructor's keepTargetAlive parameter is true.
    /// </summary>
    protected object? LiveReference
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets a WeakReference to the target passed when constructing
    /// the WeakFunc. This is not necessarily the same as
    /// <see cref="FuncReference" />, for example if the
    /// method is anonymous.
    /// </summary>
    protected WeakReference? Reference
    {
        get;
        set;
    }

    /// <summary>
    /// Gets the owner of the Func that was passed as parameter.
    /// This is not necessarily the same as
    /// <see cref="Target" />, for example if the
    /// method is anonymous.
    /// </summary>
    protected object? FuncTarget => LiveReference ?? FuncReference?.Target;

    /// <summary>
    /// Executes the action. This only happens if the Function's owner
    /// is still alive.
    /// </summary>
    /// <returns>The result of the Func stored as reference.</returns>
    public TResult? Execute()
    {
        if (_staticFunc != null)
            return _staticFunc();

        var funcTarget = FuncTarget;

        return IsAlive && Method != null
                       && (LiveReference != null
                           || FuncReference != null)
                       && funcTarget != null
            ? (TResult?)Method.Invoke(funcTarget, null)
            : default;
    }

    /// <summary>
    /// Sets the reference that this instance stores to null.
    /// </summary>
    public void MarkForDeletion()
    {
        Reference = null;
        FuncReference = null;
        LiveReference = null;
        Method = null;
        _staticFunc = null;
    }
}

/// <summary>
/// Stores a Func without causing a hard reference
/// to be created to the Function's owner. The owner can be garbage collected at any time.
/// </summary>
/// <typeparam name="T">The type of the Function's parameter.</typeparam>
/// <typeparam name="TResult">The type of the Function's return value.</typeparam>
////[ClassInfo(typeof(WeakAction))]
public class WeakFunc<T, TResult> : WeakFunc<TResult>, IExecuteWithObjectAndResult
{
    private Func<T?, TResult?>? _staticFunc;

    public WeakFunc(Func<T?, TResult?> func, bool keepTargetAlive = false)
    : this(func.Target, func, keepTargetAlive)
    {
    }

    public WeakFunc(object? target, Func<T?, TResult?> func, bool keepTargetAlive = false)
    {
        if (func.Method.IsStatic)
        {
            _staticFunc = func;

            // Keep a reference to the target to control the WeakAction's lifetime.
            if (target != null)
                Reference = new WeakReference(target);

            return;
        }

        Method = func.Method;
        FuncReference = new WeakReference(func.Target);

        LiveReference = keepTargetAlive ? func.Target : null;
        Reference = new WeakReference(target);
    }

    /// <summary>
    /// Gets the name of the method that this WeakFunc represents.
    /// </summary>
    public override string? MethodName => _staticFunc != null ? _staticFunc.Method.Name : Method?.Name;

    /// <summary>
    /// Gets a value indicating whether the Function's owner is still alive, or if it was collected
    /// by the Garbage Collector already.
    /// </summary>
    public override bool IsAlive => (_staticFunc != null
                                     || Reference != null)
                                    && (_staticFunc != null ? Reference == null || Reference.IsAlive : Reference?.IsAlive ?? false);

    /// <summary>
    /// Executes the Func. This only happens if the Function's owner
    /// is still alive. The Function's parameter is set to default(T).
    /// </summary>
    /// <returns>The result of the Func stored as reference.</returns>
    public new TResult? Execute() => Execute(default);

    /// <summary>
    /// Executes the Func. This only happens if the Function's owner
    /// is still alive.
    /// </summary>
    /// <param name="parameter">A parameter to be passed to the action.</param>
    /// <returns>The result of the Func stored as reference.</returns>
    public TResult? Execute(T? parameter)
    {
        if (_staticFunc != null)
            return _staticFunc(parameter);

        var funcTarget = FuncTarget;

        return IsAlive && Method != null
                       && (LiveReference != null
                           || FuncReference != null)
                       && funcTarget != null
            ? (TResult?)Method.Invoke(
                funcTarget,
                [
                    parameter
                ])
            : default;
    }

    /// <summary>
    /// Executes the Func with a parameter of type object. This parameter
    /// will be cast to T. This method implements <see cref="IExecuteWithObject.ExecuteWithObject" />
    /// and can be useful if you store multiple WeakFunc{T} instances but don't know in advance
    /// what type T represents.
    /// </summary>
    /// <param name="parameter">The parameter that will be passed to the Func after
    /// being cast to T.</param>
    /// <returns>The result of the execution as object, to be cast to T.</returns>
    public object? ExecuteWithObject(object? parameter)
    {
        var parameterCasted = (T?)parameter;
        return Execute(parameterCasted);
    }

    /// <summary>
    /// Sets all the Functions that this WeakFunc contains to null,
    /// which is a signal for containing objects that this WeakFunc
    /// should be deleted.
    /// </summary>
    public new void MarkForDeletion()
    {
        _staticFunc = null;
        base.MarkForDeletion();
    }
}
