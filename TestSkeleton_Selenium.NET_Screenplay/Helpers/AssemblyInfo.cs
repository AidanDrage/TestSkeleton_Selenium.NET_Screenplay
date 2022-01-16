using NUnit.Framework;
//A Global flag to tell nunit to run test in parallel,
//to run tests in series simply comment this line out
[assembly: Parallelizable(ParallelScope.Fixtures)]