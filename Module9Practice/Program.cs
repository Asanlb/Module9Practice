using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


abstract class Storage
{
    protected string name;
    protected string model;

    public Storage(string name, string model)
    {
        this.name = name;
        this.model = model;
    }

    public abstract double GetMemorySize();
    public abstract void CopyData(double dataSize);
    public abstract double GetFreeSpace();
    public abstract void GetDeviceInfo();
}

class Flash : Storage
{
    private double usbSpeed;
    private double memorySize;

    public Flash(string name, string model, double usbSpeed, double memorySize) : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.memorySize = memorySize;
    }

    public override double GetMemorySize()
    {
        return memorySize;
    }

    public override void CopyData(double dataSize)
    {
        double time = dataSize / usbSpeed;
        Console.WriteLine($"Copying data to Flash. Time required: {time} seconds");
    }

    public override double GetFreeSpace()
    {
       
        return memorySize * 0.8;
    }

    public override void GetDeviceInfo()
    {
        Console.WriteLine($"Flash Drive - {name} ({model}), USB Speed: {usbSpeed} GB/s, Memory Size: {memorySize} GB");
    }
}


class DVD : Storage
{
    private double readWriteSpeed;
    private bool isDoubleLayer;

    public DVD(string name, string model, double readWriteSpeed, bool isDoubleLayer) : base(name, model)
    {
        this.readWriteSpeed = readWriteSpeed;
        this.isDoubleLayer = isDoubleLayer;
    }

    public override double GetMemorySize()
    {
        return isDoubleLayer ? 9 : 4.7;
    }

    public override void CopyData(double dataSize)
    {
        double time = dataSize / readWriteSpeed;
        Console.WriteLine($"Copying data to DVD. Time required: {time} seconds");
    }

    public override double GetFreeSpace()
    {
        return 0;
    }

    public override void GetDeviceInfo()
    {
        string type = isDoubleLayer ? "Double Layer" : "Single Layer";
        Console.WriteLine($"DVD - {name} ({model}), Read/Write Speed: {readWriteSpeed} GB/s, Type: {type}");
    }
}

class HDD : Storage
{
    private double usbSpeed;
    private int partitions;
    private double partitionSize;

    public HDD(string name, string model, double usbSpeed, int partitions, double partitionSize) : base(name, model)
    {
        this.usbSpeed = usbSpeed;
        this.partitions = partitions;
        this.partitionSize = partitionSize;
    }

    public override double GetMemorySize()
    {
        return partitions * partitionSize;
    }

    public override void CopyData(double dataSize)
    {
        double time = dataSize / usbSpeed;
        Console.WriteLine($"Copying data to HDD. Time required: {time} seconds");
    }

    public override double GetFreeSpace()
    {
        return partitions * partitionSize * 0.9;
    }

    public override void GetDeviceInfo()
    {
        Console.WriteLine($"HDD - {name} ({model}), USB Speed: {usbSpeed} GB/s, Partitions: {partitions}, Partition Size: {partitionSize} GB");
    }
}

class Program
{
    static void Main()
    {
        Storage[] devices = new Storage[]
        {
            new Flash("FlashDrive1", "ModelA", 5, 64),
            new DVD("DVDDrive1", "ModelB", 2, true),
            new HDD("ExternalHDD1", "ModelC", 3, 2, 500)
        };

        double totalMemory = 0;
        foreach (var device in devices)
        {
            totalMemory += device.GetMemorySize();
        }
        Console.WriteLine($"Total Memory of all devices: {totalMemory} GB");

        double dataSize = 565; 
        foreach (var device in devices)
        {
            device.CopyData(dataSize);
        }

        foreach (var device in devices)
        {
            device.CopyData(dataSize);
        }

        double fileSize = 0.78; 
        int neededDevices = (int)Math.Ceiling(dataSize / fileSize);
        Console.WriteLine($"Needed Devices for transfer: {neededDevices}");
    }
}

