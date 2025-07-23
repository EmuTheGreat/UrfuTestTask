﻿namespace Logic.Infrastructure.Results;

public class OperationResult
{
    public bool Success { get; }
    public string? Error   { get; }

    protected OperationResult(bool success, string? error)
    {
        Success = success;
        Error   = error;
    }

    public static OperationResult Ok()
        => new(true, null);

    public static OperationResult Fail(string error)
        => new(false, error);
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; }

    protected OperationResult(bool success, T? data, string? error)
        : base(success, error)
    {
        Data = data;
    }

    public static OperationResult<T> Ok(T data)
        => new(true, data, null);

    public static new OperationResult<T> Fail(string error)
        => new(false, default, error);
}
