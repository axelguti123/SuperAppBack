```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3593/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method             | Mean      | Error    | StdDev    |
|------------------- |----------:|---------:|----------:|
| GetAllBenchmark    | 240.38 μs | 4.746 μs | 10.416 μs |
| GetAllEspecialidad |  65.72 μs | 1.311 μs |  2.365 μs |
| UpdateBenchMark    | 137.40 μs | 3.145 μs |  9.025 μs |
