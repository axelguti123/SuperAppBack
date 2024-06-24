```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method                 | Mean      | Error    | StdDev   | Median    |
|----------------------- |----------:|---------:|---------:|----------:|
| GetAllBenchmark        | 232.68 μs | 4.585 μs | 7.661 μs | 230.91 μs |
| GetAllEspecialidad     |  60.30 μs | 1.188 μs | 2.847 μs |  59.40 μs |
| UpdateBenchMark        | 127.54 μs | 3.440 μs | 9.646 μs | 124.55 μs |
| DeletePartidaBenchMark |  73.16 μs | 1.432 μs | 2.098 μs |  72.38 μs |
