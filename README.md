# Dora.Repository

Repository

## Design

- 由于 Insert、Delete、Update 不应该直接执行IO操作，应设计为同步方式
- GetAsync 方法会直接执行 IO 操作读取数据，应设计为异步IO方式
- GetAll 方法一般而言不应该直接从库中拉取所有(而是根据查询条件构建查询语句)，应设计为同步方式

[msdn 仓储设计](https://docs.microsoft.com/zh-cn/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core#ef-dbcontext-and-iunitofwork-instance-lifetime-in-your-ioc-container)

- DbContext 对象（作为 IUnitOfWork 对象公开）应在相同 HTTP 请求范围内的多个存储库之间进行共享。 
- Repository 的使用 和 UnitOfWork 的创建都会创建或复用 DbContext 实例

## Feature

- CRUD
  - Insert
  - Delete
  - Update
  - GetAsync、GetAll

```c#
using (var uow = _unitOfWorkManager.Begin())
{
    var user = new User{ Name = "foo" };
    user.IsTransient().ShouldBeTrue();
    _userRepository.Insert(user);
    await uow.SaveChangeAsync();
    user.IsTransient().ShouldBeFalse();
}
```

## Benchmark

``` ini

BenchmarkDotNet=v0.12.1, OS=centos 7
Intel Xeon Platinum 8163 CPU 2.50GHz, 1 CPU, 2 logical cores and 1 physical core
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT


```
|                Method |     Mean |    Error |   StdDev |      Min |      Max | Ratio | RatioSD |
|---------------------- |---------:|---------:|---------:|---------:|---------:|------:|--------:|
|          NativeInsert | 497.2 μs | 28.29 μs | 80.27 μs | 341.6 μs | 683.5 μs |  1.00 |    0.00 |
| RepositoryInsertAsync | 513.1 μs | 23.08 μs | 64.71 μs | 397.5 μs | 709.1 μs |  1.07 |    0.24 |
