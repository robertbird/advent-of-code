// See https://aka.ms/new-console-template for more information
using Day12;

Console.WriteLine("Hello, World!");

var mgr = new TerrainManager("TestFiles/puzzle.txt");
Console.WriteLine(mgr.ToString());
var steps = mgr.GetShortestPath();
Console.WriteLine($"steps: {steps}");
Console.ReadLine();