using Lab2_2;

var osHandle = new OsHandle(100);

// work with handle
Console.WriteLine("Working with osHandle...");
//Release resourses
GC.Collect();
osHandle.Dispose();

//Console.ReadLine();
