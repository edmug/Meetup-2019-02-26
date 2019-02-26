using System;

public class Class1
{
   public static void Main()
   {
       using(var r = System.Security.Cryptography.RandomNumberGenerator.Create())
       {
           var b = new Byte[4];
           r.GetBytes(b);

           Console.WriteLine(System.BitConverter.ToUInt32(b));
       }
   }
}

