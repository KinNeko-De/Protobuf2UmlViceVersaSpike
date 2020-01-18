
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

		[Test]
		public void ExampleImport()
		{
			using (var stream = File.OpenRead(@"protobuf/example.import"))
			{
				FileDescriptorSet descriptorSet = FileDescriptorSet.Parser.ParseFrom(stream);
				Google.Protobuf.Collections.RepeatedField<FileDescriptorProto> descriptorsProto = descriptorSet.File;
				FileDescriptorProto a = descriptorsProto[0];
				FileDescriptorProto b = descriptorsProto[1];
				FileDescriptorProto bb = descriptorsProto[2];
				FileDescriptorProto c = descriptorsProto[3];
				FileDescriptorProto cc = descriptorsProto[4];

				Assert.That(b.Dependency.Count, Is.EqualTo(1));
				Assert.That(b.Dependency[0], Contains.Substring("A.proto"));
				Assert.That(b.PublicDependency.Count, Is.EqualTo(0));
				
				Assert.That(bb.Dependency.Count, Is.EqualTo(1));
				Assert.That(bb.Dependency[0], Contains.Substring("B.proto"));
				Assert.That(bb.PublicDependency.Count, Is.EqualTo(0));

				Assert.That(c.Dependency.Count, Is.EqualTo(1));
				Assert.That(c.Dependency[0], Contains.Substring("A.proto"));
				Assert.That(c.PublicDependency.Count, Is.EqualTo(1));

				Assert.That(cc.Dependency.Count, Is.EqualTo(1));
				Assert.That(cc.Dependency[0], Contains.Substring("C.proto"));
				Assert.That(cc.PublicDependency.Count, Is.EqualTo(1));
			}
		}
	}
}