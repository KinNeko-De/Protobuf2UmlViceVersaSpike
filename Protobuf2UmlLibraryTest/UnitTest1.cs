
using Google.Protobuf.Reflection;
using NUnit.Framework;
using System;
using System.IO;

namespace Protobuf2UmlLibraryTest
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			using (var stream = File.OpenRead(@"C:\Users\Agando\Desktop\protoc-3.9.1-win64\bin\mymodel"))
			{
				FileDescriptorSet descriptorSet = FileDescriptorSet.Parser.ParseFrom(stream);
				Google.Protobuf.Collections.RepeatedField<FileDescriptorProto> descriptorsProto = descriptorSet.File;
				FileDescriptorProto info = descriptorsProto[0];

				var service = info.Service[0];
				 Console.WriteLine("---------------");
				 Console.WriteLine("| <<Service>> |");
				Console.WriteLine($"| {service.Name} |");
				 Console.WriteLine("---------------");
				foreach (MethodDescriptorProto method in service.Method)
				{
					Console.WriteLine($"| + {method.OutputType} {method.Name}({method.InputType}) |");
				}
				Console.WriteLine("---------------");
			}
		}
	}
}