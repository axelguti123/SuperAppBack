```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3593/23H2/2023Update/SunValley3)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.204
  [Host]     : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 8.0.4 (8.0.424.16909), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


```
| Method             | Mean     | Error    | StdDev   | Median   |
|------------------- |---------:|---------:|---------:|---------:|
| GetAllBenchmark    | 591.9 μs | 11.74 μs | 23.99 μs | 588.3 μs |
| GetAllEspecialidad | 115.8 μs |  4.05 μs | 11.56 μs | 111.8 μs |
| UpdateBenchMark    | 233.4 μs |  6.98 μs | 20.13 μs | 224.9 μs |
