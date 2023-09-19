using System;

// Abstract Storage class
abstract class Storage
{
    public string MediaType { get; set; }
    public string Model { get; set; }

    public abstract double GetCapacity();
    public abstract int Copy(double dataSize);
    public abstract double FreeMemory();
    public abstract void PrintDeviceInfo();
}

// Flash class
class Flash : Storage
{
    public double USB30Speed { get; set; }
    public double Memory { get; set; }

    public override double GetCapacity()
    {
        return Memory;
    }

    public override int Copy(double dataSize)
    {
        int requiredMediaCount = (int)Math.Ceiling(dataSize / Memory);
        double totalTime = requiredMediaCount * (Memory / USB30Speed);

        Console.WriteLine($"Datalar buraya kopyalanir {MediaType} - Model: {Model}");
        Console.WriteLine($"Lazim olan umumi media: {requiredMediaCount}");
        Console.WriteLine($"Lazim olan umumi zaman (saat): {totalTime}");

        return requiredMediaCount;
    }

    public override double FreeMemory()
    {
        return Memory;
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Media Tipi: {MediaType}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"USB 3.0 Speed: {USB30Speed} MB/s");
        Console.WriteLine($"Memory: {Memory} MB");
    }
}


// DVD class
class DVD : Storage
{
    public double ReadWriteSpeed { get; set; }
    public string Type { get; set; }

    public override double GetCapacity()
    {
        return (Type == "Single-Sided") ? 4.7 : 9.0;
    }

    public override int Copy(double dataSize)
    {
        int requiredMediaCount = (int)Math.Ceiling(dataSize / GetCapacity());
        double totalTime = requiredMediaCount * (GetCapacity() / ReadWriteSpeed);

        Console.WriteLine($"Datalar buraya kopyalanir {MediaType} - Model: {Model}");
        Console.WriteLine($"Lazim olan umumi media: {requiredMediaCount}");
        Console.WriteLine($"Lazim olan umumi zaman (saat): {totalTime}");

        return requiredMediaCount;
    }

    public override double FreeMemory()
    {
        return 0; 
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Media Tipi: {MediaType}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Read/Write Speed: {ReadWriteSpeed} MB/s");
        Console.WriteLine($"Type: {Type}");
    }
}

class HDD : Storage
{
    public double USB20Speed { get; set; }
    public double TotalSize { get; set; }

    public override double GetCapacity()
    {
        return TotalSize;
    }

    public override int Copy(double dataSize)
    {
        int requiredMediaCount = 1;
        double totalTime = TotalSize / USB20Speed;

        Console.WriteLine($"Datalar buraya kopyalanir {MediaType} - Model: {Model}");
        Console.WriteLine($"Lazim olan umumi media: {requiredMediaCount}");
        Console.WriteLine($"Lazim olan umumi zaman (saat): {totalTime}");

        return requiredMediaCount;
    }

    public override double FreeMemory()
    {
        return TotalSize;
    }

    public override void PrintDeviceInfo()
    {
        Console.WriteLine($"Media Tipi: {MediaType}");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"USB 2.0 Speed: {USB20Speed} MB/s");
        Console.WriteLine($"Total Size: {TotalSize} GB");
    }
}

class Program
{
    static void Main()
    {
        double dataSizeGB = 565 * 780 / 1024.0;
        Console.WriteLine($"Total Data Size: {dataSizeGB} GB");

        Storage[] storageDevices = new Storage[]
        {
            new Flash { MediaType = "Flash Drive", Model = "Kingston", USB30Speed = 100, Memory = 64 },
            new DVD { MediaType = "DVD", Model = "Sony", ReadWriteSpeed = 10, Type = "Single-Sided" },
            new HDD { MediaType = "External HDD", Model = "Seagate", USB20Speed = 30, TotalSize = 2000 }
        };

        foreach (var device in storageDevices)
        {
            device.PrintDeviceInfo();
            int requiredMediaCount = device.Copy(dataSizeGB);
            double freeMemory = device.FreeMemory();
            Console.WriteLine($"Free Memory: {freeMemory} GB\n");
        }
    }
}
