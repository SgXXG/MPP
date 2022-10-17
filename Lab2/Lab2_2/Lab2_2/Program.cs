using Lab2_2;

var osHandle = new OSHAndle(10);

// work with handle
Console.WriteLine("Working with osHandle...");
//Release resourses
GC.Collect();
osHandle.Dispose();

//Console.ReadLine();
