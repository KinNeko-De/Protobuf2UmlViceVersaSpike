
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
			using (var stream = File.OpenRead(@"mymodel"))
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

				foreach (DescriptorProto message in info.MessageType)
				{
					Console.WriteLine("---------------");
					Console.WriteLine($"| {message.Name} |");
					Console.WriteLine("---------------");
					foreach (var field in message.Field)
					{
						Console.WriteLine($"| + {field.TypeName} {field.Name} |");
					}
					Console.WriteLine("---------------");
				}
			}
		}
	}
}